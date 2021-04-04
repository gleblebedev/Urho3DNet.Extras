namespace Urho3DNet.Actions
{
    public class EaseExponentialInOut : ActionEase
    {
        #region Constructors

        public EaseExponentialInOut(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseExponentialInOut(InnerAction.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseExponentialInOutState(this, target);
        }
    }


    #region Action state

    public class EaseExponentialInOutState : ActionEaseState
    {
        public EaseExponentialInOutState(EaseExponentialInOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.ExponentialInOut(time));
        }
    }

    #endregion Action state
}