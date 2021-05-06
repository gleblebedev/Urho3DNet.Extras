using System;
using System.Collections;
using System.Collections.Generic;

namespace Urho3DNet.Editor.Commands
{
    public class AbstractSelectionCommand: IEnumerable<Node>, IDisposable
    {
        private readonly SharedPtr<Node>[] _nodes;

        public int Count => _nodes.Length;

        public AbstractSelectionCommand(Selection nodes)
        {
            _nodes = new SharedPtr<Node>[nodes.Count];
            var index = 0;
            foreach (var node in nodes)
            {
                _nodes[index] = node;
                ++index;
            }
        }
        
        public IEnumerator<Node> GetEnumerator()
        {
            foreach (var node in _nodes)
            {
                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            foreach (var node in _nodes)
            {
                node.Dispose();
            }
        }

        protected Node this[int index]
        {
            get { return _nodes[index]; }
        }

    }
}