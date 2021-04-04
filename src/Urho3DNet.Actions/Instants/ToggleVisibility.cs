namespace Urho3DNet.Actions
{
    public class ToggleVisibility : ActionInstant
    {
        protected internal override ActionState StartAction(Object target)
        {
            return new ToggleVisibilityState(this, target);
        }

        #region Constructors

        #endregion Constructors
    }

    public class ToggleVisibilityState : ActionInstantState
    {
        public ToggleVisibilityState(ToggleVisibility action, Object target)
            : base(action, target)
        {
            if (target is Node node) node.IsEnabled = !node.IsEnabled;
        }
    }
}