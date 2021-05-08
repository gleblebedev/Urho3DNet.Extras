using Avalonia.Controls;
using Urho3DNet.AvaliniaAdapter;

namespace Urho3DNet
{
    public sealed class PortableAppBuilder : AppBuilderBase<PortableAppBuilder>
    {
        public PortableAppBuilder() : base(new StandardRuntimePlatform(),
            builder => StandardRuntimePlatformServices.Register(builder.Instance?.GetType()?.Assembly))
        {
        }
    }
}