using System;
using Urho3DNet.Editor.Commands;

namespace Urho3DNet.Editor.Gizmos
{
    public class PlaneRotateGizmo : AbstractGizmo
    {
        private readonly Color _highlightedColor;
        private readonly Color _tonedDownColor;
        private Vector3 _startContact;
        private TransformCommand _command;
        private Matrix3x4 _startTransform;
        private Matrix3x4 _startTransformInv;

        public PlaneRotateGizmo(Context context, Color tonedDownColor, Color highlightedColor) : base(context)
        {
            _highlightedColor = highlightedColor;
            _tonedDownColor = tonedDownColor;
            Color = _tonedDownColor;
            var circle = new Vector3[64];
            var indices = new ushort[circle.Length + 1];
            for (var index = 0; index < circle.Length; index++)
            {
                var a = index * MathDefs.TwoPi / circle.Length;
                circle[index] = new Vector3((float) Math.Cos(a), (float) Math.Sin(a), 0);
                indices[index] = (ushort) index;
            }

            CreateModel(circle, PrimitiveType.LineStrip, indices);
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
                localContact.Normalize();
                var localRotation = new Quaternion(_startContact, localContact);
                var worldRotation = _startTransform.Rotation * localRotation * _startTransform.Rotation.Inversed;
                _command.WorldSpaceRotateBy(worldRotation);
                _startContact = localContact;
            }
        }

        public override void Raycast(ref GizmoRaycast result)
        {
            var localOrigin = WorldToLocal(result.Origin);
            var localDirection = WorldToLocal(new Vector4(result.Direction, 0)).Normalized;
            if (GetClosestPointOnXYPlane(new Ray(localOrigin, localDirection), out var localContact))
            {
                localContact.Normalize();

                var contact = LocalToWorld(localContact);

                if (!result.IsWithinThreshold(contact))
                    return;

                if (result.SetIfCloser(contact, this))
                {
                    _startContact = localContact;
                }
            }
        }
    }
}