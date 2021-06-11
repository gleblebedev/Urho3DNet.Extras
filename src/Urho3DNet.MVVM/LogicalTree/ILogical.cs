using System;
using System.Collections.Generic;
using System.Text;
using Urho3DNet.MVVM.Collections;

namespace Urho3DNet.MVVM.LogicalTree
{
    /// <summary>
    /// Represents a node in the logical tree.
    /// </summary>
    public interface ILogical
    {
        /// <summary>
        /// Raised when the control is attached to a rooted logical tree.
        /// </summary>
        event EventHandler<LogicalTreeAttachmentEventArgs> AttachedToLogicalTree;

        /// <summary>
        /// Raised when the control is detached from a rooted logical tree.
        /// </summary>
        event EventHandler<LogicalTreeAttachmentEventArgs> DetachedFromLogicalTree;

        /// <summary>
        /// Gets the logical parent.
        /// </summary>
        ILogical LogicalParent { get; }

        /// <summary>
        /// Gets the logical children.
        /// </summary>
        IUrhoReadOnlyList<ILogical> LogicalChildren { get; }
    }
}
