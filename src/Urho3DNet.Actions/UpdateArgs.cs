using System;

namespace Urho3DNet.Actions
{
    public class UpdateArgs : EventArgs
    {
        public float TimeStep { get; private set; }

        internal void Update(VariantMap args)
        {
            TimeStep = args[E.Update.TimeStep].Float;
        }
    }
}