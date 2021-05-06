namespace Urho3DNet.Editor.Commands
{
    public class TransformCommand: AbstractSelectionCommand, IEditorCommand
    {
        private Matrix3x4[] _transforms;

        public TransformCommand(Selection nodes):base(nodes)
        {
            _transforms = new Matrix3x4[nodes.Count];
            var index = 0;
            foreach (var node in nodes)
            {
                _transforms[index] = node.Transform;
                ++index;
            }
        }

        public void WorldSpaceMoveBy(Vector3 vector3)
        {
            foreach (var node in this)
            {
                node.WorldPosition += vector3;
            }
        }
        public void WorldSpaceScaleBy(Vector3 scaleStep)
        {
            foreach (var node in this)
            {
                var r = new Matrix3x4(Vector3.Zero, node.WorldRotation, Vector3.One).Inverse();
                var localScale = r * scaleStep;
                var vector3 = Vector3.Max(node.GetScale() + localScale, Vector3.Zero);
                node.SetScale(vector3);
            }
        }

        public void Undo()
        {
            for (var index = 0; index < this.Count; index++)
            {
                var node = this[index];
                node.WorldTransform = _transforms[index];
            }
        }

        public void WorldSpaceRotateBy(Quaternion worldRotation)
        {
            foreach (var node in this)
            {
                node.WorldRotation = worldRotation * node.WorldRotation;
            }
        }
    }
}