namespace Urho3DNet.Editor
{
    public interface IGizmo
    {
        void Show(Camera camera);
        
        void Hide();

        void ResizeGizmo(Camera camera);

        void Raycast(ref GizmoRaycast result);
        
        void Select(bool select);
    }
}