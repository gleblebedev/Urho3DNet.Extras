using Urho3DNet.Editor.Commands;

namespace Urho3DNet.Editor.Gizmos
{
    public class AxisMoveGizmo : AbstractGizmo
    {
        private TransformCommand _command;
        private readonly Color _highlightedColor;
        private readonly Color _tonedDownColor;
        private Matrix3x4 _startTransform;
        private Matrix3x4 _startTransformInv;
        private Vector3 _startContact;

        public AxisMoveGizmo(Context context, Color tonedDownColor, Color highlightedColor) : base(context)
        {
            _highlightedColor = highlightedColor;
            _tonedDownColor = tonedDownColor;
            Color = _tonedDownColor;
            CreateModel(new Vector3[]
            {
                Vector3.Zero,
                Vector3.Forward,
                Vector3.Forward *0.9f + (Vector3.Right)*0.025f,
                Vector3.Forward *0.9f - (Vector3.Right)*0.025f,
            }, PrimitiveType.LineList, new ushort[]{0,1,1,2,1,3});
        }

        public override void Highlight(bool highlight)
        {
            if (highlight)
            {
                Color = _highlightedColor;
            }
            else
            {
                Color = _tonedDownColor;
            }
        }

        public override IEditorCommand Start(Selection selection)
        {
            if (selection.IsEmpty)
                return null;
            _command = new TransformCommand(selection);
            _startTransform = Node.WorldTransform;
            _startTransformInv = _startTransform.Inverse();
            return _command;
        }

        public override void Preview(ref GizmoRaycast result)
        {
            var localOrigin = _startTransformInv * result.Origin;
            var localDirection = (_startTransformInv * new Vector4(result.Direction, 0)).Normalized;
            if (GetClosestPointOnZAxis(new Ray(localOrigin, localDirection), out var localContact))
            {
                var contact = _startTransform * localContact;
                var delta = contact - _startContact;
                _startContact = contact;
                _command.WorldSpaceMoveBy(delta);
            }
        }

        public override void Raycast(ref GizmoRaycast result)
        {
            var localOrigin = WorldToLocal(result.Origin);
            var localDirection = WorldToLocal(new Vector4(result.Direction, 0)).Normalized;
            if (GetClosestPointOnZAxis(new Ray(localOrigin, localDirection), out var localContact))
            {
                localContact = new Vector3(localContact.X, localContact.Y, MathDefs.Clamp(localContact.Z, 0.0f, 1.0f));

                var contact = LocalToWorld(localContact);

                if (!result.IsWithinThreshold(contact))
                    return;

                if (result.SetIfCloser(contact, this))
                {
                    _startContact = contact;
                }
            }
        }
    }
}