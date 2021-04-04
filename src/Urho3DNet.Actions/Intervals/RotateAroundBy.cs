namespace Urho3DNet.Actions
{
    public class RotateAroundBy : FiniteTimeAction
    {
        #region Constructors

        public RotateAroundBy(float duration, Vector3 point, float deltaX, float deltaY, float deltaZ,
            TransformSpace ts = TransformSpace.TsWorld) : base(duration)
        {
            Point = point;
            DeltaX = deltaX;
            DeltaY = deltaY;
            DeltaZ = deltaZ;
            TransformSpace = ts;
        }

        #endregion Constructors

        public Vector3 Point { get; set; }
        public float DeltaX { get; set; }
        public float DeltaY { get; set; }
        public float DeltaZ { get; set; }
        public TransformSpace TransformSpace { get; set; }

        public override FiniteTimeAction Reverse()
        {
            return new RotateAroundBy(Duration, Point, -DeltaX, -DeltaY, -DeltaZ);
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new RotateAroundByState(this, target);
        }
    }

    public class RotateAroundByState : FiniteTimeActionState
    {
        protected Vector3 Point;
        protected float DeltaX;
        protected float DeltaY;
        protected float DeltaZ;
        protected TransformSpace TransformSpace;

        private float prevTime;

        public RotateAroundByState(RotateAroundBy action, Object target)
            : base(action, target)
        {
            Point = action.Point;
            DeltaX = action.DeltaX;
            DeltaY = action.DeltaY;
            DeltaZ = action.DeltaZ;
            TransformSpace = action.TransformSpace;
        }


        public override void Update(float time)
        {
            if (Target is Node node)
            {
                var timeDelta = time - prevTime;
                node.RotateAround(Point, new Quaternion(timeDelta * DeltaX, timeDelta * DeltaY, timeDelta * DeltaZ),
                    TransformSpace);
                prevTime = time;
            }
        }
    }
}