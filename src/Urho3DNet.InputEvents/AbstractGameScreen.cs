using System;
using System.Collections.Generic;

namespace Urho3DNet.InputEvents
{
    public class AbstractGameScreen : AbstractInputListener, IDisposable
    {
        private readonly Dictionary<uint, Viewport> _viewports = new Dictionary<uint, Viewport>();

        private readonly SharedPtr<UIElement> _uiRoot;

        private Color _defaultFogColor;

        public AbstractGameScreen(Context context)
        {
            Context = context;
            _uiRoot = new SharedPtr<UIElement>(new UIElement(context));
            _uiRoot.Value.SetDefaultStyle(Context.ResourceCache.GetResource<XMLFile>("UI/DefaultStyle.xml"));
        }

        public Context Context { get; }

        public Input Input => Context.Input;

        public ResourceCache ResourceCache => Context.ResourceCache;

        public Renderer Renderer => Context.Renderer;

        public Graphics Graphics => Context.Graphics;

        public bool _needsInitialization;

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

        public virtual void OnUpdate(float timestep)
        {
            if (_needsInitialization)
            {
                Context.UI.Root.AddChild(_uiRoot);
                _needsInitialization = false;
            }
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
            foreach (var viewport in _viewports) Renderer.SetViewport(viewport.Key, viewport.Value);

            Renderer.DefaultZone.FogColor = _defaultFogColor;

            _needsInitialization = true;
        }

        protected override void OnListenerUnsubscribed()
        {
            foreach (var viewport in _viewports) Renderer.SetViewport(viewport.Key, null);

            Context.UI.Root.RemoveChild(_uiRoot);

            _needsInitialization = false;
        }

        public UIElement UIRoot => _uiRoot;
        
        public struct ViewportRay
        {
            public Viewport Viewport;
            public Ray Ray;
        }
    }
}