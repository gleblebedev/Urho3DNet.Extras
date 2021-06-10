using System;

namespace Urho3DNet.MVVM.Binding
{
    /// <summary>
    /// Exception signifying an internal logic error in Urho.
    /// </summary>
    public class UrhoInternalException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoInternalException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public UrhoInternalException(string message)
            : base(message)
        {
        }
    }
}
