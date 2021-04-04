using System.Threading.Tasks;

namespace Urho3DNet.Actions
{
    internal class TaskSource : ActionInstant
    {
        public TaskCompletionSource<ActionState> TaskCompletionSource { get; }

        protected internal override ActionState StartAction(Object target)
        {
            return new TaskSourceState(this, target);
        }

        #region Constructors

        public TaskSource()
        {
            TaskCompletionSource = null;
        }

        public TaskSource(TaskCompletionSource<ActionState> taskSource)
        {
            TaskCompletionSource = taskSource;
        }

        #endregion Constructors
    }

    internal class TaskSourceState : ActionInstantState
    {
        public TaskSourceState(TaskSource action, Object target)
            : base(action, target)
        {
            TaskCompletionSource = action.TaskCompletionSource;
        }

        private TaskCompletionSource<ActionState> TaskCompletionSource { get; }

        public override void Update(float time)
        {
            SetResult();
        }

        public void SetResult()
        {
            TaskCompletionSource.TrySetResult(this);
        }

        public void Cancel()
        {
            TaskCompletionSource.TrySetCanceled();
        }
    }
}