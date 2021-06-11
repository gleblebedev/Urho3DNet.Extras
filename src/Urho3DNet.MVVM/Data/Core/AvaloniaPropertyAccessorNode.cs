using System;
using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Reactive;

namespace Urho3DNet.MVVM.Data.Core
{
    public class UrhoPropertyAccessorNode : SettableNode
    {
        private IDisposable _subscription;
        private readonly bool _enableValidation;
        private readonly UrhoProperty _property;

        public UrhoPropertyAccessorNode(UrhoProperty property, bool enableValidation)
        {
            _property = property;
            _enableValidation = enableValidation;
        }

        public override string Description => PropertyName;
        public string PropertyName { get; }
        public override Type PropertyType => _property.PropertyType;

        protected override bool SetTargetValueCore(object value, BindingPriority priority)
        {
            try
            {
                if (Target.TryGetTarget(out object target) && target is IUrhoObject obj)
                {
                    obj.SetValue(_property, value, priority);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        protected override void StartListeningCore(WeakReference<object> reference)
        {
            if (reference.TryGetTarget(out object target) && target is IUrhoObject obj)
            {
                _subscription = new UrhoPropertyObservable<object>(obj, _property).Subscribe(ValueChanged);
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
