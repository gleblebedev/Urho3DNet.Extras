using System;

namespace Urho3DNet.InputEvents
{
    public class InputAdapter : AbstractInputSource, IDisposable
    {
        private readonly Input _input;

        public InputAdapter(Input input)
        {
            _input = input;
            _input.SubscribeToEvent(E.KeyDown, TranslateKeyDown);
            _input.SubscribeToEvent(E.KeyUp, TranslateKeyUp);
            _input.SubscribeToEvent(E.MouseButtonDown, TranslateMouseButtonDown);
            _input.SubscribeToEvent(E.MouseButtonUp, TranslateMouseButtonUp);
            _input.SubscribeToEvent(E.JoystickConnected, TranslateJoystickConnected);
            _input.SubscribeToEvent(E.JoystickDisconnected, TranslateJoystickDisconnected);
            _input.SubscribeToEvent(E.JoystickButtonDown, TranslateJoystickButtonDown);
            _input.SubscribeToEvent(E.JoystickButtonUp, TranslateJoystickButtonUp);
            _input.SubscribeToEvent(E.JoystickAxisMove, TranslateJoystickAxisMove);
            _input.SubscribeToEvent(E.TouchBegin, TranslateTouchBegin);
            _input.SubscribeToEvent(E.TouchEnd, TranslateTouchEnd);
            _input.SubscribeToEvent(E.TouchMove, TranslateTouchMove);
            _input.SubscribeToEvent(E.MouseMove, TranslateMouseMove);
            _input.SubscribeToEvent(E.MouseWheel, TranslateMouseWheel);
        }


        public void Dispose()
        {
            _input.UnsubscribeFromEvent(E.KeyDown);
            _input.UnsubscribeFromEvent(E.KeyUp);
            _input.UnsubscribeFromEvent(E.MouseButtonDown);
            _input.UnsubscribeFromEvent(E.MouseButtonUp);
            _input.UnsubscribeFromEvent(E.JoystickConnected);
            _input.UnsubscribeFromEvent(E.JoystickDisconnected);
            _input.UnsubscribeFromEvent(E.JoystickButtonDown);
            _input.UnsubscribeFromEvent(E.JoystickButtonUp);
            _input.UnsubscribeFromEvent(E.JoystickAxisMove);
            _input.UnsubscribeFromEvent(E.TouchBegin);
            _input.UnsubscribeFromEvent(E.TouchEnd);
            _input.UnsubscribeFromEvent(E.TouchMove);
            _input.UnsubscribeFromEvent(E.MouseMove);
            _input.UnsubscribeFromEvent(E.MouseWheel);
        }

        private void TranslateTouchMove(VariantMap args)
        {
            Listener.OnTouchscreenTouchMoved(this, TouchEventArgs.FromTouchMove(args));
        }


        private void TranslateTouchEnd(VariantMap args)
        {
            Listener.OnTouchscreenTouchEnd(this, TouchEventArgs.FromTouchEnd(args));
        }

        private void TranslateTouchBegin(VariantMap args)
        {
            Listener.OnTouchscreenTouchBegin(this, TouchEventArgs.FromTouchBegin(args));
        }

        private void TranslateJoystickDisconnected(VariantMap args)
        {
            Listener.OnGamepadDeviceDisconnected(this, DeviceEventArgs.FromJoystickDisconnected(args));
        }

        private void TranslateJoystickConnected(VariantMap args)
        {
            Listener.OnGamepadDeviceConnected(this, DeviceEventArgs.FromJoystickConnected(args));
        }

        private void TranslateMouseWheel(VariantMap args)
        {
        }

        private void TranslateMouseMove(VariantMap args)
        {
            Listener.OnMousePointerMoved(this, PointerEventArgs.FromMouseMove(args));
        }

        private void TranslateJoystickAxisMove(VariantMap args)
        {
            Listener.OnGamepadAxisMoved(this, AxisEventArgs.FromJoystickAxisMove(args, _input));
        }

        private void TranslateJoystickButtonUp(VariantMap args)
        {
            Listener.OnGamepadButtonUp(this, KeyEventArgs.FromJoystickButtonUpEvent(args, _input));
        }

        private void TranslateJoystickButtonDown(VariantMap args)
        {
            Listener.OnGamepadButtonDown(this, KeyEventArgs.FromJoystickButtonDownEvent(args, _input));
        }

        private void TranslateMouseButtonUp(VariantMap args)
        {
            Listener.OnMouseButtonUp(this, KeyEventArgs.FromMouseButtonUp(args));
        }

        private void TranslateMouseButtonDown(VariantMap args)
        {
            Listener.OnMouseButtonDown(this, KeyEventArgs.FromMouseButtonDown(args));
        }

        private void TranslateKeyUp(VariantMap args)
        {
            Listener.OnKeyboardButtonUp(this, KeyEventArgs.FromKeyUp(args));
        }

        private void TranslateKeyDown(VariantMap args)
        {
            Listener.OnKeyboardButtonDown(this, KeyEventArgs.FromKeyDown(args));
        }
    }
}