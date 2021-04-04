using System; // Auto-generated file from T4 template.

namespace Urho3DNet.InputEvents
{
    #region Mouse button

    public partial interface IInputListener
    {
        /// <summary>
        ///     Called when a Mouse button up event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnMouseButtonUp(object sender, KeyEventArgs args);

        /// <summary>
        ///     Called when a Mouse button down event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnMouseButtonDown(object sender, KeyEventArgs args);

        /// <summary>
        ///     Called when a Mouse button canceled event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnMouseButtonCanceled(object sender, KeyEventArgs args);
    }

    public partial class AbstractInputListener
    {
        /// <summary>
        ///     Called when a Mouse button up event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnMouseButtonUp(object sender, KeyEventArgs args)
        {
        }

        /// <summary>
        ///     Called when a Mouse button down event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnMouseButtonDown(object sender, KeyEventArgs args)
        {
        }

        /// <summary>
        ///     Called when a Mouse button canceled event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnMouseButtonCanceled(object sender, KeyEventArgs args)
        {
        }
    }

    public partial class EventEmitter
    {
        public event EventHandler<KeyEventArgs> MouseButtonUp;
        public event EventHandler<KeyEventArgs> MouseButtonDown;
        public event EventHandler<KeyEventArgs> MouseButtonCanceled;

        void IInputListener.OnMouseButtonUp(object sender, KeyEventArgs args)
        {
            MouseButtonUp?.Invoke(sender, args);
        }

        void IInputListener.OnMouseButtonDown(object sender, KeyEventArgs args)
        {
            MouseButtonDown?.Invoke(sender, args);
        }

        void IInputListener.OnMouseButtonCanceled(object sender, KeyEventArgs args)
        {
            MouseButtonCanceled?.Invoke(sender, args);
        }
    }

    #endregion //Mouse button

    #region Mouse pointer

    public partial interface IInputListener
    {
        /// <summary>
        ///     Called when a Mouse pointer moved event has occurred.
        /// </summary>
        /// <param name="sender">Pointer event source.</param>
        /// <param name="args">Pointer event arguments.</param>
        void OnMousePointerMoved(object sender, PointerEventArgs args);
    }

    public partial class AbstractInputListener
    {
        /// <summary>
        ///     Called when a Mouse pointer moved event has occurred.
        /// </summary>
        /// <param name="sender">Pointer event source.</param>
        /// <param name="args">Pointer event arguments.</param>
        public virtual void OnMousePointerMoved(object sender, PointerEventArgs args)
        {
        }
    }

    public partial class EventEmitter
    {
        public event EventHandler<PointerEventArgs> MousePointerMoved;

        void IInputListener.OnMousePointerMoved(object sender, PointerEventArgs args)
        {
            MousePointerMoved?.Invoke(sender, args);
        }
    }

    #endregion //Mouse pointer
}