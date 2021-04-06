using System.Collections.Generic;

namespace Urho3DNet.InputEvents
{
    public sealed class StatefulInputSource : AbstractInputSource, IInputListener
    {
        private readonly HashSet<PressedButton> _gamepadButtons = new HashSet<PressedButton>();
        private readonly HashSet<UniKey> _keyboardKeys = new HashSet<UniKey>();
        private readonly HashSet<UniKey> _mouseButtons = new HashSet<UniKey>();
        private readonly Dictionary<int, ActiveTouch> _activeTouches = new Dictionary<int, ActiveTouch>();
        private readonly TouchEventArgs _touchEventArgs = new TouchEventArgs();
        private readonly KeyEventArgs _keyEventArgs = new KeyEventArgs();

        public StatefulInputSource()
        {
        }

        public StatefulInputSource(IInputSource source)
        {
            source.Listener = this;
        }

        protected override void OnListenerSet(IInputListener listener)
        {
        }

        protected override void OnListenerRemoved(IInputListener listener)
        {
            foreach (var gamepadButton in _gamepadButtons)
            {
                _keyEventArgs.Set(gamepadButton.Key, gamepadButton.DeviceId, 0, 0, 0, false);
                listener.OnGamepadButtonCanceled(this, _keyEventArgs);
            }

            _gamepadButtons.Clear();
            foreach (var gamepadButton in _keyboardKeys)
            {
                _keyEventArgs.Set(gamepadButton, 0, 0, 0, 0, false);
                listener.OnKeyboardButtonCanceled(this, _keyEventArgs);
            }

            _keyboardKeys.Clear();
            foreach (var gamepadButton in _mouseButtons)
            {
                _keyEventArgs.Set(gamepadButton, 0, 0, 0, 0, false);
                listener.OnMouseButtonCanceled(this, _keyEventArgs);
            }

            _mouseButtons.Clear();
            foreach (var touch in _activeTouches)
            {
                _touchEventArgs.Set(touch.Value.TouchId, touch.Value.X, touch.Value.Y, 0, 0, 0.0f);
                listener.OnTouchscreenTouchCanceled(this, _touchEventArgs);
            }

            _activeTouches.Clear();
        }

        void IInputListener.ListenerSubscribed(IInputSource container)
        {
        }

        void IInputListener.ListenerUnsubscribed(IInputSource container)
        {
        }

        void IInputListener.OnTouchscreenTouchBegin(object sender, TouchEventArgs args)
        {
            if (_activeTouches.ContainsKey(args.TouchId))
                Listener.OnTouchscreenTouchMoved(sender, args);
            else
                Listener.OnTouchscreenTouchBegin(sender, args);
            _activeTouches[args.TouchId] = new ActiveTouch(args.TouchId, args.X, args.Y);
        }

        void IInputListener.OnTouchscreenTouchEnd(object sender, TouchEventArgs args)
        {
            if (_activeTouches.Remove(args.TouchId)) Listener.OnTouchscreenTouchEnd(sender, args);
        }

        void IInputListener.OnTouchscreenTouchMoved(object sender, TouchEventArgs args)
        {
            if (_activeTouches.ContainsKey(args.TouchId))
            {
                Listener.OnTouchscreenTouchMoved(sender, args);
                _activeTouches[args.TouchId] = new ActiveTouch(args.TouchId, args.X, args.Y);
            }
        }

        void IInputListener.OnTouchscreenTouchCanceled(object sender, TouchEventArgs args)
        {
            if (_activeTouches.Remove(args.TouchId)) Listener.OnTouchscreenTouchCanceled(sender, args);
        }

        void IInputListener.OnMousePointerMoved(object sender, PointerEventArgs args)
        {
            Listener?.OnMousePointerMoved(sender, args);
        }

        void IInputListener.OnMouseButtonUp(object sender, KeyEventArgs args)
        {
            if (_mouseButtons.Remove(args.Key)) Listener?.OnMouseButtonUp(sender, args);
        }

        void IInputListener.OnMouseButtonDown(object sender, KeyEventArgs args)
        {
            if (_mouseButtons.Add(args.Key)) Listener?.OnMouseButtonDown(sender, args);
        }

        void IInputListener.OnMouseButtonCanceled(object sender, KeyEventArgs args)
        {
            if (_mouseButtons.Remove(args.Key)) Listener?.OnMouseButtonCanceled(sender, args);
        }

        void IInputListener.OnKeyboardButtonUp(object sender, KeyEventArgs args)
        {
            if (_keyboardKeys.Remove(args.Key)) Listener?.OnKeyboardButtonUp(sender, args);
        }

        void IInputListener.OnKeyboardButtonDown(object sender, KeyEventArgs args)
        {
            if (_keyboardKeys.Add(args.Key)) Listener?.OnKeyboardButtonDown(sender, args);
        }

        void IInputListener.OnKeyboardButtonCanceled(object sender, KeyEventArgs args)
        {
            if (_keyboardKeys.Remove(args.Key)) Listener?.OnKeyboardButtonCanceled(sender, args);
        }

        void IInputListener.OnGamepadAxisMoved(object sender, AxisEventArgs args)
        {
            Listener?.OnGamepadAxisMoved(sender, args);
        }

        void IInputListener.OnGamepadButtonUp(object sender, KeyEventArgs args)
        {
            if (_gamepadButtons.Remove(new PressedButton(args.DeviceId, args.Key)))
                Listener?.OnGamepadButtonUp(sender, args);
        }

        void IInputListener.OnGamepadButtonDown(object sender, KeyEventArgs args)
        {
            if (_gamepadButtons.Add(new PressedButton(args.DeviceId, args.Key)))
                Listener?.OnGamepadButtonDown(sender, args);
        }

        void IInputListener.OnGamepadButtonCanceled(object sender, KeyEventArgs args)
        {
            if (_gamepadButtons.Remove(new PressedButton(args.DeviceId, args.Key)))
                Listener?.OnGamepadButtonCanceled(sender, args);
        }

        void IInputListener.OnGamepadDeviceConnected(object sender, DeviceEventArgs args)
        {
            Listener?.OnGamepadDeviceConnected(sender, args);
        }

        void IInputListener.OnGamepadDeviceDisconnected(object sender, DeviceEventArgs args)
        {
            Listener?.OnGamepadDeviceDisconnected(sender, args);
        }

        private struct PressedButton
        {
            public readonly int DeviceId;
            public readonly UniKey Key;

            public PressedButton(int deviceId, UniKey key)
            {
                DeviceId = deviceId;
                Key = key;
            }
        }

        private struct ActiveTouch
        {
            public readonly int TouchId;
            public readonly int X;
            public readonly int Y;

            public ActiveTouch(int touchId, int x, int y)
            {
                TouchId = touchId;
                X = x;
                Y = y;
            }
        }
    }
}