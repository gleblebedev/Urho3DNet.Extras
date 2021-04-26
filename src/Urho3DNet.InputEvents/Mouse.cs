using System.Collections.Generic;

// Auto-generated file from T4 template.
using System;
using System.Numerics;

namespace Urho3DNet.InputEvents
{
#region Mouse button

    public partial interface IInputListener
    {
        /// <summary>
        /// Called when a Mouse button up event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnMouseButtonUp(object sender, KeyEventArgs args);
        /// <summary>
        /// Called when a Mouse button down event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnMouseButtonDown(object sender, KeyEventArgs args);
        /// <summary>
        /// Called when a Mouse button canceled event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnMouseButtonCanceled(object sender, KeyEventArgs args);
    }

    public partial class AbstractInputListener
    {
        /// <summary>
        /// Called when a Mouse button up event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnMouseButtonUp(object sender, KeyEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnMouseButtonUp(sender, args);
        }
        /// <summary>
        /// Called when a Mouse button down event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnMouseButtonDown(object sender, KeyEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnMouseButtonDown(sender, args);
        }
        /// <summary>
        /// Called when a Mouse button canceled event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnMouseButtonCanceled(object sender, KeyEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnMouseButtonCanceled(sender, args);
        }
    }

    public partial class InputDemultiplexer
    {
        /// <summary>
        /// Called when a Mouse button up event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void IInputListener.OnMouseButtonUp(object sender, KeyEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnMouseButtonUp(sender, args);
            }
        }
        /// <summary>
        /// Called when a Mouse button down event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void IInputListener.OnMouseButtonDown(object sender, KeyEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnMouseButtonDown(sender, args);
            }
        }
        /// <summary>
        /// Called when a Mouse button canceled event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void IInputListener.OnMouseButtonCanceled(object sender, KeyEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnMouseButtonCanceled(sender, args);
            }
        }
    }


    public partial class EventEmitter
    {
        public event EventHandler<KeyEventArgs> MouseButtonUp;

        void IInputListener.OnMouseButtonUp(object sender, KeyEventArgs args)
        {
            MouseButtonUp?.Invoke(sender, args);
        }
        public event EventHandler<KeyEventArgs> MouseButtonDown;

        void IInputListener.OnMouseButtonDown(object sender, KeyEventArgs args)
        {
            MouseButtonDown?.Invoke(sender, args);
        }
        public event EventHandler<KeyEventArgs> MouseButtonCanceled;

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
        /// Called when a Mouse pointer moved event has occurred.
        /// </summary>
        /// <param name="sender">Pointer event source.</param>
        /// <param name="args">Pointer event arguments.</param>
        void OnMousePointerMoved(object sender, PointerEventArgs args);
    }

    public partial class AbstractInputListener
    {
        /// <summary>
        /// Called when a Mouse pointer moved event has occurred.
        /// </summary>
        /// <param name="sender">Pointer event source.</param>
        /// <param name="args">Pointer event arguments.</param>
        public virtual void OnMousePointerMoved(object sender, PointerEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnMousePointerMoved(sender, args);
        }
    }

    public partial class InputDemultiplexer
    {
        /// <summary>
        /// Called when a Mouse pointer moved event has occurred.
        /// </summary>
        /// <param name="sender">Pointer event source.</param>
        /// <param name="args">Pointer event arguments.</param>
        void IInputListener.OnMousePointerMoved(object sender, PointerEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnMousePointerMoved(sender, args);
            }
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
