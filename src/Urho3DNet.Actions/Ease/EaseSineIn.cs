namespace Urho3DNet.Actions
{
    public class EaseSineIn : ActionEase
    {
        #region Constructors

        public EaseSineIn(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseSineOut(InnerAction.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseSineInState(this, target);
        }
    }


    #region Action state

    public class EaseSineInState : ActionEaseState
    {
        public EaseSineInState(EaseSineIn action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.SineIn(time));
        }
    }

    #endregion Action state
}