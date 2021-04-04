namespace Urho3DNet.Actions
{
    public class Hide : ActionInstant
    {
        public override FiniteTimeAction Reverse()
        {
            return new Show();
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new HideState(this, target);
        }

        #region Constructors

        #endregion Constructors
    }

    public class HideState : ActionInstantState
    {
        public HideState(Hide action, Object target)
            : base(action, target)
        {
            if (target is Node node) node.IsEnabled = false;
        }
    }
}