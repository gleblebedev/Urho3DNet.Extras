using Urho3DNet.Editor.Commands;

namespace Urho3DNet.Editor.Gizmos
{
    public class AxisScaleGizmo : AbstractGizmo
    {
        private TransformCommand _command;
        private readonly Color _highlightedColor;
        private readonly Color _tonedDownColor;
        private Matrix3x4 _startTransform;
        private Matrix3x4 _startTransformInv;
        private Vector3 _startContact;

        public AxisScaleGizmo(Context context, Color tonedDownColor, Color highlightedColor) : base(context)
        {
            _highlightedColor = highlightedColor;
            _tonedDownColor = tonedDownColor;
            Color = _tonedDownColor;
            var halfSize = 0.05f;
            var boxCenter = Vector3.Forward * (1.0f- halfSize * 2.0f);
            CreateModel(new Vector3[]
            {
                new Vector3(-halfSize,-halfSize,-halfSize) + boxCenter,
                new Vector3(-halfSize,-halfSize,+halfSize) + boxCenter,
                new Vector3(-halfSize,+halfSize,-halfSize) + boxCenter,
                new Vector3(-halfSize,+halfSize,+halfSize) + boxCenter,
                new Vector3(+halfSize,-halfSize,-halfSize) + boxCenter,
                new Vector3(+halfSize,-halfSize,+halfSize) + boxCenter,
                new Vector3(+halfSize,+halfSize,-halfSize) + boxCenter,
                new Vector3(+halfSize,+halfSize,+halfSize) + boxCenter,
                Vector3.Zero,
                Vector3.Forward * (1.0f- halfSize * 2.0f),
            }, PrimitiveType.LineList, new ushort[]
            {
                0, 1, 1, 3, 3, 2, 2, 0,
                0, 4,
                4, 5, 5, 7, 7, 6, 6, 4,
                1, 5,
                3, 7,
                2, 6,
                8, 9
            });
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
                _command.WorldSpaceScaleBy(delta);
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