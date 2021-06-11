using System.Linq;

namespace Urho3DNet.MVVM.Data.Converters
{
    /// <summary>
    /// Provides a set of useful <see cref="IValueConverter"/>s for working with bool values.
    /// </summary>
    public static class BoolConverters
    {
        /// <summary>
        /// A multi-value converter that returns true if all inputs are true.
        /// </summary>
        public static readonly IMultiValueConverter And =
            new FuncMultiValueConverter<bool, bool>(x => x.All(y => y));

        /// <summary>
        /// A multi-value converter that returns true if any of the inputs is true.
        /// </summary>
        public static readonly IMultiValueConverter Or =
            new FuncMultiValueConverter<bool, bool>(x => x.Any(y => y));
    }
}
