using SkiaSharp;

namespace Urho3DNet.Samples
{
    public class SkiaSample : Sample
    {
        private readonly SharedPtr<SkiaElement> _sprite;
        private SkiaCanvas _canvas;

        public SkiaSample(Context context) : base(context)
        {
            MouseMode = MouseMode.MmFree;
            IsMouseVisible = true;
            _sprite = UIRoot.CreateChild<SkiaElement>();
            _canvas = new SkiaCanvas(context, new SKBitmap(new SKImageInfo(200, 180, SKColorType.Rgba8888)), TextureUsage.TextureDynamic);
            _sprite.Value.Canvas = _canvas;
            var canvas = _canvas.Canvas;
            canvas.Clear(new SKColor(255, 0, 0, 128));
            using (var green = new SKPaint() { Color = new SKColor(0, 255, 0, 255) })
            {
                using (var red = new SKPaint() {Color = new SKColor(255, 0, 0, 255)})
                {
                    canvas.DrawRect(10, 10, 100, 10, red);
                    canvas.DrawLine(0, 0, 256, 256, green);
                    using (var white = new SKPaint(new SKFont(SKTypeface.Default, 24))
                        {Color = new SKColor(255, 255, 255, 255)})
                    {
                        canvas.DrawText("Hello!", new SKPoint(40, 40), white);
                        canvas.DrawLine(199, 0, 199, 180, white);
                        canvas.DrawLine(0, 179, 199, 179, white);
                    }
                    canvas.Flush();
                    _canvas.Upload();
                }
            }

            DefaultFogColor = new Color(0.1f, 0.2f, 0.4f, 1.0f);
        }

        public override void OnUpdate(float timeStep)
        {
            base.OnUpdate(timeStep);

            _sprite.Value.Position = new Vector2(0.5f * (Graphics.Width - _sprite.Value.Width),
                0.5f * (Graphics.Height - _sprite.Value.Height));
        }
    }
}