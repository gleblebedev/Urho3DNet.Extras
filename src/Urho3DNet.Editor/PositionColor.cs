using System.Runtime.InteropServices;

namespace Urho3DNet.Editor
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PositionColor
    {
        public Vector3 Position;
        public Color Color;

        public PositionColor(Vector3 position, Color color)
        {
            Position = position;
            Color = color;
        }
        public PositionColor(Vector3 position)
        {
            Position = position;
            Color = Color.White;
        }
    }
}