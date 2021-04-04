using System;

namespace Urho3DNet.InputEvents
{
    public class AxisEventArgs : DeviceEventArgs
    {
        public AxisEventArgs(UniAxis axis, int deviceId, float value) : base(deviceId)
        {
            Axis = axis;
            Value = value;
        }

        public UniAxis Axis { get; }

        public float Value { get; }

        public static AxisEventArgs FromJoystickAxisMove(VariantMap args, Input input)
        {
            return new AxisEventArgs(
                AxisFromJoystickAxis(args[E.JoystickAxisMove.Button].Int,
                    input.GetJoystick(args[E.JoystickAxisMove.JoystickID].Int)),
                args[E.JoystickAxisMove.JoystickID].Int,
                args[E.JoystickAxisMove.Position].Float);
        }

        private static UniAxis AxisFromJoystickAxis(int axis, JoystickState getJoystick)
        {
            if (axis < 0) return UniAxis.Invalid;

            if (getJoystick.Controller != IntPtr.Zero) return UniAxis.Invalid + 1 + axis;

            return UniAxis.Axis0 + axis;
        }
    }
}