using Avalonia;
using Avalonia.Dialogs;

namespace Urho3DNet
{
    public static class ExtensionMethods
    {
        public static AvaloniaUrhoContext ConfigureAvalonia<T>(this Context context) where T: Avalonia.Application, new()
        {
            var avaloniaUrhoContext = new AvaloniaUrhoContext(context);
            PortableAppBuilder.Configure<T>()
                .UsePortablePlatfrom(avaloniaUrhoContext)
                .UseSkia()
                .UseManagedSystemDialogs()
                .SetupWithoutStarting();
            return avaloniaUrhoContext;
        }
    }
}