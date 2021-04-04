using System;

namespace Urho3DNet.InputEvents
{
    public class DeviceEventArgs : EventArgs
    {
        public DeviceEventArgs(int deviceId)
        {
            DeviceId = deviceId;
        }

        public int DeviceId { get; protected set; }

        public static DeviceEventArgs FromJoystickDisconnected(VariantMap args)
        {
            return new DeviceEventArgs(args[E.JoystickDisconnected.JoystickID].Int);
        }

        public static DeviceEventArgs FromJoystickConnected(VariantMap args)
        {
            return new DeviceEventArgs(args[E.JoystickConnected.JoystickID].Int);
        }
    }
}