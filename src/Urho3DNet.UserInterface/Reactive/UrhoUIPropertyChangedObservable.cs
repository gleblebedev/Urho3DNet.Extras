using System;

namespace Urho3DNet.UserInterface.Reactive
{
    internal class UrhoUIPropertyChangedObservable : 
        LightweightObservableBase<UrhoUIPropertyChangedEventArgs>,
        IDescription
    {
        private readonly WeakReference<IUrhoUIObject> _target;
        private readonly UrhoUIProperty _property;

        public UrhoUIPropertyChangedObservable(
            IUrhoUIObject target,
            UrhoUIProperty property)
        {
            _target = new WeakReference<IUrhoUIObject>(target);
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

        private void PropertyChanged(object sender, UrhoUIPropertyChangedEventArgs e)
        {
            if (e.Property == _property)
            {
                PublishNext(e);
            }
        }
    }
}
