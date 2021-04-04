namespace Urho3DNet.Actions
{
    public class EaseSineOut : ActionEase
    {
        #region Constructors

        public EaseSineOut(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseSineIn(InnerAction.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseSineOutState(this, target);
        }
    }


    #region Action state

    public class EaseSineOutState : ActionEaseState
    {
        public EaseSineOutState(EaseSineOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.SineOut(time));
        }
    }

    #endregion Action state
}