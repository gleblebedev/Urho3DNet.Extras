using System;
using System.Collections.Generic;
using Urho3DNet.UserInterface.Data;
using Urho3DNet.UserInterface.Data.Core;
using Urho3DNet.UserInterface.Utilities;

namespace Urho3DNet.UserInterface
{
    /// <summary>
    /// Base class for avalonia properties.
    /// </summary>
    public abstract class UrhoUIProperty : IEquatable<UrhoUIProperty>, IPropertyInfo
    {
        /// <summary>
        /// Represents an unset property value.
        /// </summary>
        public static readonly object UnsetValue = new UnsetValueType();

        private static int s_nextId;
        private readonly UrhoUIPropertyMetadata _defaultMetadata;
        private readonly Dictionary<Type, UrhoUIPropertyMetadata> _metadata;
        private readonly Dictionary<Type, UrhoUIPropertyMetadata> _metadataCache = new Dictionary<Type, UrhoUIPropertyMetadata>();

        private bool _hasMetadataOverrides;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoUIProperty"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="valueType">The type of the property's value.</param>
        /// <param name="ownerType">The type of the class that registers the property.</param>
        /// <param name="metadata">The property metadata.</param>
        /// <param name="notifying">A <see cref="Notifying"/> callback.</param>
        protected UrhoUIProperty(
            string name,
            Type valueType,
            Type ownerType,
            UrhoUIPropertyMetadata metadata,
            Action<IUrhoUIObject, bool> notifying = null)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentNullException>(valueType != null);
            Contract.Requires<ArgumentNullException>(ownerType != null);
            Contract.Requires<ArgumentNullException>(metadata != null);

            if (name.Contains("."))
            {
                throw new ArgumentException("'name' may not contain periods.");
            }

            _metadata = new Dictionary<Type, UrhoUIPropertyMetadata>();

            Name = name;
            PropertyType = valueType;
            OwnerType = ownerType;
            Notifying = notifying;
            Id = s_nextId++;

            _metadata.Add(ownerType, metadata);
            _defaultMetadata = metadata;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoUIProperty"/> class.
        /// </summary>
        /// <param name="source">The direct property to copy.</param>
        /// <param name="ownerType">The new owner type.</param>
        /// <param name="metadata">Optional overridden metadata.</param>
        protected UrhoUIProperty(
            UrhoUIProperty source,
            Type ownerType,
            UrhoUIPropertyMetadata metadata)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentNullException>(ownerType != null);

            _metadata = new Dictionary<Type, UrhoUIPropertyMetadata>();

            Name = source.Name;
            PropertyType = source.PropertyType;
            OwnerType = ownerType;
            Notifying = source.Notifying;
            Id = source.Id;
            _defaultMetadata = source._defaultMetadata;

            // Properties that have different owner can't use fast path for metadata.
            _hasMetadataOverrides = true;

            if (metadata != null)
            {
                _metadata.Add(ownerType, metadata);
            }
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the property's value.
        /// </summary>
        public Type PropertyType { get; }

        /// <summary>
        /// Gets the type of the class that registered the property.
        /// </summary>
        public Type OwnerType { get; }

        /// <summary>
        /// Gets a value indicating whether the property inherits its value.
        /// </summary>
        public virtual bool Inherits => false;

        /// <summary>
        /// Gets a value indicating whether this is an attached property.
        /// </summary>
        public virtual bool IsAttached => false;

        /// <summary>
        /// Gets a value indicating whether this is a direct property.
        /// </summary>
        public virtual bool IsDirect => false;

        /// <summary>
        /// Gets a value indicating whether this is a readonly property.
        /// </summary>
        public virtual bool IsReadOnly => false;

        /// <summary>
        /// Gets an observable that is fired when this property changes on any
        /// <see cref="UrhoUIObject"/> instance.
        /// </summary>
        /// <value>
        /// An observable that is fired when this property changes on any
        /// <see cref="UrhoUIObject"/> instance.
        /// </value>
        public IObservable<UrhoUIPropertyChangedEventArgs> Changed => GetChanged();

        /// <summary>
        /// Gets a method that gets called before and after the property starts being notified on an
        /// object.
        /// </summary>
        /// <remarks>
        /// When a property changes, change notifications are sent to all property subscribers;
        /// for example via the <see cref="UrhoUIProperty.Changed"/> observable and and the
        /// <see cref="UrhoUIObject.PropertyChanged"/> event. If this callback is set for a property,
        /// then it will be called before and after these notifications take place. The bool argument
        /// will be true before the property change notifications are sent and false afterwards. This
        /// callback is intended to support Control.IsDataContextChanging.
        /// </remarks>
        public Action<IUrhoUIObject, bool> Notifying { get; }

