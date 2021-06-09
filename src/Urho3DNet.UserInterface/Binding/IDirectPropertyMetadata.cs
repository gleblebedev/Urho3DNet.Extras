namespace Urho3DNet.UserInterface
{
    /// <summary>
    /// Untyped interface to <see cref="DirectPropertyMetadata{TValue}"/>
    /// </summary>
    public interface IDirectPropertyMetadata
    {
        /// <summary>
        /// Gets the to use when the property is set to <see cref="UrhoUIProperty.UnsetValue"/>.
        /// </summary>
        object UnsetValue { get; }

        /// <summary>
        /// Gets a value indicating whether the property is interested in data validation.
        /// </summary>
        bool? EnableDataValidation { get; }
    }
}