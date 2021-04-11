using Avalonia;
using Avalonia.Controls;

namespace Urho3DNet
{
    public static class UrhoApplicationExtensions
    {
        /// <summary>
        ///     Enable Skia renderer.
        /// </summary>
        /// <typeparam name="T">Builder type.</typeparam>
        /// <param name="builder">Builder.</param>
        /// <returns>Configure builder.</returns>
        public static T UsePortablePlatfrom<T>(this T builder, Context context) where T : AppBuilderBase<T>, new()
        {
            return builder.UseWindowingSubsystem(
                () => PortableWindowPlatform.Initialize(
                    AvaloniaLocator.Current.GetService<AvaloniaUrhoContext>() ??
                    new AvaloniaUrhoContext(context)),
                "PortableUrho3DPlatform");
        }
    }
}