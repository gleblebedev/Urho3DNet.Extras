namespace Urho3DNet.Actions
{
    public class ScaleBy : ScaleTo
    {
        public override FiniteTimeAction Reverse()
        {
            return new ScaleBy(Duration, 1 / EndScaleX, 1 / EndScaleY, 1 / EndScaleZ);
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new ScaleByState(this, target);
        }

        #region Constructors

        public ScaleBy(float duration, float scale) : base(duration, scale)
        {
        }

        public ScaleBy(float duration, float scaleX, float scaleY, float scaleZ) : base(duration, scaleX, scaleY,
            scaleZ)
        {
        }

        #endregion Constructors
    }

    public class ScaleByState : ScaleToState
    {
        public ScaleByState(ScaleTo action, Object target)
            : base(action, target)
        {
            DeltaX = StartScaleX * EndScaleX - StartScaleX;
            DeltaY = StartScaleY * EndScaleY - StartScaleY;
            DeltaZ = StartScaleZ * EndScaleZ - StartScaleZ;
        }
    }
}