using System;
using System.Diagnostics;
using Urho3DNet.InputEvents;

namespace Urho3DNet.Samples
{
    public class SamplesManager : Application
    {
        private InputAdapter _inputAdapter;
        private StatefulInputSource _currentSample;
        private bool isClosing_;
        private SampleList _list;

        public SamplesManager(Context context) : base(context)
        {
        }

        public override void Setup()
        {
            if (Debugger.IsAttached)
            {
                EngineParameters[Urho3D.EpFullScreen] = false;
                EngineParameters[Urho3D.EpWindowResizable] = true;
                EngineParameters[Urho3D.EpGpuDebug] = true;
                //EngineParameters[Urho3D.EpOpenXR] = true;
            }
            else
            {
                EngineParameters[Urho3D.EpFullScreen] = true;
            }

            EngineParameters[Urho3D.EpWindowTitle] = "SamplesManager";
            base.Setup();
        }

        public override void Start()
        {
            _inputAdapter = new InputAdapter(Context.Input);
            _currentSample = new StatefulInputSource(_inputAdapter);
            _list = new SampleList(Context);
            _currentSample.Listener = _list;

            var ui = Context.UI;
            var resourceCache = Context.ResourceCache;

            SubscribeToEvent(E.Released, OnClickSample);
            SubscribeToEvent(E.KeyUp, OnKeyPress);
            SubscribeToEvent(E.BeginFrame, OnFrameStart);
            SubscribeToEvent(E.LogMessage, OnLogMessage);

            // Register an object factory for our custom Rotator component so that we can create them to scene nodes
            //Context.RegisterFactory<Rotator>();

            Context.Engine.CreateDebugHud().ToggleAll();

            RegisterSample<SkiaSample>();

            base.Start();
        }

        private void OnLogMessage(VariantMap obj)
        {
            var level = (LogLevel)obj[E.LogMessage.Level].Int;
            var message = obj[E.LogMessage.Message].String;
            switch (level)
            {
                case LogLevel.LogError:
#if DEBUG
                    if (!message.Contains("Failed to register"))
                        throw new ApplicationException(message);
#endif
                    Trace.WriteLine(message);
                    break;
                default:
                    Trace.WriteLine(message);
                    break;
            }
        }

        public override void Stop()
        {
            StopRunningSample();

            Context.Engine.DumpResources(true);
            base.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void OnFrameStart(VariantMap obj)
        {
            if (isClosing_)
            {
                isClosing_ = false;
                if (_currentSample.Listener != null)
                {
                    StopRunningSample();
                }
                else
                {
                    var console = GetSubsystem<Console>();
                    if (console != null)
                        if (console.IsVisible)
                        {
                            console.IsVisible = false;
                            return;
                        }

                    Context.Engine.Exit();
                }
            }
        }

        private void OnKeyPress(VariantMap eventData)
        {
            var key = (Key)eventData[E.KeyDown.Key].Int;

            if (key == Key.KeyEscape)
                isClosing_ = true;
        }

        private void OnClickSample(VariantMap eventData)
        {
            if (_currentSample.Listener == _list)
            {
                var sampleType = ((UIElement)eventData[E.Released.Element].Ptr).Vars["SampleType"].String;
                if (string.IsNullOrWhiteSpace(sampleType))
                    return;

                StartSample(sampleType);
            }
        }

        private void StartSample(string sampleType)
        {
            var ui = Context.UI;
            ui.Root.RemoveAllChildren();
            ui.SetFocusElement(null);

            StopRunningSample();
            switch (sampleType)
            {
                case nameof(SkiaSample):
                    _currentSample.Listener = new SkiaSample(Context);
                    break;
            }
        }

        private void StopRunningSample()
        {
            var prevSample = _currentSample.Listener;
            _currentSample.Listener = null;
            if (prevSample is IDisposable disposableSample)
            {
                disposableSample.Dispose();
            }
        }

        private void RegisterSample<T>() where T : Sample
        {
            //Context.RegisterFactory<T>();
            
            _list.Add<T>();
        }
    }
}