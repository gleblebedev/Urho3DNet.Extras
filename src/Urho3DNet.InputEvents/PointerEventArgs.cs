using System;

namespace Urho3DNet.InputEvents
{
    public class PointerEventArgs : EventArgs
    {
        public PointerEventArgs(int x, int y, int dx, int dy)
        {
            X = x;
            Y = y;
            Dx = dx;
            Dy = dy;
        }

        public int X { get; }
        public int Y { get; }
        public int Dx { get; }
        public int Dy { get; }

        public static PointerEventArgs FromMouseMove(VariantMap args)
        {
            return new PointerEventArgs(
                args[E.MouseMove.X].Int,
                args[E.MouseMove.Y].Int,
                args[E.MouseMove.DX].Int,
                args[E.MouseMove.DY].Int
            );
        }
    }
}