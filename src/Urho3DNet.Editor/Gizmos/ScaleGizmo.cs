namespace Urho3DNet.Editor.Gizmos
{
    public class ScaleGizmo : CompositeGizmo
    {
        public ScaleGizmo(Context context) : base(context)
        {
            Add(new AxisScaleGizmo(context,
                new Color(0.0f, 0.0f, 0.5f, 1.0f),
                new Color(0.0f, 0.0f, 1.0f, 1.0f)));
            Add(new PlaneScaleGizmo(context,
                new Color(0.0f, 0.0f, 0.5f, 1.0f),
                new Color(0.0f, 0.0f, 1.0f, 1.0f))
            {
                Position = new Vector3(0.25f, 0.25f, 0.0f),
                Scale = new Vector3(0.25f, 0.25f, 0.25f)
            });

            Add(new AxisScaleGizmo(context,
                new Color(0.0f, 0.5f, 0.0f, 1.0f),
                new Color(0.0f, 1.0f, 0.0f, 1.0f))
            {
                Rotation = new Quaternion(-90, Vector3.Right)
            });
            Add(new PlaneScaleGizmo(context,
                new Color(0.0f, 0.5f, 0.0f, 1.0f),
                new Color(0.0f, 1.0f, 0.0f, 1.0f))
            {
                Rotation = new Quaternion(-90, Vector3.Right),
                Position = new Vector3(0.25f, 0.0f, 0.5f),
                Scale = new Vector3(0.25f, 0.25f, 0.25f)
            });
            Add(new AxisScaleGizmo(context,
                new Color(0.5f, 0.0f, 0.0f, 1.0f),
                new Color(1.0f, 0.0f, 0.0f, 1.0f))
            {
                Rotation = new Quaternion(90, Vector3.Up)
            });
            Add(new PlaneScaleGizmo(context,
                new Color(0.5f, 0.0f, 0.0f, 1.0f),
                new Color(1.0f, 0.0f, 0.0f, 1.0f))
            {
                Rotation = new Quaternion(-90, Vector3.Up),
                Position = new Vector3(0.0f, 0.25f, 0.25f),
                Scale = new Vector3(0.25f, 0.25f, 0.25f)
            });
        }
    }
}