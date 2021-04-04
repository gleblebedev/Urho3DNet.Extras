namespace Urho3DNet.Actions
{
    public class DelayTime : FiniteTimeAction
    {
        #region Constructors

        public DelayTime(float duration) : base(duration)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new DelayTime(Duration);
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new DelayTimeState(this, target);
        }
    }

    public class DelayTimeState : FiniteTimeActionState
    {
        public DelayTimeState(DelayTime action, Object target)
            : base(action, target)
        {
        }

        public override void Update(float time)
        {
        }
    }
}