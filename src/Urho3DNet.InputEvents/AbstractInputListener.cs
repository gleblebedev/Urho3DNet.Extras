using System;

namespace Urho3DNet.InputEvents
{
    public abstract partial class AbstractInputListener : IInputListener
    {
        public IInputSource InputSource { get; private set; }

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