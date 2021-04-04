using System;

namespace Urho3DNet.Actions
{
    public class RemoveSelf : ActionInstant
    {
        public override FiniteTimeAction Reverse()
        {
            throw new NotSupportedException();
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new RemoveSelfState(this, target);
        }
    }

    public class RemoveSelfState : ActionInstantState
    {
        public RemoveSelfState(RemoveSelf action, Object target)
            : base(action, target)
        {
        }

        public override void Update(float time)
        {
            if (Target is Node node) node.Parent.RemoveChild(node);
        }
    }
}