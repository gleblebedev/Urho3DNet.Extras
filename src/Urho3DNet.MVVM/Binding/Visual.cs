using System;
using Urho.VisualTree;
using Urho3DNet.MVVM.Collections;
using Urho3DNet.MVVM.Data;
using Urho3DNet.MVVM.Rendering;
using Urho3DNet.MVVM.VisualTree;

namespace Urho3DNet.MVVM.Binding
{
    /// <summary>
    /// Base class for controls that provides rendering and related visual properties.
    /// </summary>
    /// <remarks>
    /// The <see cref="Visual"/> class represents elements that have a visual on-screen
    /// representation and stores all the information needed for an 
    /// <see cref="IRenderer"/> to render the control. To traverse the visual tree, use the
    /// extension methods defined in <see cref="VisualExtensions"/>.
    /// </remarks>
    //[UsableDuringInitialization]
    public abstract class Visual : StyledElement, IVisual
    {
        private IVisual _visualParent;
        private IRenderRoot _visualRoot;
        private Rect _bounds;

        /// <summary>
        /// Defines the <see cref="Bounds"/> property.
        /// </summary>
        public static readonly DirectProperty<Visual, Rect> BoundsProperty =
            UrhoProperty.RegisterDirect<Visual, Rect>(nameof(Bounds), o => o.Bounds);

        public static readonly DirectProperty<Visual, bool> IsVisibleProperty =
            UrhoProperty.RegisterDirect<Visual, bool>(
                nameof(IsVisible),
                o => o.IsVisible,
                (o, v) => o.IsVisible = v);

        /// <summary>
        /// Defines the <see cref="IVisual.VisualParent"/> property.
        /// </summary>
        public static readonly DirectProperty<Visual, IVisual> VisualParentProperty =
            UrhoProperty.RegisterDirect<Visual, IVisual>("VisualParent", o => o._visualParent);

        public Visual(UIElement target) : base(target)
        {
            _target = target;
            var visualChildren = new UrhoList<IVisual>();
            visualChildren.ResetBehavior = ResetBehavior.Remove;
            //visualChildren.Validate = visual => ValidateVisualChild(visual);
            //visualChildren.CollectionChanged += VisualChildrenChanged;
            VisualChildren = visualChildren;

            foreach (var child in target.GetChildren())
            {
                var uiElementView = CreateView(child);
                VisualChildren.Add(uiElementView);
                LogicalChildren.Add(uiElementView);
            }
        }

        private readonly UIElement _target;

        protected override void SubscribeToEvents(Object target)
        {
            target.SubscribeToEvent(E.ElementAdded, HandleElementAdded);
            target.SubscribeToEvent(E.ElementRemoved, HandleElementRemoved);
            base.SubscribeToEvents(target);
        }

        private void HandleElementRemoved(VariantMap obj)
        {
            var parent = obj[E.ElementAdded.Parent].Ptr as UIElement;
            if (parent == Target)
            {
                var element = obj[E.ElementAdded.Element].Ptr as UIElement;
                for (var index = 0; index < VisualChildren.Count; index++)
                {
                    var visualChild = (UIElementView) VisualChildren[index];
                    if (visualChild.Target == element)
                    {
                        VisualChildren.RemoveAt(index);
                        visualChild.SetVisualParent(null);
                        break;
                    }
                }
            }
        }

        private void HandleElementAdded(VariantMap obj)
        {
            var parent = obj[E.ElementAdded.Parent].Ptr as UIElement;
            if (parent == Target)
            {
                var element = obj[E.ElementAdded.Element].Ptr as UIElement;

                var view = CreateView(element);
                VisualChildren.Add(view);
                view.SetVisualParent(this);

            }
        }

        /// <summary>
        /// Sets the visual parent of the Visual.
        /// </summary>
        /// <param name="value">The visual parent.</param>
        private void SetVisualParent(Visual value)
        {
            if (_visualParent == value)
            {
                return;
            }

            var old = _visualParent;
            _visualParent = value;

            if (_visualRoot != null)
            {
                var e = new VisualTreeAttachmentEventArgs(old, VisualRoot);
                OnDetachedFromVisualTreeCore(e);
            }

            if (_visualParent is IRenderRoot || _visualParent?.IsAttachedToVisualTree == true)
            {
                var root = this.FindAncestorOfType<IRenderRoot>();
                var e = new VisualTreeAttachmentEventArgs(_visualParent, root);
                OnAttachedToVisualTreeCore(e);
            }

            OnVisualParentChanged(old, value);
        }

        protected override void UnsubscribeFromEvents(Object target)
        {
            target.UnsubscribeFromEvent(E.ElementAdded);
            target.UnsubscribeFromEvent(E.ElementRemoved);
            base.UnsubscribeFromEvents(target);
        }

