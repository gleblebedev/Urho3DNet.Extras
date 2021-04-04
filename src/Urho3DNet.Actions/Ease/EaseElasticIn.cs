namespace Urho3DNet.Actions
{
    public class EaseElasticIn : EaseElastic
    {
        public override FiniteTimeAction Reverse()
        {
            return new EaseElasticOut(InnerAction.Reverse(), Period);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseElasticInState(this, target);
        }

        #region Constructors

        public EaseElasticIn(FiniteTimeAction action) : this(action, 0.3f)
        {
        }

        public EaseElasticIn(FiniteTimeAction action, float period) : base(action, period)
        {
        }

        #endregion Constructors
    }


    #region Action state

    public class EaseElasticInState : EaseElasticState
    {
        public EaseElasticInState(EaseElasticIn action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.ElasticIn(time, Period));
        }
    }

    #endregion Action state
}