using System;

namespace Urho3DNet.InputEvents
{
    public class TouchEventArgs : EventArgs
    {
        public TouchEventArgs(int touchId, int x, int y, int dx, int dy, float pressure)
        {
            TouchId = touchId;
            X = x;
            Y = y;
            Dx = dx;
            Dy = dy;
            Pressure = pressure;
        }

        public int TouchId { get; }
        public int X { get; }
        public int Y { get; }
        public int Dx { get; }
        public int Dy { get; }
        public float Pressure { get; }

        public static TouchEventArgs FromTouchBegin(VariantMap args)
        {
            return new TouchEventArgs(
                args[E.TouchBegin.TouchID].Int,
                args[E.TouchBegin.X].Int,
                args[E.TouchBegin.Y].Int,
                0,
                0,
                args[E.TouchBegin.Pressure].Float
            );
        }

        public static TouchEventArgs FromTouchMove(VariantMap args)
        {
            return new TouchEventArgs(
                args[E.TouchMove.TouchID].Int,
                args[E.TouchMove.X].Int,
                args[E.TouchMove.Y].Int,
                args[E.TouchMove.DX].Int,
                args[E.TouchMove.DY].Int,
                args[E.TouchMove.Pressure].Float
            );
        }

        public static TouchEventArgs FromTouchEnd(VariantMap args)
        {
            return new TouchEventArgs(
                args[E.TouchEnd.TouchID].Int,
                args[E.TouchEnd.X].Int,
                args[E.TouchEnd.Y].Int,
                0,
                0,
                0.0f
            );
        }
    }
}