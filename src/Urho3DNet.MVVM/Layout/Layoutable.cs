using System;
using Urho.VisualTree;
using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Utilities;
using Urho3DNet.MVVM.VisualTree;

namespace Urho3DNet.MVVM.Layout
{
    /// <summary>
    /// Implements layout-related functionality for a control.
    /// </summary>
    public abstract class Layoutable : Visual, ILayoutable
    {
        /// <summary>
        /// Defines the <see cref="DesiredSize"/> property.
        /// </summary>
        public static readonly DirectProperty<Layoutable, Size> DesiredSizeProperty =
            UrhoProperty.RegisterDirect<Layoutable, Size>(nameof(DesiredSize), o => o.DesiredSize);

        /// <summary>
        /// Defines the <see cref="Width"/> property.
        /// </summary>
        public static readonly StyledProperty<double> WidthProperty =
            UrhoProperty.Register<Layoutable, double>(nameof(Width), double.NaN);

        /// <summary>
        /// Defines the <see cref="Height"/> property.
        /// </summary>
        public static readonly StyledProperty<double> HeightProperty =
            UrhoProperty.Register<Layoutable, double>(nameof(Height), double.NaN);

        /// <summary>
        /// Defines the <see cref="MinWidth"/> property.
        /// </summary>
        public static readonly StyledProperty<double> MinWidthProperty =
            UrhoProperty.Register<Layoutable, double>(nameof(MinWidth));

        /// <summary>
        /// Defines the <see cref="MaxWidth"/> property.
        /// </summary>
        public static readonly StyledProperty<double> MaxWidthProperty =
            UrhoProperty.Register<Layoutable, double>(nameof(MaxWidth), double.PositiveInfinity);

        /// <summary>
        /// Defines the <see cref="MinHeight"/> property.
        /// </summary>
        public static readonly StyledProperty<double> MinHeightProperty =
            UrhoProperty.Register<Layoutable, double>(nameof(MinHeight));

        /// <summary>
        /// Defines the <see cref="MaxHeight"/> property.
        /// </summary>
        public static readonly StyledProperty<double> MaxHeightProperty =
            UrhoProperty.Register<Layoutable, double>(nameof(MaxHeight), double.PositiveInfinity);

        /// <summary>
        /// Defines the <see cref="Margin"/> property.
        /// </summary>
        public static readonly StyledProperty<Thickness> MarginProperty =
            UrhoProperty.Register<Layoutable, Thickness>(nameof(Margin));

        /// <summary>
        /// Defines the <see cref="HorizontalAlignment"/> property.
        /// </summary>
        public static readonly DirectProperty<Layoutable, HorizontalAlignment> HorizontalAlignmentProperty =
            UrhoProperty.RegisterDirect<Layoutable, HorizontalAlignment>(nameof(HorizontalAlignment), 
                layoutable => layoutable.HorizontalAlignment, (layoutable, alignment) => layoutable.HorizontalAlignment = alignment);

        /// <summary>
        /// Defines the <see cref="VerticalAlignment"/> property.
        /// </summary>
        public static readonly DirectProperty<Layoutable, VerticalAlignment> VerticalAlignmentProperty =
            UrhoProperty.RegisterDirect<Layoutable, VerticalAlignment>(nameof(VerticalAlignment),
                layoutable => layoutable.VerticalAlignment, (layoutable, alignment) => layoutable.VerticalAlignment = alignment);

        /// <summary>
        /// Defines the <see cref="UseLayoutRoundingProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> UseLayoutRoundingProperty =
            UrhoProperty.Register<Layoutable, bool>(nameof(UseLayoutRounding), defaultValue: true, inherits: true);

        private bool _measuring;
        private Size? _previousMeasure;
        private Rect? _previousArrange;
        private EventHandler<EffectiveViewportChangedEventArgs>? _effectiveViewportChanged;
        private EventHandler? _layoutUpdated;

        /// <summary>
        /// Initializes static members of the <see cref="Layoutable"/> class.
        /// </summary>
        static Layoutable()
        {
            AffectsMeasure<Layoutable>(
                IsVisibleProperty,
                WidthProperty,
                HeightProperty,
                MinWidthProperty,
                MaxWidthProperty,
                MinHeightProperty,
                MaxHeightProperty,
                MarginProperty,
                HorizontalAlignmentProperty,
                VerticalAlignmentProperty);
        }

