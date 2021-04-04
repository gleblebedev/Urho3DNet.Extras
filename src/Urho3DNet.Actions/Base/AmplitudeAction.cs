namespace Urho3DNet.Actions
{
    public abstract class AmplitudeAction : FiniteTimeAction
    {
        #region Constructors

        protected AmplitudeAction(float duration, float amplitude = 0) : base(duration)
        {
            Amplitude = amplitude;
        }

        #endregion Constructors

        public float Amplitude { get; }
    }


    #region Action state

    public abstract class AmplitudeActionState : FiniteTimeActionState
    {
        protected AmplitudeActionState(AmplitudeAction action, Object target) : base(action, target)
        {
            Amplitude = action.Amplitude;
            AmplitudeRate = 1.0f;
        }

        protected internal float AmplitudeRate { get; set; }
        protected float Amplitude { get; }
    }

    #endregion Action state
}