using System;
using ImGuiNet;

namespace Urho3DNet.Editor
{
    public class AxisMoveGizmo : AbstractGizmo
    {
        private readonly Color _selectedColor;
        private readonly Color _unselectedColor;

        public AxisMoveGizmo(Node parent, Color unselectedColor, Color selectedColor) : base(parent)
        {
            _selectedColor = selectedColor;
            _unselectedColor = unselectedColor;
            Color = _unselectedColor;
            CreateModel(new Vector3[]
            {
                new Vector3(0,0,0),
                new Vector3(1,0,0),
            }, PrimitiveType.LineList, new ushort[]{0,1});
        }

        public override void Select(bool @select)
        {
            if (@select)
            {
                Color = _selectedColor;
            }
            else
            {
                Color = _unselectedColor;
            }
        }

        public override void Raycast(ref GizmoRaycast result)
        {
            var clipSpaceFrom = result.ViewProj * result.Origin;
            var clipSpaceTo = result.ViewProj * result.Contact;
            
            var clipSpacePivot = result.ViewProj * LocalToWorld(new Vector3(0,0,0));
            var clipSpaceTip = result.ViewProj * LocalToWorld(new Vector3(1, 0, 0));

            var bbox = new BoundingBox();
            bbox.Clear();
            bbox.Merge(clipSpacePivot);
            bbox.Merge(clipSpaceTip);
            var threshold3 = new Vector3(result.Threshold, result.Threshold, result.Threshold);
            bbox = new BoundingBox(bbox.Min - threshold3, bbox.Max + threshold3);
            bbox.Merge(new Vector3(bbox.Min.X, bbox.Min.Y, 1.1f));
            if (bbox.IsInside(clipSpaceTo) == Intersection.Outside)
                return;

            var flatDir = new Vector2(clipSpaceTip.X - clipSpacePivot.X, clipSpaceTip.Y - clipSpacePivot.Y);
            var flatN = new Vector2(-flatDir.Y, flatDir.X);
            var dirLength = flatN.LengthSquared;
            if (dirLength < 1e-3f)
                return;
            dirLength = (float) Math.Sqrt(dirLength);
            flatN *= 1.0f/ dirLength;
            var distance =
                flatN.DotProduct(new Vector2(clipSpaceTip.X - clipSpaceFrom.X, clipSpaceTip.Y - clipSpaceFrom.Y));
            if (Math.Abs(distance) > result.Threshold)
                return;

            var flatContact = flatN * distance + new Vector2(clipSpaceFrom.X, clipSpaceFrom.Y);
            var flatPos = new Vector2(flatContact.X - clipSpacePivot.X, flatContact.Y - clipSpacePivot.Y);
            var clipSpaceDistance = flatPos.Length / dirLength;
            if (clipSpaceDistance < 0.0f) clipSpaceDistance = 0.0f;
            if (clipSpaceDistance > 1.0f) clipSpaceDistance = 1.0f;

            var clipSpaceContact = clipSpacePivot + (clipSpaceTip - clipSpacePivot)* clipSpaceDistance;
            
            if (clipSpaceContact.Z < clipSpaceTo.Z)
            {
                result.Gizmo = this;
                result.Contact = result.ViewProj.Inverted * clipSpaceContact;
            }

            return;
        }
    }
}