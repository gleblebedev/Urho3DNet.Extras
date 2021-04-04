using System;

namespace Urho3DNet.Actions
{
    public class RotateTo : FiniteTimeAction
    {
        public float DistanceAngleX { get; }
        public float DistanceAngleY { get; }
        public float DistanceAngleZ { get; }

        public override FiniteTimeAction Reverse()
        {
            throw new NotImplementedException();
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new RotateToState(this, target);
        }

        #region Constructors

        public RotateTo(float duration, float deltaAngleX, float deltaAngleY, float deltaAngleZ) : base(duration)
        {
            DistanceAngleX = deltaAngleX;
            DistanceAngleY = deltaAngleY;
            DistanceAngleZ = deltaAngleZ;
        }

        public RotateTo(float duration, float deltaAngle) : this(duration, deltaAngle, deltaAngle, deltaAngle)
        {
        }

        #endregion Constructors
    }

    public class RotateToState : FiniteTimeActionState
    {
        protected float DiffAngleY;
        protected float DiffAngleX;
        protected float DiffAngleZ;

        protected float StartAngleX;
        protected float StartAngleY;
        protected float StartAngleZ;

        public RotateToState(RotateTo action, Object target)
            : base(action, target)
        {
            DistanceAngleX = action.DistanceAngleX;
            DistanceAngleY = action.DistanceAngleY;
            DistanceAngleZ = action.DistanceAngleZ;

            if (Target is Node node)
            {
                var sourceRotation = node.Rotation.EulerAngles;

                // Calculate X
                StartAngleX = sourceRotation.X;
                StartAngleX = StartAngleX > 0 ? StartAngleX % 360.0f : StartAngleX % -360.0f;
                DiffAngleX = DistanceAngleX - StartAngleX;
                if (DiffAngleX > 180)
                    DiffAngleX -= 360;
                if (DiffAngleX < -180)
                    DiffAngleX += 360;

                //Calculate Y
                StartAngleY = sourceRotation.Y;
                StartAngleY = StartAngleY > 0 ? StartAngleY % 360.0f : StartAngleY % -360.0f;
                DiffAngleY = DistanceAngleY - StartAngleY;
                if (DiffAngleY > 180)
                    DiffAngleY -= 360;
                if (DiffAngleY < -180)
                    DiffAngleY += 360;

                //Calculate Z
                StartAngleZ = sourceRotation.Z;
                StartAngleZ = StartAngleZ > 0 ? StartAngleZ % 360.0f : StartAngleZ % -360.0f;
                DiffAngleZ = DistanceAngleZ - StartAngleZ;
                if (DiffAngleZ > 180)
                    DiffAngleZ -= 360;
                if (DiffAngleZ < -180)
                    DiffAngleZ += 360;
            }
        }

        protected float DistanceAngleX { get; set; }
        protected float DistanceAngleY { get; set; }
        protected float DistanceAngleZ { get; set; }

        public override void Update(float time)
        {
            if (Target is Node node)
                node.Rotation = new Quaternion(StartAngleX + DiffAngleX * time, StartAngleY + DiffAngleY * time,
                    StartAngleZ + DiffAngleZ * time);
        }
    }
}