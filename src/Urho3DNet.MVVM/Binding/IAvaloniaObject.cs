using System;
using Urho3DNet.MVVM.Data;

namespace Urho3DNet.MVVM.Binding
{
    /// <summary>
    /// Interface for getting/setting <see cref="UrhoProperty"/> values on an object.
    /// </summary>
    public interface IUrhoObject
    {
        /// <summary>
        /// Raised when a <see cref="UrhoProperty"/> value changes on this object.
        /// </summary>
        event EventHandler<UrhoPropertyChangedEventArgs> PropertyChanged;

        /// <summary>
        /// Clears an <see cref="UrhoProperty"/>'s local value.
        /// </summary>
        /// <param name="property">The property.</param>
        void ClearValue<T>(StyledPropertyBase<T> property);

        /// <summary>
        /// Clears an <see cref="UrhoProperty"/>'s local value.
        /// </summary>
        /// <param name="property">The property.</param>
        void ClearValue<T>(DirectPropertyBase<T> property);

        /// <summary>
        /// Gets a <see cref="UrhoProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The value.</returns>
        T GetValue<T>(StyledPropertyBase<T> property);

        /// <summary>
        /// Gets a <see cref="UrhoProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The value.</returns>
        T GetValue<T>(DirectPropertyBase<T> property);

        /// <summary>
        /// Gets an <see cref="UrhoProperty"/> base value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="maxPriority">The maximum priority for the value.</param>
        /// <remarks>
        /// Gets the value of the property, if set on this object with a priority equal or lower to
        /// <paramref name="maxPriority"/>, otherwise <see cref="Optional{T}.Empty"/>. Note that
        /// this method does not return property values that come from inherited or default values.
        /// </remarks>
        Optional<T> GetBaseValue<T>(StyledPropertyBase<T> property, BindingPriority maxPriority);

        /// <summary>
        /// Checks whether a <see cref="UrhoProperty"/> is animating.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>True if the property is animating, otherwise false.</returns>
        bool IsAnimating(UrhoProperty property);

        /// <summary>
        /// Checks whether a <see cref="UrhoProperty"/> is set on this object.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>True if the property is set, otherwise false.</returns>
        bool IsSet(UrhoProperty property);

        /// <summary>
        /// Sets a <see cref="UrhoProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="priority">The priority of the value.</param>
        IDisposable SetValue<T>(
            StyledPropertyBase<T> property,
            T value,
            BindingPriority priority = BindingPriority.LocalValue);

        /// <summary>
        /// Sets a <see cref="UrhoProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        void SetValue<T>(DirectPropertyBase<T> property, T value);

        /// <summary>
        /// Binds a <see cref="UrhoProperty"/> to an observable.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="source">The observable.</param>
        /// <param name="priority">The priority of the binding.</param>
        /// <returns>
        /// A disposable which can be used to terminate the binding.
        /// </returns>
        IDisposable Bind<T>(
            StyledPropertyBase<T> property,
            IObservable<BindingValue<T>> source,
            BindingPriority priority = BindingPriority.LocalValue);

        /// <summary>
        /// Binds a <see cref="UrhoProperty"/> to an observable.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="source">The observable.</param>
        /// <returns>
        /// A disposable which can be used to terminate the binding.
        /// </returns>
        IDisposable Bind<T>(
            DirectPropertyBase<T> property,
            IObservable<BindingValue<T>> source);

        /// <summary>
        /// Coerces the specified <see cref="UrhoProperty"/>.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        void CoerceValue<T>(StyledPropertyBase<T> property);

        /// <summary>
        /// Registers an object as an inheritance child.
        /// </summary>
        /// <param name="child">The inheritance child.</param>
        /// <remarks>
        /// Inheritance children will receive a call to
        /// <see cref="InheritedPropertyChanged{T}(UrhoProperty{TValue}, Optional{T}, Optional{T})"/>
        /// when an inheritable property value changes on the parent.
        /// </remarks>
        void AddInheritanceChild(IUrhoObject child);

        /// <summary>
        /// Unregisters an object as an inheritance child.
        /// </summary>
        /// <param name="child">The inheritance child.</param>
        /// <remarks>
        /// Removes an inheritance child that was added by a call to
        /// <see cref="AddInheritanceChild(IUrhoObject)"/>.
        /// </remarks>
        void RemoveInheritanceChild(IUrhoObject child);

        /// <summary>
        /// Called when an inheritable property changes on an object registered as an inheritance
        /// parent.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="property">The property that has changed.</param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        void InheritedPropertyChanged<T>(
            UrhoProperty<T> property,
            Optional<T> oldValue,
            Optional<T> newValue);
    }
}
