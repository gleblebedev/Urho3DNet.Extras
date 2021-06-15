using System;
using Urho3DNet.MVVM.Data.Core;
using Urho3DNet.MVVM.LogicalTree;

#nullable enable

namespace Urho3DNet.MVVM.Markup.Parsers.Nodes
{
    public class FindAncestorNode : ExpressionNode
    {
        private readonly int _level;
        private readonly Type? _ancestorType;
        private IDisposable? _subscription;

        public FindAncestorNode(Type? ancestorType, int level)
        {
            _level = level;
            _ancestorType = ancestorType;
        }

        public override string Description
        {
            get
            {
                if (_ancestorType == null)
                {
                    return $"$parent[{_level}]";
                }
                else
                {
                    return $"$parent[{_ancestorType.Name}, {_level}]";
                }
            }
        }

        protected override void StartListeningCore(WeakReference<object> reference)
        {
            if (reference.TryGetTarget(out object target) && target is ILogical logical)
            {
                _subscription = ControlLocator.Track(logical, _level, _ancestorType).Subscribe(ValueChanged);
            }
            else
            {
                _subscription = null;
            }
        }

        protected override void StopListeningCore()
        {
            _subscription?.Dispose();
            _subscription = null;
        }
    }
}
