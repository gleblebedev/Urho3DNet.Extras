using System;
using System.Collections.Generic;
using System.Linq;

namespace Urho3DNet.InputEvents
{
    public class AbstractGameScreen : AbstractInputListener, IDisposable
    {
        private readonly Dictionary<uint, ViewportAndScene> _viewports = new Dictionary<uint, ViewportAndScene>();
        private readonly Dictionary<Scene, MusicPerScene> _musicSoundSources = new Dictionary<Scene, MusicPerScene>();

        private readonly SharedPtr<UIElement> _uiRoot;
        private readonly SharedPtr<ManagedEventDispatcher> _subscriptionObject;

        private IntVector2 _lastKnownGraphicsSize;
        private Color _defaultFogColor;
        private MouseMode _mouseMode = MouseMode.MmAbsolute;
        private MouseMode _prevMouseMode;
        private bool _isMouseVisible;
        private bool _prevMouseVisible;
        private CoreEventsAdapter _coreEventsAdapter;
        private float _musicGain = 0.2f;

        public ManagedEventDispatcher SubscriptionObject => _subscriptionObject.Value;

        public AbstractGameScreen(Context context)
        {
            Context = context;
            _uiRoot = new SharedPtr<UIElement>(new UIElement(context));
            _subscriptionObject = new ManagedEventDispatcher(Context);
            _coreEventsAdapter = new CoreEventsAdapter(_subscriptionObject);
            if (ResourceCache.Exists("UI/DefaultStyle.xml"))
            {
                var style = ResourceCache.GetResource<XMLFile>("UI/DefaultStyle.xml");
                if (style != null)
                    _uiRoot.Value.SetDefaultStyle(style);
            }
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

        public bool IsActive { get; private set; }

        public bool IsMouseVisible
        {
            get => _isMouseVisible;
            set
            {
                if (_isMouseVisible != value)
                {
                    _isMouseVisible = value;
                    if (IsActive) Input.SetMouseVisible(_isMouseVisible);
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
                    if (IsActive) Input.SetMouseMode(_mouseMode);
                }
            }
        }

        public float MusicGain
        {
            get => _musicGain;
            set
            {
                if (_musicGain != value)
                {
                    _musicGain = value;
                    foreach (var musicSoundSource in _musicSoundSources) musicSoundSource.Value.Gain = _musicGain;
                }
            }
        }

        public virtual void OnUpdate(CoreEventsAdapter.UpdateEventArgs arg)
        {
        }

        public void PlayMusic(string musicAssetName, Scene scene)
        {
            var source = GetOrCreateSoundSource(scene);
            source.Gain = MusicGain;
            source.Track = ResourceCache.GetResource<Sound>(musicAssetName);
            if (IsActive) source.Play();
        }

        public Scene GetScene()
        {
            return _viewports.Select(_ => _.Value?.Viewport?.Scene).FirstOrDefault(_ => _ != null);
        }

        public Camera GetCamera()
        {
            return _viewports.Select(_ => _.Value?.Viewport?.Camera).FirstOrDefault(_ => _ != null);
        }

        public void HandleUpdate(object sender, CoreEventsAdapter.UpdateEventArgs args)
        {
            if (!IsActive)
                return;
            var uiRoot = _uiRoot.Value;
            if (uiRoot.GetParent() == null) Context.UI.Root.AddChild(_uiRoot);

            var graphicsSize = Graphics.Size;
            if (_lastKnownGraphicsSize != graphicsSize)
            {
                _lastKnownGraphicsSize = graphicsSize;
                uiRoot.Position = IntVector2.Zero;
                uiRoot.Size = graphicsSize;
                OnResize(graphicsSize);
            }
            OnUpdate(args);
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
                var viewport = viewportPair.Value?.Viewport;
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
            if (_viewports.TryGetValue(index, out var viewportPtr)) viewportPtr.Dispose();
            _viewports[index] = new ViewportAndScene(viewport);
            if (InputSource != null) Renderer.SetViewport(index, viewport);
        }

        public void SetViewport(uint index, Camera camera = null, Scene scene = null)
        {
            SetViewport(index, new Viewport(Context)
            {
                Camera = camera,
                Scene = scene ?? camera?.Node?.Scene
            });
        }
        
        public Viewport GetViewport(uint index)
        {
            if (_viewports.TryGetValue(index, out var viewportAndScene))
            {
                return viewportAndScene.Viewport;
            }

            return null;
        }

        public void Dispose()
        {
            FallbackInputListener = null;
            _uiRoot.Dispose();
            _coreEventsAdapter.Dispose();
            _coreEventsAdapter = null;
            _subscriptionObject.Dispose();
            foreach (var viewport in _viewports) viewport.Value.Dispose();
            Dispose(true);
        }

        protected virtual void OnResize(IntVector2 graphicsSize)
        {
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        protected virtual void OnRenderUpdate(CoreEventsAdapter.RenderUpdateEventArgs args)
        {
        }

        protected virtual void OnPostRenderUpdate(CoreEventsAdapter.PostRenderUpdateEventArgs args)
        {
        }

        protected override void OnListenerSubscribed()
        {
            IsActive = true;
            foreach (var viewport in _viewports) Renderer.SetViewport(viewport.Key, viewport.Value.Viewport);

            Renderer.DefaultZone.FogColor = _defaultFogColor;

            _coreEventsAdapter.Update += HandleUpdate;
            _coreEventsAdapter.RenderUpdate += HandleRenderUpdate;
            _coreEventsAdapter.PostRenderUpdate += HandlePostRenderUpdate;

            _prevMouseMode = Input.GetMouseMode();
            Input.SetMouseMode(_mouseMode);

            _prevMouseVisible = Input.IsMouseVisible();
            Input.SetMouseVisible(_isMouseVisible);

            foreach (var musicSoundSource in _musicSoundSources) musicSoundSource.Value.Resume();
        }

        protected override void OnListenerUnsubscribed()
        {
            foreach (var viewport in _viewports) Renderer.SetViewport(viewport.Key, null);

            _coreEventsAdapter.Update -= HandleUpdate;

            Context.UI.Root.RemoveChild(_uiRoot);

            Input.SetMouseMode(_prevMouseMode);
            Input.SetMouseVisible(_prevMouseVisible);

            foreach (var musicSoundSource in _musicSoundSources) musicSoundSource.Value.Pause();
        }

        private void HandleRenderUpdate(object sender, CoreEventsAdapter.RenderUpdateEventArgs e)
        {
    
            OnRenderUpdate(e);
        }

        private void HandlePostRenderUpdate(object sender, CoreEventsAdapter.PostRenderUpdateEventArgs e)
        {
            OnPostRenderUpdate(e);
        }

        private MusicPerScene GetOrCreateSoundSource(Scene scene)
        {
            if (_musicSoundSources.TryGetValue(scene, out var soundSource)) return soundSource;

            var soundSourceNode = scene.GetChild("MusicSoundSource", false);
            if (soundSourceNode == null) soundSourceNode = scene.CreateChild("MusicSoundSource", CreateMode.Local);

            soundSource = new MusicPerScene
            {
                SoundSource = soundSourceNode.GetOrCreateComponent<SoundSource>()
            };
            _musicSoundSources.Add(scene, soundSource);
            return soundSource;
        }

        public struct ViewportRay
        {
            public Viewport Viewport;
            public Ray Ray;
        }

        private class ViewportAndScene : IDisposable
        {
            private readonly SharedPtr<Viewport> _viewport;
            private readonly SharedPtr<Scene> _scene;

            public ViewportAndScene(Viewport viewport)
            {
                _viewport = viewport;
                _scene = viewport.Scene;
            }

            public Viewport Viewport => _viewport?.Value;

            public void Dispose()
            {
                _scene.Dispose();
                _viewport.Dispose();
            }
        }

        private class MusicPerScene
        {
            public SoundSource SoundSource;
            public Sound Track;
            public float TimePosition;

            public float Gain
            {
                get => SoundSource.Gain;
                set => SoundSource.Gain = value;
            }

            public void Play()
            {
                SoundSource?.Play(Track);
            }

            public void Pause()
            {
                TimePosition = SoundSource.TimePosition;
                SoundSource.Stop();
            }

            public void Resume()
            {
                SoundSource.Play(Track);
                //SoundSource.TimePosition = SoundSource.TimePosition;
            }
        }
    }
}