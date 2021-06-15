using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.LogicalTree;

namespace Urho3DNet.MVVM.Controls
{
    /// <summary>
    /// Defines an interface through which a <see cref="StyledElement"/>'s logical parent can be set.
    /// </summary>
    /// <remarks>
    /// You should not usually need to use this interface - it is for advanced scenarios only.
    /// </remarks>
    public interface ISetLogicalParent
    {
        /// <summary>
        /// Sets the control's parent.
        /// </summary>
        /// <param name="parent">The parent.</param>
        void SetParent(ILogical parent);
    }
}