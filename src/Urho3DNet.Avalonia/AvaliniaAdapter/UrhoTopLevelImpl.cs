using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Platform.Surfaces;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Platform;
using Avalonia.Rendering;

namespace Urho3DNet.AvaliniaAdapter
{
    public class UrhoTopLevelImpl : ITopLevelImpl, IFramebufferPlatformSurface
    {
        private Avalonia.Rect _invalidRegion = Avalonia.Rect.Empty;
        private double _dpi = 0;
        private TextureFramebufferSource _framebufferSource;
        private bool _hasActualSize;
        private Size _clientSizeCache;
        public UrhoTopLevelImpl(AvaloniaUrhoContext avaloniaUrhoContext)
        {
            UrhoContext = avaloniaUrhoContext;
            avaloniaUrhoContext.AddWindow(this);
            _framebufferSource = new TextureFramebufferSource(avaloniaUrhoContext);
            Dpi = 96.0;
            Invalidate(new Avalonia.Rect(0, 0, double.MaxValue, double.MaxValue));
        }

        public AvaloniaUrhoContext UrhoContext { get; set; }
        
        public virtual void Dispose()
        {
            UrhoContext.RemoveWindow(this);
            _framebufferSource.Dispose();
        }

        public IRenderer CreateRenderer(IRenderRoot root)
        {
            return new ImmediateRenderer(root);
        }

        /// <summary>Invalidates a rect on the toplevel.</summary>
        public virtual void Invalidate(Avalonia.Rect rect)
        {
            _invalidRegion = _invalidRegion.Union(rect);
            SchedulePaint();
        }

        private void SchedulePaint()
        {
            UrhoContext.SchedulePaint(this);
        }

        public void SetInputRoot(IInputRoot inputRoot)
        {
            InputRoot = inputRoot;
        }

        public IInputRoot InputRoot { get; private set; }

        public Point PointToClient(PixelPoint point)
        {
            //var position = IsFullscreen ? new PixelPoint(0, 0) : Position;
            var position = new PixelPoint(0, 0);
            return (point - position).ToPoint(RenderScaling);
        }

        public PixelPoint PointToScreen(Point point)
        {
            //var position = IsFullscreen ? new PixelPoint(0, 0) : Position;
            var position = new PixelPoint(0, 0);
            return PixelPoint.FromPoint(point, RenderScaling) + position;
        }

        public void SetCursor(ICursorImpl cursor)
        {
        }

        //public void SetCursor(IPlatformHandle cursor)
        //{
        //}

        public IPopupImpl CreatePopup()
        {
            throw new NotImplementedException();
        }

        public void SetTransparencyLevelHint(WindowTransparencyLevel transparencyLevel)
        {
        }

        /// <summary>
        ///     Gets the client size of the toplevel.
        /// </summary>
        public virtual Size ClientSize
        {
            get
            {
                var framebufferSize = _framebufferSource.Size;
                return new Size(framebufferSize.Width / RenderScaling, framebufferSize.Height / RenderScaling);
            }
        }
        
        /// <summary>
        ///     Gets the scaling factor for the toplevel.
        /// </summary>
        public virtual double RenderScaling
        {
            get => _dpi / 96.0;
            set
            {
                var scaling = RenderScaling;
                if (scaling != value) Dpi = 96.0 * value;
            }
        }

        public double Dpi
        {
            get => _dpi;
            set
            {
                if (_dpi != value)
                {
                    var clientSize = ClientSize;
                    _dpi = value;
                    if (_framebufferSource != null) _framebufferSource.Dpi = new Vector(_dpi, _dpi);
                    if (_hasActualSize) Resize(clientSize);
                    ScalingChanged?.Invoke(RenderScaling);
                    //Invalidate(new Rect(new Point(0,0), ClientSize));
                }
            }
        }

        /// <summary>
        ///     The list of native platform's surfaces that can be consumed by rendering subsystems.
        /// </summary>
        /// <remarks>
        ///     Rendering platform will check that list and see if it can utilize one of them to output.
        ///     It should be enough to expose a native window handle via IPlatformHandle
        ///     and add support for framebuffer (even if it's emulated one) via IFramebufferPlatformSurface.
        ///     If you have some rendering platform that's tied to your particular windowing platform,
        ///     just expose some toolkit-specific object (e. g. Func&lt;Gdk.Drawable&gt; in case of GTK#+Cairo)
        /// </remarks>
        public virtual IEnumerable<object> Surfaces
        {
            get { yield return this; }
        }
        
        public Action<RawInputEventArgs> Input { get; set; }
        public Action<Avalonia.Rect> Paint { get; set; }
        public Action<Size> Resized { get; set; }
        public Action<double> ScalingChanged { get; set; }
        public Action<WindowTransparencyLevel> TransparencyLevelChanged { get; set; }
        public Action Closed { get; set; }
        public Action LostFocus { get; set; }
        public virtual IMouseDevice MouseDevice => UrhoContext.MouseDevice;
        public WindowTransparencyLevel TransparencyLevel { get; } = WindowTransparencyLevel.None;
        public AcrylicPlatformCompensationLevels AcrylicCompensationLevels { get; } = new AcrylicPlatformCompensationLevels(1, 1, 1);
        public Texture2D Texture => _framebufferSource.Texture;
        public IntVector2 VisibleSize => new IntVector2(_framebufferSource.Size.Width, _framebufferSource.Size.Height);

        public ILockedFramebuffer Lock()
        {
            return _framebufferSource.Lock();
        }

        public virtual void Resize(Size clientSize)
        {
            _hasActualSize = true;
            var scaling = RenderScaling;
            _framebufferSource.Size = new PixelSize((int) (clientSize.Width * scaling), (int) (clientSize.Height * scaling));
            FireResizedIfNecessary();
        }

        private void FireResizedIfNecessary()
        {
            var size = ClientSize;
            if (_clientSizeCache != size)
            {
                _clientSizeCache = size;
                Resized?.Invoke(size);
            }
        }
        
        internal void PaintImpl()
        {
            var paint = Paint;
            if (paint == null)
                return;

            var updateTexture = _invalidRegion != Avalonia.Rect.Empty;
            if (updateTexture)
            {
                var paintArea = _invalidRegion.Intersect(new Avalonia.Rect(new Point(0, 0), ClientSize));
                _invalidRegion = Avalonia.Rect.Empty;
                if (paintArea.Width * paintArea.Height > 0)
                    paint?.Invoke(paintArea);
                //_hasUpdatedImage = true;
            }
        }
    }
}