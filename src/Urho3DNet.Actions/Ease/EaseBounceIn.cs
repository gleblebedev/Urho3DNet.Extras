namespace Urho3DNet.Actions
{
    public class EaseBounceIn : ActionEase
    {
        #region Constructors

        public EaseBounceIn(FiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseBounceOut(InnerAction.Reverse());
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseBounceInState(this, target);
        }
    }


    #region Action state

    public class EaseBounceInState : ActionEaseState
    {
        public EaseBounceInState(EaseBounceIn action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseMath.BounceIn(time));
        }
    }

    #endregion Action state
}