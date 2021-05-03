using System;

namespace Urho3DNet.Editor
{
    public class PlaneMoveGizmo : AbstractGizmo
    {
        private readonly Color _highlightedColor;
        private readonly Color _tonedDownColor;
        private Vector3 _offset;
        private Vector3 _scale;

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

        public override void Raycast(ref GizmoRaycast result)
        {
            var localFrom = WorldToLocal(result.Origin);
            var localTo = WorldToLocal(result.Contact);
            
            if (localFrom.Z * localTo.Z > 0.0f)
                return;

            var distance = localTo.Z - localFrom.Z;
            if (Math.Abs(distance) < 1e-3f)
                return;

            var factor = (-localFrom.Z) / distance;

            var contact = (localTo - localFrom) * factor + localFrom;

            var bbox = new BoundingBox(new Vector3(0, 0, -0.1f), new Vector3(1, 1, 0.1f));
            if (bbox.IsInside(contact) == Intersection.Inside)
            {
                result.Gizmo = this;
                result.Contact = LocalToWorld(contact);
            }

            return;
        }
    }
}