namespace Urho3DNet.Actions
{
    public class MoveBy : FiniteTimeAction
    {
        #region Constructors

        public MoveBy(float duration, Vector3 position) : base(duration)
        {
            PositionDelta = position;
        }

        #endregion Constructors

        public Vector3 PositionDelta { get; }

        public override FiniteTimeAction Reverse()
        {
            return new MoveBy(Duration, new Vector3(-PositionDelta.X, -PositionDelta.Y, -PositionDelta.Z));
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new MoveByState(this, target);
        }
    }

    public class MoveByState : FiniteTimeActionState
    {
        protected Vector3 PositionDelta;
        protected Vector3 EndPosition;
        protected Vector3 StartPosition;
        protected Vector3 PreviousPosition;

        public MoveByState(MoveBy action, Object target)
            : base(action, target)
        {
            PositionDelta = action.PositionDelta;
            if (Target is Node node)
                PreviousPosition = StartPosition = node.Position;
        }

        public override void Update(float time)
        {
            if (Target is Node node)
            {
                var currentPos = node.Position;
                var diff = currentPos - PreviousPosition;
                StartPosition = StartPosition + diff;
                var newPos = StartPosition + PositionDelta * time;
                node.Position = newPos;
                PreviousPosition = newPos;
            }
        }
    }
}