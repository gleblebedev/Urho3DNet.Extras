namespace Urho3DNet.Actions
{
    public class EaseSineInOut : ActionEase
    {
        #region Constructors

        public EaseSineInOut(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseSineInOut(InnerAction.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseSineInOutState(this, target);
        }
    }


    #region Action state

    public class EaseSineInOutState : ActionEaseState
    {
        public EaseSineInOutState(EaseSineInOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.SineInOut(time));
        }
    }

    #endregion Action state
}