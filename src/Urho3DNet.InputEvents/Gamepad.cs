using System.Collections.Generic;

// Auto-generated file from T4 template.
using System;
using System.Numerics;

namespace Urho3DNet.InputEvents
{
#region Gamepad device

    public partial interface IInputListener
    {
        /// <summary>
        /// Called when a Gamepad device connected event has occurred.
        /// </summary>
        /// <param name="sender">Device event source.</param>
        /// <param name="args">Device event arguments.</param>
        void OnGamepadDeviceConnected(object sender, DeviceEventArgs args);
        /// <summary>
        /// Called when a Gamepad device disconnected event has occurred.
        /// </summary>
        /// <param name="sender">Device event source.</param>
        /// <param name="args">Device event arguments.</param>
        void OnGamepadDeviceDisconnected(object sender, DeviceEventArgs args);
    }

    public partial class AbstractInputListener
    {
        /// <summary>
        /// Called when a Gamepad device connected event has occurred.
        /// </summary>
        /// <param name="sender">Device event source.</param>
        /// <param name="args">Device event arguments.</param>
        public virtual void OnGamepadDeviceConnected(object sender, DeviceEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnGamepadDeviceConnected(sender, args);
        }
        /// <summary>
        /// Called when a Gamepad device disconnected event has occurred.
        /// </summary>
        /// <param name="sender">Device event source.</param>
        /// <param name="args">Device event arguments.</param>
        public virtual void OnGamepadDeviceDisconnected(object sender, DeviceEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnGamepadDeviceDisconnected(sender, args);
        }
    }

    public partial class InputDemultiplexer
    {
        /// <summary>
        /// Called when a Gamepad device connected event has occurred.
        /// </summary>
        /// <param name="sender">Device event source.</param>
        /// <param name="args">Device event arguments.</param>
        void IInputListener.OnGamepadDeviceConnected(object sender, DeviceEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnGamepadDeviceConnected(sender, args);
            }
        }
        /// <summary>
        /// Called when a Gamepad device disconnected event has occurred.
        /// </summary>
        /// <param name="sender">Device event source.</param>
        /// <param name="args">Device event arguments.</param>
        void IInputListener.OnGamepadDeviceDisconnected(object sender, DeviceEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnGamepadDeviceDisconnected(sender, args);
            }
        }
    }


    public partial class EventEmitter
    {
        public event EventHandler<DeviceEventArgs> GamepadDeviceConnected;

        void IInputListener.OnGamepadDeviceConnected(object sender, DeviceEventArgs args)
        {
            GamepadDeviceConnected?.Invoke(sender, args);
        }
        public event EventHandler<DeviceEventArgs> GamepadDeviceDisconnected;

        void IInputListener.OnGamepadDeviceDisconnected(object sender, DeviceEventArgs args)
        {
            GamepadDeviceDisconnected?.Invoke(sender, args);
        }
    }

#endregion //Gamepad device

#region Gamepad button

    public partial interface IInputListener
    {
        /// <summary>
        /// Called when a Gamepad button up event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnGamepadButtonUp(object sender, KeyEventArgs args);
        /// <summary>
        /// Called when a Gamepad button down event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnGamepadButtonDown(object sender, KeyEventArgs args);
        /// <summary>
        /// Called when a Gamepad button canceled event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void OnGamepadButtonCanceled(object sender, KeyEventArgs args);
    }

    public partial class AbstractInputListener
    {
        /// <summary>
        /// Called when a Gamepad button up event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnGamepadButtonUp(object sender, KeyEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnGamepadButtonUp(sender, args);
        }
        /// <summary>
        /// Called when a Gamepad button down event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnGamepadButtonDown(object sender, KeyEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnGamepadButtonDown(sender, args);
        }
        /// <summary>
        /// Called when a Gamepad button canceled event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        public virtual void OnGamepadButtonCanceled(object sender, KeyEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnGamepadButtonCanceled(sender, args);
        }
    }

    public partial class InputDemultiplexer
    {
        /// <summary>
        /// Called when a Gamepad button up event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void IInputListener.OnGamepadButtonUp(object sender, KeyEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnGamepadButtonUp(sender, args);
            }
        }
        /// <summary>
        /// Called when a Gamepad button down event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void IInputListener.OnGamepadButtonDown(object sender, KeyEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnGamepadButtonDown(sender, args);
            }
        }
        /// <summary>
        /// Called when a Gamepad button canceled event has occurred.
        /// </summary>
        /// <param name="sender">Button event source.</param>
        /// <param name="args">Button event arguments.</param>
        void IInputListener.OnGamepadButtonCanceled(object sender, KeyEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnGamepadButtonCanceled(sender, args);
            }
        }
    }


    public partial class EventEmitter
    {
        public event EventHandler<KeyEventArgs> GamepadButtonUp;

        void IInputListener.OnGamepadButtonUp(object sender, KeyEventArgs args)
        {
            GamepadButtonUp?.Invoke(sender, args);
        }
        public event EventHandler<KeyEventArgs> GamepadButtonDown;

        void IInputListener.OnGamepadButtonDown(object sender, KeyEventArgs args)
        {
            GamepadButtonDown?.Invoke(sender, args);
        }
        public event EventHandler<KeyEventArgs> GamepadButtonCanceled;

        void IInputListener.OnGamepadButtonCanceled(object sender, KeyEventArgs args)
        {
            GamepadButtonCanceled?.Invoke(sender, args);
        }
    }

#endregion //Gamepad button

#region Gamepad axis

    public partial interface IInputListener
    {
        /// <summary>
        /// Called when a Gamepad axis moved event has occurred.
        /// </summary>
        /// <param name="sender">Axis event source.</param>
        /// <param name="args">Axis event arguments.</param>
        void OnGamepadAxisMoved(object sender, AxisEventArgs args);
    }

    public partial class AbstractInputListener
    {
        /// <summary>
        /// Called when a Gamepad axis moved event has occurred.
        /// </summary>
        /// <param name="sender">Axis event source.</param>
        /// <param name="args">Axis event arguments.</param>
        public virtual void OnGamepadAxisMoved(object sender, AxisEventArgs args)
        {
            ((IInputListener)_inputProxy)?.OnGamepadAxisMoved(sender, args);
        }
    }

    public partial class InputDemultiplexer
    {
        /// <summary>
        /// Called when a Gamepad axis moved event has occurred.
        /// </summary>
        /// <param name="sender">Axis event source.</param>
        /// <param name="args">Axis event arguments.</param>
        void IInputListener.OnGamepadAxisMoved(object sender, AxisEventArgs args)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener?.OnGamepadAxisMoved(sender, args);
            }
        }
    }


    public partial class EventEmitter
    {
        public event EventHandler<AxisEventArgs> GamepadAxisMoved;

        void IInputListener.OnGamepadAxisMoved(object sender, AxisEventArgs args)
        {
            GamepadAxisMoved?.Invoke(sender, args);
        }
    }

#endregion //Gamepad axis


}
