using System;

namespace Urho3DNet.Actions
{
    public class ScaleTo : FiniteTimeAction
    {
        public float EndScaleX { get; }
        public float EndScaleY { get; }
        public float EndScaleZ { get; }

        public override FiniteTimeAction Reverse()
        {
            throw new NotImplementedException();
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new ScaleToState(this, target);
        }


        #region Constructors

        public ScaleTo(float duration, float scale) : this(duration, scale, scale, scale)
        {
        }

        public ScaleTo(float duration, float scaleX, float scaleY, float scaleZ) : base(duration)
        {
            EndScaleX = scaleX;
            EndScaleY = scaleY;
            EndScaleZ = scaleZ;
        }

        #endregion Constructors
    }

    public class ScaleToState : FiniteTimeActionState
    {
        protected float DeltaX;
        protected float DeltaY;
        protected float DeltaZ;
        protected float EndScaleX;
        protected float EndScaleY;
        protected float EndScaleZ;
        protected float StartScaleX;
        protected float StartScaleY;
        protected float StartScaleZ;

        public ScaleToState(ScaleTo action, Object target)
            : base(action, target)
        {
            if (Target is Node node)
            {
                var scale = node.GetScale();
                StartScaleX = scale.X;
                StartScaleY = scale.Y;
                StartScaleZ = scale.Z;
            }

            EndScaleX = action.EndScaleX;
            EndScaleY = action.EndScaleY;
            EndScaleZ = action.EndScaleZ;
            DeltaX = EndScaleX - StartScaleX;
            DeltaY = EndScaleY - StartScaleY;
            DeltaZ = EndScaleZ - StartScaleZ;
        }

        public override void Update(float time)
        {
            if (Target is Node node)
                node.SetScale(new Vector3(StartScaleX + DeltaX * time, StartScaleY + DeltaY * time,
                    StartScaleZ + DeltaZ * time));
        }
    }
}