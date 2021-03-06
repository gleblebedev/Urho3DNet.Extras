using System;
using System.Threading;
using Avalonia;
using Avalonia.Input.Platform;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Skia;
using Urho3DNet.AvaliniaAdapter;

namespace Urho3DNet
{
    public class PortableWindowPlatform : PlatformThreadingInterfaceBase, IPlatformSettings, IWindowingPlatform
    {
        private static readonly PortableWindowPlatform s_instance = new PortableWindowPlatform();
        private static AvaloniaUrhoContext _context;

        public Size DoubleClickSize { get; } = new Size(2, 2);
        public TimeSpan DoubleClickTime { get; } = TimeSpan.FromSeconds(0.5);

        public static void Initialize(AvaloniaUrhoContext context)
        {
            _context = context;
            AvaloniaLocator.CurrentMutable
                .Bind<IPlatformSettings>().ToConstant(s_instance)
                .Bind<ICursorFactory>().ToTransient<CursorFactory>()
                .Bind<IPlatformThreadingInterface>().ToConstant(s_instance)
                .Bind<IRenderLoop>().ToConstant(new RenderLoop())
                .Bind<IRenderTimer>().ToConstant(new DefaultRenderTimer(60))
                .Bind<IWindowingPlatform>().ToConstant(s_instance)
                .Bind<PlatformHotkeyConfiguration>().ToSingleton<PlatformHotkeyConfiguration>()
                .Bind<IPlatformIconLoader>().ToConstant(new PlatformIconLoader());
            SkiaPlatform.Initialize();
        }

        public override void EnsureInvokeOnMainThread(Action action)
        {
            _context.EnsureInvokeOnMainThread(action);
        }

        public override void RunLoop(CancellationToken cancellationToken)
        {
            _context.RunLoop(cancellationToken);
        }

        public IWindowImpl CreateWindow()
        {
            return new UrhoWindowImpl(_context);
        }

        public IWindowImpl CreateEmbeddableWindow()
        {
            return new UrhoWindowImpl(_context);
        }
    }
}