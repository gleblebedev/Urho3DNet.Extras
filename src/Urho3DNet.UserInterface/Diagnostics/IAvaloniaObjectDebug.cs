using System;
using Urho3DNet.MVVM.Binding;

namespace Urho3DNet.MVVM.Diagnostics
{
    /// <summary>
    /// Provides a debug interface into <see cref="ObjectView"/>.
    /// </summary>
    public interface IUrhoObjectDebug
    {
        /// <summary>
        /// Gets the subscriber list for the <see cref="IUrhoObject.PropertyChanged"/>
        /// event.
        /// </summary>
        /// <returns>
        /// The subscribers or null if no subscribers.
        /// </returns>
        Delegate[] GetPropertyChangedSubscribers();
    }
}
