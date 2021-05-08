namespace Urho3DNet.InputEvents
{
    [ObjectFactory]
    public class ManagedEventDispatcher : Object
    {
        public ManagedEventDispatcher(Context context): base(context)
        {
            
        }

        public override string GetTypeName()
        {
            return nameof(ManagedEventDispatcher);
        }

        public override StringHash GetTypeHash()
        {
            return new StringHash(nameof(ManagedEventDispatcher));
        }
    }
}