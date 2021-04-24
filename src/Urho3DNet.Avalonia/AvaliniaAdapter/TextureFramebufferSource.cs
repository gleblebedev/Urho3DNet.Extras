using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

namespace Urho3DNet.AvaliniaAdapter
{
    public class TextureFramebufferSource
    {
        private readonly AvaloniaUrhoContext _avaloniaContext;
        private LockedFramebuffer _lockedFramebuffer;
        private byte[] _data = Array.Empty<byte>();
        private Texture2D _texture;
        private PixelSize _size;
        private TextureUsage _textureUsage = TextureUsage.TextureDynamic;

        public TextureFramebufferSource(AvaloniaUrhoContext avaloniaContext)
        {
            _avaloniaContext = avaloniaContext;
            _texture = new Texture2D(avaloniaContext.Context);
            _texture.SetNumLevels(1);
            _lockedFramebuffer = new LockedFramebuffer(this);
            switch (SKImageInfo.PlatformColorType)
            {
                //case SKColorType.Bgra8888:
                //    Format = PixelFormat.Bgra8888;
                //    break;
                default:
                    Format = PixelFormat.Rgba8888;
                    break;
            }

        }

        public ILockedFramebuffer Lock()
        {
            _lockedFramebuffer.Lock();
            return _lockedFramebuffer;
        }

        public PixelSize Size
        {
            get => _size;
            set
            {
                if (_size != value)
                {
                    _size = value;

                    if (value.Width == 0 || value.Height == 0)
                    {
                        return;
                        //throw new ArgumentOutOfRangeException("Size can't be zero");
                    }

                    var width = MathDefs.NextPowerOfTwo(_size.Width);
                    var height = MathDefs.NextPowerOfTwo(_size.Height);
                    if (width != _texture.Width || height != _texture.Height)
                    {
                        RowBytes = width * 4;
                        if (RowBytes * height > _data.Length)
                        {
                            _data = new byte[RowBytes * height];
                        }

                        _texture.SetSize(width, height, GetFormat(Format), _textureUsage);
                    }
                }
            }
        }

        private uint GetFormat(PixelFormat format)
        {
            switch (format)
            {
                case PixelFormat.Rgba8888:
                    return Graphics.GetRGBAFormat();
                default:
                    throw new NotImplementedException(format.ToString());
            }
        }

        class LockedFramebuffer: ILockedFramebuffer
        {
            private readonly TextureFramebufferSource _source;
            private GCHandle _pinnedArray;

            public LockedFramebuffer(TextureFramebufferSource source)
            {
                _source = source;
            }

            public void Dispose()
            {
                var texture = _source._texture;
                texture.SetData(0, 0, 0, texture.Width, texture.Height, Address);
                _pinnedArray.Free();
                _pinnedArray = default;
            }

            public IntPtr Address { get; private set; }

            public PixelSize Size => _source.Size;

            public int RowBytes => _source.RowBytes;
            public Vector Dpi => _source.Dpi;
            public PixelFormat Format => _source.Format;

            public void Lock()
            {
                _pinnedArray = GCHandle.Alloc(_source._data, GCHandleType.Pinned);
                Address = _pinnedArray.AddrOfPinnedObject();
            }
        }

        public int RowBytes { get; private set; }

        public PixelFormat Format { get; }

        public Vector Dpi { get; set; }

        public Texture2D Texture => _texture;
    }
}