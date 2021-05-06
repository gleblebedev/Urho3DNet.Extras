using System.Collections.Generic;

namespace Urho3DNet.Editor.Gizmos
{
    public class CompositeGizmo : IGizmo
    {
        private readonly SharedPtr<Node> _gizmoNode;
        private Context _context;
        
        private readonly List<IGizmo> _gismos = new List<IGizmo>();
        
        protected CompositeGizmo(Context context)
        {
            _context = context;
            _gizmoNode = new Node(context);
        }

        protected void Add(IGizmo gizmo)
        {
            gizmo.Node.Parent = _gizmoNode;
            _gismos.Add(gizmo);
        }

        public void Show(Scene scene)
        {
            foreach (var gismo in _gismos)
            {
                gismo.Show(scene);
            }
        }

        public void Hide()
        {
            foreach (var gismo in _gismos)
            {
                gismo.Hide();
            }
        }

        public virtual void ResizeGizmo(Camera camera)
        {
            float scale = 0.1f / camera.Zoom;

            if (camera.IsOrthographic)
                scale *= camera.OrthoSize;
            else
                scale *= (camera.View * _gizmoNode.Value.Position).Z;

            _gizmoNode.Value.SetScale(new Vector3(scale, scale, scale));
        }
        //public void ResizeGizmo(Camera camera)
        //{
        //    foreach (var gismo in _gismos)
        //    {
        //        gismo.ResizeGizmo(camera);
        //    }
        //}

        public void Raycast(ref GizmoRaycast result)
        {
            foreach (var gismo in _gismos)
            {
                gismo.Raycast(ref result);
            }
        }

        public void Highlight(bool highlight)
        {
            foreach (var gismo in _gismos)
            {
                gismo.Highlight(highlight);
            }
        }

        public Node Node => _gizmoNode;

        public IEditorCommand Start(Selection selection)
        {
            return null;
        }

        public void Apply()
        {
        }

        public void Cancel()
        {
        }

        public void Preview(ref GizmoRaycast raycast)
        {
        }
    }
}