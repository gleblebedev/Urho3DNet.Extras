using Urho3DNet.MVVM.Data;

namespace Urho3DNet.MVVM.Binding
{
    /// <summary>
    /// Base class for avalonia property metadata.
    /// </summary>
    public class UrhoPropertyMetadata
    {
        private BindingMode _defaultBindingMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrhoPropertyMetadata"/> class.
        /// </summary>
        /// <param name="defaultBindingMode">The default binding mode.</param>
        public UrhoPropertyMetadata(
            BindingMode defaultBindingMode = BindingMode.Default)
        {
            _defaultBindingMode = defaultBindingMode;
        }

        /// <summary>
        /// Gets the default binding mode for the property.
        /// </summary>
        public BindingMode DefaultBindingMode
        {
            get
            {
                return _defaultBindingMode == BindingMode.Default ?
                    BindingMode.OneWay : _defaultBindingMode;
            }
        }

        /// <summary>
        /// Merges the metadata with the base metadata.
        /// </summary>
        /// <param name="baseMetadata">The base metadata to merge.</param>
        /// <param name="property">The property to which the metadata is being applied.</param>
        public virtual void Merge(
            UrhoPropertyMetadata baseMetadata, 
            UrhoProperty property)
        {
            if (_defaultBindingMode == BindingMode.Default)
            {
                _defaultBindingMode = baseMetadata.DefaultBindingMode;
            }
        }
    }
}
