using System.Collections.Specialized;

namespace Urho3DNet.MVVM.Collections
{
    /// <summary>
    /// Describes the action notified on a clear of a <see cref="UrhoList{T}"/>.
    /// </summary>
    public enum ResetBehavior
    {
        /// <summary>
        /// Clearing the list notifies a with a 
        /// <see cref="NotifyCollectionChangedAction.Reset"/>.
        /// </summary>
        Reset,

        /// <summary>
        /// Clearing the list notifies a with a
        /// <see cref="NotifyCollectionChangedAction.Remove"/>.
        /// </summary>
        Remove,
    }
}