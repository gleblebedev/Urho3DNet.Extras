using System;

namespace Urho3DNet.InputEvents
{
    public abstract partial class AbstractInputListener : IInputListener
    {
        private StatefulInputSource _inputProxy;

        public IInputSource InputSource { get; private set; }

        protected IInputListener FallbackInputListener
        {
            get => _inputProxy?.Listener;
            set
            {
                if (FallbackInputListener != value)
                    (_inputProxy ?? (_inputProxy = new StatefulInputSource())).Listener = value;
            }
        }

        protected virtual void OnListenerSubscribed()
        {
        }

        protected virtual void OnListenerUnsubscribed()
        {
        }


        void IInputListener.ListenerSubscribed(IInputSource source)
        {
            if (InputSource != null)
                throw new InvalidOperationException("Listener already subscribed to a source");
            InputSource = source;
            OnListenerSubscribed();
        }

        void IInputListener.ListenerUnsubscribed(IInputSource source)
        {
            if (InputSource != source)
            {
                if (InputSource == null)
                    throw new InvalidOperationException("Listener source already unsubscribed");
                throw new InvalidOperationException("Listener source subscribed to a different source");
            }

            OnListenerUnsubscribed();
            InputSource = null;
        }
    }
}