using System; // Auto-generated file from T4 template.

namespace Urho3DNet.InputEvents
{
    #region Touchscreen touch

    public partial interface IInputListener
    {
        /// <summary>
        ///     Called when a Touchscreen touch begin event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void OnTouchscreenTouchBegin(object sender, TouchEventArgs args);

        /// <summary>
        ///     Called when a Touchscreen touch end event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void OnTouchscreenTouchEnd(object sender, TouchEventArgs args);

        /// <summary>
        ///     Called when a Touchscreen touch moved event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void OnTouchscreenTouchMoved(object sender, TouchEventArgs args);

        /// <summary>
        ///     Called when a Touchscreen touch canceled event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void OnTouchscreenTouchCanceled(object sender, TouchEventArgs args);
    }

    public partial class AbstractInputListener
    {
        /// <summary>
        ///     Called when a Touchscreen touch begin event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        public virtual void OnTouchscreenTouchBegin(object sender, TouchEventArgs args)
        {
        }

        /// <summary>
        ///     Called when a Touchscreen touch end event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        public virtual void OnTouchscreenTouchEnd(object sender, TouchEventArgs args)
        {
        }

        /// <summary>
        ///     Called when a Touchscreen touch moved event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        public virtual void OnTouchscreenTouchMoved(object sender, TouchEventArgs args)
        {
        }

        /// <summary>
        ///     Called when a Touchscreen touch canceled event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        public virtual void OnTouchscreenTouchCanceled(object sender, TouchEventArgs args)
        {
        }
    }

    public partial class EventEmitter
    {
        public event EventHandler<TouchEventArgs> TouchscreenTouchBegin;
        public event EventHandler<TouchEventArgs> TouchscreenTouchEnd;
        public event EventHandler<TouchEventArgs> TouchscreenTouchMoved;
        public event EventHandler<TouchEventArgs> TouchscreenTouchCanceled;

        void IInputListener.OnTouchscreenTouchBegin(object sender, TouchEventArgs args)
        {
            TouchscreenTouchBegin?.Invoke(sender, args);
        }

        void IInputListener.OnTouchscreenTouchEnd(object sender, TouchEventArgs args)
        {
            TouchscreenTouchEnd?.Invoke(sender, args);
        }

        void IInputListener.OnTouchscreenTouchMoved(object sender, TouchEventArgs args)
        {
            TouchscreenTouchMoved?.Invoke(sender, args);
        }

        void IInputListener.OnTouchscreenTouchCanceled(object sender, TouchEventArgs args)
        {
            TouchscreenTouchCanceled?.Invoke(sender, args);
        }
    }

    #endregion //Touchscreen touch
}