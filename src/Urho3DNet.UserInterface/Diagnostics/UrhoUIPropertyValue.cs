using Urho3DNet.UserInterface.Data;

namespace Urho3DNet.UserInterface.Diagnostics
{
    /// <summary>
    /// Holds diagnostic-related information about the value of a <see cref="UrhoUIProperty"/>
    /// on a <see cref="UrhoUIObject"/>.
    /// </summary>
    public class UrhoUIPropertyValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoUIPropertyValue"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The current property value.</param>
        /// <param name="priority">The priority of the current value.</param>
        /// <param name="diagnostic">A diagnostic string.</param>
        public UrhoUIPropertyValue(
            UrhoUIProperty property,
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
        public UrhoUIProperty Property { get; }

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
