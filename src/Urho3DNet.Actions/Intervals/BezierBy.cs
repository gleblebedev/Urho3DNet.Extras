namespace Urho3DNet.Actions
{
    public class BezierBy : FiniteTimeAction
    {
        #region Constructors

        public BezierBy(float t, BezierConfig config) : base(t)
        {
            BezierConfig = config;
        }

        #endregion Constructors

        public BezierConfig BezierConfig { get; }

        public override FiniteTimeAction Reverse()
        {
            BezierConfig r;

            r.EndPosition = -BezierConfig.EndPosition;
            r.ControlPoint1 = BezierConfig.ControlPoint2 + -BezierConfig.EndPosition;
            r.ControlPoint2 = BezierConfig.ControlPoint1 + -BezierConfig.EndPosition;

            var action = new BezierBy(Duration, r);
            return action;
        }


        protected internal override ActionState StartAction(Object target)
        {
            return new BezierByState(this, target);
        }
    }

    public class BezierByState : FiniteTimeActionState
    {
        public BezierByState(BezierBy action, Object target)
            : base(action, target)
        {
            BezierConfig = action.BezierConfig;
            if (Target is Node node) PreviousPosition = StartPosition = node.Position;
        }

        protected BezierConfig BezierConfig { get; set; }

        protected Vector3 StartPosition { get; set; }

        protected Vector3 PreviousPosition { get; set; }

        public override void Update(float time)
        {
            if (Target is Node node)
            {
                float xa = 0;
                var xb = BezierConfig.ControlPoint1.X;
                var xc = BezierConfig.ControlPoint2.X;
                var xd = BezierConfig.EndPosition.X;

                float ya = 0;
                var yb = BezierConfig.ControlPoint1.Y;
                var yc = BezierConfig.ControlPoint2.Y;
                var yd = BezierConfig.EndPosition.Y;

                float za = 0;
                var zb = BezierConfig.ControlPoint1.Z;
                var zc = BezierConfig.ControlPoint2.Z;
                var zd = BezierConfig.EndPosition.Z;

                var x = SplineMath.CubicBezier(xa, xb, xc, xd, time);
                var y = SplineMath.CubicBezier(ya, yb, yc, yd, time);
                var z = SplineMath.CubicBezier(za, zb, zc, zd, time);

                var currentPos = node.Position;
                var diff = currentPos - PreviousPosition;
                StartPosition = StartPosition + diff;

                var newPos = StartPosition + new Vector3(x, y, z);
                node.Position = newPos;

                PreviousPosition = newPos;
            }
        }
    }

    public struct BezierConfig
    {
        public Vector3 ControlPoint1;
        public Vector3 ControlPoint2;
        public Vector3 EndPosition;
    }

    internal static class SplineMath
    {
        // CatmullRom Spline formula:
        /// <summary>
        ///     See http://en.wikipedia.org/wiki/Cubic_Hermite_spline#Cardinal_spline
        /// </summary>
        /// <param name="p0">Control point 1</param>
        /// <param name="p1">Control point 2</param>
        /// <param name="p2">Control point 3</param>
        /// <param name="p3">Control point 4</param>
        /// <param name="tension">
        ///     The parameter c is a tension parameter that must be in the interval (0,1). In some sense, this
        ///     can be interpreted as the "length" of the tangent. c=1 will yield all zero tangents, and c=0 yields a Catmull–Rom
        ///     spline.
        /// </param>
        /// <param name="t">Time along the spline</param>
        /// <returns>The point along the spline for the given time (t)</returns>
        internal static Vector2 CardinalSplineAt(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float tension, float t)
        {
            if (tension < 0f) tension = 0f;
            if (tension > 1f) tension = 1f;
            var t2 = t * t;
            var t3 = t2 * t;

            /*
             * Formula: s(-ttt + 2tt - t)P1 + s(-ttt + tt)P2 + (2ttt - 3tt + 1)P2 + s(ttt - 2tt + t)P3 + (-2ttt + 3tt)P3 + s(ttt - tt)P4
             */
            var s = (1 - tension) / 2;

            var b1 = s * (-t3 + 2 * t2 - t); // s(-t3 + 2 t2 - t)P1
            var b2 = s * (-t3 + t2) + (2 * t3 - 3 * t2 + 1); // s(-t3 + t2)P2 + (2 t3 - 3 t2 + 1)P2
            var b3 = s * (t3 - 2 * t2 + t) + (-2 * t3 + 3 * t2); // s(t3 - 2 t2 + t)P3 + (-2 t3 + 3 t2)P3
            var b4 = s * (t3 - t2); // s(t3 - t2)P4

            var x = p0.X * b1 + p1.X * b2 + p2.X * b3 + p3.X * b4;
            var y = p0.Y * b1 + p1.Y * b2 + p2.Y * b3 + p3.Y * b4;

            return new Vector2(x, y);
        }

        // Bezier cubic formula:
        //	((1 - t) + t)3 = 1 
        // Expands to 
        //   (1 - t)3 + 3t(1-t)2 + 3t2(1 - t) + t3 = 1 
        internal static float CubicBezier(float a, float b, float c, float d, float t)
        {
            var t1 = 1f - t;
            return t1 * t1 * t1 * a + 3f * t * (t1 * t1) * b + 3f * (t * t) * t1 * c + t * t * t * d;
        }

        internal static float QuadBezier(float a, float b, float c, float t)
        {
            var t1 = 1f - t;
            return t1 * t1 * a + 2.0f * t1 * t * b + t * t * c;
        }
    }
}