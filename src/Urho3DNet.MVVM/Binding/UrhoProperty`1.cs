using System;
using System.Reactive.Subjects;
using Urho3DNet.MVVM.Data;
using Urho3DNet.MVVM.Utilities;

namespace Urho3DNet.MVVM.Binding
{
    /// <summary>
    /// A typed avalonia property.
    /// </summary>
    /// <typeparam name="TValue">The value type of the property.</typeparam>
    public abstract class UrhoProperty<TValue> : UrhoProperty
    {
        private readonly Subject<UrhoPropertyChangedEventArgs<TValue>> _changed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoProperty{TValue}"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="ownerType">The type of the class that registers the property.</param>
        /// <param name="metadata">The property metadata.</param>
        /// <param name="notifying">A <see cref="UrhoProperty.Notifying"/> callback.</param>
        protected UrhoProperty(
            string name,
            Type ownerType,
            UrhoPropertyMetadata metadata,
            Action<IUrhoObject, bool> notifying = null)
            : base(name, typeof(TValue), ownerType, metadata, notifying)
        {
            _changed = new Subject<UrhoPropertyChangedEventArgs<TValue>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoProperty{TValue}"/> class.
        /// </summary>
        /// <param name="source">The property to copy.</param>
        /// <param name="ownerType">The new owner type.</param>
        /// <param name="metadata">Optional overridden metadata.</param>
        [Obsolete("Use constructor with UrhoProperty<TValue> instead.", true)]
        protected UrhoProperty(
            UrhoProperty source,
            Type ownerType,
            UrhoPropertyMetadata metadata)
            : this(source as UrhoProperty<TValue> ?? throw new InvalidOperationException(), ownerType, metadata)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoProperty{TValue}"/> class.
        /// </summary>
        /// <param name="source">The property to copy.</param>
        /// <param name="ownerType">The new owner type.</param>
        /// <param name="metadata">Optional overridden metadata.</param>
        protected UrhoProperty(
            UrhoProperty<TValue> source,
            Type ownerType,
            UrhoPropertyMetadata metadata)
            : base(source, ownerType, metadata)
        {
            _changed = source._changed;
        }

        /// <summary>
        /// Gets an observable that is fired when this property changes on any
        /// <see cref="ObjectView"/> instance.
        /// </summary>
        /// <value>
        /// An observable that is fired when this property changes on any
        /// <see cref="ObjectView"/> instance.
        /// </value>

        public new IObservable<UrhoPropertyChangedEventArgs<TValue>> Changed => _changed;

        /// <summary>
        /// Notifies the <see cref="Changed"/> observable.
        /// </summary>
        /// <param name="e">The observable arguments.</param>
        internal void NotifyChanged(UrhoPropertyChangedEventArgs<TValue> e)
        {
            _changed.OnNext(e);
        }

        protected override IObservable<UrhoPropertyChangedEventArgs> GetChanged() => Changed;

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
