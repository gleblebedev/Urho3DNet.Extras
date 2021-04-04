namespace Urho3DNet.Actions
{
    public class Speed : BaseAction
    {
        #region Constructors

        public Speed(FiniteTimeAction action, float speedValue)
        {
            InnerAction = action;
            SpeedValue = speedValue;
        }

        #endregion Constructors

        public float SpeedValue { get; }

        protected internal FiniteTimeAction InnerAction { get; }

        public virtual FiniteTimeAction Reverse()
        {
            return (FiniteTimeAction) (BaseAction) new Speed(InnerAction.Reverse(), SpeedValue);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new SpeedState(this, target);
        }
    }


    #region Action state

    internal class SpeedState : ActionState
    {
        public SpeedState(Speed action, Object target) : base(action, target)
        {
            InnerActionState = (FiniteTimeActionState) action.InnerAction.StartAction(target);
            Speed = action.SpeedValue;
        }

        protected internal override void Stop()
        {
            InnerActionState.Stop();
            base.Stop();
        }

        protected internal override void Step(float dt)
        {
            InnerActionState.Step(dt * Speed);
        }

        #region Properties

        public float Speed { get; }

        protected FiniteTimeActionState InnerActionState { get; }

        public override bool IsDone => InnerActionState.IsDone;

        #endregion Properties
    }

    #endregion Action state
}