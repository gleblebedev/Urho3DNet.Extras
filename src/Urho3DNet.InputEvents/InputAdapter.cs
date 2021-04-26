using System;

namespace Urho3DNet.InputEvents
{
    public class InputAdapter : AbstractInputSource, IDisposable
    {
        private readonly InputEventsAdapter _inputAdapter;
        private readonly Input _input;
        private readonly KeyEventArgs _keyEventArgs = new KeyEventArgs();
        private readonly AxisEventArgs _axisEventArgs = new AxisEventArgs();
        private readonly PointerEventArgs _pointerEventArgs = new PointerEventArgs();
        private readonly TouchEventArgs _touchEventArgs = new TouchEventArgs();
        private readonly DeviceEventArgs _deviceEventArgs = new DeviceEventArgs();
        private readonly SharedPtr<Object> _subscription;

        public InputAdapter(Input input)
        {
            _input = input;
            _subscription = new Object(input.Context); // input.Context.CreateObject<Object>();
            _inputAdapter = new InputEventsAdapter(_subscription);
            _inputAdapter.JoystickAxisMove += TranslateJoystickAxisMove;
            _inputAdapter.JoystickButtonDown += TranslateJoystickButtonDown;
            _inputAdapter.JoystickButtonUp += TranslateJoystickButtonUp;
            _inputAdapter.JoystickConnected += TranslateJoystickConnected;
            _inputAdapter.JoystickDisconnected += TranslateJoystickDisconnected;

            _inputAdapter.MouseButtonDown += TranslateMouseButtonDown;
            _inputAdapter.MouseButtonUp += TranslateMouseButtonUp;
            _inputAdapter.MouseMove += TranslateMouseMove;
            _inputAdapter.MouseWheel += TranslateMouseWheel;

            _inputAdapter.KeyDown += TranslateKeyDown;
            _inputAdapter.KeyUp += TranslateKeyUp;

            _inputAdapter.TouchBegin += TranslateTouchBegin;
            _inputAdapter.TouchEnd += TranslateTouchEnd;
            _inputAdapter.TouchMove += TranslateTouchMove;
        }

        public float AxisDeadZone { get; set; } = 0.1f;

        public void Dispose()
        {
            _input.Dispose();
            _subscription.Dispose();
        }

        private void TranslateTouchMove(object sender, InputEventsAdapter.TouchMoveEventArgs args)
        {
            if (Listener != null)
            {
                TouchEventArgs.FromTouchMove(_touchEventArgs, args);
                Listener.OnTouchscreenTouchMoved(this, _touchEventArgs);
            }
        }


        private void TranslateTouchEnd(object sender, InputEventsAdapter.TouchEndEventArgs args)
        {
            if (Listener != null)
            {
                TouchEventArgs.FromTouchEnd(_touchEventArgs, args);
                Listener.OnTouchscreenTouchEnd(this, _touchEventArgs);
            }
        }

        private void TranslateTouchBegin(object sender, InputEventsAdapter.TouchBeginEventArgs args)
        {
            if (Listener != null)
            {
                TouchEventArgs.FromTouchBegin(_touchEventArgs, args);
                Listener.OnTouchscreenTouchBegin(this, _touchEventArgs);
            }
        }

        private void TranslateJoystickDisconnected(object sender, InputEventsAdapter.JoystickDisconnectedEventArgs args)
        {
            if (Listener != null)
            {
                DeviceEventArgs.FromJoystickDisconnected(_deviceEventArgs, args);
                Listener.OnGamepadDeviceDisconnected(this, _deviceEventArgs);
            }
        }

        private void TranslateJoystickConnected(object sender, InputEventsAdapter.JoystickConnectedEventArgs args)
        {
            if (Listener != null)
            {
                DeviceEventArgs.FromJoystickConnected(_deviceEventArgs, args);
                Listener.OnGamepadDeviceConnected(this, _deviceEventArgs);
            }
        }

        private void TranslateMouseWheel(object sender, InputEventsAdapter.MouseWheelEventArgs args)
        {
            if (Listener != null)
            {
                //PointerEventArgs.FromMouseMove(_pointerEventArgs, args);
                //Listener.OnMousePointerMoved(this, _pointerEventArgs);
            }
        }

        private void TranslateMouseMove(object sender, InputEventsAdapter.MouseMoveEventArgs args)
        {
            if (Listener != null)
            {
                PointerEventArgs.FromMouseMove(_pointerEventArgs, args);
                Listener.OnMousePointerMoved(this, _pointerEventArgs);
            }
        }

        private void TranslateJoystickAxisMove(object sender, InputEventsAdapter.JoystickAxisMoveEventArgs args)
        {
            if (Listener != null)
            {
                AxisEventArgs.FromJoystickAxisMove(_axisEventArgs, args, _input, AxisDeadZone);
                Listener.OnGamepadAxisMoved(this, _axisEventArgs);
            }
        }

        private void TranslateJoystickButtonUp(object sender, InputEventsAdapter.JoystickButtonUpEventArgs args)
        {
            if (Listener != null)
            {
                KeyEventArgs.FromJoystickButtonUpEvent(_keyEventArgs, args, _input);
                Listener.OnGamepadButtonUp(this, _keyEventArgs);
            }
        }

        private void TranslateJoystickButtonDown(object sender, InputEventsAdapter.JoystickButtonDownEventArgs args)
        {
            if (Listener != null)
            {
                KeyEventArgs.FromJoystickButtonDownEvent(_keyEventArgs, args, _input);
                Listener.OnGamepadButtonDown(this, _keyEventArgs);
            }
        }

        private void TranslateMouseButtonUp(object sender, InputEventsAdapter.MouseButtonUpEventArgs args)
        {
            if (Listener != null)
            {
                KeyEventArgs.FromMouseButtonUp(_keyEventArgs, args);
                Listener.OnMouseButtonUp(this, _keyEventArgs);
            }
        }

        private void TranslateMouseButtonDown(object sender, InputEventsAdapter.MouseButtonDownEventArgs args)
        {
            if (Listener != null)
            {
                KeyEventArgs.FromMouseButtonDown(_keyEventArgs, args);
                Listener.OnMouseButtonDown(this, _keyEventArgs);
            }
        }

        private void TranslateKeyUp(object sender, InputEventsAdapter.KeyUpEventArgs args)
        {
            if (Listener != null)
            {
                KeyEventArgs.FromKeyUp(_keyEventArgs, args);
                Listener.OnKeyboardButtonUp(this, _keyEventArgs);
            }
        }

        private void TranslateKeyDown(object sender, InputEventsAdapter.KeyDownEventArgs args)
        {
            if (Listener != null)
            {
                KeyEventArgs.FromKeyDown(_keyEventArgs, args);
                Listener.OnKeyboardButtonDown(this, _keyEventArgs);
            }
        }
    }
}