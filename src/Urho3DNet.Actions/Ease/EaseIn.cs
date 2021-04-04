using System;

namespace Urho3DNet.Actions
{
    public class EaseIn : EaseRateAction
    {
        #region Constructors

        public EaseIn(FiniteTimeAction action, float rate) : base(action, rate)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseIn(InnerAction.Reverse(), 1 / Rate);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseInState(this, target);
        }
    }


    #region Action state

    public class EaseInState : EaseRateActionState
    {
        public EaseInState(EaseIn action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update((float) Math.Pow(time, Rate));
        }
    }

    #endregion Action state
}