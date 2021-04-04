namespace Urho3DNet.Actions
{
    public class EaseExponentialOut : ActionEase
    {
        #region Constructors

        public EaseExponentialOut(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseExponentialIn(InnerAction.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseExponentialOutState(this, target);
        }
    }


    #region Action state

    public class EaseExponentialOutState : ActionEaseState
    {
        public EaseExponentialOutState(EaseExponentialOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.ExponentialOut(time));
        }
    }

    #endregion Action state
}