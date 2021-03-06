using System.Diagnostics.CodeAnalysis;
using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Data;

#nullable enable

namespace Urho3DNet.MVVM.PropertyStore
{
    /// <summary>
    /// Stores a value with local value priority in a <see cref="ValueStore"/> or
    /// <see cref="PriorityValue{T}"/>.
    /// </summary>
    /// <typeparam name="T">The property type.</typeparam>
    internal class LocalValueEntry<T> : IValue<T>
    {
        /*[AllowNull]*/ private T _value;

        public LocalValueEntry(/*[AllowNull]*/ T value) => _value = value;
        public BindingPriority Priority => BindingPriority.LocalValue;
        Optional<object> IValue.GetValue() => new Optional<object>(_value);
        
        public Optional<T> GetValue(BindingPriority maxPriority)
        {
            return BindingPriority.LocalValue >= maxPriority ? _value : Optional<T>.Empty;
        }

        public void SetValue(T value) => _value = value;
        public void Start() { }

        public void RaiseValueChanged(
            IValueSink sink,
            IUrhoObject owner,
            UrhoProperty property,
            Optional<object> oldValue,
            Optional<object> newValue)
        {
            sink.ValueChanged(new UrhoPropertyChangedEventArgs<T>(
                owner,
                (UrhoProperty<T>)property,
                oldValue.Cast<T>(),
                newValue.Cast<T>(),
                BindingPriority.LocalValue));
        }
    }
}
