using System;

namespace Urho3DNet.InputEvents
{
    public class PointerEventArgs : EventArgs
    {
        public PointerEventArgs()
        {
        }


        public void Set(int x, int y, int dx, int dy)
        {
            X = x;
            Y = y;
            Dx = dx;
            Dy = dy;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Dx { get; private set; }
        public int Dy { get; private set; }

        public static void FromMouseMove(PointerEventArgs eventArgs, InputEventsAdapter.MouseMoveEventArgs args)
        {
            eventArgs.Set(
                args.X,
                args.Y,
                args.DX,
                args.DY
            );
        }
    }
}