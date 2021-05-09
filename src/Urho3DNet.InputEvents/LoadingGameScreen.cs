using System;

namespace Urho3DNet.InputEvents
{
    public class LoadingGameScreen : AbstractGameScreen, IDisposable
    {
        private readonly Action _complete;
        private ResourceEventsAdapter _resourceEvents;
        private string _lastLoadedResource;
        private readonly SharedPtr<Scene> _scene = new SharedPtr<Scene>();
        private Text _text;
        private ProgressBar _progressBar;

        public LoadingGameScreen(Context context, Action complete):base(context)
        {
            _complete = complete;

            DefaultFogColor = new Color(0.1f, 0.2f, 0.4f, 1.0f);
            IsMouseVisible = true;
            MouseMode = MouseMode.MmFree;

            var scene = new Scene(Context);
            SetViewport(0, null, scene);

            _scene = new Scene(Context);

            _resourceEvents = new ResourceEventsAdapter(SubscriptionObject);
            _resourceEvents.ResourceBackgroundLoaded += OnResourceBackgroundLoaded;
            
            _text = UIRoot.CreateChild<Text>();
            _text.SetStyleAuto();
            _text.VerticalAlignment = VerticalAlignment.VaTop;
            _text.HorizontalAlignment = HorizontalAlignment.HaCenter;
            _text.TextAlignment = HorizontalAlignment.HaCenter;

            _progressBar = UIRoot.CreateChild<ProgressBar>();
            _progressBar.SetStyleAuto();
            _progressBar.VerticalAlignment = VerticalAlignment.VaBottom;
            _progressBar.HorizontalAlignment = HorizontalAlignment.HaCenter;
        }

        private void OnResourceBackgroundLoaded(object sender, ResourceEventsAdapter.ResourceBackgroundLoadedEventArgs e)
        {
            _text.SetText(e.ResourceName);
        }

        public override void OnUpdate(CoreEventsAdapter.UpdateEventArgs arg)
        {
            base.OnUpdate(arg);

            _text.Position = new IntVector2(0, Graphics.Height/2);
            _text.Size = new IntVector2(Graphics.Width, Graphics.Height/2);

            _progressBar.Position = new IntVector2(0, 0);
            _progressBar.Size = new IntVector2(Graphics.Width, Graphics.Height/10);

            if (ResourceCache.NumBackgroundLoadResources == 0)
            {
                _text.SetText("");
                _complete();
            }
            else
            {
                if (MaxNumBackgroundLoadResources < ResourceCache.NumBackgroundLoadResources)
                {
                    MaxNumBackgroundLoadResources = ResourceCache.NumBackgroundLoadResources;
                }

                _progressBar.Value = 1.0f - ResourceCache.NumBackgroundLoadResources / (float)MaxNumBackgroundLoadResources;
            }
        }

        protected override void OnListenerSubscribed()
        {
            MaxNumBackgroundLoadResources = ResourceCache.NumBackgroundLoadResources;
            base.OnListenerSubscribed();
        }

        public uint MaxNumBackgroundLoadResources { get; set; }

        public void PrepareSceneResources(string scene)
        {
            var file = ResourceCache.GetFile(scene, true);
            if (file != null)
            {
                _scene.Value.LoadAsyncXML(file, LoadMode.LoadResourcesOnly);
            }
        }
    }
}