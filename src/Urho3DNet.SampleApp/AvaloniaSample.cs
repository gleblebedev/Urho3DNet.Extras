using Avalonia;
using Urho3DNet.InputEvents;
using Urho3DNet.SampleApp.View;

namespace Urho3DNet.Samples
{
    public class AvaloniaSample : Sample
    {
        private readonly SampleAvaloniaWindow _window;

        public AvaloniaSample(Context context) : base(context)
        {
            _window = new SampleAvaloniaWindow();
            _window.Width = 960;
            _window.Height = 63;
            _window.Show(UIRoot);
            DefaultFogColor = new Color(0.1f, 0.2f, 0.4f, 1.0f);
            IsMouseVisible = true;
            MouseMode = MouseMode.MmFree;
        }

        public override void OnUpdate(CoreEventsAdapter.UpdateEventArgs args)
        {
            base.OnUpdate(args);
        }


        protected override void Dispose(bool disposing)
        {
            _window.Close();
            base.Dispose(disposing);
        }

        protected override void OnResize(IntVector2 graphicsSize)
        {
            base.OnResize(graphicsSize);

            var avaloniaElement = _window;
            avaloniaElement.Width = Graphics.Width/2;
            avaloniaElement.Height =  Graphics.Height/16;
            avaloniaElement.Position = new PixelPoint((int)(Graphics.Width - avaloniaElement.Width)/2,
                (int)(Graphics.Height - avaloniaElement.Height)/2);

        }
    }
}