        /// <summary>
        /// Gets the integer ID that represents this property.
        /// </summary>
        internal int Id { get; }

        /// <summary>
        /// Provides access to a property's binding via the <see cref="UrhoUIObject"/>
        /// indexer.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>A <see cref="IndexerDescriptor"/> describing the binding.</returns>
        public static IndexerDescriptor operator !(UrhoUIProperty property)
        {
            return new IndexerDescriptor
            {
                Priority = BindingPriority.LocalValue,
                Property = property,
            };
        }

        /// <summary>
        /// Provides access to a property's template binding via the <see cref="UrhoUIObject"/>
        /// indexer.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>A <see cref="IndexerDescriptor"/> describing the binding.</returns>
        public static IndexerDescriptor operator ~(UrhoUIProperty property)
        {
            return new IndexerDescriptor
            {
                Priority = BindingPriority.TemplatedParent,
                Property = property,
            };
        }

        /// <summary>
        /// Tests two <see cref="UrhoUIProperty"/>s for equality.
        /// </summary>
        /// <param name="a">The first property.</param>
        /// <param name="b">The second property.</param>
        /// <returns>True if the properties are equal, otherwise false.</returns>
        public static bool operator ==(UrhoUIProperty a, UrhoUIProperty b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }
            else if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            else
            {
                return a.Equals(b);
            }
        }

