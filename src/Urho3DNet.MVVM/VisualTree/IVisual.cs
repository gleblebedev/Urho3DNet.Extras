using System;
using Urho.VisualTree;
using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Collections;
using Urho3DNet.MVVM.Rendering;

namespace Urho3DNet.MVVM.VisualTree
{
    /// <summary>
    /// Holds the event arguments for the <see cref="Visual.AttachedToVisualTree"/> and 
    /// <see cref="Visual.DetachedFromVisualTree"/> events.
    /// </summary>
    public class VisualTreeAttachmentEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualTreeAttachmentEventArgs"/> class.
        /// </summary>
        /// <param name="parent">The parent that the visual is being attached to or detached from.</param>
        /// <param name="root">The root visual.</param>
        public VisualTreeAttachmentEventArgs(IVisual parent, IRenderRoot root)
        {
            Contract.Requires<ArgumentNullException>(parent != null);
            Contract.Requires<ArgumentNullException>(root != null);

            Parent = parent;
            Root = root;
        }

        /// <summary>
        /// Gets the parent that the visual is being attached to or detached from.
        /// </summary>
        public IVisual Parent { get; }

        /// <summary>
        /// Gets the root of the visual tree that the visual is being attached to or detached from.
        /// </summary>
        public IRenderRoot Root { get; }
    }
    /// <summary>
    /// Represents control that has a visual on-screen representation.
    /// </summary>
    /// <remarks>
    /// The <see cref="IVisual"/> interface defines the interface required for a renderer to
    /// render a control. You should not usually need to reference this interface unless
    /// you are writing a renderer; instead use the extension methods defined in
    /// <see cref="VisualExtensions"/> to traverse the visual tree. This interface is
    /// implemented by <see cref="Visual"/>. It should not be necessary to implement it
    /// anywhere else.
    /// </remarks>
    public interface IVisual
    {
        /// <summary>
        /// Raised when the control is attached to a rooted visual tree.
        /// </summary>
        event EventHandler<VisualTreeAttachmentEventArgs> AttachedToVisualTree;

        /// <summary>
        /// Raised when the control is detached from a rooted visual tree.
        /// </summary>
        event EventHandler<VisualTreeAttachmentEventArgs> DetachedFromVisualTree;

        ///// <summary>
        ///// Gets the bounds of the control relative to its parent.
        ///// </summary>
        //Rect Bounds { get; }

        ///// <summary>
        ///// Gets the bounds of the control relative to the window, accounting for rendering transforms.
        ///// </summary>
        //TransformedBounds? TransformedBounds { get; set; }

        /// <summary>
        /// Gets a value indicating whether the control should be clipped to its bounds.
        /// </summary>
        bool ClipToBounds { get; set; }

        ///// <summary>
        ///// Gets or sets the geometry clip for this visual.
        ///// </summary>
        //Geometry Clip { get; set; }

        /// <summary>
        /// Gets a value indicating whether this control is attached to a visual root.
        /// </summary>
        bool IsAttachedToVisualTree { get; }

        ///// <summary>
        ///// Gets a value indicating whether this control and all its parents are visible.
        ///// </summary>
        //bool IsEffectivelyVisible { get; }

        ///// <summary>
        ///// Gets or sets a value indicating whether this control is visible.
        ///// </summary>
        //bool IsVisible { get; set; }

        ///// <summary>
        ///// Gets or sets the opacity of the control.
        ///// </summary>
        //double Opacity { get; set; }

        ///// <summary>
        ///// Gets or sets the opacity mask for the control.
        ///// </summary>
        //IBrush OpacityMask { get; set; }

        ///// <summary>
        ///// Gets or sets the render transform of the control.
        ///// </summary>
        //ITransform RenderTransform { get; set; }

        ///// <summary>
        ///// Gets or sets the render transform origin of the control.
        ///// </summary>
        //RelativePoint RenderTransformOrigin { get; set; }

        /// <summary>
        /// Gets the control's child visuals.
        /// </summary>
        IUrhoReadOnlyList<IVisual> VisualChildren { get; }

        /// <summary>
        /// Gets the control's parent visual.
        /// </summary>
        IVisual VisualParent { get; }

        /// <summary>
        /// Gets the root of the visual tree, if the control is attached to a visual tree.
        /// </summary>
        IRenderRoot VisualRoot { get; }

        /// <summary>
        /// Gets or sets the Z index of the node.
        /// </summary>
        int ZIndex { get; set; }

        /// <summary>
        /// Invalidates the visual and queues a repaint.
        /// </summary>
        void InvalidateVisual();

        ///// <summary>
        ///// Renders the control to a <see cref="DrawingContext"/>.
        ///// </summary>
        ///// <param name="context">The context.</param>
        //void Render(DrawingContext context);
    }
}
