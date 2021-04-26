using System;

namespace Urho3DNet.InputEvents
{
    public class AxisEventArgs : DeviceEventArgs
    {
        public AxisEventArgs()
        {
        }

        public AxisEventArgs(UniAxis axis, int deviceId, float value) : base(deviceId)
        {
            Axis = axis;
            Value = value;
        }

        public UniAxis Axis { get; private set; }

        public float Value { get; private set; }

        public static void FromJoystickAxisMove(AxisEventArgs eventArgs,
            InputEventsAdapter.JoystickAxisMoveEventArgs args, Input input, float deadZone = 0.0f)
        {
            eventArgs.Set(
                AxisFromJoystickAxis(args.Button,
                    input.GetJoystick(args.JoystickID)),
                args.JoystickID,
                ApplyDeadZone(args.Position, deadZone));
        }

        private static float ApplyDeadZone(float position, float deadZone)
        {
            if (deadZone <= 0.0f) return position;
            if (position > deadZone) return (position - deadZone) / (1.0f - deadZone);
            if (-position > deadZone) return (position + deadZone) / (1.0f - deadZone);

            return 0;
        }

        private static UniAxis AxisFromJoystickAxis(int axis, JoystickState getJoystick)
        {
            if (axis < 0) return UniAxis.Invalid;

            if (getJoystick.Controller != IntPtr.Zero) return UniAxis.Invalid + 1 + axis;

            return UniAxis.Axis0 + axis;
        }

        public void Set(UniAxis axis, int deviceId, float value)
        {
            base.Set(deviceId);
            Axis = axis;
            Value = value;
        }
    }
}