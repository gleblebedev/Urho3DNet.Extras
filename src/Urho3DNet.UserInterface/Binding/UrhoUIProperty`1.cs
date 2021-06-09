using System;
using System.Reactive.Subjects;
using Urho3DNet.UserInterface.Data;
using Urho3DNet.UserInterface.Utilities;

namespace Urho3DNet.UserInterface
{
    /// <summary>
    /// A typed avalonia property.
    /// </summary>
    /// <typeparam name="TValue">The value type of the property.</typeparam>
    public abstract class UrhoUIProperty<TValue> : UrhoUIProperty
    {
        private readonly Subject<UrhoUIPropertyChangedEventArgs<TValue>> _changed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoUIProperty{TValue}"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="ownerType">The type of the class that registers the property.</param>
        /// <param name="metadata">The property metadata.</param>
        /// <param name="notifying">A <see cref="UrhoUIProperty.Notifying"/> callback.</param>
        protected UrhoUIProperty(
            string name,
            Type ownerType,
            UrhoUIPropertyMetadata metadata,
            Action<IUrhoUIObject, bool> notifying = null)
            : base(name, typeof(TValue), ownerType, metadata, notifying)
        {
            _changed = new Subject<UrhoUIPropertyChangedEventArgs<TValue>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoUIProperty{TValue}"/> class.
        /// </summary>
        /// <param name="source">The property to copy.</param>
        /// <param name="ownerType">The new owner type.</param>
        /// <param name="metadata">Optional overridden metadata.</param>
        [Obsolete("Use constructor with UrhoUIProperty<TValue> instead.", true)]
        protected UrhoUIProperty(
            UrhoUIProperty source,
            Type ownerType,
            UrhoUIPropertyMetadata metadata)
            : this(source as UrhoUIProperty<TValue> ?? throw new InvalidOperationException(), ownerType, metadata)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoUIProperty{TValue}"/> class.
        /// </summary>
        /// <param name="source">The property to copy.</param>
        /// <param name="ownerType">The new owner type.</param>
        /// <param name="metadata">Optional overridden metadata.</param>
        protected UrhoUIProperty(
            UrhoUIProperty<TValue> source,
            Type ownerType,
            UrhoUIPropertyMetadata metadata)
            : base(source, ownerType, metadata)
        {
            _changed = source._changed;
        }

        /// <summary>
        /// Gets an observable that is fired when this property changes on any
        /// <see cref="UrhoUIObject"/> instance.
        /// </summary>
        /// <value>
        /// An observable that is fired when this property changes on any
        /// <see cref="UrhoUIObject"/> instance.
        /// </value>

        public new IObservable<UrhoUIPropertyChangedEventArgs<TValue>> Changed => _changed;

        /// <summary>
        /// Notifies the <see cref="Changed"/> observable.
        /// </summary>
        /// <param name="e">The observable arguments.</param>
        internal void NotifyChanged(UrhoUIPropertyChangedEventArgs<TValue> e)
        {
            _changed.OnNext(e);
        }

        protected override IObservable<UrhoUIPropertyChangedEventArgs> GetChanged() => Changed;

        protected BindingValue<object> TryConvert(object value)
        {
            if (value == UnsetValue)
            {
                return BindingValue<object>.Unset;
            }
            else if (value == BindingOperations.DoNothing)
            {
                return BindingValue<object>.DoNothing;
            }

            if (!TypeUtilities.TryConvertImplicit(PropertyType, value, out var converted))
            {
                var error = new ArgumentException(string.Format(
                    "Invalid value for Property '{0}': '{1}' ({2})",
                    Name,
                    value,
                    value?.GetType().FullName ?? "(null)"));
                return BindingValue<object>.BindingError(error);
            }

            return converted;
        }
    }
}
