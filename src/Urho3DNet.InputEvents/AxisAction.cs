using System;

namespace Urho3DNet.InputEvents
{
    public class AxisAction : IAxisAction
    {
        public float Value { get; private set; }

        public static implicit operator float(AxisAction action)
        {
            if (action == null)
                return 0.0f;
            return action.Value;
        }

        public float MergeWith(float value)
        {
            if (Math.Abs(Value) > Math.Abs(value))
                return Value;
            return value;
        }

        public void Update(int deviceId, float value)
        {
            Value = value;
        }
    }
}