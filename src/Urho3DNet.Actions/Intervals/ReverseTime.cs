namespace Urho3DNet.Actions
{
    public class ReverseTime : FiniteTimeAction
    {
        #region Constructors

        public ReverseTime(FiniteTimeAction action) : base(action.Duration)
        {
            Other = action;
        }

        #endregion Constructors

        public FiniteTimeAction Other { get; }

        public override FiniteTimeAction Reverse()
        {
            return Other;
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new ReverseTimeState(this, target);
        }
    }

    public class ReverseTimeState : FiniteTimeActionState
    {
        public ReverseTimeState(ReverseTime action, Object target)
            : base(action, target)
        {
            Other = action.Other;
            OtherState = (FiniteTimeActionState) Other.StartAction(target);
        }

        protected FiniteTimeAction Other { get; set; }

        protected FiniteTimeActionState OtherState { get; set; }

        public override void Update(float time)
        {
            if (Other != null) OtherState.Update(1 - time);
        }

        protected internal override void Stop()
        {
            OtherState.Stop();
        }
    }
}