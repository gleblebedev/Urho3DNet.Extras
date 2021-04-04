using System;
using System.Collections.Generic;

namespace Urho3DNet.Actions
{
    public class ActionBuilder<T> where T : Object
    {
        private List<FiniteTimeAction> _sequence;

        public ActionBuilder()
        {
            _sequence = new List<FiniteTimeAction>();
        }

        public static ActionBuilder<T> Build()
        {
            return new ActionBuilder<T>();
        }

        public ActionBuilder<T> Add(FiniteTimeAction action)
        {
            _sequence.Add(action);
            return this;
        }

        public ActionBuilder<T> RepeatForever()
        {
            return ReplaceAll(_ => new RepeatForever(_));
        }

        public ActionBuilder<T> Then(Action<ActionBuilder<T>> factory)
        {
            var innerBuilder = new ActionBuilder<T>();
            factory(innerBuilder);
            _sequence.Add(innerBuilder.Complete().Action);
            return this;
        }

        /// <summary>
        /// Run all previous actions in parallel.
        /// </summary>
        /// <returns>Action builder.</returns>
        public ActionBuilder<T> InParallel()
        {
            var actions = _sequence.ToArray();
            _sequence.Clear();
            _sequence.Add(new Parallel(actions));
            return this;
        }

        public UrhoAction<T> Complete()
        {
            if (_sequence.Count == 0) return new UrhoAction<T>();
            if (_sequence.Count == 1) return new UrhoAction<T>(_sequence[0]);
            return new UrhoAction<T>(new Sequence(_sequence));
        }

        public ActionBuilder<T> ReplaceLast(Func<FiniteTimeAction, FiniteTimeAction> factory)
        {
            var index = _sequence.Count - 1;
            _sequence[index] = factory(_sequence[index]);
            return this;
        }

        public ActionBuilder<T> ReplaceAll(Func<List<FiniteTimeAction>, FiniteTimeAction> factory)
        {
            _sequence = new List<FiniteTimeAction> {factory(_sequence)};
            return this;
        }

        public ActionBuilder<T> ReverseTime()
        {
            return ReplaceLast(_ => new ReverseTime(_));
        }

        public ActionBuilder<T> Call(Action action)
        {
            return Add(new CallFunc(action));
        }

        public ActionBuilder<T> Call(Action<T> action)
        {
            return Add(new CallFuncN<T>(action));
        }

        public ActionBuilder<T> Call<TData>(Action<T, TData> action, TData data)
        {
            return Add(new CallFuncND<T, TData>(action, data));
        }

        public ActionBuilder<T> Call<TData>(Action<TData> action, TData data)
        {
            return Add(new CallFuncO<TData>(action, data));
        }

        public ActionBuilder<T> ActionTween(float duration, string key, float from, float to,
            Action<float, string> tweenAction)
        {
            return Add(new ActionTween(duration, key, from, to, tweenAction));
        }

        public ActionBuilder<T> DelayTime(float duration)
        {
            return Add(new DelayTime(duration));
        }

        public ActionBuilder<T> EaseBackIn()
        {
            return ReplaceLast(_ => new EaseBackIn(_));
        }

        public ActionBuilder<T> EaseBackInOut()
        {
            return ReplaceLast(_ => new EaseBackInOut(_));
        }

        public ActionBuilder<T> EaseBackOut()
        {
            return ReplaceLast(_ => new EaseBackOut(_));
        }

        public ActionBuilder<T> EaseBounceIn()
        {
            return ReplaceLast(_ => new EaseBounceIn(_));
        }

        public ActionBuilder<T> EaseBounceInOut()
        {
            return ReplaceLast(_ => new EaseBounceInOut(_));
        }

        public ActionBuilder<T> EaseBounceOut()
        {
            return ReplaceLast(_ => new EaseBounceOut(_));
        }

        public ActionBuilder<T> EaseCustom(Func<float, float> easeFunc)
        {
            return ReplaceLast(_ => new EaseCustom(_, easeFunc));
        }

        public ActionBuilder<T> EaseElastic(float period)
        {
            return ReplaceLast(_ => new EaseElastic(_, period));
        }

        public ActionBuilder<T> EaseElastic()
        {
            return ReplaceLast(_ => new EaseElastic(_));
        }

        public ActionBuilder<T> EaseElasticIn()
        {
            return ReplaceLast(_ => new EaseElasticIn(_));
        }

        public ActionBuilder<T> EaseElasticIn(float period)
        {
            return ReplaceLast(_ => new EaseElasticIn(_, period));
        }

        public ActionBuilder<T> EaseElasticInOut()
        {
            return ReplaceLast(_ => new EaseElasticInOut(_));
        }

        public ActionBuilder<T> EaseElasticInOut(float period)
        {
            return ReplaceLast(_ => new EaseElasticInOut(_, period));
        }

        public ActionBuilder<T> EaseElasticOut()
        {
            return ReplaceLast(_ => new EaseElasticOut(_));
        }

        public ActionBuilder<T> EaseElasticOut(float period)
        {
            return ReplaceLast(_ => new EaseElasticOut(_, period));
        }

        public ActionBuilder<T> EaseExponentialIn()
        {
            return ReplaceLast(_ => new EaseExponentialIn(_));
        }

        public ActionBuilder<T> EaseExponentialInOut()
        {
            return ReplaceLast(_ => new EaseExponentialInOut(_));
        }

        public ActionBuilder<T> EaseExponentialOut()
        {
            return ReplaceLast(_ => new EaseExponentialOut(_));
        }

        public ActionBuilder<T> EaseIn(float rate)
        {
            return ReplaceLast(_ => new EaseIn(_, rate));
        }

        public ActionBuilder<T> EaseInOut(float rate)
        {
            return ReplaceLast(_ => new EaseInOut(_, rate));
        }

        public ActionBuilder<T> EaseOut(float rate)
        {
            return ReplaceLast(_ => new EaseOut(_, rate));
        }

        public ActionBuilder<T> EaseRateAction(float rate)
        {
            return ReplaceLast(_ => new EaseRateAction(_, rate));
        }

        public ActionBuilder<T> EaseSineIn()
        {
            return ReplaceLast(_ => new EaseSineIn(_));
        }

        public ActionBuilder<T> EaseSineInOut()
        {
            return ReplaceLast(_ => new EaseSineInOut(_));
        }

        public ActionBuilder<T> EaseSineOut()
        {
            return ReplaceLast(_ => new EaseSineOut(_));
        }
    }
}