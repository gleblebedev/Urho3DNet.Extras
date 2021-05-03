namespace Urho3DNet.Editor
{
    public interface IGizmo
    {
        void Show(Scene scene);
        
        void Hide();

        void Raycast(ref GizmoRaycast result);
        
        void Highlight(bool highlight);
        
        Node Node { get; }
    }
}