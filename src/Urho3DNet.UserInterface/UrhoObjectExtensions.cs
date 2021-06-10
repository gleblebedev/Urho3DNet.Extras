using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Data;
using Urho3DNet.MVVM.Reactive;

namespace Urho3DNet.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="UrhoObject"/> and related classes.
    /// </summary>
    public static class UrhoObjectExtensions
    {
        /// <summary>
        /// Converts an <see cref="IObservable{T}"/> to an <see cref="IBinding"/>.
        /// </summary>
        /// <typeparam name="T">The type produced by the observable.</typeparam>
        /// <param name="source">The observable</param>
        /// <returns>An <see cref="IBinding"/>.</returns>
        public static IBinding ToBinding<T>(this IObservable<T> source)
        {
            return new BindingAdaptor(source.Select(x => (object)x));
        }

        /// <summary>
        /// Gets an observable for a <see cref="UrhoProperty"/>.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="property">The property.</param>
        /// <returns>
        /// An observable which fires immediately with the current value of the property on the
        /// object and subsequently each time the property value changes.
        /// </returns>
        /// <remarks>
        /// The subscription to <paramref name="o"/> is created using a weak reference.
        /// </remarks>
        public static IObservable<object> GetObservable(this IUrhoObject o, UrhoProperty property)
        {
            Contract.Requires<ArgumentNullException>(o != null);
            Contract.Requires<ArgumentNullException>(property != null);

            return new UrhoPropertyObservable<object>(o, property);
        }

        /// <summary>
        /// Gets an observable for a <see cref="UrhoProperty"/>.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>
        /// An observable which fires immediately with the current value of the property on the
        /// object and subsequently each time the property value changes.
        /// </returns>
        /// <remarks>
        /// The subscription to <paramref name="o"/> is created using a weak reference.
        /// </remarks>
        public static IObservable<T> GetObservable<T>(this IUrhoObject o, UrhoProperty<T> property)
        {
            Contract.Requires<ArgumentNullException>(o != null);
            Contract.Requires<ArgumentNullException>(property != null);

            return new UrhoPropertyObservable<T>(o, property);
        }

        /// <summary>
        /// Gets an observable for a <see cref="UrhoProperty"/>.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="property">The property.</param>
        /// <returns>
        /// An observable which fires immediately with the current value of the property on the
        /// object and subsequently each time the property value changes.
        /// </returns>
        /// <remarks>
        /// The subscription to <paramref name="o"/> is created using a weak reference.
        /// </remarks>
        public static IObservable<BindingValue<object>> GetBindingObservable(
            this IUrhoObject o,
            UrhoProperty property)
        {
            Contract.Requires<ArgumentNullException>(o != null);
            Contract.Requires<ArgumentNullException>(property != null);

            return new UrhoPropertyBindingObservable<object>(o, property);
        }

        /// <summary>
        /// Gets an observable for a <see cref="UrhoProperty"/>.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>
        /// An observable which fires immediately with the current value of the property on the
        /// object and subsequently each time the property value changes.
        /// </returns>
        /// <remarks>
        /// The subscription to <paramref name="o"/> is created using a weak reference.
        /// </remarks>
        public static IObservable<BindingValue<T>> GetBindingObservable<T>(
            this IUrhoObject o,
            UrhoProperty<T> property)
        {
            Contract.Requires<ArgumentNullException>(o != null);
            Contract.Requires<ArgumentNullException>(property != null);

            return new UrhoPropertyBindingObservable<T>(o, property);
        }

        /// <summary>
        /// Gets an observable that listens for property changed events for an
        /// <see cref="UrhoProperty"/>.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="property">The property.</param>
        /// <returns>
        /// An observable which when subscribed pushes the property changed event args
        /// each time a <see cref="IUrhoObject.PropertyChanged"/> event is raised
        /// for the specified property.
        /// </returns>
        public static IObservable<UrhoPropertyChangedEventArgs> GetPropertyChangedObservable(
            this IUrhoObject o,
            UrhoProperty property)
        {
            Contract.Requires<ArgumentNullException>(o != null);
            Contract.Requires<ArgumentNullException>(property != null);

            return new UrhoPropertyChangedObservable(o, property);
        }

        /// <summary>
        /// Gets a subject for a <see cref="UrhoProperty"/>.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="priority">
        /// The priority with which binding values are written to the object.
        /// </param>
        /// <returns>
        /// An <see cref="ISubject{Object}"/> which can be used for two-way binding to/from the 
        /// property.
        /// </returns>
        public static ISubject<object> GetSubject(
            this IUrhoObject o,
            UrhoProperty property,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            return Subject.Create<object>(
                Observer.Create<object>(x => o.SetValue(property, x, priority)),
                o.GetObservable(property));
        }

        /// <summary>
        /// Gets a subject for a <see cref="UrhoProperty"/>.
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="o">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="priority">
        /// The priority with which binding values are written to the object.
        /// </param>
        /// <returns>
        /// An <see cref="ISubject{T}"/> which can be used for two-way binding to/from the 
        /// property.
        /// </returns>
        public static ISubject<T> GetSubject<T>(
            this IUrhoObject o,
            UrhoProperty<T> property,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            return Subject.Create<T>(
                Observer.Create<T>(x => o.SetValue(property, x, priority)),
                o.GetObservable(property));
        }

        /// <summary>
        /// Gets a subject for a <see cref="UrhoProperty"/>.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="priority">
        /// The priority with which binding values are written to the object.
        /// </param>
        /// <returns>
        /// An <see cref="ISubject{Object}"/> which can be used for two-way binding to/from the 
        /// property.
        /// </returns>
        public static ISubject<BindingValue<object>> GetBindingSubject(
            this IUrhoObject o,
            UrhoProperty property,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            return Subject.Create<BindingValue<object>>(
                Observer.Create<BindingValue<object>>(x =>
                {
                    if (x.HasValue)
                    {
                        o.SetValue(property, x.Value, priority);
                    }
                }),
                o.GetBindingObservable(property));
        }

        /// <summary>
        /// Gets a subject for a <see cref="UrhoProperty"/>.
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="o">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="priority">
        /// The priority with which binding values are written to the object.
        /// </param>
        /// <returns>
        /// An <see cref="ISubject{T}"/> which can be used for two-way binding to/from the 
        /// property.
        /// </returns>
        public static ISubject<BindingValue<T>> GetBindingSubject<T>(
            this IUrhoObject o,
            UrhoProperty<T> property,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            return Subject.Create<BindingValue<T>>(
                Observer.Create<BindingValue<T>>(x =>
                {
                    if (x.HasValue)
                    {
                        o.SetValue(property, x.Value, priority);
                    }
                }),
                o.GetBindingObservable(property));
        }

        /// <summary>
        /// Binds a <see cref="UrhoProperty"/> to an observable.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="source">The observable.</param>
        /// <param name="priority">The priority of the binding.</param>
        /// <returns>
        /// A disposable which can be used to terminate the binding.
        /// </returns>
        public static IDisposable Bind(
            this IUrhoObject target,
            UrhoProperty property,
            IObservable<BindingValue<object>> source,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));
            source = source ?? throw new ArgumentNullException(nameof(source));

            return property.RouteBind(target, source, priority);
        }

        /// <summary>
        /// Binds a <see cref="UrhoProperty"/> to an observable.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="source">The observable.</param>
        /// <param name="priority">The priority of the binding.</param>
        /// <returns>
        /// A disposable which can be used to terminate the binding.
        /// </returns>
        public static IDisposable Bind<T>(
            this IUrhoObject target,
            UrhoProperty<T> property,
            IObservable<BindingValue<T>> source,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));
            source = source ?? throw new ArgumentNullException(nameof(source));

            return property switch
            {
                StyledPropertyBase<T> styled => target.Bind(styled, source, priority),
                DirectPropertyBase<T> direct => target.Bind(direct, source),
                _ => throw new NotSupportedException("Unsupported UrhoProperty type."),
            };
        }

        /// <summary>
        /// Binds a <see cref="UrhoProperty"/> to an observable.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="source">The observable.</param>
        /// <param name="priority">The priority of the binding.</param>
        /// <returns>
        /// A disposable which can be used to terminate the binding.
        /// </returns>
        public static IDisposable Bind(
            this IUrhoObject target,
            UrhoProperty property,
            IObservable<object> source,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));
            source = source ?? throw new ArgumentNullException(nameof(source));

            return target.Bind(
                property,
                source.ToBindingValue(),
                priority);
        }

        /// <summary>
        /// Binds a <see cref="UrhoProperty"/> to an observable.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="source">The observable.</param>
        /// <param name="priority">The priority of the binding.</param>
        /// <returns>
        /// A disposable which can be used to terminate the binding.
        /// </returns>
        public static IDisposable Bind<T>(
            this IUrhoObject target,
            UrhoProperty<T> property,
            IObservable<T> source,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));
            source = source ?? throw new ArgumentNullException(nameof(source));

            return target.Bind(
                property,
                source.ToBindingValue(),
                priority);
        }

        /// <summary>
        /// Binds a property on an <see cref="IUrhoObject"/> to an <see cref="IBinding"/>.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="property">The property to bind.</param>
        /// <param name="binding">The binding.</param>
        /// <param name="anchor">
        /// An optional anchor from which to locate required context. When binding to objects that
        /// are not in the logical tree, certain types of binding need an anchor into the tree in 
        /// order to locate named controls or resources. The <paramref name="anchor"/> parameter 
        /// can be used to provice this context.
        /// </param>
        /// <returns>An <see cref="IDisposable"/> which can be used to cancel the binding.</returns>
        public static IDisposable Bind(
            this IUrhoObject target,
            UrhoProperty property,
            IBinding binding,
            object anchor = null)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));
            binding = binding ?? throw new ArgumentNullException(nameof(binding));

            var metadata = property.GetMetadata(target.GetType()) as IDirectPropertyMetadata;

            var result = binding.Initiate(
                target,
                property,
                anchor,
                metadata?.EnableDataValidation ?? false);

            if (result != null)
            {
                return BindingOperations.Apply(target, property, result, anchor);
            }
            else
            {
                return Disposable.Empty;
            }
        }

        /// <summary>
        /// Clears a <see cref="UrhoProperty"/>'s local value.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        public static void ClearValue(this IUrhoObject target, UrhoProperty property)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));

            property.RouteClearValue(target);
        }

        /// <summary>
        /// Clears a <see cref="UrhoProperty"/>'s local value.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        public static void ClearValue<T>(this IUrhoObject target, UrhoProperty<T> property)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));

            switch (property)
            {
                case StyledPropertyBase<T> styled:
                    target.ClearValue(styled);
                    break;
                case DirectPropertyBase<T> direct:
                    target.ClearValue(direct);
                    break;
                default:
                    throw new NotSupportedException("Unsupported UrhoProperty type.");
            }
        }

        /// <summary>
        /// Gets a <see cref="UrhoProperty"/> value.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        /// <returns>The value.</returns>
        public static object GetValue(this IUrhoObject target, UrhoProperty property)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));

            return property.RouteGetValue(target);
        }

        /// <summary>
        /// Gets a <see cref="UrhoProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        /// <returns>The value.</returns>
        public static T GetValue<T>(this IUrhoObject target, UrhoProperty<T> property)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));

            return property switch
            {
                StyledPropertyBase<T> styled => target.GetValue(styled),
                DirectPropertyBase<T> direct => target.GetValue(direct),
                _ => throw new NotSupportedException("Unsupported UrhoProperty type.")
            };
        }

        /// <summary>
        /// Gets an <see cref="UrhoProperty"/> base value.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="maxPriority">The maximum priority for the value.</param>
        /// <remarks>
        /// For styled properties, gets the value of the property if set on the object with a
        /// priority equal or lower to <paramref name="maxPriority"/>, otherwise
        /// <see cref="UrhoProperty.UnsetValue"/>. Note that this method does not return
        /// property values that come from inherited or default values.
        /// 
        /// For direct properties returns <see cref="GetValue(IUrhoObject, UrhoProperty)"/>.
        /// </remarks>
        public static object GetBaseValue(
            this IUrhoObject target,
            UrhoProperty property,
            BindingPriority maxPriority)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));

            return property.RouteGetBaseValue(target, maxPriority);
        }

        /// <summary>
        /// Gets an <see cref="UrhoProperty"/> base value.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="maxPriority">The maximum priority for the value.</param>
        /// <remarks>
        /// For styled properties, gets the value of the property if set on the object with a
        /// priority equal or lower to <paramref name="maxPriority"/>, otherwise
        /// <see cref="Optional{T}.Empty"/>. Note that this method does not return property values
        /// that come from inherited or default values.
        /// 
        /// For direct properties returns
        /// <see cref="IUrhoObject.GetValue{T}(DirectPropertyBase{T})"/>.
        /// </remarks>
        public static Optional<T> GetBaseValue<T>(
            this IUrhoObject target,
            UrhoProperty<T> property,
            BindingPriority maxPriority)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));

            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));

            return property switch
            {
                StyledPropertyBase<T> styled => target.GetBaseValue(styled, maxPriority),
                DirectPropertyBase<T> direct => target.GetValue(direct),
                _ => throw new NotSupportedException("Unsupported UrhoProperty type.")
            };
        }

        /// <summary>
        /// Sets a <see cref="UrhoProperty"/> value.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="priority">The priority of the value.</param>
        /// <returns>
        /// An <see cref="IDisposable"/> if setting the property can be undone, otherwise null.
        /// </returns>
        public static IDisposable SetValue(
            this IUrhoObject target,
            UrhoProperty property,
            object value,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));

            return property.RouteSetValue(target, value, priority);
        }

        /// <summary>
        /// Sets a <see cref="UrhoProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="target">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="priority">The priority of the value.</param>
        /// <returns>
        /// An <see cref="IDisposable"/> if setting the property can be undone, otherwise null.
        /// </returns>
        public static IDisposable SetValue<T>(
            this IUrhoObject target,
            UrhoProperty<T> property,
            T value,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            property = property ?? throw new ArgumentNullException(nameof(property));

            switch (property)
            {
                case StyledPropertyBase<T> styled:
                    return target.SetValue(styled, value, priority);
                case DirectPropertyBase<T> direct:
                    target.SetValue(direct, value);
                    return null;
                default:
                    throw new NotSupportedException("Unsupported UrhoProperty type.");
            }
        }

        /// <summary>
        /// Subscribes to a property changed notifications for changes that originate from a
        /// <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TTarget">The type of the property change sender.</typeparam>
        /// <param name="observable">The property changed observable.</param>
        /// <param name="action">
        /// The method to call. The parameters are the sender and the event args.
        /// </param>
        /// <returns>A disposable that can be used to terminate the subscription.</returns>
        public static IDisposable AddClassHandler<TTarget>(
            this IObservable<UrhoPropertyChangedEventArgs> observable,
            Action<TTarget, UrhoPropertyChangedEventArgs> action)
            where TTarget : UrhoObject
        {
            return observable.Subscribe(e =>
            {
                if (e.Sender is TTarget target)
                {
                    action(target, e);
                }
            });
        }

        /// <summary>
        /// Subscribes to a property changed notifications for changes that originate from a
        /// <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TTarget">The type of the property change sender.</typeparam>
        /// <param name="observable">The property changed observable.</param>
        /// <param name="handler">Given a TTarget, returns the handler.</param>
        /// <returns>A disposable that can be used to terminate the subscription.</returns>
        [Obsolete("Use overload taking Action<TTarget, UrhoPropertyChangedEventArgs>.")]
        public static IDisposable AddClassHandler<TTarget>(
            this IObservable<UrhoPropertyChangedEventArgs> observable,
            Func<TTarget, Action<UrhoPropertyChangedEventArgs>> handler)
            where TTarget : class
        {
            return observable.Subscribe(e => SubscribeAdapter(e, handler));
        }

        /// <summary>
        /// Gets a description of a property that van be used in observables.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="property">The property</param>
        /// <returns>The description.</returns>
        private static string GetDescription(IUrhoObject o, UrhoProperty property)
        {
            return $"{o.GetType().Name}.{property.Name}";
        }

        /// <summary>
        /// Observer method for <see cref="AddClassHandler{TTarget}(IObservable{UrhoPropertyChangedEventArgs},
        /// Func{TTarget, Action{UrhoPropertyChangedEventArgs}})"/>.
        /// </summary>
        /// <typeparam name="TTarget">The sender type to accept.</typeparam>
        /// <param name="e">The event args.</param>
        /// <param name="handler">Given a TTarget, returns the handler.</param>
        private static void SubscribeAdapter<TTarget>(
            UrhoPropertyChangedEventArgs e,
            Func<TTarget, Action<UrhoPropertyChangedEventArgs>> handler)
            where TTarget : class
        {
            if (e.Sender is TTarget target)
            {
                handler(target)(e);
            }
        }

        private class BindingAdaptor : IBinding
        {
            private IObservable<object> _source;

            public BindingAdaptor(IObservable<object> source)
            {
                this._source = source;
            }

            public InstancedBinding Initiate(
                IUrhoObject target,
                UrhoProperty targetProperty,
                object anchor = null,
                bool enableDataValidation = false)
            {
                return InstancedBinding.OneWay(_source);
            }
        }
    }
}
