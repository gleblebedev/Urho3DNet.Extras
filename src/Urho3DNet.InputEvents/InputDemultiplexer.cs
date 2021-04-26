namespace Urho3DNet.InputEvents
{
    public partial class InputDemultiplexer : IInputListener
    {
        private readonly IInputListener[] _listeners;

        public InputDemultiplexer(params IInputListener[] listeners)
        {
            _listeners = listeners;
        }

        void IInputListener.ListenerSubscribed(IInputSource container)
        {
            foreach (var inputListener in _listeners) inputListener.ListenerSubscribed(container);
        }

        void IInputListener.ListenerUnsubscribed(IInputSource container)
        {
            foreach (var inputListener in _listeners) inputListener.ListenerUnsubscribed(container);
        }
    }
}