namespace Urho3DNet.Actions
{
    public class JumpBy : FiniteTimeAction
    {
        #region Constructors

        public JumpBy(float duration, Vector3 position, float height, uint jumps) : base(duration)
        {
            Position = position;
            Height = height;
            Jumps = jumps;
        }

        #endregion Constructors

        public override FiniteTimeAction Reverse()
        {
            return new JumpBy(Duration, new Vector3(-Position.X, -Position.Y, -Position.Z), Height, Jumps);
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new JumpByState(this, target);
        }

        #region Properties

        public uint Jumps { get; protected set; }
        public float Height { get; protected set; }
        public Vector3 Position { get; protected set; }

        #endregion Properties
    }

    public class JumpByState : FiniteTimeActionState
    {
        protected Vector3 Delta;
        protected float Height;
        protected uint Jumps;
        protected Vector3 StartPosition;
        protected Vector3 P;

        public JumpByState(JumpBy action, Object target)
            : base(action, target)
        {
            Delta = action.Position;
            Height = action.Height;
            Jumps = action.Jumps;
            if (Target is Node node)
                P = StartPosition = node.Position;
        }

        public override void Update(float time)
        {
            if (Target is Node node)
            {
                // Is % equal to fmodf()???
                var frac = time * Jumps % 1f;
                var y = Height * 4f * frac * (1f - frac);
                y += Delta.Y * time;
                var x = Delta.X * time;
                var z = Delta.Z * time;

                var currentPos = node.Position;

                var diff = currentPos - P;
                StartPosition = diff + StartPosition;

                var newPos = StartPosition + new Vector3(x, y, z);
                node.Position = newPos;

                P = newPos;
            }
        }
    }
}