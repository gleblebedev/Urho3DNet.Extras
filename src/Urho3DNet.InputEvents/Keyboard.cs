using System; // Auto-generated file from T4 template.

namespace Urho3DNet.InputEvents
{
    #region Keyboard button

    public partial interface IInputListener
    {
        /// <summary>
        ///     Called when a Keyboard button up event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnKeyboardButtonUp(object sender, KeyEventArgs args);

        /// <summary>
        ///     Called when a Keyboard button down event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnKeyboardButtonDown(object sender, KeyEventArgs args);

        /// <summary>
        ///     Called when a Keyboard button canceled event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnKeyboardButtonCanceled(object sender, KeyEventArgs args);
    }

    public partial class AbstractInputListener
    {
        /// <summary>
        ///     Called when a Keyboard button up event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnKeyboardButtonUp(object sender, KeyEventArgs args)
        {
        }

        /// <summary>
        ///     Called when a Keyboard button down event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnKeyboardButtonDown(object sender, KeyEventArgs args)
        {
        }

        /// <summary>
        ///     Called when a Keyboard button canceled event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnKeyboardButtonCanceled(object sender, KeyEventArgs args)
        {
        }
    }

    public partial class EventEmitter
    {
        public event EventHandler<KeyEventArgs> KeyboardButtonUp;
        public event EventHandler<KeyEventArgs> KeyboardButtonDown;
        public event EventHandler<KeyEventArgs> KeyboardButtonCanceled;

        void IInputListener.OnKeyboardButtonUp(object sender, KeyEventArgs args)
        {
            KeyboardButtonUp?.Invoke(sender, args);
        }

        void IInputListener.OnKeyboardButtonDown(object sender, KeyEventArgs args)
        {
            KeyboardButtonDown?.Invoke(sender, args);
        }

        void IInputListener.OnKeyboardButtonCanceled(object sender, KeyEventArgs args)
        {
            KeyboardButtonCanceled?.Invoke(sender, args);
        }
    }

    #endregion //Keyboard button
}