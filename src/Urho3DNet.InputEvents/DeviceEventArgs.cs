using System;

namespace Urho3DNet.InputEvents
{
    public class DeviceEventArgs : EventArgs
    {
        public DeviceEventArgs() : this(-1)
        {
        }

        public DeviceEventArgs(int deviceId)
        {
            DeviceId = deviceId;
        }

        public int DeviceId { get; protected set; }

        public static void FromJoystickDisconnected(DeviceEventArgs eventArgs,
            InputEventsAdapter.JoystickDisconnectedEventArgs args)
        {
            eventArgs.Set(args.JoystickID);
        }

        public static void FromJoystickConnected(DeviceEventArgs eventArgs,
            InputEventsAdapter.JoystickConnectedEventArgs args)
        {
            eventArgs.Set(args.JoystickID);
        }

        protected void Set(int deviceId)
        {
            DeviceId = deviceId;
        }
    }
}