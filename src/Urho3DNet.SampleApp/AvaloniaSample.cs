using Urho3DNet.InputEvents;
using Urho3DNet.SampleApp.View;

namespace Urho3DNet.Samples
{
    public class AvaloniaSample : Sample
    {
        private readonly SharedPtr<AvaloniaElement> _sprite;
        private readonly SampleAvaloniaWindow _window;

        public AvaloniaSample(Context context) : base(context)
        {
            _window = new SampleAvaloniaWindow();
            _window.Width = 960;
            _window.Height = 63;
            _window.Show();
            //_window.InvalidateVisual();
            _sprite = UIRoot.CreateChild<AvaloniaElement>();
            _sprite.Value.Canvas = _window;
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
            _sprite.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnResize(IntVector2 graphicsSize)
        {
            base.OnResize(graphicsSize);

            var avaloniaElement = _sprite.Value;
            avaloniaElement.Size = new IntVector2(Graphics.Width/2, Graphics.Height/16);
            avaloniaElement.Position = new Vector2(0.5f * (Graphics.Width - avaloniaElement.Width),
                0.5f * (Graphics.Height - avaloniaElement.Height));

        }
    }
}