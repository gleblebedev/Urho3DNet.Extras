using System;
using System.Diagnostics;

namespace Urho3DNet.InputEvents
{
    public class AbstractApplication : Application
    {
        private InputAdapter _inputAdapter;
        private StatefulInputSource _inputDispatcher;

        public AbstractApplication(Context context) : base(context)
        {
        }

        public override void Setup()
        {
            if (Debugger.IsAttached)
                EngineParameters[Urho3D.EpFullScreen] = false;
            base.Setup();
        }

        public AbstractGameScreen GameScreen
        {
            get
            {
                return _inputDispatcher.Listener as AbstractGameScreen;
            }
            set
            {
                _inputDispatcher.Listener = value;
            }
        }

        public override void Start()
        {
            _inputAdapter = new InputAdapter(Context.Input);
            _inputDispatcher = new StatefulInputSource(_inputAdapter);
            SubscribeToEvent(E.LogMessage, OnLogMessage);
            base.Start();
        }

        protected override void Dispose(bool disposing)
        {
            GameScreen = null;
            _inputAdapter?.Dispose();
            base.Dispose(disposing);
        }

        private void OnLogMessage(VariantMap obj)
        {
            var level = (LogLevel)obj[E.LogMessage.Level].Int;
            var message = obj[E.LogMessage.Message].String;
            switch (level)
            {
                case LogLevel.LogError:
#if DEBUG
                    //throw new ApplicationException(message);
#endif
                    Trace.WriteLine(message);
                    break;
                default:
                    Trace.WriteLine(message);
                    break;
            }
        }

    }
}