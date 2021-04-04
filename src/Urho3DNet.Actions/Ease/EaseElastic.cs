namespace Urho3DNet.Actions
{
    public class EaseElastic : ActionEase
    {
        public float Period { get; }

        public override FiniteTimeAction Reverse()
        {
            return null;
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseElasticState(this, target);
        }


        #region Constructors

        public EaseElastic(FiniteTimeAction action, float period) : base(action)
        {
            Period = period;
        }

        public EaseElastic(FiniteTimeAction action) : this(action, 0.3f)
        {
        }

        #endregion Constructors
    }


    #region Action state

    public class EaseElasticState : ActionEaseState
    {
        public EaseElasticState(EaseElastic action, Object target) : base(action, target)
        {
            Period = action.Period;
        }

        protected float Period { get; }
    }

    #endregion Action state
}