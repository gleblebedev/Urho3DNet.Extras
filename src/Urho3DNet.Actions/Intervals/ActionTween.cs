using System;

namespace Urho3DNet.Actions
{
    public class ActionTween : FiniteTimeAction
    {
        #region Constructors

        public ActionTween(float duration, string key, float from, float to, Action<float, string> tweenAction) :
            base(duration)
        {
            Key = key;
            To = to;
            From = from;
            TweenAction = tweenAction;
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new ActionTween(Duration, Key, To, From, TweenAction);
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new ActionTweenState(this, target);
        }

        #region Properties

        public float From { get; }
        public float To { get; }
        public string Key { get; }
        public Action<float, string> TweenAction { get; }

        #endregion Properties
    }

    public class ActionTweenState : FiniteTimeActionState
    {
        protected float Delta;

        public ActionTweenState(ActionTween action, Object target)
            : base(action, target)
        {
            TweenAction = action.TweenAction;
            From = action.From;
            To = action.To;
            Key = action.Key;
            Delta = To - From;
        }

        protected float From { get; }

        protected float To { get; }

        protected string Key { get; }

        protected Action<float, string> TweenAction { get; }

        public override void Update(float time)
        {
            var amt = To - Delta * (1 - time);
            TweenAction?.Invoke(amt, Key);
        }
    }
}