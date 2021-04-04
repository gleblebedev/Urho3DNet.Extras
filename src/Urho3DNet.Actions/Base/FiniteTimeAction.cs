using System;

namespace Urho3DNet.Actions
{
    public abstract class FiniteTimeAction : BaseAction
    {
        private float duration;

        #region Properties

        public virtual float Duration
        {
            get => duration;
            set
            {
                var newDuration = value;

                // Prevent division by 0
                if (newDuration == 0) newDuration = float.Epsilon;

                duration = newDuration;
            }
        }

        #endregion Properties

        public abstract FiniteTimeAction Reverse();

        protected internal override ActionState StartAction(Object target)
        {
            return new FiniteTimeActionState(this, target);
        }


        #region Constructors

        protected FiniteTimeAction()
            : this(0)
        {
        }

        protected FiniteTimeAction(float duration)
        {
            Duration = duration;
        }

        #endregion Constructors
    }

    public class FiniteTimeActionState : ActionState
    {
        private bool firstTick;

        public FiniteTimeActionState(FiniteTimeAction action, Object target)
            : base(action, target)
        {
            Duration = action.Duration;
            Elapsed = 0.0f;
            firstTick = true;
        }

        protected internal override void Step(float dt)
        {
            if (firstTick)
            {
                firstTick = false;
                Elapsed = 0f;
            }
            else
            {
                Elapsed += dt;
            }

            Update(Math.Max(0f, Math.Min(1, Elapsed / Math.Max(Duration, float.Epsilon))));
        }

        #region Properties

        public virtual float Duration { get; set; }
        public float Elapsed { get; private set; }

        public override bool IsDone => Elapsed >= Duration;

        #endregion Properties
    }
}