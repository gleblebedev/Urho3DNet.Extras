namespace Urho3DNet.InputEvents
{
    public abstract class AbstractInputSource : IInputSource
    {
        private IInputListener _listener;

        public IInputListener Listener
        {
            get => _listener;
            set
            {
                if (_listener != value)
                {
                    if (_listener != null)
                    {
                        OnListenerRemoved(_listener);
                        _listener.ListenerUnsubscribed(this);
                    }

                    _listener = value;
                    if (_listener != null)
                    {
                        _listener.ListenerSubscribed(this);
                        OnListenerSet(_listener);
                    }
                }
            }
        }

        protected virtual void OnListenerSet(IInputListener listener)
        {
        }

        protected virtual void OnListenerRemoved(IInputListener listener)
        {
        }
    }
}