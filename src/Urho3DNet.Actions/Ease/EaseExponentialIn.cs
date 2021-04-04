namespace Urho3DNet.Actions
{
    public class EaseExponentialIn : ActionEase
    {
        #region Constructors

        public EaseExponentialIn(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseExponentialOut(InnerAction.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseExponentialInState(this, target);
        }
    }


    #region Action state

    public class EaseExponentialInState : ActionEaseState
    {
        public EaseExponentialInState(EaseExponentialIn action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.ExponentialIn(time));
        }
    }

    #endregion Action state
}