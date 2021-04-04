namespace Urho3DNet.Actions
{
    public class EaseElasticInOut : EaseElastic
    {
        public override FiniteTimeAction Reverse()
        {
            return new EaseElasticInOut(InnerAction.Reverse(), Period);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseElasticInOutState(this, target);
        }

        #region Constructors

        public EaseElasticInOut(FiniteTimeAction action) : this(action, 0.3f)
        {
        }

        public EaseElasticInOut(FiniteTimeAction action, float period) : base(action, period)
        {
        }

        #endregion Constructors
    }


    #region Action state

    public class EaseElasticInOutState : EaseElasticState
    {
        public EaseElasticInOutState(EaseElasticInOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.ElasticInOut(time, Period));
        }
    }

    #endregion Action state
}