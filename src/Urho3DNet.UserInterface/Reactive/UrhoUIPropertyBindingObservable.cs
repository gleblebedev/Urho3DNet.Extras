using System;
using System.Collections.Generic;
using Urho3DNet.UserInterface.Data;

#nullable enable

namespace Urho3DNet.UserInterface.Reactive
{
    internal class UrhoUIPropertyBindingObservable<T> : LightweightObservableBase<BindingValue<T>>, IDescription
    {
        private readonly WeakReference<IUrhoUIObject> _target;
        private readonly UrhoUIProperty _property;
        private T _value;

#nullable disable
        public UrhoUIPropertyBindingObservable(
            IUrhoUIObject target,
            UrhoUIProperty property)
        {
            _target = new WeakReference<IUrhoUIObject>(target);
            _property = property;
        }
#nullable enable

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

        protected override void Subscribed(IObserver<BindingValue<T>> observer, bool first)
        {
            observer.OnNext(new BindingValue<T>(_value));
        }

        private void PropertyChanged(object sender, UrhoUIPropertyChangedEventArgs e)
        {
            if (e.Property == _property)
            {
                if (e is UrhoUIPropertyChangedEventArgs<T> typedArgs)
                {
                    var newValue = e.Sender.GetValue(typedArgs.Property);

                    if (!typedArgs.OldValue.HasValue || !EqualityComparer<T>.Default.Equals(newValue, _value))
                    {
                        _value = newValue;
                        PublishNext(_value);
                    }
                }
                else
                {
                    var newValue = e.Sender.GetValue(e.Property);

                    if (!Equals(newValue, _value))
                    {
                        _value = (T)newValue;
                        PublishNext(_value);
                    }
                }
            }
        }
    }
}
