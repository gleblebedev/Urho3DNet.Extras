using System;
using Urho.VisualTree;
using Urho3DNet.MVVM.Collections;
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
    public class Visual : StyledElement, IVisual
    {
        private IVisual _visualParent;
        private IRenderRoot _visualRoot;

        /// <summary>
        /// Defines the <see cref="IVisual.VisualParent"/> property.
        /// </summary>
        public static readonly DirectProperty<Visual, IVisual> VisualParentProperty =
            UrhoProperty.RegisterDirect<Visual, IVisual>("VisualParent", o => o._visualParent);

        public Visual(UIElement target) : base(target)
        {
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

        private static UIElementView CreateView(UIElement child)
        {
            var stringHash = child.GetTypeHash();
            switch (stringHash.Hash)
            {
                case 0x1F0A61E1:
                    return new SliderView((Slider) child);
                case 0xC90F98CF:
                    return new BorderImageView((BorderImage)child);
                default:
                    throw new NotImplementedException("Unknown type: "+child.GetTypeName()+" (hash "+ stringHash + ")");
            }
        }

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
    }
}