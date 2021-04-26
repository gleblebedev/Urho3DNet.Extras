using System;

namespace Urho3DNet.InputEvents
{
    public class TouchEventArgs : EventArgs
    {
        public int TouchId { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Dx { get; private set; }
        public int Dy { get; private set; }
        public float Pressure { get; private set; }

        public static void FromTouchBegin(TouchEventArgs eventArgs, InputEventsAdapter.TouchBeginEventArgs args)
        {
            eventArgs.Set(
                args.TouchID,
                args.X,
                args.Y,
                0,
                0,
                args.Pressure
            );
        }

        public static void FromTouchMove(TouchEventArgs eventArgs, InputEventsAdapter.TouchMoveEventArgs args)
        {
            eventArgs.Set(
                args.TouchID,
                args.X,
                args.Y,
                args.DX,
                args.DY,
                args.Pressure
            );
        }

        public static void FromTouchEnd(TouchEventArgs eventArgs, InputEventsAdapter.TouchEndEventArgs args)
        {
            eventArgs.Set(
                args.TouchID,
                args.X,
                args.Y,
                0,
                0,
                0.0f
            );
        }

        public void Set(int touchId, int x, int y, int dx, int dy, float pressure)
        {
            TouchId = touchId;
            X = x;
            Y = y;
            Dx = dx;
            Dy = dy;
            Pressure = pressure;
        }
    }
}