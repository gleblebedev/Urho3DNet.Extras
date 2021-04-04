namespace Urho3DNet.InputEvents
{
    public interface IInputSource
    {
        IInputListener Listener { get; set; }
    }
}