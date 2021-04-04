using System;

namespace Urho3DNet.Actions
{
    public class CallFuncO<TData> : CallFunc
    {
        public Action<TData> CallFunctionO { get; }
        public TData Object { get; }

        protected internal override ActionState StartAction(Object target)
        {
            return new CallFuncOState<TData>(this, target);
        }

        #region Constructors

        public CallFuncO()
        {
            Object = default;
            CallFunctionO = null;
        }

        public CallFuncO(Action<TData> selector, TData pObject) : this()
        {
            Object = pObject;
            CallFunctionO = selector;
        }

        #endregion Constructors
    }

    public class CallFuncOState<TData> : CallFuncState
    {
        public CallFuncOState(CallFuncO<TData> action, Object target)
            : base(action, target)
        {
            CallFunctionO = action.CallFunctionO;
            Object = action.Object;
        }

        protected Action<TData> CallFunctionO { get; set; }
        protected TData Object { get; set; }

        public override void Execute()
        {
            CallFunctionO?.Invoke(Object);
        }
    }
}