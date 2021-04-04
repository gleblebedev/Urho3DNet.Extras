namespace Urho3DNet.Actions
{
    public class ActionEase : FiniteTimeAction
    {
        #region Constructors

        public ActionEase(FiniteTimeAction action) : base(action.Duration)
        {
            InnerAction = action;
        }

        #endregion Constructors

        protected internal FiniteTimeAction InnerAction { get; }

        public override FiniteTimeAction Reverse()
        {
            return new ActionEase(InnerAction.Reverse());
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new ActionEaseState(this, target);
        }
    }


    #region Action state

    public class ActionEaseState : FiniteTimeActionState
    {
        public ActionEaseState(ActionEase action, Object target) : base(action, target)
        {
            InnerActionState = (FiniteTimeActionState) action.InnerAction.StartAction(target);
        }

        protected FiniteTimeActionState InnerActionState { get; }

        public override void Update(float time)
        {
            InnerActionState.Update(time);
        }

        protected internal override void Stop()
        {
            InnerActionState.Stop();
            base.Stop();
        }
    }

    #endregion Action state
}