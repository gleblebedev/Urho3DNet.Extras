using System;

namespace Urho3DNet.InputEvents
{
    public class RelayKeyAction : IKeyAction
    {
        private readonly Action<int> _start;
        private readonly Action<int> _stop;
        private readonly Action<int> _chancel;

        public RelayKeyAction(Action start = null, Action stop = null, Action chancel = null)
        {
            if (start != null) _start = _ => start();
            if (stop != null) _stop = _ => stop();
            if (chancel != null) _chancel = _ => chancel();
        }

        public RelayKeyAction(Action<int> start = null, Action<int> stop = null, Action<int> chancel = null)
        {
            _start = start;
            _stop = stop;
            _chancel = chancel;
        }

        void IKeyAction.Start(int deviceId)
        {
            _start?.Invoke(deviceId);
        }

        void IKeyAction.Stop(int deviceId)
        {
            _stop?.Invoke(deviceId);
        }

        void IKeyAction.Cancel(int deviceId)
        {
            _chancel?.Invoke(deviceId);
        }
    }
}