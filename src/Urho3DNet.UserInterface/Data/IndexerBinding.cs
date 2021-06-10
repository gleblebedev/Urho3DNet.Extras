using Urho3DNet.MVVM.Binding;

namespace Urho3DNet.MVVM.Data
{
    public class IndexerBinding : IBinding
    {
        public IndexerBinding(
            IUrhoObject source,
            UrhoProperty property,
            BindingMode mode)
        {
            Source = source;
            Property = property;
            Mode = mode;
        }

        private IUrhoObject Source { get; }
        public UrhoProperty Property { get; }
        private BindingMode Mode { get; }

        public InstancedBinding Initiate(
            IUrhoObject target,
            UrhoProperty targetProperty,
            object anchor = null,
            bool enableDataValidation = false)
        {
            return new InstancedBinding(Source.GetSubject(Property), Mode, BindingPriority.LocalValue);
        }
    }
}
