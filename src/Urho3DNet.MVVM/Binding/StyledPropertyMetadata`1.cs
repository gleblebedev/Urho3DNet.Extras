using System;
using Urho3DNet.MVVM.Data;

namespace Urho3DNet.MVVM.Binding
{
    /// <summary>
    /// Metadata for styled avalonia properties.
    /// </summary>
    public class StyledPropertyMetadata<TValue> : UrhoPropertyMetadata, IStyledPropertyMetadata
    {
        private Optional<TValue> _defaultValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="StyledPropertyMetadata{TValue}"/> class.
        /// </summary>
        /// <param name="defaultValue">The default value of the property.</param>
        /// <param name="defaultBindingMode">The default binding mode.</param>
        /// <param name="coerce">A value coercion callback.</param>
        public StyledPropertyMetadata(
            Optional<TValue> defaultValue = default,
            BindingMode defaultBindingMode = BindingMode.Default,
            Func<IUrhoObject, TValue, TValue> coerce = null)
                : base(defaultBindingMode)
        {
            _defaultValue = defaultValue;
            CoerceValue = coerce;
        }

        /// <summary>
        /// Gets the default value for the property.
        /// </summary>
        public TValue DefaultValue => _defaultValue.GetValueOrDefault();

        /// <summary>
        /// Gets the value coercion callback, if any.
        /// </summary>
        public Func<IUrhoObject, TValue, TValue> CoerceValue { get; private set; }

        object IStyledPropertyMetadata.DefaultValue => DefaultValue;

        /// <inheritdoc/>
        public override void Merge(UrhoPropertyMetadata baseMetadata, UrhoProperty property)
        {
            base.Merge(baseMetadata, property);

            if (baseMetadata is StyledPropertyMetadata<TValue> src)
            {
                if (!_defaultValue.HasValue)
                {
                    _defaultValue = src.DefaultValue;
                }

                if (CoerceValue == null)
                {
                    CoerceValue = src.CoerceValue;
                }
            }
        }
    }
}