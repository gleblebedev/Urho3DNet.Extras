using System;
using System.Collections.Generic;

namespace Urho3DNet.InputEvents
{
    public class AbstractGameScreen : AbstractInputListener, IDisposable
    {
        private readonly Dictionary<uint, Viewport> _viewports = new Dictionary<uint, Viewport>();

        private readonly SharedPtr<UIElement> _uiRoot;

        private Color _defaultFogColor;
        private MouseMode _mouseMode = MouseMode.MmAbsolute;
        private MouseMode _prevMouseMode;
        private bool _isMouseVisible = false;
        private bool _prevMouseVisible;

        public AbstractGameScreen(Context context)
        {
            Context = context;
            _uiRoot = new SharedPtr<UIElement>(new UIElement(context));
            var style = ResourceCache.GetResource<XMLFile>("UI/DefaultStyle.xml");
            if (style != null)
                _uiRoot.Value.SetDefaultStyle(style);
        }

        public Context Context { get; }

        public Input Input => Context.Input;

        public ResourceCache ResourceCache => Context.ResourceCache;

        public Renderer Renderer => Context.Renderer;

        public Graphics Graphics => Context.Graphics;

        public Color DefaultFogColor
        {
            get => _defaultFogColor;
            set
            {
                if (_defaultFogColor != value)
                {
                    _defaultFogColor = value;
                    if (InputSource != null)
                        Renderer.DefaultZone.FogColor = _defaultFogColor;
                }
            }
        }

        public UIElement UIRoot => _uiRoot;

        public void HandleUpdate(VariantMap eventData)
        {
            if (!IsActive)
                return;
            var uiRoot = _uiRoot.Value;
            if (uiRoot.GetParent() == null)
            {
                Context.UI.Root.AddChild(_uiRoot);
            }
            uiRoot.Position = IntVector2.Zero;
            uiRoot.Size = Graphics.Size;
            uiRoot.Size = new IntVector2(1920, 1280);
            OnUpdate(eventData[E.Update.TimeStep].Float);
        }

        public virtual void OnUpdate(float timeStep)
        {
        }

        public void Raycast(int x, int y, RayQueryResultList result, RayQueryLevel level, float maxDistance,
            DrawableFlags drawableFlags)
        {
            var screenRay = GetScreenRay(x, y);
            if (screenRay.Viewport != null)
            {
                var octree = screenRay.Viewport.Scene.GetComponent<Octree>();
                octree.Raycast(new RayOctreeQuery(result, screenRay.Ray, level, maxDistance, drawableFlags));
            }
        }

        public void RaycastSingle(int x, int y, RayQueryResultList result, RayQueryLevel level, float maxDistance,
            DrawableFlags drawableFlags)
        {
            var screenRay = GetScreenRay(x, y);
            if (screenRay.Viewport != null)
            {
                var octree = screenRay.Viewport.Scene.GetComponent<Octree>();
                octree.RaycastSingle(new RayOctreeQuery(result, screenRay.Ray, level, maxDistance, drawableFlags));
            }
        }

        public ViewportRay GetScreenRay(int x, int y)
        {
            foreach (var viewportPair in _viewports)
            {
                var viewport = viewportPair.Value;
                var view = viewport?.View;
                if (view != null)
                {
                    var rect = viewport.View.ViewRect;
                    if (rect.Min.X <= x && rect.Max.X >= x && rect.Min.Y <= y && rect.Max.Y >= y)
                        return new ViewportRay
                        {
                            Viewport = viewport,
                            Ray = viewport.Camera.GetScreenRay((x - rect.Min.X) / (float) rect.Width,
                                (y - rect.Min.Y) / (float) rect.Height)
                        };
                }
            }

            return new ViewportRay();
        }

        public void SetViewport(uint index, Viewport viewport)
        {
            _viewports[index] = viewport;
            if (InputSource != null) Renderer.SetViewport(index, viewport);
        }

        public virtual void Dispose()
        {
            _uiRoot.Dispose();
        }

        protected override void OnListenerSubscribed()
        {
            IsActive = true;
            foreach (var viewport in _viewports) Renderer.SetViewport(viewport.Key, viewport.Value);

            Renderer.DefaultZone.FogColor = _defaultFogColor;

            _uiRoot.Value.SubscribeToEvent(E.Update, HandleUpdate);

            _prevMouseMode = Input.GetMouseMode();
            Input.SetMouseMode(_mouseMode);

            _prevMouseVisible = Input.IsMouseVisible();
            Input.SetMouseVisible(_isMouseVisible);
        }

        public bool IsActive { get; private set; }

        public bool IsMouseVisible
        {
            get => _isMouseVisible;
            set
            {
                if (_isMouseVisible != value)
                {
                    _isMouseVisible = value;
                    if (IsActive)
                    {
                        Input.SetMouseVisible(_isMouseVisible);
                    }
                }
            }
        }

        public MouseMode MouseMode
        {
            get => _mouseMode;
            set
            {
                if (_mouseMode != value)
                {
                    _mouseMode = value;
                    if (IsActive)
                    {
                        Input.SetMouseMode(_mouseMode);
                    }
                }
            }
        }

        protected override void OnListenerUnsubscribed()
        {
            foreach (var viewport in _viewports) Renderer.SetViewport(viewport.Key, null);

            _uiRoot.Value.UnsubscribeFromEvent(E.Update);

            Context.UI.Root.RemoveChild(_uiRoot);
            
            Input.SetMouseMode(_prevMouseMode);
            Input.SetMouseVisible(_prevMouseVisible);
        }

        public struct ViewportRay
        {
            public Viewport Viewport;
            public Ray Ray;
        }
    }
}