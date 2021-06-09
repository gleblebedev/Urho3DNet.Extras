using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Urho3DNet.UserInterface
{
    /// <summary>
    /// Tracks registered <see cref="UrhoUIProperty"/> instances.
    /// </summary>
    public class UrhoUIPropertyRegistry
    {
        private readonly Dictionary<int, UrhoUIProperty> _properties =
            new Dictionary<int, UrhoUIProperty>();
        private readonly Dictionary<Type, Dictionary<int, UrhoUIProperty>> _registered =
            new Dictionary<Type, Dictionary<int, UrhoUIProperty>>();
        private readonly Dictionary<Type, Dictionary<int, UrhoUIProperty>> _attached =
            new Dictionary<Type, Dictionary<int, UrhoUIProperty>>();
        private readonly Dictionary<Type, Dictionary<int, UrhoUIProperty>> _direct =
            new Dictionary<Type, Dictionary<int, UrhoUIProperty>>();
        private readonly Dictionary<Type, List<UrhoUIProperty>> _registeredCache =
            new Dictionary<Type, List<UrhoUIProperty>>();
        private readonly Dictionary<Type, List<UrhoUIProperty>> _attachedCache =
            new Dictionary<Type, List<UrhoUIProperty>>();
        private readonly Dictionary<Type, List<UrhoUIProperty>> _directCache =
            new Dictionary<Type, List<UrhoUIProperty>>();
        private readonly Dictionary<Type, List<UrhoUIProperty>> _inheritedCache =
            new Dictionary<Type, List<UrhoUIProperty>>();

        /// <summary>
        /// Gets the <see cref="UrhoUIPropertyRegistry"/> instance
        /// </summary>
        public static UrhoUIPropertyRegistry Instance { get; }
            = new UrhoUIPropertyRegistry();

        /// <summary>
        /// Gets a list of all registered properties.
        /// </summary>
        internal IReadOnlyCollection<UrhoUIProperty> Properties => _properties.Values;

        /// <summary>
        /// Gets all non-attached <see cref="UrhoUIProperty"/>s registered on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A collection of <see cref="UrhoUIProperty"/> definitions.</returns>
        public IReadOnlyList<UrhoUIProperty> GetRegistered(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            if (_registeredCache.TryGetValue(type, out var result))
            {
                return result;
            }

            var t = type;
            result = new List<UrhoUIProperty>();

            while (t != null)
            {
                // Ensure the type's static ctor has been run.
                RuntimeHelpers.RunClassConstructor(t.TypeHandle);

                if (_registered.TryGetValue(t, out var registered))
                {
                    result.AddRange(registered.Values);
                }

                t = t.BaseType;
            }

            _registeredCache.Add(type, result);
            return result;
        }

        /// <summary>
        /// Gets all attached <see cref="UrhoUIProperty"/>s registered on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A collection of <see cref="UrhoUIProperty"/> definitions.</returns>
        public IReadOnlyList<UrhoUIProperty> GetRegisteredAttached(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            if (_attachedCache.TryGetValue(type, out var result))
            {
                return result;
            }

            var t = type;
            result = new List<UrhoUIProperty>();

            while (t != null)
            {
                if (_attached.TryGetValue(t, out var attached))
                {
                    result.AddRange(attached.Values);
                }

                t = t.BaseType;
            }

            _attachedCache.Add(type, result);
            return result;
        }

        /// <summary>
        /// Gets all direct <see cref="UrhoUIProperty"/>s registered on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A collection of <see cref="UrhoUIProperty"/> definitions.</returns>
        public IReadOnlyList<UrhoUIProperty> GetRegisteredDirect(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            if (_directCache.TryGetValue(type, out var result))
            {
                return result;
            }

            var t = type;
            result = new List<UrhoUIProperty>();

            while (t != null)
            {
                if (_direct.TryGetValue(t, out var direct))
                {
                    result.AddRange(direct.Values);
                }

                t = t.BaseType;
            }

            _directCache.Add(type, result);
            return result;
        }

        /// <summary>
        /// Gets all inherited <see cref="UrhoUIProperty"/>s registered on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A collection of <see cref="UrhoUIProperty"/> definitions.</returns>
        public IReadOnlyList<UrhoUIProperty> GetRegisteredInherited(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            if (_inheritedCache.TryGetValue(type, out var result))
            {
                return result;
            }

            result = new List<UrhoUIProperty>();
            var visited = new HashSet<UrhoUIProperty>();

            var registered = GetRegistered(type);
            var registeredCount = registered.Count;

            for (var i = 0; i < registeredCount; i++)
            {
                var property = registered[i];

                if (property.Inherits)
                {
                    result.Add(property);
                    visited.Add(property);
                }
            }

            var registeredAttached = GetRegisteredAttached(type);
            var registeredAttachedCount = registeredAttached.Count;

            for (var i = 0; i < registeredAttachedCount; i++)
            {
                var property = registeredAttached[i];

                if (property.Inherits)
                {
                    if (!visited.Contains(property))
                    {
                        result.Add(property);
                    }
                }
            }

            _inheritedCache.Add(type, result);
            return result;
        }

        /// <summary>
        /// Gets all <see cref="UrhoUIProperty"/>s registered on a object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns>A collection of <see cref="UrhoUIProperty"/> definitions.</returns>
        public IReadOnlyList<UrhoUIProperty> GetRegistered(IUrhoUIObject o)
        {
            Contract.Requires<ArgumentNullException>(o != null);

            return GetRegistered(o.GetType());
        }

        /// <summary>
        /// Finds a direct property as registered on an object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="property">The direct property.</param>
        /// <returns>
        /// The registered property or null if no matching property found.
        /// </returns>
        public DirectPropertyBase<T> GetRegisteredDirect<T>(
            IUrhoUIObject o,
            DirectPropertyBase<T> property)
        {
            return FindRegisteredDirect(o, property) ??
                throw new ArgumentException($"Property '{property.Name} not registered on '{o.GetType()}");
        }

        /// <summary>
        /// Finds a registered property on a type by name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The property name.</param>
        /// <returns>
        /// The registered property or null if no matching property found.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The property name contains a '.'.
        /// </exception>
        public UrhoUIProperty FindRegistered(Type type, string name)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(name != null);

            if (name.Contains("."))
            {
                throw new InvalidOperationException("Attached properties not supported.");
            }

            var registered = GetRegistered(type);
            var registeredCount = registered.Count;

            for (var i = 0; i < registeredCount; i++)
            {
                UrhoUIProperty x = registered[i];

                if (x.Name == name)
                {
                    return x;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a registered property on an object by name.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="name">The property name.</param>
        /// <returns>
        /// The registered property or null if no matching property found.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The property name contains a '.'.
        /// </exception>
        public UrhoUIProperty FindRegistered(IUrhoUIObject o, string name)
        {
            Contract.Requires<ArgumentNullException>(o != null);
            Contract.Requires<ArgumentNullException>(name != null);

            return FindRegistered(o.GetType(), name);
        }

        /// <summary>
        /// Finds a direct property as registered on an object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="property">The direct property.</param>
        /// <returns>
        /// The registered property or null if no matching property found.
        /// </returns>
        public DirectPropertyBase<T> FindRegisteredDirect<T>(
            IUrhoUIObject o,
            DirectPropertyBase<T> property)
        {
            if (property.Owner == o.GetType())
            {
                return property;
            }

            var registeredDirect = GetRegisteredDirect(o.GetType());
            var registeredDirectCount = registeredDirect.Count;

            for (var i = 0; i < registeredDirectCount; i++)
            {
                var p = registeredDirect[i];

                if (p == property)
                {
                    return (DirectPropertyBase<T>)p;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a registered property by Id.
        /// </summary>
        /// <param name="id">The property Id.</param>
        /// <returns>The registered property or null if no matching property found.</returns>
        internal UrhoUIProperty FindRegistered(int id)
        {
            return id < _properties.Count ? _properties[id] : null;
        }

        /// <summary>
        /// Checks whether a <see cref="UrhoUIProperty"/> is registered on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="property">The property.</param>
        /// <returns>True if the property is registered, otherwise false.</returns>
        public bool IsRegistered(Type type, UrhoUIProperty property)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(property != null);

            static bool ContainsProperty(IReadOnlyList<UrhoUIProperty> properties, UrhoUIProperty property)
            {
                var propertiesCount = properties.Count;

                for (var i = 0; i < propertiesCount; i++)
                {
                    if (properties[i] == property)
                    {
                        return true;
                    }
                }

                return false;
            }

            return ContainsProperty(Instance.GetRegistered(type), property) ||
                   ContainsProperty(Instance.GetRegisteredAttached(type), property);
        }

        /// <summary>
        /// Checks whether a <see cref="UrhoUIProperty"/> is registered on a object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="property">The property.</param>
        /// <returns>True if the property is registered, otherwise false.</returns>
        public bool IsRegistered(object o, UrhoUIProperty property)
        {
            Contract.Requires<ArgumentNullException>(o != null);
            Contract.Requires<ArgumentNullException>(property != null);

            return IsRegistered(o.GetType(), property);
        }

        /// <summary>
        /// Registers a <see cref="UrhoUIProperty"/> on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="property">The property.</param>
        /// <remarks>
        /// You won't usually want to call this method directly, instead use the
        /// <see cref="UrhoUIProperty.Register{TOwner, TValue}(string, TValue, bool, Data.BindingMode, Func{TValue, bool}, Func{IUrhoUIObject, TValue, TValue}, Action{IUrhoUIObject, bool})"/>
        /// method.
        /// </remarks>
        public void Register(Type type, UrhoUIProperty property)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(property != null);

            if (!_registered.TryGetValue(type, out var inner))
            {
                inner = new Dictionary<int, UrhoUIProperty>();
                inner.Add(property.Id, property);
                _registered.Add(type, inner);
            }
            else if (!inner.ContainsKey(property.Id))
            {
                inner.Add(property.Id, property);
            }

            if (property.IsDirect)
            {
                if (!_direct.TryGetValue(type, out inner))
                {
                    inner = new Dictionary<int, UrhoUIProperty>();
                    inner.Add(property.Id, property);
                    _direct.Add(type, inner);
                }
                else if (!inner.ContainsKey(property.Id))
                {
                    inner.Add(property.Id, property);
                }

                _directCache.Clear();
            }

            if (!_properties.ContainsKey(property.Id))
            {
                _properties.Add(property.Id, property);
            }
            
            _registeredCache.Clear();
            _inheritedCache.Clear();
        }

        /// <summary>
        /// Registers an attached <see cref="UrhoUIProperty"/> on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="property">The property.</param>
        /// <remarks>
        /// You won't usually want to call this method directly, instead use the
        /// <see cref="UrhoUIProperty.RegisterAttached{THost, TValue}(string, Type, TValue, bool, Data.BindingMode, Func{TValue, bool}, Func{IUrhoUIObject, TValue, TValue})"/>
        /// method.
        /// </remarks>
        public void RegisterAttached(Type type, UrhoUIProperty property)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(property != null);

            if (!property.IsAttached)
            {
                throw new InvalidOperationException(
                    "Cannot register a non-attached property as attached.");
            }

            if (!_attached.TryGetValue(type, out var inner))
            {
                inner = new Dictionary<int, UrhoUIProperty>();
                inner.Add(property.Id, property);
                _attached.Add(type, inner);
            }
            else
            {
                inner.Add(property.Id, property);
            }
            
            _attachedCache.Clear();
            _inheritedCache.Clear();
        }
    }
}
