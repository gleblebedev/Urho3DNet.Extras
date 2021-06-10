using System;
using Urho3DNet.MVVM.Binding;

namespace Urho3DNet.MVVM.Reactive
{
    internal class UrhoPropertyChangedObservable : 
        LightweightObservableBase<UrhoPropertyChangedEventArgs>,
        IDescription
    {
        private readonly WeakReference<IUrhoObject> _target;
        private readonly UrhoProperty _property;

        public UrhoPropertyChangedObservable(
            IUrhoObject target,
            UrhoProperty property)
        {
            _target = new WeakReference<IUrhoObject>(target);
            _property = property;
        }

        public string Description => $"{_target.GetType().Name}.{_property.Name}";

        protected override void Initialize()
        {
            if (_target.TryGetTarget(out var target))
            {
                target.PropertyChanged += PropertyChanged;
            }
        }

        protected override void Deinitialize()
        {
            if (_target.TryGetTarget(out var target))
            {
                target.PropertyChanged -= PropertyChanged;
            }
        }

        private void PropertyChanged(object sender, UrhoPropertyChangedEventArgs e)
        {
            if (e.Property == _property)
            {
                PublishNext(e);
            }
        }
    }
}
