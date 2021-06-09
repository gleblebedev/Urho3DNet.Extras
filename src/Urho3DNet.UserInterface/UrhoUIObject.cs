﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Urho3DNet.UserInterface.Data;
using Urho3DNet.UserInterface.Diagnostics;
using Urho3DNet.UserInterface.PropertyStore;
using Urho3DNet.UserInterface.Threading;

namespace Urho3DNet.UserInterface
{
    /// <summary>
    /// An object with <see cref="UrhoUIProperty"/> support.
    /// </summary>
    /// <remarks>
    /// This class is analogous to DependencyObject in WPF.
    /// </remarks>
    public class UrhoUIObject : IUrhoUIObject, IUrhoUIObjectDebug, INotifyPropertyChanged, IValueSink
    {
        private IUrhoUIObject _inheritanceParent;
        private List<IDisposable> _directBindings;
        private PropertyChangedEventHandler _inpcChanged;
        private EventHandler<UrhoUIPropertyChangedEventArgs> _propertyChanged;
        private List<IUrhoUIObject> _inheritanceChildren;
        private ValueStore _values;
        private bool _batchUpdate;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoUIObject"/> class.
        /// </summary>
        public UrhoUIObject()
        {
            VerifyAccess();
        }

        /// <summary>
        /// Raised when a <see cref="UrhoUIProperty"/> value changes on this object.
        /// </summary>
        public event EventHandler<UrhoUIPropertyChangedEventArgs> PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }

