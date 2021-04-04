using System;

namespace Urho3DNet.Actions
{
    public class UpdateSource
    {
        private readonly Object _updateSource;
        private readonly UpdateArgs _updateArgs = new UpdateArgs();
        private EventHandler<UpdateArgs> _update;

        public UpdateSource(Object updateSource)
        {
            _updateSource = updateSource;
        }

        public event EventHandler<UpdateArgs> Update
        {
            add
            {
                if (_update == null) _updateSource.SubscribeToEvent(E.Update, DispatchUpdate);
                _update += value;
            }
            remove
            {
                _update -= value;
                if (_update == null) _updateSource.UnsubscribeFromEvent(E.Update);
            }
        }

        private void DispatchUpdate(VariantMap args)
        {
            _updateArgs.Update(args);
            _update?.Invoke(this, _updateArgs);
        }
    }
}