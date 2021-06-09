using System;

namespace Urho3DNet.UserInterface
{
    /// <summary>
    /// Exception signifying an internal logic error in UrhoUI.
    /// </summary>
    public class UrhoUIInternalException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoUIInternalException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public UrhoUIInternalException(string message)
            : base(message)
        {
        }
    }
}
