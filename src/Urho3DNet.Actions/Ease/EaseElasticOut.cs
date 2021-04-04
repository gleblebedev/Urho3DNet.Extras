namespace Urho3DNet.Actions
{
    public class EaseElasticOut : EaseElastic
    {
        public override FiniteTimeAction Reverse()
        {
            return new EaseElasticIn(InnerAction.Reverse(), Period);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseElasticOutState(this, target);
        }

        #region Constructors

        public EaseElasticOut(FiniteTimeAction action) : base(action, 0.3f)
        {
        }

        public EaseElasticOut(FiniteTimeAction action, float period) : base(action, period)
        {
        }

        #endregion Constructors
    }


    #region Action state

    public class EaseElasticOutState : EaseElasticState
    {
        public EaseElasticOutState(EaseElasticOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.ElasticOut(time, Period));
        }
    }

    #endregion Action state
}