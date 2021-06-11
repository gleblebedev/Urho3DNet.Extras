namespace Urho3DNet.MVVM.Binding
{
    /// <summary>
    /// Defines an element with a data context that can be used for binding.
    /// </summary>
    public interface IDataContextProvider : IUrhoObject
    {
        /// <summary>
        /// Gets or sets the element's data context.
        /// </summary>
        object DataContext { get; set; }
    }
}