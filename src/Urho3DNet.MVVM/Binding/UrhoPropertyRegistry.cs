using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Urho3DNet.MVVM.Binding
{
    /// <summary>
    /// Tracks registered <see cref="UrhoProperty"/> instances.
    /// </summary>
    public class UrhoPropertyRegistry
    {
        private readonly Dictionary<int, UrhoProperty> _properties =
            new Dictionary<int, UrhoProperty>();
        private readonly Dictionary<Type, Dictionary<int, UrhoProperty>> _registered =
            new Dictionary<Type, Dictionary<int, UrhoProperty>>();
        private readonly Dictionary<Type, Dictionary<int, UrhoProperty>> _attached =
            new Dictionary<Type, Dictionary<int, UrhoProperty>>();
        private readonly Dictionary<Type, Dictionary<int, UrhoProperty>> _direct =
            new Dictionary<Type, Dictionary<int, UrhoProperty>>();
        private readonly Dictionary<Type, List<UrhoProperty>> _registeredCache =
            new Dictionary<Type, List<UrhoProperty>>();
        private readonly Dictionary<Type, List<UrhoProperty>> _attachedCache =
            new Dictionary<Type, List<UrhoProperty>>();
        private readonly Dictionary<Type, List<UrhoProperty>> _directCache =
            new Dictionary<Type, List<UrhoProperty>>();
        private readonly Dictionary<Type, List<UrhoProperty>> _inheritedCache =
            new Dictionary<Type, List<UrhoProperty>>();

        /// <summary>
        /// Gets the <see cref="UrhoPropertyRegistry"/> instance
        /// </summary>
        public static UrhoPropertyRegistry Instance { get; }
            = new UrhoPropertyRegistry();

        /// <summary>
        /// Gets a list of all registered properties.
        /// </summary>
        internal IReadOnlyCollection<UrhoProperty> Properties => _properties.Values;

        /// <summary>
        /// Gets all non-attached <see cref="UrhoProperty"/>s registered on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A collection of <see cref="UrhoProperty"/> definitions.</returns>
        public IReadOnlyList<UrhoProperty> GetRegistered(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            if (_registeredCache.TryGetValue(type, out var result))
            {
                return result;
            }

            var t = type;
            result = new List<UrhoProperty>();

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
        /// Gets all attached <see cref="UrhoProperty"/>s registered on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A collection of <see cref="UrhoProperty"/> definitions.</returns>
        public IReadOnlyList<UrhoProperty> GetRegisteredAttached(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            if (_attachedCache.TryGetValue(type, out var result))
            {
                return result;
            }

            var t = type;
            result = new List<UrhoProperty>();

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
        /// Gets all direct <see cref="UrhoProperty"/>s registered on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A collection of <see cref="UrhoProperty"/> definitions.</returns>
        public IReadOnlyList<UrhoProperty> GetRegisteredDirect(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            if (_directCache.TryGetValue(type, out var result))
            {
                return result;
            }

            var t = type;
            result = new List<UrhoProperty>();

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
        /// Gets all inherited <see cref="UrhoProperty"/>s registered on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A collection of <see cref="UrhoProperty"/> definitions.</returns>
        public IReadOnlyList<UrhoProperty> GetRegisteredInherited(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            if (_inheritedCache.TryGetValue(type, out var result))
            {
                return result;
            }

            result = new List<UrhoProperty>();
            var visited = new HashSet<UrhoProperty>();

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
        /// Gets all <see cref="UrhoProperty"/>s registered on a object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns>A collection of <see cref="UrhoProperty"/> definitions.</returns>
        public IReadOnlyList<UrhoProperty> GetRegistered(IUrhoObject o)
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
            IUrhoObject o,
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
        public UrhoProperty FindRegistered(Type type, string name)
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
                UrhoProperty x = registered[i];

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
        public UrhoProperty FindRegistered(IUrhoObject o, string name)
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
            IUrhoObject o,
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
        internal UrhoProperty FindRegistered(int id)
        {
            return id < _properties.Count ? _properties[id] : null;
        }

        /// <summary>
        /// Checks whether a <see cref="UrhoProperty"/> is registered on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="property">The property.</param>
        /// <returns>True if the property is registered, otherwise false.</returns>
        public bool IsRegistered(Type type, UrhoProperty property)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(property != null);

            static bool ContainsProperty(IReadOnlyList<UrhoProperty> properties, UrhoProperty property)
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
        /// Checks whether a <see cref="UrhoProperty"/> is registered on a object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="property">The property.</param>
        /// <returns>True if the property is registered, otherwise false.</returns>
        public bool IsRegistered(object o, UrhoProperty property)
        {
            Contract.Requires<ArgumentNullException>(o != null);
            Contract.Requires<ArgumentNullException>(property != null);

            return IsRegistered(o.GetType(), property);
        }

        /// <summary>
        /// Registers a <see cref="UrhoProperty"/> on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="property">The property.</param>
        /// <remarks>
        /// You won't usually want to call this method directly, instead use the
        /// <see cref="UrhoProperty.Register{TOwner, TValue}(string, TValue, bool, Data.BindingMode, Func{TValue, bool}, Func{IUrhoObject, TValue, TValue}, Action{IUrhoObject, bool})"/>
        /// method.
        /// </remarks>
        public void Register(Type type, UrhoProperty property)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(property != null);

            if (!_registered.TryGetValue(type, out var inner))
            {
                inner = new Dictionary<int, UrhoProperty>();
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
                    inner = new Dictionary<int, UrhoProperty>();
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
        /// Registers an attached <see cref="UrhoProperty"/> on a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="property">The property.</param>
        /// <remarks>
        /// You won't usually want to call this method directly, instead use the
        /// <see cref="UrhoProperty.RegisterAttached{THost, TValue}(string, Type, TValue, bool, Data.BindingMode, Func{TValue, bool}, Func{IUrhoObject, TValue, TValue})"/>
        /// method.
        /// </remarks>
        public void RegisterAttached(Type type, UrhoProperty property)
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
                inner = new Dictionary<int, UrhoProperty>();
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
