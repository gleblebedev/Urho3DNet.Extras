using System;

namespace Urho3DNet.MVVM.Diagnostics
{
    /// <summary>
    /// Provides a debug interface into <see cref="INotifyCollectionChanged"/> subscribers on
    /// <see cref="UrhoList{T}"/>
    /// </summary>
    public interface INotifyCollectionChangedDebug
    {
        /// <summary>
        /// Gets the subscriber list for the <see cref="INotifyCollectionChanged.CollectionChanged"/>
        /// event.
        /// </summary>
        /// <returns>
        /// The subscribers or null if no subscribers.
        /// </returns>
        Delegate[] GetCollectionChangedSubscribers();
    }
}