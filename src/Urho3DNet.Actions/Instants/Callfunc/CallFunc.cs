using System;

namespace Urho3DNet.Actions
{
    public class CallFunc : ActionInstant
    {
        public Action CallFunction { get; }
        public string ScriptFuncName { get; }

        protected internal override ActionState StartAction(Object target)
        {
            return new CallFuncState(this, target);
        }

        #region Constructors

        public CallFunc()
        {
            ScriptFuncName = "";
            CallFunction = null;
        }

        public CallFunc(Action selector)
        {
            CallFunction = selector;
        }

        #endregion Constructors
    }

    public class CallFuncState : ActionInstantState
    {
        public CallFuncState(CallFunc action, Object target)
            : base(action, target)
        {
            CallFunction = action.CallFunction;
            ScriptFuncName = action.ScriptFuncName;
        }

        protected Action CallFunction { get; set; }
        protected string ScriptFuncName { get; set; }

        public virtual void Execute()
        {
            CallFunction?.Invoke();
        }

        public override void Update(float time)
        {
            Execute();
        }
    }
}