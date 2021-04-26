namespace Urho3DNet.InputEvents
{
    public interface IKeyAction
    {
        void Start(int deviceId);
        void Stop(int deviceId);
        void Cancel(int deviceId);
    }
}