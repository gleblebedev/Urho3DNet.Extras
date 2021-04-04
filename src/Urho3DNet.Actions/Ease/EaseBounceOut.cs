namespace Urho3DNet.Actions
{
    public class EaseBounceOut : ActionEase
    {
        #region Constructors

        public EaseBounceOut(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseBounceIn(InnerAction.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseBounceOutState(this, target);
        }
    }


    #region Action state

    public class EaseBounceOutState : ActionEaseState
    {
        public EaseBounceOutState(EaseBounceOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.BounceOut(time));
        }
    }

    #endregion Action state
}