using System.Collections.Generic;

namespace Urho3DNet.Editor.Gizmos
{
    public class SelectGizmo : IGizmo
    {
        private readonly SharedPtr<Node> _gizmoNode;
        private Context _context;

        private readonly List<IGizmo> _gismos = new List<IGizmo>();
        
        private int _selectedIndex = -1;
        private Scene _activeScene;

        public SelectGizmo(Context context)
        {
            _context = context;
            _gizmoNode = new Node(context);
        }

        public void Add(IGizmo gizmo)
        {
            gizmo.Node.Parent = _gizmoNode;
            _gismos.Add(gizmo);
        }

        public void Show(Scene scene)
        {
            if (_activeScene != null)
                Hide();
            _activeScene = scene;
            Selected?.Show(scene);
        }

        public void Hide()
        {
            if (_activeScene != null)
            {
                _activeScene = null;
                Selected?.Hide();
            }
        }

        public IGizmo Selected
        {
            get
            {
                if (_selectedIndex < 0 || _selectedIndex >= _gismos.Count)
                    return null;
                return _gismos[_selectedIndex];
            }
        }
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (_selectedIndex != value)
                {
                    if (_activeScene != null) Selected?.Hide();
                    _selectedIndex = value;
                    if (_activeScene != null) Selected?.Show(_activeScene);
                }
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


        public void Raycast(ref GizmoRaycast result)
        {
            if (_activeScene == null)
            {
                return;
            }
            Selected?.Raycast(ref result);
        }

        public void Highlight(bool highlight)
        {
            Selected?.Highlight(highlight);
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