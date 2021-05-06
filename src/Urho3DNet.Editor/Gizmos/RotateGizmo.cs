namespace Urho3DNet.Editor.Gizmos
{
    public class RotateGizmo : CompositeGizmo
    {
        public RotateGizmo(Context context) : base(context)
        {
            Add(new PlaneRotateGizmo(context,
                new Color(0.0f, 0.0f, 0.5f, 1.0f),
                new Color(0.0f, 0.0f, 1.0f, 1.0f)));
            Add(new PlaneRotateGizmo(context,
                new Color(0.0f, 0.5f, 0.0f, 1.0f),
                new Color(0.0f, 1.0f, 0.0f, 1.0f))
            {
                Rotation = new Quaternion(-90.0f, Vector3.Right)
            });
            Add(new PlaneRotateGizmo(context,
                new Color(0.5f, 0.0f, 0.0f, 1.0f),
                new Color(1.0f, 0.0f, 0.0f, 1.0f))
            {
                Rotation = new Quaternion(-90.0f, Vector3.Up)
            });
        }
    }
}