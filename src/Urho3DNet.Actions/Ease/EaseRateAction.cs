namespace Urho3DNet.Actions
{
    public class EaseRateAction : ActionEase
    {
        #region Constructors

        public EaseRateAction(FiniteTimeAction action, float rate) : base(action)
        {
            Rate = rate;
        }

        #endregion Constructors

        public float Rate { get; }

        public override FiniteTimeAction Reverse()
        {
            return new EaseRateAction(InnerAction.Reverse(), 1 / Rate);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseRateActionState(this, target);
        }
    }


    #region Action state

    public class EaseRateActionState : ActionEaseState
    {
        public EaseRateActionState(EaseRateAction action, Object target) : base(action, target)
        {
            Rate = action.Rate;
        }

        protected float Rate { get; }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.ExponentialOut(time));
        }
    }

    #endregion Action state
}