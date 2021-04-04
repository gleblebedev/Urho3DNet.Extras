using System;

namespace Urho3DNet.InputEvents
{
    public class KeyEventArgs : DeviceEventArgs
    {
        public KeyEventArgs():base(0)
        {
        }
        
        public KeyEventArgs(UniKey key, int deviceId, int scancode, int buttons, Qualifier qualifiers, bool repeat) :
            base(deviceId)
        {
            Init(key, deviceId, scancode, buttons, qualifiers, repeat);
        }
        
        public void Init(UniKey key, int deviceId = 0, int scancode = 0, int buttons = 0, Qualifier qualifiers = Qualifier.QualNone, bool repeat = false)
        {
            DeviceId = deviceId;
            Key = key;
            Scancode = scancode;
            Buttons = buttons;
            Qualifiers = qualifiers;
            Repeat = repeat;
        }
        
        public UniKey Key { get; private set; }
        public int Scancode { get; private set; }
        public int Buttons { get; private set; }
        public Qualifier Qualifiers { get; private set; }
        public bool Repeat { get; private set; }

        public static KeyEventArgs FromKeyDown(VariantMap args)
        {
            return new KeyEventArgs(
                (UniKey) args[E.KeyDown.Key].Int,
                0,
                args[E.KeyDown.Scancode].Int,
                args[E.KeyDown.Buttons].Int,
                (Qualifier)args[E.KeyDown.Qualifiers].Int,
                args[E.KeyDown.Repeat].Bool);
        }

        public static KeyEventArgs FromKeyUp(VariantMap args)
        {
            return new KeyEventArgs(
                (UniKey) args[E.KeyUp.Key].Int,
                0,
                args[E.KeyUp.Scancode].Int,
                args[E.KeyUp.Buttons].Int,
                (Qualifier)args[E.KeyUp.Qualifiers].Int,
                false);
        }

        public static KeyEventArgs FromMouseButtonDown(VariantMap args)
        {
            return new KeyEventArgs(
                KeyFromMouseButton(args[E.MouseButtonDown.Button].Int),
                0,
                0,
                args[E.MouseButtonDown.Buttons].Int,
                (Qualifier)args[E.MouseButtonDown.Qualifiers].Int,
                false);
        }

        public static KeyEventArgs FromMouseButtonUp(VariantMap args)
        {
            return new KeyEventArgs(
                KeyFromMouseButton(args[E.MouseButtonUp.Button].Int),
                0,
                0,
                args[E.MouseButtonUp.Buttons].Int,
                (Qualifier)args[E.MouseButtonUp.Qualifiers].Int,
                false);
        }

        public static KeyEventArgs FromJoystickButtonDownEvent(VariantMap args, Input input)
        {
            var deviceId = args[E.JoystickButtonDown.JoystickID].Int;
            return new KeyEventArgs(
                KeyFromJoystickButton(args[E.JoystickButtonDown.Button].Int, input.GetJoystick(deviceId)),
                deviceId,
                0,
                0,
                0,
                false);
        }

        public static KeyEventArgs FromJoystickButtonUpEvent(VariantMap args, Input input)
        {
            var deviceId = args[E.JoystickButtonUp.JoystickID].Int;
            return new KeyEventArgs(
                KeyFromJoystickButton(args[E.JoystickButtonUp.Button].Int,
                    input.GetJoystick(deviceId)),
                deviceId,
                0,
                0,
                0,
                false);
        }

        private static UniKey KeyFromJoystickButton(int button, JoystickState joystickState)
        {
            if (button < 0) return UniKey.KeyUnknown;

            if (joystickState.Controller != IntPtr.Zero) return UniKey.ButtonA + button;
            return UniKey.Button0 + button;
        }

        private static UniKey KeyFromMouseButton(int button)
        {
            switch (button)
            {
                case 1: return UniKey.MouseButtonLeft;
                case 2: return UniKey.MouseButtonRight;
                case 4: return UniKey.MouseButtonMiddle;
                case 8: return UniKey.MouseButton1;
                case 16: return UniKey.MouseButton2;
            }

            return UniKey.KeyUnknown;
        }
    }
}