using System;

namespace Urho3DNet.Editor.Commands
{
    public class SnapCommand : TransformCommand
    {
        public SnapCommand(Selection nodes) : base(nodes)
        {
            foreach (var node in nodes)
            {
                node.Position = Snap(node.Position, 10);
                node.Rotation = new Quaternion(Snap(node.Rotation.EulerAngles, 1.0f/15.0f));
                node.SetScale(Snap(node.GetScale(), 10));
            }
        }

        private Vector3 Snap(Vector3 v, float scale)
        {
            return new Vector3(SnapCommand.Snap(v.X, scale),
                SnapCommand.Snap(v.Y, scale),
                SnapCommand.Snap(v.Z, scale));
        }

        private static float Snap(float v, float scale)
        {
            return (float)(Math.Round(v*scale)/scale);
        }
    }
}