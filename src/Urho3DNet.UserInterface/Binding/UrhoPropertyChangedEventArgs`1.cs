using Urho3DNet.MVVM.Data;

#nullable enable

namespace Urho3DNet.MVVM.Binding
{
    /// <summary>
    /// Provides information for an Urho property change.
    /// </summary>
    public class UrhoPropertyChangedEventArgs<T> : UrhoPropertyChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="sender">The object that the property changed on.</param>
        /// <param name="property">The property that changed.</param>
        /// <param name="oldValue">The old value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="priority">The priority of the binding that produced the value.</param>
        public UrhoPropertyChangedEventArgs(
            IUrhoObject sender,
            UrhoProperty<T> property,
            Optional<T> oldValue,
            BindingValue<T> newValue,
            BindingPriority priority)
            : base(sender, priority)
        {
            Property = property;
            OldValue = oldValue;
            NewValue = newValue;
        }

        /// <summary>
        /// Gets the property that changed.
        /// </summary>
        /// <value>
        /// The property that changed.
        /// </value>
        public new UrhoProperty<T> Property { get; }

        /// <summary>
        /// Gets the old value of the property.
        /// </summary>
        /// <remarks>
        /// When <see cref="UrhoPropertyChangedEventArgs.IsEffectiveValueChange"/> is true, returns the
        /// old value of the property on the object. 
        /// When <see cref="UrhoPropertyChangedEventArgs.IsEffectiveValueChange"/> is false, returns
        /// <see cref="Optional{T}.Empty"/>.
        /// </remarks>
        public new Optional<T> OldValue { get; private set; }

        /// <summary>
        /// Gets the new value of the property.
        /// </summary>
        /// <remarks>
        /// When <see cref="UrhoPropertyChangedEventArgs.IsEffectiveValueChange"/> is true, returns the
        /// value of the property on the object.
        /// When <see cref="UrhoPropertyChangedEventArgs.IsEffectiveValueChange"/> is false returns the
        /// changed value, or <see cref="Optional{T}.Empty"/> if the value was removed.
        /// </remarks>
        public new BindingValue<T> NewValue { get; private set; }

        internal void SetOldValue(Optional<T> value) => OldValue = value;
        internal void SetNewValue(BindingValue<T> value) => NewValue = value;

        protected override UrhoProperty GetProperty() => Property;

        protected override object? GetOldValue() => OldValue.GetValueOrDefault(UrhoProperty.UnsetValue);

        protected override object? GetNewValue() => NewValue.GetValueOrDefault(UrhoProperty.UnsetValue);
    }
}
