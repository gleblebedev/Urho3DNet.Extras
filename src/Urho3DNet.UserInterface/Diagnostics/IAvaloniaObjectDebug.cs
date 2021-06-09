using System;

namespace Urho3DNet.UserInterface.Diagnostics
{
    /// <summary>
    /// Provides a debug interface into <see cref="UrhoUIObject"/>.
    /// </summary>
    public interface IUrhoUIObjectDebug
    {
        /// <summary>
        /// Gets the subscriber list for the <see cref="IUrhoUIObject.PropertyChanged"/>
        /// event.
        /// </summary>
        /// <returns>
        /// The subscribers or null if no subscribers.
        /// </returns>
        Delegate[] GetPropertyChangedSubscribers();
    }
}
