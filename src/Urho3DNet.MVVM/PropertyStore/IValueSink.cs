using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Data;

#nullable enable

namespace Urho3DNet.MVVM.PropertyStore
{
    /// <summary>
    /// Represents an entity that can receive change notifications in a <see cref="ValueStore"/>.
    /// </summary>
    internal interface IValueSink
    {
        void ValueChanged<T>(UrhoPropertyChangedEventArgs<T> change);

        void Completed<T>(
            StyledPropertyBase<T> property,
            IPriorityValueEntry entry,
            Optional<T> oldValue);
    }
}
