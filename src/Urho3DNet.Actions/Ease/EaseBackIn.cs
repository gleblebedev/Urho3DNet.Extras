namespace Urho3DNet.Actions
{
    public class EaseBackIn : ActionEase
    {
        #region Constructors

        public EaseBackIn(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseBackOut(InnerAction.Reverse());
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new EaseBackInState(this, target);
        }
    }


    #region Action state

    public class EaseBackInState : ActionEaseState
    {
        public EaseBackInState(EaseBackIn action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.BackIn(time));
        }
    }

    #endregion Action state
}