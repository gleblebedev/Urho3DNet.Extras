﻿using System;
using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Data;
//using Urho3DNet.MVVM.Logging;

#nullable enable

namespace Urho3DNet.MVVM.Reactive
{
    internal class TypedBindingAdapter<T> : SingleSubscriberObservableBase<BindingValue<T>>,
        IObserver<BindingValue<object>>
    {
        private readonly IUrhoObject _target;
        private readonly UrhoProperty<T> _property;
        private readonly IObservable<BindingValue<object>> _source;
        private IDisposable? _subscription;

        public TypedBindingAdapter(
            IUrhoObject target,
            UrhoProperty<T> property,
            IObservable<BindingValue<object>> source)
        {
            _target = target;
            _property = property;
            _source = source;
        }

        public void OnNext(BindingValue<object> value)
        {
            try
            {
                PublishNext(value.Convert<T>());
            }
            catch (InvalidCastException e)
            {
                //Logger.TryGet(LogEventLevel.Error, LogArea.Binding)?.Log(
                //    _target,
                //    "Binding produced invalid value for {$Property} ({$PropertyType}): {$Value} ({$ValueType})",
                //    _property.Name,
                //    _property.PropertyType,
                //    value.Value,
                //    value.Value?.GetType());
                PublishNext(BindingValue<T>.BindingError(e));
            }
        }

        public void OnCompleted() => PublishCompleted();
        public void OnError(Exception error) => PublishError(error);

        public static IObservable<BindingValue<T>> Create(
            IUrhoObject target,
            UrhoProperty<T> property,
            IObservable<BindingValue<object>> source)
        {
            return source is IObservable<BindingValue<T>> result ?
                result :
                new TypedBindingAdapter<T>(target, property, source);
        }

        protected override void Subscribed() => _subscription = _source.Subscribe(this);
        protected override void Unsubscribed() => _subscription?.Dispose();
    }
}