        /// <summary>
        /// Tests two <see cref="UrhoUIProperty"/>s for inequality.
        /// </summary>
        /// <param name="a">The first property.</param>
        /// <param name="b">The second property.</param>
        /// <returns>True if the properties are equal, otherwise false.</returns>
        public static bool operator !=(UrhoUIProperty a, UrhoUIProperty b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Registers a <see cref="UrhoUIProperty"/>.
        /// </summary>
        /// <typeparam name="TOwner">The type of the class that is registering the property.</typeparam>
        /// <typeparam name="TValue">The type of the property's value.</typeparam>
        /// <param name="name">The name of the property.</param>
        /// <param name="defaultValue">The default value of the property.</param>
        /// <param name="inherits">Whether the property inherits its value.</param>
        /// <param name="defaultBindingMode">The default binding mode for the property.</param>
         /// <param name="validate">A value validation callback.</param>
        /// <param name="coerce">A value coercion callback.</param>
        /// <param name="notifying">
        /// A method that gets called before and after the property starts being notified on an
        /// object; the bool argument will be true before and false afterwards. This callback is
        /// intended to support IsDataContextChanging.
        /// </param>
        /// <returns>A <see cref="StyledProperty{TValue}"/></returns>
        public static StyledProperty<TValue> Register<TOwner, TValue>(
            string name,
            TValue defaultValue = default(TValue),
            bool inherits = false,
            BindingMode defaultBindingMode = BindingMode.OneWay,
            Func<TValue, bool> validate = null,
            Func<IUrhoUIObject, TValue, TValue> coerce = null,
            Action<IUrhoUIObject, bool> notifying = null)
                where TOwner : IUrhoUIObject
        {
            Contract.Requires<ArgumentNullException>(name != null);

            var metadata = new StyledPropertyMetadata<TValue>(
                defaultValue,
                defaultBindingMode: defaultBindingMode,
                coerce: coerce);

            var result = new StyledProperty<TValue>(
                name,
                typeof(TOwner),
                metadata,
                inherits,
                validate,
                notifying);
            UrhoUIPropertyRegistry.Instance.Register(typeof(TOwner), result);
            return result;
        }

        /// <summary>
        /// Registers an attached <see cref="UrhoUIProperty"/>.
        /// </summary>
        /// <typeparam name="TOwner">The type of the class that is registering the property.</typeparam>
        /// <typeparam name="THost">The type of the class that the property is to be registered on.</typeparam>
        /// <typeparam name="TValue">The type of the property's value.</typeparam>
        /// <param name="name">The name of the property.</param>
        /// <param name="defaultValue">The default value of the property.</param>
        /// <param name="inherits">Whether the property inherits its value.</param>
        /// <param name="defaultBindingMode">The default binding mode for the property.</param>
        /// <param name="validate">A value validation callback.</param>
        /// <param name="coerce">A value coercion callback.</param>
        /// <returns>A <see cref="UrhoUIProperty{TValue}"/></returns>
        public static AttachedProperty<TValue> RegisterAttached<TOwner, THost, TValue>(
            string name,
            TValue defaultValue = default(TValue),
            bool inherits = false,
            BindingMode defaultBindingMode = BindingMode.OneWay,
            Func<TValue, bool> validate = null,
            Func<IUrhoUIObject, TValue, TValue> coerce = null)
                where THost : IUrhoUIObject
        {
            Contract.Requires<ArgumentNullException>(name != null);

            var metadata = new StyledPropertyMetadata<TValue>(
                defaultValue,
                defaultBindingMode: defaultBindingMode,
                coerce: coerce);

            var result = new AttachedProperty<TValue>(name, typeof(TOwner), metadata, inherits, validate);
            var registry = UrhoUIPropertyRegistry.Instance;
            registry.Register(typeof(TOwner), result);
            registry.RegisterAttached(typeof(THost), result);
            return result;
        }

        /// <summary>
        /// Registers an attached <see cref="UrhoUIProperty"/>.
        /// </summary>
        /// <typeparam name="THost">The type of the class that the property is to be registered on.</typeparam>
        /// <typeparam name="TValue">The type of the property's value.</typeparam>
        /// <param name="name">The name of the property.</param>
        /// <param name="ownerType">The type of the class that is registering the property.</param>
        /// <param name="defaultValue">The default value of the property.</param>
        /// <param name="inherits">Whether the property inherits its value.</param>
        /// <param name="defaultBindingMode">The default binding mode for the property.</param>
        /// <param name="validate">A value validation callback.</param>
        /// <param name="coerce">A value coercion callback.</param>
        /// <returns>A <see cref="UrhoUIProperty{TValue}"/></returns>
        public static AttachedProperty<TValue> RegisterAttached<THost, TValue>(
            string name,
            Type ownerType,
            TValue defaultValue = default(TValue),
            bool inherits = false,
            BindingMode defaultBindingMode = BindingMode.OneWay,
            Func<TValue, bool> validate = null,
            Func<IUrhoUIObject, TValue, TValue> coerce = null)
                where THost : IUrhoUIObject
        {
            Contract.Requires<ArgumentNullException>(name != null);

            var metadata = new StyledPropertyMetadata<TValue>(
                defaultValue,
                defaultBindingMode: defaultBindingMode,
                coerce: coerce);

            var result = new AttachedProperty<TValue>(name, ownerType, metadata, inherits, validate);
            var registry = UrhoUIPropertyRegistry.Instance;
            registry.Register(ownerType, result);
            registry.RegisterAttached(typeof(THost), result);
            return result;
        }

        /// <summary>
        /// Registers a direct <see cref="UrhoUIProperty"/>.
        /// </summary>
        /// <typeparam name="TOwner">The type of the class that is registering the property.</typeparam>
        /// <typeparam name="TValue">The type of the property's value.</typeparam>
        /// <param name="name">The name of the property.</param>
        /// <param name="getter">Gets the current value of the property.</param>
        /// <param name="setter">Sets the value of the property.</param>
        /// <param name="unsetValue">The value to use when the property is cleared.</param>
        /// <param name="defaultBindingMode">The default binding mode for the property.</param>
        /// <param name="enableDataValidation">
        /// Whether the property is interested in data validation.
        /// </param>
        /// <returns>A <see cref="UrhoUIProperty{TValue}"/></returns>
        public static DirectProperty<TOwner, TValue> RegisterDirect<TOwner, TValue>(
            string name,
            Func<TOwner, TValue> getter,
            Action<TOwner, TValue> setter = null,
            TValue unsetValue = default(TValue),
            BindingMode defaultBindingMode = BindingMode.OneWay,
            bool enableDataValidation = false)
                where TOwner : IUrhoUIObject
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentNullException>(getter != null);

            var metadata = new DirectPropertyMetadata<TValue>(
                unsetValue: unsetValue,
                defaultBindingMode: defaultBindingMode,
                enableDataValidation: enableDataValidation);

            var result = new DirectProperty<TOwner, TValue>(
                name,
                getter,
                setter,
                metadata);
            UrhoUIPropertyRegistry.Instance.Register(typeof(TOwner), result);
            return result;
        }

        /// <summary>
        /// Returns a binding accessor that can be passed to <see cref="UrhoUIObject"/>'s []
        /// operator to initiate a binding.
        /// </summary>
        /// <returns>A <see cref="IndexerDescriptor"/>.</returns>
        /// <remarks>
        /// The ! and ~ operators are short forms of this.
        /// </remarks>
        public IndexerDescriptor Bind()
        {
            return new IndexerDescriptor
            {
                Property = this,
            };
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var p = obj as UrhoUIProperty;
            return p != null && Equals(p);
        }

