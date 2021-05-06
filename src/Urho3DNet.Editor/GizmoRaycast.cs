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

        public bool IsWithinThreshold(Vector3 contact)
        {
            var clipSpaceContact = ViewProj * contact;
            var clipSpaceRay = ViewProj * Origin;

            var clipSpaceDistance =
                (new Vector2(clipSpaceRay.X, clipSpaceRay.Y) - new Vector2(clipSpaceContact.X, clipSpaceContact.Y)).Length;

            if (clipSpaceDistance > Threshold)
                return false;
            return true;
        }

        public bool SetIfCloser(Vector3 contact, IGizmo gizmo)
        {
            var newDist = (contact - Origin).DotProduct(Direction);
            var oldDist = (Contact - Origin).DotProduct(Direction);
            if (newDist < oldDist)
            {
                Contact = contact;
                Gizmo = gizmo;
                return true;
            }

            return false;
        }
    }
}