        public Layoutable(UIElement target) : base(target)
        {
        }

        /// <summary>
        /// Occurs when the element's effective viewport changes.
        /// </summary>
        public event EventHandler<EffectiveViewportChangedEventArgs>? EffectiveViewportChanged
        {
            add
            {
                if (_effectiveViewportChanged is null && VisualRoot is ILayoutRoot r)
                {
                    r.LayoutManager.RegisterEffectiveViewportListener(this);
                }

                _effectiveViewportChanged += value;
            }

            remove
            {
                _effectiveViewportChanged -= value;

                if (_effectiveViewportChanged is null && VisualRoot is ILayoutRoot r)
                {
                    r.LayoutManager.UnregisterEffectiveViewportListener(this);
                }
            }
        }

        /// <summary>
        /// Occurs when a layout pass completes for the control.
        /// </summary>
        public event EventHandler? LayoutUpdated
        {
            add
            {
                if (_layoutUpdated is null && VisualRoot is ILayoutRoot r)
                {
                    r.LayoutManager.LayoutUpdated += LayoutManagedLayoutUpdated;
                }

                _layoutUpdated += value;
            }

            remove
            {
                _layoutUpdated -= value;

                if (_layoutUpdated is null && VisualRoot is ILayoutRoot r)
                {
                    r.LayoutManager.LayoutUpdated -= LayoutManagedLayoutUpdated;
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the element.
        /// </summary>
        public double Width
        {
            get { return GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the height of the element.
        /// </summary>
        public double Height
        {
            get { return GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the minimum width of the element.
        /// </summary>
        public double MinWidth
        {
            get { return GetValue(MinWidthProperty); }
            set { SetValue(MinWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the maximum width of the element.
        /// </summary>
        public double MaxWidth
        {
            get { return GetValue(MaxWidthProperty); }
            set { SetValue(MaxWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the minimum height of the element.
        /// </summary>
        public double MinHeight
        {
            get { return GetValue(MinHeightProperty); }
            set { SetValue(MinHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the maximum height of the element.
        /// </summary>
        public double MaxHeight
        {
            get { return GetValue(MaxHeightProperty); }
            set { SetValue(MaxHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the margin around the element.
        /// </summary>
        public Thickness Margin
        {
            get { return GetValue(MarginProperty); }
            set { SetValue(MarginProperty, value); }
        }

        /// <summary>
        /// Gets or sets the element's preferred horizontal alignment in its parent.
        /// </summary>
        public abstract HorizontalAlignment HorizontalAlignment
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the element's preferred vertical alignment in its parent.
        /// </summary>
        public abstract VerticalAlignment VerticalAlignment
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the size that this element computed during the measure pass of the layout process.
        /// </summary>
        public Size DesiredSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the control's layout measure is valid.
        /// </summary>
        public bool IsMeasureValid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the control's layouts arrange is valid.
        /// </summary>
        public bool IsArrangeValid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value that determines whether the element should be snapped to pixel
        /// boundaries at layout time.
        /// </summary>
        public bool UseLayoutRounding
        {
            get { return GetValue(UseLayoutRoundingProperty); }
            set { SetValue(UseLayoutRoundingProperty, value); }
        }

        /// <summary>
        /// Gets the available size passed in the previous layout pass, if any.
        /// </summary>
        Size? ILayoutable.PreviousMeasure => _previousMeasure;

        /// <summary>
        /// Gets the layout rect passed in the previous layout pass, if any.
        /// </summary>
        Rect? ILayoutable.PreviousArrange => _previousArrange;

        /// <summary>
        /// Creates the visual children of the control, if necessary
        /// </summary>
        public virtual void ApplyTemplate()
        {
        }

        /// <summary>
        /// Carries out a measure of the control.
        /// </summary>
        /// <param name="availableSize">The available size for the control.</param>
        public void Measure(Size availableSize)
        {
            if (double.IsNaN(availableSize.Width) || double.IsNaN(availableSize.Height))
            {
                throw new InvalidOperationException("Cannot call Measure using a size with NaN values.");
            }

            if (!IsMeasureValid || _previousMeasure != availableSize)
            {
                var previousDesiredSize = DesiredSize;
                var desiredSize = default(Size);

                IsMeasureValid = true;

                try
                {
                    _measuring = true;
                    desiredSize = MeasureCore(availableSize);
                }
                finally
                {
                    _measuring = false;
                }

                if (IsInvalidSize(desiredSize))
                {
                    throw new InvalidOperationException("Invalid size returned for Measure.");
                }

                DesiredSize = desiredSize;
                _previousMeasure = availableSize;

                //Logger.TryGet(LogEventLevel.Verbose, LogArea.Layout)?.Log(this, "Measure requested {DesiredSize}", DesiredSize);

                if (DesiredSize != previousDesiredSize)
                {
                    this.GetVisualParent<ILayoutable>()?.ChildDesiredSizeChanged(this);
                }
            }
        }

        /// <summary>
        /// Arranges the control and its children.
        /// </summary>
        /// <param name="rect">The control's new bounds.</param>
        public void Arrange(Rect rect)
        {
            if (IsInvalidRect(rect))
            {
                throw new InvalidOperationException("Invalid Arrange rectangle.");
            }

            if (!IsMeasureValid)
            {
                Measure(_previousMeasure ?? rect.Size);
            }

            if (!IsArrangeValid || _previousArrange != rect)
            {
                //Logger.TryGet(LogEventLevel.Verbose, LogArea.Layout)?.Log(this, "Arrange to {Rect} ", rect);

                IsArrangeValid = true;
                ArrangeCore(rect);
                _previousArrange = rect;
            }
        }

        /// <summary>
        /// Invalidates the measurement of the control and queues a new layout pass.
        /// </summary>
        public void InvalidateMeasure()
        {
            if (IsMeasureValid)
            {
                //Logger.TryGet(LogEventLevel.Verbose, LogArea.Layout)?.Log(this, "Invalidated measure");

                IsMeasureValid = false;
                IsArrangeValid = false;

                if (((ILayoutable)this).IsAttachedToVisualTree)
                {
                    (VisualRoot as ILayoutRoot)?.LayoutManager.InvalidateMeasure(this);
                    InvalidateVisual();
                }
                OnMeasureInvalidated();
            }
        }

        /// <summary>
        /// Invalidates the visual and queues a repaint.
        /// </summary>
        public void InvalidateVisual()
        {
            //VisualRoot?.Renderer?.AddDirty(this);
        }

        /// <summary>
        /// Invalidates the arrangement of the control and queues a new layout pass.
        /// </summary>
        public void InvalidateArrange()
        {
            if (IsArrangeValid)
            {
                //Logger.TryGet(LogEventLevel.Verbose, LogArea.Layout)?.Log(this, "Invalidated arrange");

                IsArrangeValid = false;
                (VisualRoot as ILayoutRoot)?.LayoutManager?.InvalidateArrange(this);
                InvalidateVisual();
            }
        }

        /// <inheritdoc/>
        void ILayoutable.ChildDesiredSizeChanged(ILayoutable control)
        {
            if (!_measuring)
            {
                InvalidateMeasure();
            }
        }

        void ILayoutable.EffectiveViewportChanged(EffectiveViewportChangedEventArgs e)
        {
            _effectiveViewportChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Marks a property as affecting the control's measurement.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <remarks>
        /// After a call to this method in a control's static constructor, any change to the
        /// property will cause <see cref="InvalidateMeasure"/> to be called on the element.
        /// </remarks>
        [Obsolete("Use AffectsMeasure<T> and specify the control type.")]
        protected static void AffectsMeasure(params UrhoProperty[] properties)
        {
            AffectsMeasure<Layoutable>(properties);
        }

        /// <summary>
        /// Marks a property as affecting the control's measurement.
        /// </summary>
        /// <typeparam name="T">The control which the property affects.</typeparam>
        /// <param name="properties">The properties.</param>
        /// <remarks>
        /// After a call to this method in a control's static constructor, any change to the
        /// property will cause <see cref="InvalidateMeasure"/> to be called on the element.
        /// </remarks>
        protected static void AffectsMeasure<T>(params UrhoProperty[] properties)
            where T : class, ILayoutable
        {
            void Invalidate(UrhoPropertyChangedEventArgs e)
            {
                (e.Sender as T)?.InvalidateMeasure();
            }

            foreach (var property in properties)
            {
                property.Changed.Subscribe(Invalidate);
            }
        }

        /// <summary>
        /// Marks a property as affecting the control's arrangement.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <remarks>
        /// After a call to this method in a control's static constructor, any change to the
        /// property will cause <see cref="InvalidateArrange"/> to be called on the element.
        /// </remarks>
        [Obsolete("Use AffectsArrange<T> and specify the control type.")]
        protected static void AffectsArrange(params UrhoProperty[] properties)
        {
            AffectsArrange<Layoutable>(properties);
        }

        /// <summary>
        /// Marks a property as affecting the control's arrangement.
        /// </summary>
        /// <typeparam name="T">The control which the property affects.</typeparam>
        /// <param name="properties">The properties.</param>
        /// <remarks>
        /// After a call to this method in a control's static constructor, any change to the
        /// property will cause <see cref="InvalidateArrange"/> to be called on the element.
        /// </remarks>
        protected static void AffectsArrange<T>(params UrhoProperty[] properties)
            where T : class, ILayoutable
        {
            void Invalidate(UrhoPropertyChangedEventArgs e)
            {
                (e.Sender as T)?.InvalidateArrange();
            }

            foreach (var property in properties)
            {
                property.Changed.Subscribe(Invalidate);
            }
        }

        /// <summary>
        /// The default implementation of the control's measure pass.
        /// </summary>
        /// <param name="availableSize">The size available to the control.</param>
        /// <returns>The desired size for the control.</returns>
        /// <remarks>
        /// This method calls <see cref="MeasureOverride(Size)"/> which is probably the method you
        /// want to override in order to modify a control's arrangement.
        /// </remarks>
        protected virtual Size MeasureCore(Size availableSize)
        {
            if (IsVisible)
            {
                var margin = Margin;

                ApplyStyling();
                ApplyTemplate();

                var constrained = LayoutHelper.ApplyLayoutConstraints(
                    this,
                    availableSize.Deflate(margin));
                var measured = MeasureOverride(constrained);

                var width = measured.Width;
                var height = measured.Height;

                {
                    double widthCache = Width;

                    if (!double.IsNaN(widthCache))
                    {
                        width = widthCache;
                    }
                }

                width = Math.Min(width, MaxWidth);
                width = Math.Max(width, MinWidth);

                {
                    double heightCache = Height;

                    if (!double.IsNaN(heightCache))
                    {
                        height = heightCache;
                    }
                }

                height = Math.Min(height, MaxHeight);
                height = Math.Max(height, MinHeight);

                width = Math.Min(width, availableSize.Width);
                height = Math.Min(height, availableSize.Height);

                if (UseLayoutRounding)
                {
                    var scale = LayoutHelper.GetLayoutScale(this);
                    width = LayoutHelper.RoundLayoutValue(width, scale);
                    height = LayoutHelper.RoundLayoutValue(height, scale);
                }

                return NonNegative(new Size(width, height).Inflate(margin));
            }
            else
            {
                return new Size();
            }
        }

        /// <summary>
        /// Measures the control and its child elements as part of a layout pass.
        /// </summary>
        /// <param name="availableSize">The size available to the control.</param>
        /// <returns>The desired size for the control.</returns>
        protected virtual Size MeasureOverride(Size availableSize)
        {
            double width = 0;
            double height = 0;

            var visualChildren = VisualChildren;
            var visualCount = visualChildren.Count;

            for (var i = 0; i < visualCount; i++)
            {
                IVisual visual = visualChildren[i];

                if (visual is ILayoutable layoutable)
                {
                    layoutable.Measure(availableSize);
                    width = Math.Max(width, layoutable.DesiredSize.Width);
                    height = Math.Max(height, layoutable.DesiredSize.Height);
                }
            }

            return new Size(width, height);
        }

        /// <summary>
        /// The default implementation of the control's arrange pass.
        /// </summary>
        /// <param name="finalRect">The control's new bounds.</param>
        /// <remarks>
        /// This method calls <see cref="ArrangeOverride(Size)"/> which is probably the method you
        /// want to override in order to modify a control's arrangement.
        /// </remarks>
        protected virtual void ArrangeCore(Rect finalRect)
        {
            if (IsVisible)
            {
                var margin = Margin;
                var originX = finalRect.X + margin.Left;
                var originY = finalRect.Y + margin.Top;
                var availableSizeMinusMargins = new Size(
                    Math.Max(0, finalRect.Width - margin.Left - margin.Right),
                    Math.Max(0, finalRect.Height - margin.Top - margin.Bottom));
                var horizontalAlignment = HorizontalAlignment;
                var verticalAlignment = VerticalAlignment;
                var size = availableSizeMinusMargins;
                var scale = LayoutHelper.GetLayoutScale(this);
                var useLayoutRounding = UseLayoutRounding;

                if (horizontalAlignment != HorizontalAlignment.Stretch)
                {
                    size = size.WithWidth(Math.Min(size.Width, DesiredSize.Width - margin.Left - margin.Right));
                }

                if (verticalAlignment != VerticalAlignment.Stretch)
                {
                    size = size.WithHeight(Math.Min(size.Height, DesiredSize.Height - margin.Top - margin.Bottom));
                }

                size = LayoutHelper.ApplyLayoutConstraints(this, size);

                if (useLayoutRounding)
                {
                    size = LayoutHelper.RoundLayoutSize(size, scale, scale);
                    availableSizeMinusMargins = LayoutHelper.RoundLayoutSize(availableSizeMinusMargins, scale, scale);
                }

                size = ArrangeOverride(size).Constrain(size);

                switch (horizontalAlignment)
                {
                    case HorizontalAlignment.Center:
                    case HorizontalAlignment.Stretch:
                        originX += (availableSizeMinusMargins.Width - size.Width) / 2;
                        break;
                    case HorizontalAlignment.Right:
                        originX += availableSizeMinusMargins.Width - size.Width;
                        break;
                }

                switch (verticalAlignment)
                {
                    case VerticalAlignment.Center:
                    case VerticalAlignment.Stretch:
                        originY += (availableSizeMinusMargins.Height - size.Height) / 2;
                        break;
                    case VerticalAlignment.Bottom:
                        originY += availableSizeMinusMargins.Height - size.Height;
                        break;
                }

                if (useLayoutRounding)
                {
                    originX = LayoutHelper.RoundLayoutValue(originX, scale);
                    originY = LayoutHelper.RoundLayoutValue(originY, scale);
                }

                Bounds = new Rect(originX, originY, size.Width, size.Height);
            }
        }

        /// <summary>
        /// Positions child elements as part of a layout pass.
        /// </summary>
        /// <param name="finalSize">The size available to the control.</param>
        /// <returns>The actual size used.</returns>
        protected virtual Size ArrangeOverride(Size finalSize)
        {
            var arrangeRect = new Rect(finalSize);

            var visualChildren = VisualChildren;
            var visualCount = visualChildren.Count;

            for (var i = 0; i < visualCount; i++)
            {
                IVisual visual = visualChildren[i];

                if (visual is ILayoutable layoutable)
                {
                    layoutable.Arrange(arrangeRect);
                }
            }

            return finalSize;
        }

        protected sealed override void InvalidateStyles()
        {
            base.InvalidateStyles();
            InvalidateMeasure();
        }

        protected override void OnAttachedToVisualTreeCore(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTreeCore(e);

            if (e.Root is ILayoutRoot r)
            {
                if (_layoutUpdated is object)
                {
                    r.LayoutManager.LayoutUpdated += LayoutManagedLayoutUpdated;
                }

                if (_effectiveViewportChanged is object)
                {
                    r.LayoutManager.RegisterEffectiveViewportListener(this);
                }
            }
        }

        protected override void OnDetachedFromVisualTreeCore(VisualTreeAttachmentEventArgs e)
        {
            if (e.Root is ILayoutRoot r)
            {
                if (_layoutUpdated is object)
                {
                    r.LayoutManager.LayoutUpdated -= LayoutManagedLayoutUpdated;
                }

                if (_effectiveViewportChanged is object)
                {
                    r.LayoutManager.UnregisterEffectiveViewportListener(this);
                }
            }

            base.OnDetachedFromVisualTreeCore(e);
        }

        /// <summary>
        /// Called by InvalidateMeasure
        /// </summary>
        protected virtual void OnMeasureInvalidated()
        {
        }

        /// <inheritdoc/>
        protected sealed override void OnVisualParentChanged(IVisual oldParent, IVisual newParent)
        {
            LayoutHelper.InvalidateSelfAndChildrenMeasure(this);

            base.OnVisualParentChanged(oldParent, newParent);
        }

        /// <summary>
        /// Called when the layout manager raises a LayoutUpdated event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void LayoutManagedLayoutUpdated(object sender, EventArgs e) => _layoutUpdated?.Invoke(this, e);

        /// <summary>
        /// Tests whether any of a <see cref="Rect"/>'s properties include negative values,
        /// a NaN or Infinity.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>True if the rect is invalid; otherwise false.</returns>
        private static bool IsInvalidRect(Rect rect)
        {
            return rect.Width < 0 || rect.Height < 0 ||
                double.IsInfinity(rect.X) || double.IsInfinity(rect.Y) ||
                double.IsInfinity(rect.Width) || double.IsInfinity(rect.Height) ||
                double.IsNaN(rect.X) || double.IsNaN(rect.Y) ||
                double.IsNaN(rect.Width) || double.IsNaN(rect.Height);
        }

        /// <summary>
        /// Tests whether any of a <see cref="Size"/>'s properties include negative values,
        /// a NaN or Infinity.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>True if the size is invalid; otherwise false.</returns>
        private static bool IsInvalidSize(Size size)
        {
            return size.Width < 0 || size.Height < 0 ||
                double.IsInfinity(size.Width) || double.IsInfinity(size.Height) ||
                double.IsNaN(size.Width) || double.IsNaN(size.Height);
        }

        /// <summary>
        /// Ensures neither component of a <see cref="Size"/> is negative.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>The non-negative size.</returns>
        private static Size NonNegative(Size size)
        {
            return new Size(Math.Max(size.Width, 0), Math.Max(size.Height, 0));
        }
    }
    /// <summary>
    /// Provides helper methods needed for layout.
    /// </summary>
    public static class LayoutHelper
    {
        /// <summary>
        /// Epsilon value used for certain layout calculations.
        /// Based on the value in WPF LayoutDoubleUtil.
        /// </summary>
        public static double LayoutEpsilon { get; } = 0.00000153;

        /// <summary>
        /// Calculates a control's size based on its <see cref="ILayoutable.Width"/>,
        /// <see cref="ILayoutable.Height"/>, <see cref="ILayoutable.MinWidth"/>,
        /// <see cref="ILayoutable.MaxWidth"/>, <see cref="ILayoutable.MinHeight"/> and
        /// <see cref="ILayoutable.MaxHeight"/>.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="constraints">The space available for the control.</param>
        /// <returns>The control's size.</returns>
        public static Size ApplyLayoutConstraints(ILayoutable control, Size constraints)
        {
            var minmax = new MinMax(control);

            return new Size(
                MathUtilities.Clamp(constraints.Width, minmax.MinWidth, minmax.MaxWidth),
                MathUtilities.Clamp(constraints.Height, minmax.MinHeight, minmax.MaxHeight));
        }

        public static Size MeasureChild(ILayoutable control, Size availableSize, Thickness padding,
            Thickness borderThickness)
        {
            return MeasureChild(control, availableSize, padding + borderThickness);
        }

        public static Size MeasureChild(ILayoutable control, Size availableSize, Thickness padding)
        {
            if (control != null)
            {
                control.Measure(availableSize.Deflate(padding));
                return control.DesiredSize.Inflate(padding);
            }

            return new Size(padding.Left + padding.Right, padding.Bottom + padding.Top);
        }

        public static Size ArrangeChild(ILayoutable child, Size availableSize, Thickness padding, Thickness borderThickness)
        {
            return ArrangeChild(child, availableSize, padding + borderThickness);
        }

        public static Size ArrangeChild(ILayoutable child, Size availableSize, Thickness padding)
        {
            child?.Arrange(new Rect(availableSize).Deflate(padding));

            return availableSize;
        }

        /// <summary>
        /// Invalidates measure for given control and all visual children recursively.
        /// </summary>
        public static void InvalidateSelfAndChildrenMeasure(ILayoutable control)
        {
            void InnerInvalidateMeasure(IVisual target)
            {
                if (target is ILayoutable targetLayoutable)
                {
                    targetLayoutable.InvalidateMeasure();
                }

                var visualChildren = target.VisualChildren;
                var visualChildrenCount = visualChildren.Count;

                for (int i = 0; i < visualChildrenCount; i++)
                {
                    IVisual child = visualChildren[i];

                    InnerInvalidateMeasure(child);
                }
            }

            InnerInvalidateMeasure(control);
        }

        /// <summary>
        /// Obtains layout scale of the given control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <exception cref="Exception">Thrown when control has no root or returned layout scaling is invalid.</exception>
        public static double GetLayoutScale(ILayoutable control)
        {
            var visualRoot = control.VisualRoot;

            var result = (visualRoot as ILayoutRoot)?.LayoutScaling ?? 1.0;

            if (result == 0 || double.IsNaN(result) || double.IsInfinity(result))
            {
                throw new Exception($"Invalid LayoutScaling returned from {visualRoot.GetType()}");
            }

            return result;
        }

        /// <summary>
        /// Rounds a size to integer values for layout purposes, compensating for high DPI screen
        /// coordinates.
        /// </summary>
        /// <param name="size">Input size.</param>
        /// <param name="dpiScaleX">DPI along x-dimension.</param>
        /// <param name="dpiScaleY">DPI along y-dimension.</param>
        /// <returns>Value of size that will be rounded under screen DPI.</returns>
        /// <remarks>
        /// This is a layout helper method. It takes DPI into account and also does not return
        /// the rounded value if it is unacceptable for layout, e.g. Infinity or NaN. It's a helper
        /// associated with the UseLayoutRounding property and should not be used as a general rounding
        /// utility.
        /// </remarks>
        public static Size RoundLayoutSize(Size size, double dpiScaleX, double dpiScaleY)
        {
            return new Size(RoundLayoutValue(size.Width, dpiScaleX), RoundLayoutValue(size.Height, dpiScaleY));
        }

        /// <summary>
        /// Calculates the value to be used for layout rounding at high DPI.
        /// </summary>
        /// <param name="value">Input value to be rounded.</param>
        /// <param name="dpiScale">Ratio of screen's DPI to layout DPI</param>
        /// <returns>Adjusted value that will produce layout rounding on screen at high dpi.</returns>
        /// <remarks>
        /// This is a layout helper method. It takes DPI into account and also does not return
        /// the rounded value if it is unacceptable for layout, e.g. Infinity or NaN. It's a helper
        /// associated with the UseLayoutRounding property and should not be used as a general rounding
        /// utility.
        /// </remarks>
        public static double RoundLayoutValue(double value, double dpiScale)
        {
            double newValue;

            // If DPI == 1, don't use DPI-aware rounding.
            if (!MathUtilities.IsOne(dpiScale))
            {
                newValue = Math.Round(value * dpiScale) / dpiScale;

                // If rounding produces a value unacceptable to layout (NaN, Infinity or MaxValue),
                // use the original value.
                if (double.IsNaN(newValue) ||
                    double.IsInfinity(newValue) ||
                    MathUtilities.AreClose(newValue, double.MaxValue))
                {
                    newValue = value;
                }
            }
            else
            {
                newValue = Math.Round(value);
            }

            return newValue;
        }


        /// <summary>
        /// Calculates the min and max height for a control. Ported from WPF.
        /// </summary>
        private readonly struct MinMax
        {
            public MinMax(ILayoutable e)
            {
                MaxHeight = e.MaxHeight;
                MinHeight = e.MinHeight;
                double l = e.Height;

                double height = (double.IsNaN(l) ? double.PositiveInfinity : l);
                MaxHeight = Math.Max(Math.Min(height, MaxHeight), MinHeight);

                height = (double.IsNaN(l) ? 0 : l);
                MinHeight = Math.Max(Math.Min(MaxHeight, height), MinHeight);

                MaxWidth = e.MaxWidth;
                MinWidth = e.MinWidth;
                l = e.Width;

                double width = (double.IsNaN(l) ? double.PositiveInfinity : l);
                MaxWidth = Math.Max(Math.Min(width, MaxWidth), MinWidth);

                width = (double.IsNaN(l) ? 0 : l);
                MinWidth = Math.Max(Math.Min(MaxWidth, width), MinWidth);
            }

            public double MinWidth { get; }
            public double MaxWidth { get; }
            public double MinHeight { get; }
            public double MaxHeight { get; }
        }
    }
}