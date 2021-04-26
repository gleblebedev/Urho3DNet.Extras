using System;

namespace Urho3DNet.InputEvents
{
    public class RelayAxisAction : IAxisAction
    {
        private readonly Action<int, float> _update;

        public RelayAxisAction(Action<float> update)
        {
            _update = (i, v) => update(v);
        }

        public RelayAxisAction(Action<int, float> update)
        {
            _update = update;
        }

        public void Update(int deviceId, float value)
        {
            _update?.Invoke(deviceId, value);
        }
    }
}