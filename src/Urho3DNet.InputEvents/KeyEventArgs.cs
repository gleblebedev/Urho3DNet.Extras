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
            Set(key, deviceId, scancode, buttons, qualifiers, repeat);
        }
        
        public void Set(UniKey key, int deviceId = 0, int scancode = 0, int buttons = 0, Qualifier qualifiers = Qualifier.QualNone, bool repeat = false)
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

        public static void FromKeyDown(KeyEventArgs eventArgs, InputEventsAdapter.KeyDownEventArgs args)
        {
            eventArgs.Set(
                (UniKey) args.Key,
                0,
                args.Scancode,
                args.Buttons,
                (Qualifier)args.Qualifiers,
                args.Repeat);
        }

        public static void FromKeyUp(KeyEventArgs eventArgs, InputEventsAdapter.KeyUpEventArgs args)
        {
            eventArgs.Set(
                (UniKey) args.Key,
                0,
                args.Scancode,
                args.Buttons,
                (Qualifier)args.Qualifiers,
                false);
        }

        public static void FromMouseButtonDown(KeyEventArgs eventArgs, InputEventsAdapter.MouseButtonDownEventArgs args)
        {
            eventArgs.Set(
                KeyFromMouseButton(args.Button),
                0,
                0,
                args.Buttons,
                (Qualifier)args.Qualifiers,
                false);
        }

        public static void FromMouseButtonUp(KeyEventArgs eventArgs, InputEventsAdapter.MouseButtonUpEventArgs args)
        {
            eventArgs.Set(
                KeyFromMouseButton(args.Button),
                0,
                0,
                args.Buttons,
                (Qualifier)args.Qualifiers,
                false);
        }

        public static void FromJoystickButtonDownEvent(KeyEventArgs eventArgs, InputEventsAdapter.JoystickButtonDownEventArgs args, Input input)
        {
            var deviceId = args.JoystickID;
            eventArgs.Set(
                KeyFromJoystickButton(args.Button, input.GetJoystick(deviceId)),
                deviceId,
                0,
                0,
                0,
                false);
        }

        public static void FromJoystickButtonUpEvent(KeyEventArgs eventArgs, InputEventsAdapter.JoystickButtonUpEventArgs args, Input input)
        {
            var deviceId = args.JoystickID;
            eventArgs.Set(
                KeyFromJoystickButton(args.Button,
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