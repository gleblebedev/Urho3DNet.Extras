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

        public void Init(UniAxis axis, int deviceId, float value)
        {
            Axis = axis;
            Value = value;
            base.Set(deviceId);
        }

        public UniAxis Axis { get; private set; }

        public float Value { get; private set; }

        public static void FromJoystickAxisMove(AxisEventArgs eventArgs, InputEventsAdapter.JoystickAxisMoveEventArgs args, Input input)
        {
            eventArgs.Init(
                AxisFromJoystickAxis(args.Button,
                    input.GetJoystick(args.JoystickID)),
                args.JoystickID,
                args.Position);
        }

        private static UniAxis AxisFromJoystickAxis(int axis, JoystickState getJoystick)
        {
            if (axis < 0) return UniAxis.Invalid;

            if (getJoystick.Controller != IntPtr.Zero) return UniAxis.Invalid + 1 + axis;

            return UniAxis.Axis0 + axis;
        }
    }
}