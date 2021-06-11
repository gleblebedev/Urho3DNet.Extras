using System;
using System.Runtime.ExceptionServices;
using Urho.Data.Core.Plugins;
using Urho3DNet.MVVM.Binding;
using UrhoObject= Urho3DNet.MVVM.ObjectView;

namespace Urho3DNet.MVVM.Data.Core.Plugins
{
    /// <summary>
    /// Reads a property from a <see cref="UrhoObject"/>.
    /// </summary>
    public class UrhoPropertyAccessorPlugin : IPropertyAccessorPlugin
    {
        /// <inheritdoc/>
        public bool Match(object obj, string propertyName)
        {
            if (obj is UrhoObject o)
            {
                return LookupProperty(o, propertyName) != null;
            }

            return false;
        }

        /// <summary>
        /// Starts monitoring the value of a property on an object.
        /// </summary>
        /// <param name="reference">A weak reference to the object.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>
        /// An <see cref="IPropertyAccessor"/> interface through which future interactions with the 
        /// property will be made.
        /// </returns>
        public IPropertyAccessor Start(WeakReference<object> reference, string propertyName)
        {
            Contract.Requires<ArgumentNullException>(reference != null);
            Contract.Requires<ArgumentNullException>(propertyName != null);

            reference.TryGetTarget(out object instance);
            var o = (UrhoObject)instance;
            var p = LookupProperty(o, propertyName);

            if (p != null)
            {
                return new Accessor(new WeakReference<UrhoObject>(o), p);
            }
            else if (instance != UrhoProperty.UnsetValue)
            {
                var message = $"Could not find UrhoProperty '{propertyName}' on '{instance}'";
                var exception = new MissingMemberException(message);
                return new PropertyError(new BindingNotification(exception, BindingErrorType.Error));
            }
            else
            {
                return null;
            }
        }

        private static UrhoProperty LookupProperty(ObjectView o, string propertyName)
        {
            return UrhoPropertyRegistry.Instance.FindRegistered(o, propertyName);
        }

        private static bool IsOfType(Type type, string typeName)
        {
            while (type != null)
            {
                if (type.Name == typeName)
                {
                    return true;
                }

                type = type.BaseType;
            }

            return false;
        }

        private class Accessor : PropertyAccessorBase, IObserver<object>
        {
            private readonly WeakReference<UrhoObject> _reference;
            private readonly UrhoProperty _property;
            private IDisposable _subscription;

            public Accessor(WeakReference<UrhoObject> reference, UrhoProperty property)
            {
                Contract.Requires<ArgumentNullException>(reference != null);
                Contract.Requires<ArgumentNullException>(property != null);

                _reference = reference;
                _property = property;
            }

            public UrhoObject Instance
            {
                get
                {
                    UrhoObject result;
                    _reference.TryGetTarget(out result);
                    return result;
                }
            }

            public override Type PropertyType => _property.PropertyType;
            public override object Value => Instance?.GetValue(_property);

            public override bool SetValue(object value, BindingPriority priority)
            {
                if (!_property.IsReadOnly)
                {
                    Instance.SetValue(_property, value, priority);
                    return true;
                }

                return false;
            }

            protected override void SubscribeCore()
            {
                _subscription = Instance?.GetObservable(_property).Subscribe(this);
            }

            protected override void UnsubscribeCore()
            {
                _subscription?.Dispose();
                _subscription = null;
            }

            void IObserver<object>.OnCompleted()
            {
            }

            void IObserver<object>.OnError(Exception error)
            {
                ExceptionDispatchInfo.Capture(error).Throw();
            }

            void IObserver<object>.OnNext(object value)
            {
                PublishValue(value);
            }
        }
    }
}
