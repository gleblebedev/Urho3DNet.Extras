using System.Threading.Tasks;

namespace Urho3DNet.Actions
{
    public struct UrhoAction<T> where T : Object
    {
        public UrhoAction(FiniteTimeAction action)
        {
            Action = action;
        }

        public FiniteTimeAction Action { get; }

        public void Run(ActionManager actionManager, T target)
        {
            if (target != null && Action != null)
            {
                target.RunActions(actionManager, Action);
            }
        }
        public Task<ActionState> RunAsync(ActionManager actionManager, T target)
        {
            if (target != null && Action != null)
            {
                return target.RunActionsAsync(actionManager, Action);
            }

            return Task.FromResult<ActionState>(null);
        }
    }
}