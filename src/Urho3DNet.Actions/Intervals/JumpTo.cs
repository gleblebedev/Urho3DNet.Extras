namespace Urho3DNet.Actions
{
    public class JumpTo : JumpBy
    {
        #region Constructors

        public JumpTo(float duration, Vector3 position, float height, uint jumps)
            : base(duration, position, height, jumps)
        {
        }

        #endregion Constructors

        protected internal override ActionState StartAction(Object target)
        {
            return new JumpToState(this, target);
        }
    }

    public class JumpToState : JumpByState
    {
        public JumpToState(JumpBy action, Object target)
            : base(action, target)
        {
            Delta = new Vector3(Delta.X - StartPosition.X, Delta.Y - StartPosition.Y, Delta.Z - StartPosition.Z);
        }
    }
}