        /// <summary>
        /// Raised when a <see cref="UrhoUIProperty"/> value changes on this object.
        /// </summary>
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { _inpcChanged += value; }
            remove { _inpcChanged -= value; }
        }

        /// <summary>
        /// Gets or sets the parent object that inherited <see cref="UrhoUIProperty"/> values
        /// are inherited from.
        /// </summary>
        /// <value>
        /// The inheritance parent.
        /// </value>
        protected IUrhoUIObject InheritanceParent
        {
            get
            {
                return _inheritanceParent;
            }

            set
            {
                VerifyAccess();

                if (_inheritanceParent != value)
                {
                    var oldParent = _inheritanceParent;
                    var valuestore = _values;

                    _inheritanceParent?.RemoveInheritanceChild(this);
                    _inheritanceParent = value;

                    var properties = UrhoUIPropertyRegistry.Instance.GetRegisteredInherited(GetType());
                    var propertiesCount = properties.Count;

                    for (var i = 0; i < propertiesCount; i++)
                    {
                        var property = properties[i];
                        if (valuestore?.IsSet(property) == true)
                        {
                            // If local value set there can be no change.
                            continue;
                        }

                        property.RouteInheritanceParentChanged(this, oldParent);
                    }

                    _inheritanceParent?.AddInheritanceChild(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of a <see cref="UrhoUIProperty"/>.
        /// </summary>
        /// <param name="property">The property.</param>
        public object this[UrhoUIProperty property]
        {
            get { return GetValue(property); }
            set { SetValue(property, value); }
        }

        /// <summary>
        /// Gets or sets a binding for a <see cref="UrhoUIProperty"/>.
        /// </summary>
        /// <param name="binding">The binding information.</param>
        public IBinding this[IndexerDescriptor binding]
        {
            get { return new IndexerBinding(this, binding.Property, binding.Mode); }
            set { this.Bind(binding.Property, value); }
        }

        private ValueStore Values
        {
            get
            {
                if (_values is null)
                {
                    _values = new ValueStore(this);

                    if (_batchUpdate)
                        _values.BeginBatchUpdate();
                }

                return _values;
            }
        }

        public bool CheckAccess() => Dispatcher.UIThread.CheckAccess();

        public void VerifyAccess() => Dispatcher.UIThread.VerifyAccess();

        /// <summary>
        /// Clears a <see cref="UrhoUIProperty"/>'s local value.
        /// </summary>
        /// <param name="property">The property.</param>
        public void ClearValue(UrhoUIProperty property)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));

            property.RouteClearValue(this);
        }

        /// <summary>
        /// Clears a <see cref="UrhoUIProperty"/>'s local value.
        /// </summary>
        /// <param name="property">The property.</param>
        public void ClearValue<T>(UrhoUIProperty<T> property)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));
            VerifyAccess();

            switch (property)
            {
                case StyledPropertyBase<T> styled:
                    ClearValue(styled);
                    break;
                case DirectPropertyBase<T> direct:
                    ClearValue(direct);
                    break;
                default:
                    throw new NotSupportedException("Unsupported UrhoUIProperty type.");
            }
        }

        /// <summary>
        /// Clears a <see cref="UrhoUIProperty"/>'s local value.
        /// </summary>
        /// <param name="property">The property.</param>
        public void ClearValue<T>(StyledPropertyBase<T> property)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));
            VerifyAccess();

            _values?.ClearLocalValue(property);
        }

        /// <summary>
        /// Clears a <see cref="UrhoUIProperty"/>'s local value.
        /// </summary>
        /// <param name="property">The property.</param>
        public void ClearValue<T>(DirectPropertyBase<T> property)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));
            VerifyAccess();

            var p = UrhoUIPropertyRegistry.Instance.GetRegisteredDirect(this, property);
            p.InvokeSetter(this, p.GetUnsetValue(GetType()));
        }

        /// <summary>
        /// Compares two objects using reference equality.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <remarks>
        /// Overriding Equals and GetHashCode on an UrhoUIObject is disallowed for two reasons:
        /// 
        /// - UrhoUIObjects are by their nature mutable
        /// - The presence of attached properties means that the semantics of equality are
        ///   difficult to define
        /// 
        /// See https://github.com/UrhoUIUI/UrhoUI/pull/2747 for the discussion that prompted
        /// this.
        /// </remarks>
        public sealed override bool Equals(object obj) => base.Equals(obj);

        /// <summary>
        /// Gets the hash code for the object.
        /// </summary>
        /// <remarks>
        /// Overriding Equals and GetHashCode on an UrhoUIObject is disallowed for two reasons:
        /// 
        /// - UrhoUIObjects are by their nature mutable
        /// - The presence of attached properties means that the semantics of equality are
        ///   difficult to define
        /// 
        /// See https://github.com/UrhoUIUI/UrhoUI/pull/2747 for the discussion that prompted
        /// this.
        /// </remarks>
        public sealed override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Gets a <see cref="UrhoUIProperty"/> value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The value.</returns>
        public object GetValue(UrhoUIProperty property)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));

            return property.RouteGetValue(this);
        }

        /// <summary>
        /// Gets a <see cref="UrhoUIProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The value.</returns>
        public T GetValue<T>(StyledPropertyBase<T> property)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));
            VerifyAccess();

            return GetValueOrInheritedOrDefault(property);
        }

        /// <summary>
        /// Gets a <see cref="UrhoUIProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The value.</returns>
        public T GetValue<T>(DirectPropertyBase<T> property)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));
            VerifyAccess();

            var registered = UrhoUIPropertyRegistry.Instance.GetRegisteredDirect(this, property);
            return registered.InvokeGetter(this);
        }

        /// <inheritdoc/>
        public Optional<T> GetBaseValue<T>(StyledPropertyBase<T> property, BindingPriority maxPriority)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));
            VerifyAccess();

            if (_values is object &&
                _values.TryGetValue(property, maxPriority, out var value))
            {
                return value;
            }

            return default;
        }

        /// <summary>
        /// Checks whether a <see cref="UrhoUIProperty"/> is animating.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>True if the property is animating, otherwise false.</returns>
        public bool IsAnimating(UrhoUIProperty property)
        {
            Contract.Requires<ArgumentNullException>(property != null);
            VerifyAccess();

            return _values?.IsAnimating(property) ?? false;
        }

        /// <summary>
        /// Checks whether a <see cref="UrhoUIProperty"/> is set on this object.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>True if the property is set, otherwise false.</returns>
        /// <remarks>
        /// Checks whether a value is assigned to the property, or that there is a binding to the
        /// property that is producing a value other than <see cref="UrhoUIProperty.UnsetValue"/>.
        /// </remarks>
        public bool IsSet(UrhoUIProperty property)
        {
            Contract.Requires<ArgumentNullException>(property != null);
            VerifyAccess();

            return _values?.IsSet(property) ?? false;
        }

        /// <summary>
        /// Sets a <see cref="UrhoUIProperty"/> value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="priority">The priority of the value.</param>
        public void SetValue(
            UrhoUIProperty property,
            object value,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));

            property.RouteSetValue(this, value, priority);
        }

        /// <summary>
        /// Sets a <see cref="UrhoUIProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="priority">The priority of the value.</param>
        /// <returns>
        /// An <see cref="IDisposable"/> if setting the property can be undone, otherwise null.
        /// </returns>
        public IDisposable SetValue<T>(
            StyledPropertyBase<T> property,
            T value,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));
            VerifyAccess();

            LogPropertySet(property, value, priority);

            if (value is UnsetValueType)
            {
                if (priority == BindingPriority.LocalValue)
                {
                    Values.ClearLocalValue(property);
                }
                else
                {
                    throw new NotSupportedException(
                        "Cannot set property to Unset at non-local value priority.");
                }
            }
            else if (!(value is DoNothingType))
            {
                return Values.SetValue(property, value, priority);
            }

            return null;
        }

        /// <summary>
        /// Sets a <see cref="UrhoUIProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        public void SetValue<T>(DirectPropertyBase<T> property, T value)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));
            VerifyAccess();

            LogPropertySet(property, value, BindingPriority.LocalValue);
            SetDirectValueUnchecked(property, value);
        }

        /// <summary>
        /// Binds a <see cref="UrhoUIProperty"/> to an observable.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="source">The observable.</param>
        /// <param name="priority">The priority of the binding.</param>
        /// <returns>
        /// A disposable which can be used to terminate the binding.
        /// </returns>
        public IDisposable Bind<T>(
            StyledPropertyBase<T> property,
            IObservable<BindingValue<T>> source,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));
            source = source ?? throw new ArgumentNullException(nameof(source));
            VerifyAccess();

            return Values.AddBinding(property, source, priority);
        }

        /// <summary>
        /// Binds a <see cref="UrhoUIProperty"/> to an observable.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="source">The observable.</param>
        /// <returns>
        /// A disposable which can be used to terminate the binding.
        /// </returns>
        public IDisposable Bind<T>(
            DirectPropertyBase<T> property,
            IObservable<BindingValue<T>> source)
        {
            property = property ?? throw new ArgumentNullException(nameof(property));
            source = source ?? throw new ArgumentNullException(nameof(source));
            VerifyAccess();

            property = UrhoUIPropertyRegistry.Instance.GetRegisteredDirect(this, property);

            if (property.IsReadOnly)
            {
                throw new ArgumentException($"The property {property.Name} is readonly.");
            }

            //Logger.TryGet(LogEventLevel.Verbose, LogArea.Property)?.Log(
            //    this,
            //    "Bound {Property} to {Binding} with priority LocalValue",
            //    property,
            //    GetDescription(source));

            _directBindings ??= new List<IDisposable>();

            return new DirectBindingSubscription<T>(this, property, source);
        }

        /// <summary>
        /// Coerces the specified <see cref="UrhoUIProperty"/>.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        public void CoerceValue<T>(StyledPropertyBase<T> property)
        {
            _values?.CoerceValue(property);
        }

        public void BeginBatchUpdate()
        {
            if (_batchUpdate)
            {
                throw new InvalidOperationException("Batch update already in progress.");
            }

            _batchUpdate = true;
            _values?.BeginBatchUpdate();
        }

        public void EndBatchUpdate()
        {
            if (!_batchUpdate)
            {
                throw new InvalidOperationException("No batch update in progress.");
            }

            _batchUpdate = false;
            _values?.EndBatchUpdate();
        }

        /// <inheritdoc/>
        void IUrhoUIObject.AddInheritanceChild(IUrhoUIObject child)
        {
            _inheritanceChildren ??= new List<IUrhoUIObject>();
            _inheritanceChildren.Add(child);
        }

        /// <inheritdoc/>
        void IUrhoUIObject.RemoveInheritanceChild(IUrhoUIObject child)
        {
            _inheritanceChildren?.Remove(child);
        }

        void IUrhoUIObject.InheritedPropertyChanged<T>(
            UrhoUIProperty<T> property,
            Optional<T> oldValue,
            Optional<T> newValue)
        {
            if (property.Inherits && (_values == null || !_values.IsSet(property)))
            {
                RaisePropertyChanged(property, oldValue, newValue, BindingPriority.LocalValue);
            }
        }

        /// <inheritdoc/>
        Delegate[] IUrhoUIObjectDebug.GetPropertyChangedSubscribers()
        {
            return _propertyChanged?.GetInvocationList();
        }

        void IValueSink.ValueChanged<T>(UrhoUIPropertyChangedEventArgs<T> change)
        {
            var property = (StyledPropertyBase<T>)change.Property;

            LogIfError(property, change.NewValue);

            // If the change is to the effective value of the property and no old/new value is set
            // then fill in the old/new value from property inheritance/default value. We don't do
            // this for non-effective value changes because these are only needed for property
            // transitions, where knowing e.g. that an inherited value is active at an arbitrary
            // priority isn't of any use and would introduce overhead.
            if (change.IsEffectiveValueChange && !change.OldValue.HasValue)
            {
                change.SetOldValue(GetInheritedOrDefault<T>(property));
            }

            if (change.IsEffectiveValueChange && !change.NewValue.HasValue)
            {
                change.SetNewValue(GetInheritedOrDefault(property));
            }

            if (!change.IsEffectiveValueChange ||
                !EqualityComparer<T>.Default.Equals(change.OldValue.Value, change.NewValue.Value))
            {
                RaisePropertyChanged(change);

                if (change.IsEffectiveValueChange)
                {
                    //Logger.TryGet(LogEventLevel.Verbose, LogArea.Property)?.Log(
                    //    this,
                    //    "{Property} changed from {$Old} to {$Value} with priority {Priority}",
                    //    property,
                    //    change.OldValue,
                    //    change.NewValue,
                    //    change.Priority);
                }
            }
        }

        void IValueSink.Completed<T>(
            StyledPropertyBase<T> property,
            IPriorityValueEntry entry,
            Optional<T> oldValue)
        {
            var change = new UrhoUIPropertyChangedEventArgs<T>(
                this,
                property,
                oldValue,
                default,
                BindingPriority.Unset);
            ((IValueSink)this).ValueChanged(change);
        }

        /// <summary>
        /// Called for each inherited property when the <see cref="InheritanceParent"/> changes.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="oldParent">The old inheritance parent.</param>
        internal void InheritanceParentChanged<T>(
            StyledPropertyBase<T> property,
            IUrhoUIObject oldParent)
        {
            var oldValue = oldParent switch
            {
                UrhoUIObject o => o.GetValueOrInheritedOrDefault(property),
                null => property.GetDefaultValue(GetType()),
                _ => oldParent.GetValue(property)
            };

            var newValue = GetInheritedOrDefault(property);

            if (!EqualityComparer<T>.Default.Equals(oldValue, newValue))
            {
                RaisePropertyChanged(property, oldValue, newValue);
            }
        }

        internal UrhoUIPropertyValue GetDiagnosticInternal(UrhoUIProperty property)
        {
            if (property.IsDirect)
            {
                return new UrhoUIPropertyValue(
                    property,
                    GetValue(property),
                    BindingPriority.Unset,
                    "Local Value");
            }
            else if (_values != null)
            {
                var result = _values.GetDiagnostic(property);

                if (result != null)
                {
                    return result;
                }
            }

            return new UrhoUIPropertyValue(
                property,
                GetValue(property),
                BindingPriority.Unset,
                "Unset");
        }

        /// <summary>
        /// Logs a binding error for a property.
        /// </summary>
        /// <param name="property">The property that the error occurred on.</param>
        /// <param name="e">The binding error.</param>
        protected internal virtual void LogBindingError(UrhoUIProperty property, Exception e)
        {
            //Logger.TryGet(LogEventLevel.Warning, LogArea.Binding)?.Log(
            //    this,
            //    "Error in binding to {Target}.{Property}: {Message}",
            //    this,
            //    property,
            //    e.Message);
        }

        /// <summary>
        /// Called to update the validation state for properties for which data validation is
        /// enabled.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The new binding value for the property.</param>
        protected virtual void UpdateDataValidation<T>(
            UrhoUIProperty<T> property,
            BindingValue<T> value)
        {
        }

        /// <summary>
        /// Called when a avalonia property changes on the object.
        /// </summary>
        /// <param name="change">The property change details.</param>
        protected virtual void OnPropertyChangedCore<T>(UrhoUIPropertyChangedEventArgs<T> change)
        {
            if (change.IsEffectiveValueChange)
            {
                OnPropertyChanged(change);
            }
        }

        /// <summary>
        /// Called when a avalonia property changes on the object.
        /// </summary>
        /// <param name="change">The property change details.</param>
        protected virtual void OnPropertyChanged<T>(UrhoUIPropertyChangedEventArgs<T> change)
        {
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="property">The property that has changed.</param>
        /// <param name="oldValue">The old property value.</param>
        /// <param name="newValue">The new property value.</param>
        /// <param name="priority">The priority of the binding that produced the value.</param>
        protected internal void RaisePropertyChanged<T>(
            UrhoUIProperty<T> property,
            Optional<T> oldValue,
            BindingValue<T> newValue,
            BindingPriority priority = BindingPriority.LocalValue)
        {
            RaisePropertyChanged(new UrhoUIPropertyChangedEventArgs<T>(
                this,
                property,
                oldValue,
                newValue,
                priority));
        }

        /// <summary>
        /// Sets the backing field for a direct avalonia property, raising the 
        /// <see cref="PropertyChanged"/> event if the value has changed.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="field">The backing field.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// True if the value changed, otherwise false.
        /// </returns>
        protected bool SetAndRaise<T>(UrhoUIProperty<T> property, ref T field, T value)
        {
            VerifyAccess();

            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            var old = field;
            field = value;
            RaisePropertyChanged(property, old, value);
            return true;
        }

        protected bool SetAndRaise<T>(UrhoUIProperty<T> property, T old, T value, Action<T> setter)
        {
            VerifyAccess();

            if (EqualityComparer<T>.Default.Equals(old, value))
            {
                return false;
            }

            setter(value);
            RaisePropertyChanged(property, old, value);
            return true;
        }

        private T GetInheritedOrDefault<T>(StyledPropertyBase<T> property)
        {
            if (property.Inherits && InheritanceParent is UrhoUIObject o)
            {
                return o.GetValueOrInheritedOrDefault(property);
            }

            return property.GetDefaultValue(GetType());
        }

        private T GetValueOrInheritedOrDefault<T>(
            StyledPropertyBase<T> property,
            BindingPriority maxPriority = BindingPriority.Animation)
        {
            var o = this;
            var inherits = property.Inherits;
            var value = default(T);

            while (o != null)
            {
                var values = o._values;

                if (values?.TryGetValue(property, maxPriority, out value) == true)
                {
                    return value;
                }

                if (!inherits)
                {
                    break;
                }

                o = o.InheritanceParent as UrhoUIObject;
            }

            return property.GetDefaultValue(GetType());
        }

        protected internal void RaisePropertyChanged<T>(UrhoUIPropertyChangedEventArgs<T> change)
        {
            VerifyAccess();

            if (change.IsEffectiveValueChange)
            {
                change.Property.Notifying?.Invoke(this, true);
            }

            try
            {
                OnPropertyChangedCore(change);

                if (change.IsEffectiveValueChange)
                {
                    change.Property.NotifyChanged(change);
                    _propertyChanged?.Invoke(this, change);

                    if (_inpcChanged != null)
                    {
                        var inpce = new PropertyChangedEventArgs(change.Property.Name);
                        _inpcChanged(this, inpce);
                    }

                    if (change.Property.Inherits && _inheritanceChildren != null)
                    {
                        foreach (var child in _inheritanceChildren)
                        {
                            child.InheritedPropertyChanged(
                                change.Property,
                                change.OldValue,
                                change.NewValue.ToOptional());
                        }
                    }
                }
            }
            finally
            {
                if (change.IsEffectiveValueChange)
                {
                    change.Property.Notifying?.Invoke(this, false);
                }
            }
        }

        /// <summary>
        /// Sets the value of a direct property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        private void SetDirectValueUnchecked<T>(DirectPropertyBase<T> property, T value)
        {
            var p = UrhoUIPropertyRegistry.Instance.GetRegisteredDirect(this, property);

            if (value is UnsetValueType)
            {
                p.InvokeSetter(this, p.GetUnsetValue(GetType()));
            }
            else if (!(value is DoNothingType))
            {
                p.InvokeSetter(this, value);
            }
        }

        /// <summary>
        /// Sets the value of a direct property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        private void SetDirectValueUnchecked<T>(DirectPropertyBase<T> property, BindingValue<T> value)
        {
            var p = UrhoUIPropertyRegistry.Instance.FindRegisteredDirect(this, property);

            if (p == null)
            {
                throw new ArgumentException($"Property '{property.Name} not registered on '{this.GetType()}");
            }

            LogIfError(property, value);

            switch (value.Type)
            {
                case BindingValueType.UnsetValue:
                case BindingValueType.BindingError:
                    var fallback = value.HasValue ? value : value.WithValue(property.GetUnsetValue(GetType()));
                    property.InvokeSetter(this, fallback);
                    break;
                case BindingValueType.DataValidationError:
                    property.InvokeSetter(this, value);
                    break;
                case BindingValueType.Value:
                case BindingValueType.BindingErrorWithFallback:
                case BindingValueType.DataValidationErrorWithFallback:
                    property.InvokeSetter(this, value);
                    break;
            }

            var metadata = p.GetMetadata(GetType());

            if (metadata.EnableDataValidation == true)
            {
                UpdateDataValidation(property, value);
            }
        }

        /// <summary>
        /// Gets a description of an observable that van be used in logs.
        /// </summary>
        /// <param name="o">The observable.</param>
        /// <returns>The description.</returns>
        private string GetDescription(object o)
        {
            var description = o as IDescription;
            return description?.Description ?? o.ToString();
        }

        /// <summary>
        /// Logs a mesage if the notification represents a binding error.
        /// </summary>
        /// <param name="property">The property being bound.</param>
        /// <param name="value">The binding notification.</param>
        private void LogIfError<T>(UrhoUIProperty property, BindingValue<T> value)
        {
            if (value.HasError)
            {
                if (value.Error is AggregateException aggregate)
                {
                    foreach (var inner in aggregate.InnerExceptions)
                    {
                        LogBindingError(property, inner);
                    }
                }
                else
                {
                    LogBindingError(property, value.Error);
                }
            }
        }

        /// <summary>
        /// Logs a property set message.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The new value.</param>
        /// <param name="priority">The priority.</param>
        private void LogPropertySet<T>(UrhoUIProperty<T> property, T value, BindingPriority priority)
        {
            //Logger.TryGet(LogEventLevel.Verbose, LogArea.Property)?.Log(
            //    this,
            //    "Set {Property} to {$Value} with priority {Priority}",
            //    property,
            //    value,
            //    priority);
        }

        private class DirectBindingSubscription<T> : IObserver<BindingValue<T>>, IDisposable
        {
            private readonly UrhoUIObject _owner;
            private readonly DirectPropertyBase<T> _property;
            private readonly IDisposable _subscription;

            public DirectBindingSubscription(
                UrhoUIObject owner,
                DirectPropertyBase<T> property,
                IObservable<BindingValue<T>> source)
            {
                _owner = owner;
                _property = property;
                _owner._directBindings.Add(this);
                _subscription = source.Subscribe(this);
            }

            public void Dispose()
            {
                _subscription.Dispose();
                _owner._directBindings.Remove(this);
            }

            public void OnCompleted() => Dispose();
            public void OnError(Exception error) => Dispose();
            public void OnNext(BindingValue<T> value)
            {
                if (Dispatcher.UIThread.CheckAccess())
                {
                    _owner.SetDirectValueUnchecked(_property, value);
                }
                else
                {
                    // To avoid allocating closure in the outer scope we need to capture variables
                    // locally. This allows us to skip most of the allocations when on UI thread.
                    var instance = _owner;
                    var property = _property;
                    var newValue = value;

                    Dispatcher.UIThread.Post(() => instance.SetDirectValueUnchecked(property, newValue));
                }
            }
        }
    }
}
