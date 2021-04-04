using System;

namespace Urho3DNet.Actions
{
    public class CallFuncN<T> : CallFunc where T : Object
    {
        public Action<T> CallFunctionN { get; }

        protected internal override ActionState StartAction(Object target)
        {
            return new CallFuncNState<T>(this, target);
        }

        #region Constructors

        public CallFuncN()
        {
        }

        public CallFuncN(Action<T> selector)
        {
            CallFunctionN = selector;
        }

        #endregion Constructors
    }

    public class CallFuncNState<T> : CallFuncState where T : Object
    {
        public CallFuncNState(CallFuncN<T> action, Object target)
            : base(action, target)
        {
            CallFunctionN = action.CallFunctionN;
        }

        protected Action<T> CallFunctionN { get; set; }

        public override void Execute()
        {
            CallFunctionN?.Invoke(Target as T);
        }
    }
}