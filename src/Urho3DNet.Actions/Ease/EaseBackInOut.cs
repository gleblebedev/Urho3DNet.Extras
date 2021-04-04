namespace Urho3DNet.Actions
{
    public class EaseBackInOut : ActionEase
    {
        #region Constructors

        public EaseBackInOut(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseBackInOut(InnerAction.Reverse());
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new EaseBackInOutState(this, target);
        }
    }


    #region Action state

    public class EaseBackInOutState : ActionEaseState
    {
        public EaseBackInOutState(EaseBackInOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.BackInOut(time));
        }
    }

    #endregion Action state
}