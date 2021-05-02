using System.Collections.Generic;

namespace Urho3DNet.Editor
{
    public class CompositeGizmo : IGizmo
    {
        private readonly List<IGizmo> _gismos = new List<IGizmo>();
        
        public CompositeGizmo()
        {
        }

        protected void Add(IGizmo gizmo)
        {
            _gismos.Add(gizmo);
        }

        public void Show(Camera camera)
        {
            foreach (var gismo in _gismos)
            {
                gismo.Show(camera);
            }
        }

        public void Hide()
        {
            foreach (var gismo in _gismos)
            {
                gismo.Hide();
            }
        }

        public void ResizeGizmo(Camera camera)
        {
            foreach (var gismo in _gismos)
            {
                gismo.ResizeGizmo(camera);
            }
        }

        public void Raycast(ref GizmoRaycast result)
        {
            foreach (var gismo in _gismos)
            {
                gismo.Raycast(ref result);
            }
        }

        public void Select(bool @select)
        {
            foreach (var gismo in _gismos)
            {
                gismo.Select(@select);
            }
        }
    }
}