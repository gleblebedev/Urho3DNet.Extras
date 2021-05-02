namespace Urho3DNet.Editor
{
    public struct GizmoRaycast
    {
        public Matrix4 ViewProj;
        public Vector3 Origin;
        public Vector3 Direction;
        public Vector3 Contact;
        public float Threshold;
        public IGizmo Gizmo;
    }
}