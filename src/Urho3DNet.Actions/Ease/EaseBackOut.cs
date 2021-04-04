namespace Urho3DNet.Actions
{
    public class EaseBackOut : ActionEase
    {
        #region Constructors

        public EaseBackOut(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseBackIn(InnerAction.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseBackOutState(this, target);
        }
    }


    #region Action state

    public class EaseBackOutState : ActionEaseState
    {
        public EaseBackOutState(EaseBackOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.BackOut(time));
        }
    }

    #endregion Action state
}