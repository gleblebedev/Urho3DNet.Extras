using System;
using System.Reactive;
using System.Reactive.Linq;
using Urho.VisualTree;
using Urho3DNet.MVVM.Data.Core;
using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Controls;
using Urho3DNet.MVVM.Data.Converters;
using Urho3DNet.MVVM.LogicalTree;
using Urho3DNet.MVVM.Reactive;
using Urho3DNet.MVVM.VisualTree;


namespace Urho3DNet.MVVM.Data
{
    public abstract class BindingBase : IBinding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Binding"/> class.
        /// </summary>
        public BindingBase()
        {
            FallbackValue = UrhoProperty.UnsetValue;
            TargetNullValue = UrhoProperty.UnsetValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Binding"/> class.
        /// </summary>
        /// <param name="mode">The binding mode.</param>
        public BindingBase(BindingMode mode = BindingMode.Default)
            :this()
        {
            Mode = mode;
        }

        /// <summary>
        /// Gets or sets the <see cref="IValueConverter"/> to use.
        /// </summary>
        public IValueConverter? Converter { get; set; }

        /// <summary>
        /// Gets or sets a parameter to pass to <see cref="Converter"/>.
        /// </summary>
        public object? ConverterParameter { get; set; }

        /// <summary>
        /// Gets or sets the value to use when the binding is unable to produce a value.
        /// </summary>
        public object? FallbackValue { get; set; }

        /// <summary>
        /// Gets or sets the value to use when the binding result is null.
        /// </summary>
        public object? TargetNullValue { get; set; }

        /// <summary>
        /// Gets or sets the binding mode.
        /// </summary>
        public BindingMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the binding priority.
        /// </summary>
        public BindingPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the string format.
        /// </summary>
        public string? StringFormat { get; set; }

        public WeakReference? DefaultAnchor { get; set; }

        public WeakReference<INameScope>? NameScope { get; set; }

        protected abstract ExpressionObserver CreateExpressionObserver(
            IUrhoObject target,
            UrhoProperty targetProperty,
            object? anchor,
            bool enableDataValidation);

        /// <inheritdoc/>
        public InstancedBinding Initiate(
            IUrhoObject target,
            UrhoProperty targetProperty,
            object? anchor = null,
            bool enableDataValidation = false)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));

            anchor = anchor ?? DefaultAnchor?.Target;

            enableDataValidation = enableDataValidation && Priority == BindingPriority.LocalValue;

            var observer = CreateExpressionObserver(target, targetProperty, anchor, enableDataValidation);

            var fallback = FallbackValue;

            // If we're binding to DataContext and our fallback is UnsetValue then override
            // the fallback value to null, as broken bindings to DataContext must reset the
            // DataContext in order to not propagate incorrect DataContexts to child controls.
            // See Urho.Markup.UnitTests.Data.DataContext_Binding_Should_Produce_Correct_Results.
            if (targetProperty == StyledElement.DataContextProperty && fallback == UrhoProperty.UnsetValue)
            {
                fallback = null;
            }

            var converter = Converter;
            var targetType = targetProperty?.PropertyType ?? typeof(object);

            // We only respect `StringFormat` if the type of the property we're assigning to will
            // accept a string. Note that this is slightly different to WPF in that WPF only applies
            // `StringFormat` for target type `string` (not `object`).
            if (!string.IsNullOrWhiteSpace(StringFormat) &&
                (targetType == typeof(string) || targetType == typeof(object)))
            {
                converter = new StringFormatValueConverter(StringFormat, converter);
            }

            var subject = new BindingExpression(
                observer,
                targetType,
                fallback,
                TargetNullValue,
                converter ?? DefaultValueConverter.Instance,
                ConverterParameter,
                Priority);

            return new InstancedBinding(subject, Mode, Priority);
        }

        protected ExpressionObserver CreateDataContextObserver(
            IUrhoObject target,
            ExpressionNode node,
            bool targetIsDataContext,
            object? anchor)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));

            if (!(target is IDataContextProvider))
            {
                target = anchor as IDataContextProvider ?? throw new InvalidOperationException("Cannot find a DataContext to bind to.");
            }

            if (!targetIsDataContext)
            {
                var result = new ExpressionObserver(
                    () => target.GetValue(StyledElement.DataContextProperty),
                    node,
                    new UpdateSignal(target, StyledElement.DataContextProperty),
                    null);

                return result;
            }
            else
            {
                return new ExpressionObserver(
                    GetParentDataContext(target),
                    node,
                    null);
            }
        }

        protected ExpressionObserver CreateElementObserver(
            IStyledElement target,
            string elementName,
            ExpressionNode node)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));

            if (NameScope is null || !NameScope.TryGetTarget(out var scope) || scope is null)
                throw new InvalidOperationException("Name scope is null or was already collected");
            var result = new ExpressionObserver(
                NameScopeLocator.Track(scope, elementName),
                node,
                null);
            return result;
        }

        protected ExpressionObserver CreateFindAncestorObserver(
            IStyledElement target,
            RelativeSource relativeSource,
            ExpressionNode node)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));

            IObservable<object?> controlLocator;

            switch (relativeSource.Tree)
            {
                case TreeType.Logical:
                    controlLocator = ControlLocator.Track(
                        (ILogical)target,
                        relativeSource.AncestorLevel - 1,
                        relativeSource.AncestorType);
                    break;
                case TreeType.Visual:
                    controlLocator = VisualLocator.Track(
                        (IVisual)target,
                        relativeSource.AncestorLevel - 1,
                        relativeSource.AncestorType);
                    break;
                default:
                    throw new InvalidOperationException("Invalid tree to traverse.");
            }

            return new ExpressionObserver(
                controlLocator,
                node,
                null);
        }

        protected ExpressionObserver CreateSourceObserver(
            object source,
            ExpressionNode node)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));

            return new ExpressionObserver(source, node);
        }

        protected ExpressionObserver CreateTemplatedParentObserver(
            IUrhoObject target,
            ExpressionNode node)
        {
            _ = target ?? throw new ArgumentNullException(nameof(target));

            var result = new ExpressionObserver(
                () => target.GetValue(StyledElement.TemplatedParentProperty),
                node,
                new UpdateSignal(target, StyledElement.TemplatedParentProperty),
                null);

            return result;
        }

        protected IObservable<object?> GetParentDataContext(IUrhoObject target)
        {
            // The DataContext is based on the visual parent and not the logical parent: this may
            // seem counter intuitive considering the fact that property inheritance works on the logical
            // tree, but consider a ContentControl with a ContentPresenter. The ContentControl's
            // Content property is bound to a value which becomes the ContentPresenter's 
            // DataContext - it is from this that the child hosted by the ContentPresenter needs to
            // inherit its DataContext.
            return target.GetObservable(Visual.VisualParentProperty)
                .Select(x =>
                {
                    return (x as IUrhoObject)?.GetObservable(StyledElement.DataContextProperty) ??
                           Observable.Return((object?)null);
                }).Switch();
        }

        private class UpdateSignal : SingleSubscriberObservableBase<Unit>
        {
            private readonly IUrhoObject _target;
            private readonly UrhoProperty _property;

            public UpdateSignal(IUrhoObject target, UrhoProperty property)
            {
                _target = target;
                _property = property;
            }

            protected override void Subscribed()
            {
                _target.PropertyChanged += PropertyChanged;
            }

            protected override void Unsubscribed()
            {
                _target.PropertyChanged -= PropertyChanged;
            }

            private void PropertyChanged(object sender, UrhoPropertyChangedEventArgs e)
            {
                if (e.Property == _property)
                {
                    PublishNext(Unit.Default);
                }
            }
        }
    }
}
