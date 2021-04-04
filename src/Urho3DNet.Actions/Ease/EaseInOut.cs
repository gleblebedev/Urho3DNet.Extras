using System;

namespace Urho3DNet.Actions
{
    public class EaseInOut : EaseRateAction
    {
        #region Constructors

        public EaseInOut(FiniteTimeAction action, float rate) : base(action, rate)
        {
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new EaseInOut(InnerAction.Reverse(), Rate);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseInOutState(this, target);
        }
    }


    #region Action state

    public class EaseInOutState : EaseRateActionState
    {
        public EaseInOutState(EaseInOut action, Object target) : base(action, target)
        {
        }

        public override void Update(float time)
        {
            var actionRate = Rate;
            time *= 2;

            if (time < 1)
                InnerActionState.Update(0.5f * (float) Math.Pow(time, actionRate));
            else
                InnerActionState.Update(1.0f - 0.5f * (float) Math.Pow(2 - time, actionRate));
        }
    }

    #endregion Action state
}