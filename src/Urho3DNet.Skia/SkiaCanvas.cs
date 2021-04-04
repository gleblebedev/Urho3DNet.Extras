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
        private readonly SharedPtr<Texture2D> _texture = new SharedPtr<Texture2D>(null);

        public SkiaCanvas(Context context, SKBitmap bitmap, TextureUsage textureUsage = TextureUsage.TextureDynamic)
        {
            _context = context;
            _bitmap = bitmap;
            Canvas = new SKCanvas(_bitmap);
            _texture.Value = new Texture2D(_context);
            Texture.SetSize(_bitmap.Info.Width, _bitmap.Info.Height, GetFormat(_bitmap.Info.ColorType), textureUsage);
        }

        public SKCanvas Canvas { get; }

        public Texture2D Texture => _texture;

        public int Width => _bitmap.Info.Width;

        public int Height => _bitmap.Info.Height;

        public float FullUpdateThreshold { get; set; } = 0.75f;

        public void Upload(int x, int y, int width, int height)
        {
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
                Texture.SetData(0, 0, y, Width, height, _bitmap.GetAddress(0, y));
            }
        }

        public void Upload()
        {
            Texture.SetData(0, 0, 0, Width, Height, _bitmap.GetPixels());
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