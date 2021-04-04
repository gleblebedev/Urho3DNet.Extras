namespace Urho3DNet.Actions
{
    public class ActionInstant : FiniteTimeAction
    {
        #region Constructors

        protected ActionInstant()
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new ActionInstant();
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new ActionInstantState(this, target);
        }
    }

    public class ActionInstantState : FiniteTimeActionState
    {
        public ActionInstantState(ActionInstant action, Object target)
            : base(action, target)
        {
        }

        public override bool IsDone => true;

        public override void Update(float time)
        {
            // ignore
        }

        protected internal override void Step(float dt)
        {
            Update(1);
        }
    }
}