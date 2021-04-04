namespace Urho3DNet.Actions
{
    public class RotateBy : FiniteTimeAction
    {
        public float AngleX { get; }
        public float AngleY { get; }
        public float AngleZ { get; }

        public override FiniteTimeAction Reverse()
        {
            return new RotateBy(Duration, -AngleX, -AngleY, -AngleZ);
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new RotateByState(this, target);
        }

        #region Constructors

        public RotateBy(float duration, float deltaAngleX, float deltaAngleY, float deltaAngleZ) : base(duration)
        {
            AngleX = deltaAngleX;
            AngleY = deltaAngleY;
            AngleZ = deltaAngleZ;
        }

        public RotateBy(float duration, float deltaAngle) : this(duration, deltaAngle, deltaAngle, deltaAngle)
        {
        }

        #endregion Constructors
    }

    public class RotateByState : FiniteTimeActionState
    {
        public RotateByState(RotateBy action, Object target)
            : base(action, target)
        {
            AngleX = action.AngleX;
            AngleY = action.AngleY;
            AngleZ = action.AngleZ;
            if (Target is Node node)
                StartAngles = node.Rotation;
        }

        protected Quaternion StartAngles { get; set; }

        protected float AngleX { get; set; }

        protected float AngleY { get; set; }

        protected float AngleZ { get; set; }

        public override void Update(float time)
        {
            if (Target is Node node)
            {
                var newRot = StartAngles * new Quaternion(AngleX * time, AngleY * time, AngleZ * time);
                newRot.Normalize();
                node.Rotation = newRot;
            }
        }
    }
}