using System;

namespace Urho3DNet.Actions
{
    public class EaseOut : EaseRateAction
    {
        #region Constructors

        public EaseOut(FiniteTimeAction action, float rate) : base(action, rate)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseOut(InnerAction.Reverse(), 1 / Rate);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseOutState(this, target);
        }
    }


    #region Action state

    public class EaseOutState : EaseRateActionState
    {
        public EaseOutState(EaseOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            InnerActionState.Update((float) Math.Pow(time, 1 / Rate));
        }
    }

    #endregion Action state
}