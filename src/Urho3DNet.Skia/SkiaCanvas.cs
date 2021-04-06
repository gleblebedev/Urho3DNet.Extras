using System;
using System.Buffers;
using System.Runtime.InteropServices;
using SkiaSharp;

namespace Urho3DNet
{
    public class SkiaCanvas : IDisposable
    {
        private readonly Context _context;
        private readonly SKBitmap _bitmap;
        private readonly SharedPtr<Texture2D> _texture;

        public SkiaCanvas(Context context, SKBitmap bitmap, TextureUsage textureUsage = TextureUsage.TextureDynamic)
        {
            _context = context;
            _bitmap = bitmap;
            Canvas = new SKCanvas(_bitmap);
            _texture = new Texture2D(_context);
            var texWidth = MathDefs.NextPowerOfTwo(_bitmap.Info.Width);
            var texHeight = MathDefs.NextPowerOfTwo(_bitmap.Info.Height);
            Texture.SetSize(texWidth, texHeight, GetFormat(_bitmap.Info.ColorType), textureUsage);
        }
        
        public SKCanvas Canvas { get; }

        public Texture2D Texture => _texture;

        public IntVector2 Size => new IntVector2(_bitmap.Info.Width, _bitmap.Info.Height);

        public IntVector2 TextureSize => Texture.Size;

        public float FullUpdateThreshold { get; set; } = 0.75f;

        public void Upload(int x, int y, int width, int height)
        {
            var Width = Size.X;
            var fullUpdateArea = height * Width;
            var updateArea = height * width;
            if (updateArea < FullUpdateThreshold * fullUpdateArea)
            {
                var targetPitch = width * _bitmap.BytesPerPixel;
                var sourcePitch = Width * _bitmap.BytesPerPixel;
                var buffer = ArrayPool<byte>.Shared.Rent(height * targetPitch);
                var targetSpan = new Span<byte>(buffer, 0, buffer.Length);
                unsafe
                {
                    var ptr = _bitmap.GetAddress(x, y).ToPointer();
                    var sourceSpan = new Span<byte>(ptr, height * sourcePitch);

                    for (var row = 0; row < height; ++row)
                    {
                        var t = targetSpan.Slice(row * targetPitch, targetPitch);
                        var s = sourceSpan.Slice(row * sourcePitch, targetPitch);
                        s.CopyTo(t);
                    }
                }

                var pinnedArray = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                var pointer = pinnedArray.AddrOfPinnedObject();
                Texture.SetData(0, x, y, width, height, pointer);
                pinnedArray.Free();
            }
            else
            {
                Texture.SetData(0, 0, y, Size.X, height, _bitmap.GetAddress(0, y));
            }
        }

        public void Upload()
        {
            var size = Size;
            Texture.SetData(0, 0, 0, size.X, size.Y, _bitmap.GetPixels());
        }

        public void Dispose()
        {
            Canvas.Dispose();
            _texture.Dispose();
        }

        private uint GetFormat(SKColorType infoColorType)
        {
            switch (infoColorType)
            {
                case SKColorType.Rgb888x:
                case SKColorType.Rgba8888:
                    return Graphics.GetRGBAFormat();
            }

            throw new NotImplementedException(infoColorType + " not supported");
        }
    }
}