        /// <inheritdoc/>
        public bool Equals(UrhoUIProperty other)
        {
            return other != null && Id == other.Id;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// Gets the property metadata for the specified type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>
        /// The property metadata.
        /// </returns>
        public UrhoUIPropertyMetadata GetMetadata<T>() where T : IUrhoUIObject
        {
            return GetMetadata(typeof(T));
        }

        /// <summary>
        /// Gets the property metadata for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The property metadata.
        /// </returns>
        ///
        public UrhoUIPropertyMetadata GetMetadata(Type type)
        {
            if (!_hasMetadataOverrides)
            {
                return _defaultMetadata;
            }

            return GetMetadataWithOverrides(type);
        }

        /// <summary>
        /// Checks whether the <paramref name="value"/> is valid for the property.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        public bool IsValidValue(object value)
        {
            return TypeUtilities.TryConvertImplicit(PropertyType, value, out value);
        }

        /// <summary>
        /// Gets the string representation of the property.
        /// </summary>
        /// <returns>The property's string representation.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Uses the visitor pattern to resolve an untyped property to a typed property.
        /// </summary>
        /// <typeparam name="TData">The type of user data passed.</typeparam>
        /// <param name="vistor">The visitor which will accept the typed property.</param>
        /// <param name="data">The user data to pass.</param>
        public abstract void Accept<TData>(IUrhoUIPropertyVisitor<TData> vistor, ref TData data)
            where TData : struct;

        /// <summary>
        /// Routes an untyped ClearValue call to a typed call.
        /// </summary>
        /// <param name="o">The object instance.</param>
        internal abstract void RouteClearValue(IUrhoUIObject o);

        /// <summary>
        /// Routes an untyped GetValue call to a typed call.
        /// </summary>
        /// <param name="o">The object instance.</param>
        internal abstract object RouteGetValue(IUrhoUIObject o);

        /// <summary>
        /// Routes an untyped GetBaseValue call to a typed call.
        /// </summary>
        /// <param name="o">The object instance.</param>
        /// <param name="maxPriority">The maximum priority for the value.</param>
        internal abstract object RouteGetBaseValue(IUrhoUIObject o, BindingPriority maxPriority);

        /// <summary>
        /// Routes an untyped SetValue call to a typed call.
        /// </summary>
        /// <param name="o">The object instance.</param>
        /// <param name="value">The value.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>
        /// An <see cref="IDisposable"/> if setting the property can be undone, otherwise null.
        /// </returns>
        internal abstract IDisposable RouteSetValue(
            IUrhoUIObject o,
            object value,
            BindingPriority priority);

        /// <summary>
        /// Routes an untyped Bind call to a typed call.
        /// </summary>
        /// <param name="o">The object instance.</param>
        /// <param name="source">The binding source.</param>
        /// <param name="priority">The priority.</param>
        internal abstract IDisposable RouteBind(
            IUrhoUIObject o,
            IObservable<BindingValue<object>> source,
            BindingPriority priority);

        internal abstract void RouteInheritanceParentChanged(UrhoUIObject o, IUrhoUIObject oldParent);

        /// <summary>
        /// Overrides the metadata for the property on the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="metadata">The metadata.</param>
        protected void OverrideMetadata(Type type, UrhoUIPropertyMetadata metadata)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(metadata != null);

            if (_metadata.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    $"Metadata is already set for {Name} on {type}.");
            }

            var baseMetadata = GetMetadata(type);
            metadata.Merge(baseMetadata, this);
            _metadata.Add(type, metadata);
            _metadataCache.Clear();

            _hasMetadataOverrides = true;
        }

        protected abstract IObservable<UrhoUIPropertyChangedEventArgs> GetChanged();

        private UrhoUIPropertyMetadata GetMetadataWithOverrides(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (_metadataCache.TryGetValue(type, out UrhoUIPropertyMetadata result))
            {
                return result;
            }

            Type currentType = type;

            while (currentType != null)
            {
                if (_metadata.TryGetValue(currentType, out result))
                {
                    _metadataCache[type] = result;

                    return result;
                }

                currentType = currentType.BaseType;
            }

            _metadataCache[type] = _defaultMetadata;

            return _defaultMetadata;
        }

        bool IPropertyInfo.CanGet => true;
        bool IPropertyInfo.CanSet => true;
        object IPropertyInfo.Get(object target) => ((UrhoUIObject)target).GetValue(this);
        void IPropertyInfo.Set(object target, object value) => ((UrhoUIObject)target).SetValue(this, value);
    }

    /// <summary>
    /// Class representing the <see cref="UrhoUIProperty.UnsetValue"/>.
    /// </summary>
    public sealed class UnsetValueType
    {
        internal UnsetValueType() { }

        /// <summary>
        /// Returns the string representation of the <see cref="UrhoUIProperty.UnsetValue"/>.
        /// </summary>
        /// <returns>The string "(unset)".</returns>
        public override string ToString() => "(unset)";
    }
}