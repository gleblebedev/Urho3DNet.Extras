using System.Collections.Generic;

// Auto-generated file from T4 template.
using System;
using System.Numerics;

namespace Urho3DNet.InputEvents
{
#region Touchscreen touch

    public partial interface IInputListener
    {
        /// <summary>
        /// Called when a Touchscreen touch begin event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void OnTouchscreenTouchBegin(object sender, TouchEventArgs args);
        /// <summary>
        /// Called when a Touchscreen touch end event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void OnTouchscreenTouchEnd(object sender, TouchEventArgs args);
        /// <summary>
        /// Called when a Touchscreen touch moved event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void OnTouchscreenTouchMoved(object sender, TouchEventArgs args);
        /// <summary>
        /// Called when a Touchscreen touch canceled event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void OnTouchscreenTouchCanceled(object sender, TouchEventArgs args);
    }

    public partial class AbstractInputListener
    {
        /// <summary>
        /// Called when a Touchscreen touch begin event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        public virtual void OnTouchscreenTouchBegin(object sender, TouchEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnTouchscreenTouchBegin(sender, args);
        }
        /// <summary>
        /// Called when a Touchscreen touch end event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        public virtual void OnTouchscreenTouchEnd(object sender, TouchEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnTouchscreenTouchEnd(sender, args);
        }
        /// <summary>
        /// Called when a Touchscreen touch moved event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        public virtual void OnTouchscreenTouchMoved(object sender, TouchEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnTouchscreenTouchMoved(sender, args);
        }
        /// <summary>
        /// Called when a Touchscreen touch canceled event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        public virtual void OnTouchscreenTouchCanceled(object sender, TouchEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnTouchscreenTouchCanceled(sender, args);
        }
    }

    public partial class InputDemultiplexer
    {
        /// <summary>
        /// Called when a Touchscreen touch begin event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void IInputListener.OnTouchscreenTouchBegin(object sender, TouchEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnTouchscreenTouchBegin(sender, args);
            }
        }
        /// <summary>
        /// Called when a Touchscreen touch end event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void IInputListener.OnTouchscreenTouchEnd(object sender, TouchEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnTouchscreenTouchEnd(sender, args);
            }
        }
        /// <summary>
        /// Called when a Touchscreen touch moved event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void IInputListener.OnTouchscreenTouchMoved(object sender, TouchEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnTouchscreenTouchMoved(sender, args);
            }
        }
        /// <summary>
        /// Called when a Touchscreen touch canceled event has occurred.
        /// </summary>
        /// <param name="sender">Touch event source.</param>
        /// <param name="args">Touch event arguments.</param>
        void IInputListener.OnTouchscreenTouchCanceled(object sender, TouchEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnTouchscreenTouchCanceled(sender, args);
            }
        }
    }


    public partial class EventEmitter
    {
        public event EventHandler<TouchEventArgs> TouchscreenTouchBegin;

        void IInputListener.OnTouchscreenTouchBegin(object sender, TouchEventArgs args)
        {
            TouchscreenTouchBegin?.Invoke(sender, args);
        }
        public event EventHandler<TouchEventArgs> TouchscreenTouchEnd;

        void IInputListener.OnTouchscreenTouchEnd(object sender, TouchEventArgs args)
        {
            TouchscreenTouchEnd?.Invoke(sender, args);
        }
        public event EventHandler<TouchEventArgs> TouchscreenTouchMoved;

        void IInputListener.OnTouchscreenTouchMoved(object sender, TouchEventArgs args)
        {
            TouchscreenTouchMoved?.Invoke(sender, args);
        }
        public event EventHandler<TouchEventArgs> TouchscreenTouchCanceled;

        void IInputListener.OnTouchscreenTouchCanceled(object sender, TouchEventArgs args)
        {
            TouchscreenTouchCanceled?.Invoke(sender, args);
        }
    }

#endregion //Touchscreen touch


}
