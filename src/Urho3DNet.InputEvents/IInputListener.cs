namespace Urho3DNet.InputEvents
{
    public partial interface IInputListener
    {
        void ListenerSubscribed(IInputSource container);

        void ListenerUnsubscribed(IInputSource container);
    }
}