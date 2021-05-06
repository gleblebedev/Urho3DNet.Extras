namespace Urho3DNet.Editor.Gizmos
{
    public class BoxGizmo : AbstractGizmo
    {
        private Color _highlightedColor;
        private Color _tonedDownColor;

        public BoxGizmo(Context context, float boxSize, Color tonedDownColor, Color highlightedColor) : base(context)
        {
            _highlightedColor = highlightedColor;
            _tonedDownColor = tonedDownColor;
            Color = _tonedDownColor;
            float halfSize = boxSize * 0.5f;
            CreateModel(new Vector3[]
            {
                new Vector3(-halfSize,-halfSize,-halfSize),
                new Vector3(-halfSize,-halfSize,+halfSize),
                new Vector3(-halfSize,+halfSize,-halfSize),
                new Vector3(-halfSize,+halfSize,+halfSize),
                new Vector3(+halfSize,-halfSize,-halfSize),
                new Vector3(+halfSize,-halfSize,+halfSize),
                new Vector3(+halfSize,+halfSize,-halfSize),
                new Vector3(+halfSize,+halfSize,+halfSize),
            }, PrimitiveType.LineList, new ushort[]
            {
                0, 1, 1, 3, 3, 2, 2, 0,
                0, 4,
                4, 5, 5, 7, 7, 6, 6, 4,
                1, 5,
                3, 7,
                2, 6
            });
        }
    }
}