using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Urho3DNet.Editor
{
    public class Selection : IEnumerable<Node>
    {
        private readonly UndoStack _undoStack;
        
        private HashSet<Node> _nodes = new HashSet<Node>();

        public event EventHandler SelectionChanged;
        
        public Selection(UndoStack undoStack = null)
        {
            _undoStack = undoStack;
        }

        private void RaiseSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private class SelectCommand : IEditorCommand
        {
            private readonly Selection _selection;
            private readonly Node _node;
            private readonly bool _add;

            public SelectCommand(Selection selection, Node node, bool add)
            {
                _selection = selection;
                _node = node;
                _add = add;
            }
            
            public void Dispose()
            {
            }

            public void Undo()
            {
                if (_add)
                {
                    if (_selection._nodes.Remove(_node))
                        _selection.RaiseSelectionChanged();
                }
                else
                {
                    if (_selection._nodes.Add(_node))
                        _selection.RaiseSelectionChanged();
                }
            }
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
                _undoStack?.Push(new SelectCommand(this, node, true));
                RaiseSelectionChanged();
            }
        }

        public void Remove(Node node)
        {
            if (node == null)
                return;

            if (_nodes.Remove(node))
            {
                _undoStack?.Push(new SelectCommand(this, node, false));
                RaiseSelectionChanged();
            }
        }

        public void Clear()
        {
            if (_nodes.Count > 0)
            {
                _nodes.Clear();
                // TODO: add command to undo stack
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
                _undoStack?.Push(new SelectCommand(this, node, false));
                RaiseSelectionChanged();
            }
            else
            {
                if (_nodes.Add(node))
                {
                    _undoStack?.Push(new SelectCommand(this, node, true));
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