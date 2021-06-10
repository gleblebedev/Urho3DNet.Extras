using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Data;

namespace Urho3DNet.MVVM.Diagnostics
{
    /// <summary>
    /// Holds diagnostic-related information about the value of a <see cref="UrhoProperty"/>
    /// on a <see cref="UrhoObject"/>.
    /// </summary>
    public class UrhoPropertyValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoPropertyValue"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The current property value.</param>
        /// <param name="priority">The priority of the current value.</param>
        /// <param name="diagnostic">A diagnostic string.</param>
        public UrhoPropertyValue(
            UrhoProperty property,
            object value,
            BindingPriority priority,
            string diagnostic)
        {
            Property = property;
            Value = value;
            Priority = priority;
            Diagnostic = diagnostic;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        public UrhoProperty Property { get; }

        /// <summary>
        /// Gets the current property value.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Gets the priority of the current value.
        /// </summary>
        public BindingPriority Priority { get; }

        /// <summary>
        /// Gets a diagnostic string.
        /// </summary>
        public string Diagnostic { get; }
    }
}
