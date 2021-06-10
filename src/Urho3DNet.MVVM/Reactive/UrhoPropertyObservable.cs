using System;
using System.Collections.Generic;
using Urho3DNet.MVVM.Binding;

namespace Urho3DNet.MVVM.Reactive
{
    internal class UrhoPropertyObservable<T> : LightweightObservableBase<T>, IDescription
    {
        private readonly WeakReference<IUrhoObject> _target;
        private readonly UrhoProperty _property;
        private T _value;

        public UrhoPropertyObservable(
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
                _value = (T)target.GetValue(_property);
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

        protected override void Subscribed(IObserver<T> observer, bool first)
        {
            observer.OnNext(_value);
        }

        private void PropertyChanged(object sender, UrhoPropertyChangedEventArgs e)
        {
            if (e.Property == _property)
            {
                T newValue;

                if (e is UrhoPropertyChangedEventArgs<T> typed)
                {
                    newValue = typed.Sender.GetValue(typed.Property);
                }
                else
                {
                    newValue = (T)e.Sender.GetValue(e.Property);
                }

                if (!EqualityComparer<T>.Default.Equals(newValue, _value))
                {
                    _value = newValue;
                    PublishNext(_value);
                }
            }
        }
    }
}
