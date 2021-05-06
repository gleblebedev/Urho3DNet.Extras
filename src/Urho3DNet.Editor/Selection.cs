using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Urho3DNet.Editor
{
    public class Selection : IEnumerable<Node>
    {
        private HashSet<Node> _nodes = new HashSet<Node>();

        public event EventHandler SelectionChanged;
        
        public Selection()
        {
        }

        private void RaiseSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public int Count => _nodes.Count;

        public bool IsEmpty => _nodes.Count == 0;

        public Vector3 GetWorldPosition()
        {
            if (IsEmpty)
                return Vector3.Zero;
            if (Count == 1)
            {
                return _nodes.First().WorldPosition;
            }

            if (TryGetBoundingBox(out var bbox))
            {
                return bbox.Center;
            }

            return Vector3.Zero;
        }

        public void Add(Node node)
        {
            if (node == null)
                return;

            if (_nodes.Add(node))
            {
                RaiseSelectionChanged();
            }
        }

        public void Remove(Node node)
        {
            if (node == null)
                return;

            if (_nodes.Remove(node))
            {
                RaiseSelectionChanged();
            }
        }

        public void Clear()
        {
            if (_nodes.Count > 0)
            {
                _nodes.Clear();
                RaiseSelectionChanged();
            }
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Toggle(Node node)
        {
            if (node == null)
                return;

            if (_nodes.Remove(node))
            {
                RaiseSelectionChanged();
            }
            else
            {
                if (_nodes.Add(node))
                {
                    RaiseSelectionChanged();
                }
            }
        }

        public bool TryGetBoundingBox(out BoundingBox bbox)
        {
            bbox = new BoundingBox();
            bbox.Clear();

            foreach (var node in _nodes)
            {
                foreach (Drawable component in node.GetComponents().Select(_=>_ as Drawable).Where(_=>_!=null))
                {
                    bbox.Merge(component.WorldBoundingBox);
                }
            }
            
            return bbox.Max.X > bbox.Min.X;
        }

        public Quaternion GetWorldRotation()
        {
            if (Count == 1)
            {
                return _nodes.First().WorldRotation;
            }
            return Quaternion.IDENTITY;
        }

        public Quaternion GetParentRotation()
        {
            if (Count == 1)
            {
                return _nodes.First().Parent.WorldRotation;
            }
            return Quaternion.IDENTITY;
        }
    }
}