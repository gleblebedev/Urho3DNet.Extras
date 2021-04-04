namespace Urho3DNet.Actions
{
    public class Blink : FiniteTimeAction
    {
        #region Constructors

        public Blink(float duration, uint numOfBlinks) : base(duration)
        {
            Times = numOfBlinks;
        }

        #endregion Constructors

        public uint Times { get; }

        public override FiniteTimeAction Reverse()
        {
            return new Blink(Duration, Times);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new BlinkState(this, target);
        }
    }

    public class BlinkState : FiniteTimeActionState
    {
        public BlinkState(Blink action, Object target)
            : base(action, target)
        {
            if (target is Node node)
            {
                Times = action.Times;
                OriginalState = node.IsEnabled;
            }
        }

        protected uint Times { get; set; }

        protected bool OriginalState { get; set; }

        public override void Update(float time)
        {
            if (Target is Node node && !IsDone)
            {
                var slice = 1.0f / Times;
                var m = time % slice;
                node.IsEnabled = m > slice / 2;
            }
        }

        protected internal override void Stop()
        {
            if (Target is Node node)
            {
                node.IsEnabled = OriginalState;
                base.Stop();
            }
        }
    }
}