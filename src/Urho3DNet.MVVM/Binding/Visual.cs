using Urho.VisualTree;
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
    public class Visual : StyledElement//, IVisual
    {
        private IVisual _visualParent;

        /// <summary>
        /// Defines the <see cref="IVisual.VisualParent"/> property.
        /// </summary>
        public static readonly DirectProperty<Visual, IVisual> VisualParentProperty =
            UrhoProperty.RegisterDirect<Visual, IVisual>("VisualParent", o => o._visualParent);

        public Visual(Object target) : base(target)
        {
        }
    }
}