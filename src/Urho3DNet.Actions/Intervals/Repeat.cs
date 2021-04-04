namespace Urho3DNet.Actions
{
    public class Repeat : FiniteTimeAction
    {
        #region Constructors

        public Repeat(FiniteTimeAction action, uint times) : base(action.Duration * times)
        {
            Times = times;
            InnerAction = action;

            ActionInstant = action is ActionInstant;
            //an instant action needs to be executed one time less in the update method since it uses startWithTarget to execute the action
            if (ActionInstant) Times -= 1;
            Total = 0;
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new Repeat(InnerAction.Reverse(), Times);
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new RepeatState(this, target);
        }

        #region Properties

        public bool ActionInstant { get; }
        public uint Times { get; }
        public uint Total { get; }
        public FiniteTimeAction InnerAction { get; }

        #endregion Properties
    }

    public class RepeatState : FiniteTimeActionState
    {
        public RepeatState(Repeat action, Object target)
            : base(action, target)
        {
            InnerAction = action.InnerAction;
            Times = action.Times;
            Total = action.Total;
            ActionInstant = action.ActionInstant;

            NextDt = InnerAction.Duration / Duration;

            InnerActionState = (FiniteTimeActionState) InnerAction.StartAction(target);
        }

        public override bool IsDone => Total == Times;

        protected bool ActionInstant { get; set; }

        protected float NextDt { get; set; }

        protected FiniteTimeAction InnerAction { get; set; }

        protected FiniteTimeActionState InnerActionState { get; set; }

        protected uint Times { get; set; }

        protected uint Total { get; set; }

        // issue #80. Instead of hooking step:, hook update: since it can be called by any
        // container action like Repeat, Sequence, AelDeel, etc..
        public override void Update(float time)
        {
            if (time >= NextDt)
            {
                while (time > NextDt && Total < Times)
                {
                    InnerActionState.Update(1.0f);
                    Total++;

                    InnerActionState.Stop();
                    InnerActionState = (FiniteTimeActionState) InnerAction.StartAction(Target);
                    NextDt = InnerAction.Duration / Duration * (Total + 1f);
                }

                // fix for issue #1288, incorrect end value of repeat
                if (time >= 1.0f && Total < Times) Total++;

                // don't set an instant action back or update it, it has no use because it has no duration
                if (!ActionInstant)
                {
                    if (Total == Times)
                    {
                        InnerActionState.Update(1f);
                        InnerActionState.Stop();
                    }
                    else
                    {
                        // issue #390 prevent jerk, use right update
                        InnerActionState.Update(time - (NextDt - InnerAction.Duration / Duration));
                    }
                }
            }
            else
            {
                InnerActionState.Update(time * Times % 1.0f);
            }
        }

        protected internal override void Stop()
        {
            InnerActionState.Stop();
            base.Stop();
        }
    }
}