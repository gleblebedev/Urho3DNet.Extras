using System;
using System.Reactive.Subjects;
using Urho3DNet.MVVM.Data;

#nullable enable

namespace Urho3DNet.MVVM.Reactive
{
    public static class BindingValueExtensions
    {
        public static IObservable<BindingValue<T>> ToBindingValue<T>(this IObservable<T> source)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));
            return new BindingValueAdapter<T>(source);
        }

        public static ISubject<BindingValue<T>> ToBindingValue<T>(this ISubject<T> source)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));
            return new BindingValueSubjectAdapter<T>(source);
        }

        public static IObservable<object?> ToUntyped<T>(this IObservable<BindingValue<T>> source)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));
            return new UntypedBindingAdapter<T>(source);
        }

        public static ISubject<object?> ToUntyped<T>(this ISubject<BindingValue<T>> source)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));
            return new UntypedBindingSubjectAdapter<T>(source);
        }
    }
}
