using System;
using System.Collections.Generic;
using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Collections;

namespace Urho3DNet.MVVM.Styling
{
    /// <summary>
    /// Interface for styleable elements.
    /// </summary>
    public interface IStyleable : IUrhoObject, INamed
    {
        /// <summary>
        /// Gets the list of classes for the control.
        /// </summary>
        IUrhoReadOnlyList<string> Classes { get; }

        /// <summary>
        /// Gets the type by which the control is styled.
        /// </summary>
        Type StyleKey { get; }

        /// <summary>
        /// Gets the template parent of this element if the control comes from a template.
        /// </summary>
        ITemplatedControl? TemplatedParent { get; }

        /// <summary>
        /// Notifies the element that a style has been applied.
        /// </summary>
        /// <param name="instance">The style instance.</param>
        void StyleApplied(IStyleInstance instance);

        /// <summary>
        /// Detaches all styles applied to the element.
        /// </summary>
        void DetachStyles();

        /// <summary>
        /// Detaches a collection of styles, if applied to the element.
        /// </summary>
        void DetachStyles(IReadOnlyList<IStyle> styles);

        /// <summary>
        /// Detaches all styles from the element and queues a restyle.
        /// </summary>
        void InvalidateStyles();
    }
}