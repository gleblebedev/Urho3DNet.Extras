using System;
using Urho3DNet.UserInterface.Data;

#nullable enable

namespace Urho3DNet.UserInterface
{
    /// <summary>
    /// Provides information for a avalonia property change.
    /// </summary>
    public abstract class UrhoUIPropertyChangedEventArgs : EventArgs
    {
        public UrhoUIPropertyChangedEventArgs(
            IUrhoUIObject sender,
            BindingPriority priority)
        {
            Sender = sender;
            Priority = priority;
            IsEffectiveValueChange = true;
        }

        /// <summary>
        /// Gets the <see cref="UrhoUIObject"/> that the property changed on.
        /// </summary>
        /// <value>The sender object.</value>
        public IUrhoUIObject Sender { get; }

        /// <summary>
        /// Gets the property that changed.
        /// </summary>
        /// <value>
        /// The property that changed.
        /// </value>
        public UrhoUIProperty Property => GetProperty();

        /// <summary>
        /// Gets the old value of the property.
        /// </summary>
        public object? OldValue => GetOldValue();

        /// <summary>
        /// Gets the new value of the property.
        /// </summary>
        public object? NewValue => GetNewValue();

        /// <summary>
        /// Gets the priority of the binding that produced the value.
        /// </summary>
        /// <value>
        /// The priority of the new value.
        /// </value>
        public BindingPriority Priority { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the change represents a change to the effective value of
        /// the property.
        /// </summary>
        /// <remarks>
        /// This will usually be true, except in
        /// <see cref="UrhoUIObject.OnPropertyChangedCore{T}(UrhoUIPropertyChangedEventArgs{T})"/>
        /// which recieves notifications for all changes to property values, whether a value with a higher
        /// priority is present or not. When this property is false, the change that is being signalled
        /// has not resulted in a change to the property value on the object.
        /// </remarks>
        public bool IsEffectiveValueChange { get; private set; }

        internal void MarkNonEffectiveValue() => IsEffectiveValueChange = false;
        protected abstract UrhoUIProperty GetProperty();
        protected abstract object? GetOldValue();
        protected abstract object? GetNewValue();
    }
}
