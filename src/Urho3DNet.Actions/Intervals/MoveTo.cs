namespace Urho3DNet.Actions
{
    public class MoveTo : MoveBy
    {
        protected Vector3 EndPosition;

        #region Constructors

        public MoveTo(float duration, Vector3 position) : base(duration, position)
        {
            EndPosition = position;
        }

        #endregion Constructors

        public Vector3 PositionEnd => EndPosition;

        protected internal override ActionState StartAction(Object target)
        {
            return new MoveToState(this, target);
        }
    }

    public class MoveToState : MoveByState
    {
        public MoveToState(MoveTo action, Object target)
            : base(action, target)
        {
            if (Target is Node node)
            {
                StartPosition = node.Position;
                PositionDelta = action.PositionEnd - node.Position;
            }
        }

        public override void Update(float time)
        {
            if (Target is Node node)
            {
                var newPos = StartPosition + PositionDelta * time;
                node.Position = newPos;
                PreviousPosition = newPos;
            }
        }
    }
}