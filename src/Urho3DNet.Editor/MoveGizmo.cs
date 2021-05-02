namespace Urho3DNet.Editor
{
    public class MoveGizmo : CompositeGizmo
    {
        private readonly SharedPtr<Node> _gizmoNode;
        private Context _context;

        public MoveGizmo(Context context) : base()
        {
            _context = context;
            _gizmoNode = new Node(context);

            Add(new AxisMoveGizmo(_gizmoNode,
                new Color(0.5f, 0.0f, 0.0f, 1.0f),
                new Color(1.0f, 0.0f, 0.0f, 1.0f)));
            Add(new AxisMoveGizmo(_gizmoNode,
                new Color(0.0f, 0.5f, 0.0f, 1.0f),
                new Color(0.0f, 1.0f, 0.0f, 1.0f))
            {
                Rotation = new Quaternion(-90, Vector3.Up)
            });
            Add(new AxisMoveGizmo(_gizmoNode,
                new Color(0.0f, 0.0f, 0.5f, 1.0f),
                new Color(0.0f, 0.0f, 1.0f, 1.0f))
            {
                Rotation = new Quaternion(90, Vector3.Forward)
            });
        }
    }
}