        private static UIElementView CreateView(UIElement child)
        {
            var stringHash = child.GetTypeHash();
            switch (stringHash.Hash)
            {
                case 0x1F0A61E1:
                    return new SliderView((Slider) child);
                case 0xC90F98CF:
                    return new BorderImageView((BorderImage)child);
                case 0x111D3672:
                    return new ButtonView((Button)child);
                case 0x79B5AB48:
                    return new UIElementView((UIElement)child);
                default:
                    throw new NotImplementedException("Unknown type: "+child.GetTypeName()+" (hash "+ stringHash + ")");
            }
        }

        public abstract bool IsVisible { get; set; }

        /// <summary>
        /// Raised when the control is attached to a rooted visual tree.
        /// </summary>
        public event EventHandler<VisualTreeAttachmentEventArgs> AttachedToVisualTree;

        /// <summary>
        /// Raised when the control is detached from a rooted visual tree.
        /// </summary>
        public event EventHandler<VisualTreeAttachmentEventArgs> DetachedFromVisualTree;

        /// <summary>
        /// Gets the root of the visual tree, if the control is attached to a visual tree.
        /// </summary>
        protected IRenderRoot VisualRoot => _visualRoot ?? (this as IRenderRoot);

        /// <summary>
        /// Gets a value indicating whether this control is attached to a visual root.
        /// </summary>
        bool IVisual.IsAttachedToVisualTree => VisualRoot != null;

        /// <summary>
        /// Gets the control's child controls.
        /// </summary>
        IUrhoReadOnlyList<IVisual> IVisual.VisualChildren => VisualChildren;

        /// <summary>
        /// Gets the control's parent visual.
        /// </summary>
        IVisual IVisual.VisualParent => _visualParent;

        /// <summary>
        /// Gets the root of the visual tree, if the control is attached to a visual tree.
        /// </summary>
        IRenderRoot IVisual.VisualRoot => VisualRoot;

        /// <summary>
        /// Gets the control's child visuals.
        /// </summary>
        protected IUrhoList<IVisual> VisualChildren
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the bounds of the control relative to its parent.
        /// </summary>
        public Rect Bounds
        {
            get { return new Rect(_target.Position.X, _target.Position.Y, _target.Width, _target.Height); }
            protected set
            {
                SetAndRaise(BoundsProperty, Bounds, value, value =>
                {
                    _target.Position = new IntVector2((int) value.Top, (int) value.Left);
                    _target.Size = new IntVector2((int)value.Width, (int)value.Height);
                });
            }
        }

        /// <summary>
        /// Called when the control's visual parent changes.
        /// </summary>
        /// <param name="oldParent">The old visual parent.</param>
        /// <param name="newParent">The new visual parent.</param>
        protected virtual void OnVisualParentChanged(IVisual oldParent, IVisual newParent)
        {
            RaisePropertyChanged(
                VisualParentProperty,
                new Optional<IVisual>(oldParent),
                new BindingValue<IVisual>(newParent),
                BindingPriority.LocalValue);
        }


        /// <summary>
        /// Calls the <see cref="OnAttachedToVisualTree(VisualTreeAttachmentEventArgs)"/> method 
        /// for this control and all of its visual descendants.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnAttachedToVisualTreeCore(VisualTreeAttachmentEventArgs e)
        {
            //Logger.TryGet(LogEventLevel.Verbose, LogArea.Visual)?.Log(this, "Attached to visual tree");

            _visualRoot = e.Root;

            //if (RenderTransform is IMutableTransform mutableTransform)
            //{
            //    mutableTransform.Changed += RenderTransformChanged;
            //}

            //EnableTransitions();
            OnAttachedToVisualTree(e);
            AttachedToVisualTree?.Invoke(this, e);
            //InvalidateVisual();

            var visualChildren = VisualChildren;

            if (visualChildren != null)
            {
                var visualChildrenCount = visualChildren.Count;

                for (var i = 0; i < visualChildrenCount; i++)
                {
                    if (visualChildren[i] is Visual child)
                    {
                        child.OnAttachedToVisualTreeCore(e);
                    }
                }
            }
        }

        /// <summary>
        /// Calls the <see cref="OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs)"/> method 
        /// for this control and all of its visual descendants.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnDetachedFromVisualTreeCore(VisualTreeAttachmentEventArgs e)
        {
            //Logger.TryGet(LogEventLevel.Verbose, LogArea.Visual)?.Log(this, "Detached from visual tree");

            _visualRoot = null;

            //if (RenderTransform is IMutableTransform mutableTransform)
            //{
            //    mutableTransform.Changed -= RenderTransformChanged;
            //}

            //DisableTransitions();
            OnDetachedFromVisualTree(e);
            DetachedFromVisualTree?.Invoke(this, e);
            //e.Root?.Renderer?.AddDirty(this);

            var visualChildren = VisualChildren;

            if (visualChildren != null)
            {
                var visualChildrenCount = visualChildren.Count;

                for (var i = 0; i < visualChildrenCount; i++)
                {
                    if (visualChildren[i] is Visual child)
                    {
                        child.OnDetachedFromVisualTreeCore(e);
                    }
                }
            }
        }

        /// <summary>
        /// Called when the control is added to a rooted visual tree.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
        }

        /// <summary>
        /// Called when the control is removed from a rooted visual tree.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
        }
    }
}