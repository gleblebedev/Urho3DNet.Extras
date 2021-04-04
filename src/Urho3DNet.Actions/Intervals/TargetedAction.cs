namespace Urho3DNet.Actions
{
    public class TargetedAction : FiniteTimeAction
    {
        #region Constructors

        public TargetedAction(Object target, FiniteTimeAction action) : base(action.Duration)
        {
            ForcedTarget = target;
            Action = action;
        }

        #endregion Constructors

        public FiniteTimeAction Action { get; }
        public Object ForcedTarget { get; }

        public override FiniteTimeAction Reverse()
        {
            return new TargetedAction(ForcedTarget, Action.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new TargetedActionState(this, target);
        }
    }

    public class TargetedActionState : FiniteTimeActionState
    {
        public TargetedActionState(TargetedAction action, Object target)
            : base(action, target)
        {
            ForcedTarget = action.ForcedTarget;
            TargetedAction = action.Action;

            ActionState = (FiniteTimeActionState) TargetedAction.StartAction(ForcedTarget);
        }

        protected FiniteTimeAction TargetedAction { get; set; }

        protected FiniteTimeActionState ActionState { get; set; }

        protected Object ForcedTarget { get; set; }

        public override void Update(float time)
        {
            ActionState.Update(time);
        }

        protected internal override void Stop()
        {
            ActionState.Stop();
        }
    }
}