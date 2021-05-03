using System;
using ImGuiNet;

namespace Urho3DNet.Editor
{
    public class AxisMoveGizmo : AbstractGizmo
    {
        private readonly Color _highlightedColor;
        private readonly Color _tonedDownColor;

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

        public override void Raycast(ref GizmoRaycast result)
        {
            var clipSpaceFrom = result.ViewProj * result.Origin;
            var clipSpaceTo = result.ViewProj * result.Contact;
            
            var clipSpacePivot = result.ViewProj * LocalToWorld(new Vector3(0,0,0));
            var clipSpaceTip = result.ViewProj * LocalToWorld(new Vector3(0, 0, 1));

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