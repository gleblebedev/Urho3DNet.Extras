namespace Urho3DNet.Actions
{
    // Extra action for making a Sequence or Spawn when only adding one action to it.
    internal class ExtraAction : FiniteTimeAction
    {
        public override FiniteTimeAction Reverse()
        {
            return new ExtraAction();
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new ExtraActionState(this, target);
        }

        #region Action State

        public class ExtraActionState : FiniteTimeActionState
        {
            public ExtraActionState(ExtraAction action, Object target)
                : base(action, target)
            {
            }

            public override void Update(float time)
            {
            }

            protected internal override void Step(float dt)
            {
            }
        }

        #endregion Action State
    }
}