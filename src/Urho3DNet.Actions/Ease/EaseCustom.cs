using System;

namespace Urho3DNet.Actions
{
    public class EaseCustom : ActionEase
    {
        #region Constructors

        public EaseCustom(FiniteTimeAction action, Func<float, float> easeFunc) : base(action)
        {
            EaseFunc = easeFunc;
        }

        #endregion Constructors

        public Func<float, float> EaseFunc { get; }

        public override FiniteTimeAction Reverse()
        {
            return new ReverseTime(this);
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new EaseCustomState(this, target);
        }
    }


    #region Action state

    public class EaseCustomState : ActionEaseState
    {
        public EaseCustomState(EaseCustom action, Object target) : base(action, target)
        {
            EaseFunc = action.EaseFunc;
        }

        protected Func<float, float> EaseFunc { get; }

        public override void Update(float time)
        {
            InnerActionState.Update(EaseFunc(time));
        }
    }

    #endregion Action state
}