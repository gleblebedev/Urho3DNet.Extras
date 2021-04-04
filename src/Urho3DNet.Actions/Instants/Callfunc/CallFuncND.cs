using System;

namespace Urho3DNet.Actions
{
    public class CallFuncND<T, TData> : CallFuncN<T> where T : Object
    {
        #region Constructors

        public CallFuncND(Action<T, TData> selector, TData d)
        {
            Data = d;
            CallFunctionND = selector;
        }

        #endregion Constructors

        public Action<T, TData> CallFunctionND { get; }
        public TData Data { get; }

        protected internal override ActionState StartAction(Object target)
        {
            return new CallFuncNDState<T, TData>(this, target);
        }
    }

    public class CallFuncNDState<T, TData> : CallFuncState where T : Object
    {
        public CallFuncNDState(CallFuncND<T, TData> action, Object target)
            : base(action, target)
        {
            CallFunctionND = action.CallFunctionND;
            Data = action.Data;
        }

        protected Action<T, TData> CallFunctionND { get; set; }
        protected TData Data { get; set; }

        public override void Execute()
        {
            if (null != CallFunctionND) CallFunctionND(Target as T, Data);
        }
    }
}