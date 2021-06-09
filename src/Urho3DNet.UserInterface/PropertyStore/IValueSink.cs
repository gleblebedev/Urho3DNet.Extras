using Urho3DNet.UserInterface.Data;

#nullable enable

namespace Urho3DNet.UserInterface.PropertyStore
{
    /// <summary>
    /// Represents an entity that can receive change notifications in a <see cref="ValueStore"/>.
    /// </summary>
    internal interface IValueSink
    {
        void ValueChanged<T>(UrhoUIPropertyChangedEventArgs<T> change);

        void Completed<T>(
            StyledPropertyBase<T> property,
            IPriorityValueEntry entry,
            Optional<T> oldValue);
    }
}
