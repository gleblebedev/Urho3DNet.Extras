using SkiaSharp;

namespace Urho3DNet.Samples
{
    public class SkiaSample : Sample
    {
        private Sprite _sprite;
        private SkiaCanvas _canvas;

        public SkiaSample(Context context) : base(context)
        {
            _sprite = UIRoot.CreateChild<Sprite>();
            _canvas = new SkiaCanvas(context, new SKBitmap(new SKImageInfo(256, 256, SKColorType.Rgba8888)), TextureUsage.TextureStatic);
            _sprite.Texture = _canvas.Texture;
            _sprite.Position = new Vector2(0, 0);
            _sprite.Size = new IntVector2(256, 256);
            var canvas = _canvas.Canvas;
            canvas.Clear(new SKColor(255, 0, 0, 128));
            canvas.Flush();
            _canvas.Upload();
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
                    }
                    canvas.Flush();
                    _canvas.Upload(10,10,100,100);
                    _canvas.Upload(200, 10, 10, 200);
                }
            }

            DefaultFogColor = new Color(0.1f, 0.2f, 0.4f, 1.0f);
        }

        public override void OnUpdate(float timeStep)
        {
            base.OnUpdate(timeStep);

            _sprite.Position = new Vector2(0.5f * (Graphics.Width - _sprite.Width),
                0.5f * (Graphics.Height - _sprite.Height));
        }
    }
}