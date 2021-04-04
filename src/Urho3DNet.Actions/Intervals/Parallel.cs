namespace Urho3DNet.Actions
{
    public class Parallel : FiniteTimeAction
    {
        #region Constructors

        public Parallel(params FiniteTimeAction[] actions)
        {
            // Can't call base(duration) because max action duration needs to be determined here
            var maxDuration = 0.0f;
            foreach (var action in actions)
                if (action.Duration > maxDuration)
                    maxDuration = action.Duration;
            Duration = maxDuration;

            Actions = actions;

            for (var i = 0; i < Actions.Length; i++)
            {
                var actionDuration = Actions[i].Duration;
                if (actionDuration < Duration)
                    Actions[i] = new Sequence(Actions[i], new DelayTime(Duration - actionDuration));
            }
        }

        #endregion Constructors

        public FiniteTimeAction[] Actions { get; }

        public override FiniteTimeAction Reverse()
        {
            var rev = new FiniteTimeAction[Actions.Length];
            for (var i = 0; i < Actions.Length; i++) rev[i] = Actions[i].Reverse();

            return new Parallel(rev);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new ParallelState(this, target);
        }
    }

    public class ParallelState : FiniteTimeActionState
    {
        public ParallelState(Parallel action, Object target)
            : base(action, target)
        {
            Actions = action.Actions;
            ActionStates = new FiniteTimeActionState[Actions.Length];

            for (var i = 0; i < Actions.Length; i++)
                ActionStates[i] = (FiniteTimeActionState) Actions[i].StartAction(target);
        }

        protected FiniteTimeAction[] Actions { get; set; }

        protected FiniteTimeActionState[] ActionStates { get; set; }

        public override void Update(float time)
        {
            for (var i = 0; i < Actions.Length; i++) ActionStates[i].Update(time);
        }

        protected internal override void Stop()
        {
            for (var i = 0; i < Actions.Length; i++) ActionStates[i].Stop();
            base.Stop();
        }
    }
}