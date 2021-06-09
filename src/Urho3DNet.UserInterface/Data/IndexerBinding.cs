namespace Urho3DNet.UserInterface.Data
{
    public class IndexerBinding : IBinding
    {
        public IndexerBinding(
            IUrhoUIObject source,
            UrhoUIProperty property,
            BindingMode mode)
        {
            Source = source;
            Property = property;
            Mode = mode;
        }

        private IUrhoUIObject Source { get; }
        public UrhoUIProperty Property { get; }
        private BindingMode Mode { get; }

        public InstancedBinding Initiate(
            IUrhoUIObject target,
            UrhoUIProperty targetProperty,
            object anchor = null,
            bool enableDataValidation = false)
        {
            return new InstancedBinding(Source.GetSubject(Property), Mode, BindingPriority.LocalValue);
        }
    }
}
