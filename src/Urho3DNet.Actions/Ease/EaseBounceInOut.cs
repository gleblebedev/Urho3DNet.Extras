namespace Urho3DNet.Actions
{
    public class EaseBounceInOut : ActionEase
    {
        #region Constructors

        public EaseBounceInOut(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseBounceInOut(InnerAction.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseBounceInOutState(this, target);
        }
    }


    #region Action state

    public class EaseBounceInOutState : ActionEaseState
    {
        public EaseBounceInOutState(EaseBounceInOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.BounceInOut(time));
        }
    }

    #endregion Action state
}