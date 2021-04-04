namespace Urho3DNet.Actions
{
    public class Show : ActionInstant
    {
        public override FiniteTimeAction Reverse()
        {
            return new Hide();
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new ShowState(this, target);
        }
    }

    public class ShowState : ActionInstantState
    {
        public ShowState(Show action, Object target)
            : base(action, target)
        {
            if (target is Node node) node.IsEnabled = true;
        }
    }
}