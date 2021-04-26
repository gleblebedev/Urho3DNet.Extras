namespace Urho3DNet.InputEvents
{
    public class KeyAction : IKeyAction
    {
        public bool Value { get; private set; }

        public static implicit operator bool(KeyAction action)
        {
            if (action == null)
                return false;
            return action.Value;
        }

        void IKeyAction.Start(int deviceId)
        {
            Value = true;
        }

        void IKeyAction.Stop(int deviceId)
        {
            Value = false;
        }

        void IKeyAction.Cancel(int deviceId)
        {
            Value = false;
        }
    }
}