using System.Collections.Generic;
using System.Diagnostics;

namespace Urho3DNet.Actions
{
    public class RepeatForever : FiniteTimeAction
    {
        public FiniteTimeAction InnerAction { get; }

        public override FiniteTimeAction Reverse()
        {
            return new RepeatForever(InnerAction.Reverse());
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new RepeatForeverState(this, target);
        }


        #region Constructors

        public RepeatForever(params FiniteTimeAction[] actions)
        {
            Debug.Assert(actions != null);
            InnerAction = new Sequence(actions);
        }

        public RepeatForever(IReadOnlyList<FiniteTimeAction> actions)
        {
            Debug.Assert(actions != null);
            InnerAction = new Sequence(actions);
        }

        public RepeatForever(FiniteTimeAction action)
        {
            Debug.Assert(action != null);
            InnerAction = action;
        }

        #endregion Constructors
    }

    public class RepeatForeverState : FiniteTimeActionState
    {
        public RepeatForeverState(RepeatForever action, Object target)
            : base(action, target)
        {
            InnerAction = action.InnerAction;
            InnerActionState = (FiniteTimeActionState) InnerAction.StartAction(target);
        }

        public override bool IsDone => false;

        private FiniteTimeAction InnerAction { get; }

        private FiniteTimeActionState InnerActionState { get; set; }

        protected internal override void Step(float dt)
        {
            InnerActionState.Step(dt);

            if (InnerActionState.IsDone)
            {
                var diff = InnerActionState.Elapsed - InnerActionState.Duration;
                InnerActionState = (FiniteTimeActionState) InnerAction.StartAction(Target);
                InnerActionState.Step(0f);
                InnerActionState.Step(diff);
            }
        }
    }
}