using Urho3DNet.Editor.Commands;

namespace Urho3DNet.Editor.Gizmos
{
    public class PlaneMoveGizmo : AbstractGizmo
    {
        private readonly Color _highlightedColor;
        private readonly Color _tonedDownColor;
        private Vector3 _startContact;
        private TransformCommand _command;
        private Matrix3x4 _startTransform;
        private Matrix3x4 _startTransformInv;

        public PlaneMoveGizmo(Context context, Color tonedDownColor, Color highlightedColor) : base(context)
        {
            _highlightedColor = highlightedColor;
            _tonedDownColor = tonedDownColor;
            Color = _tonedDownColor;
            CreateModel(new Vector3[]
            {
                new Vector3(0,0,0),
                new Vector3(1,0,0),
                new Vector3(0,1,0),
                new Vector3(1,1,0),
            }, PrimitiveType.TriangleList, new ushort[]
            {
                0, 1, 3, 0, 3, 2,
                0, 3, 1, 0, 2, 3,
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
            if (GetClosestPointOnXYPlane(new Ray(localOrigin, localDirection), out var localContact))
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
            if (GetClosestPointOnXYPlane(new Ray(localOrigin, localDirection), out var localContact))
            {
                localContact = new Vector3(MathDefs.Clamp(localContact.X, 0.0f, 1.0f), MathDefs.Clamp(localContact.Y, 0.0f, 1.0f), 0);
                
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