using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Avalonia.Input;
using Avalonia.Platform;
using Urho3DNet.AvaliniaAdapter;
using Urho3DNet.InputEvents;

namespace Urho3DNet
{
    public class AvaloniaUrhoContext:IDisposable
    {
        private readonly object _windowsCollectionLock = new object();
        private readonly HashSet<UrhoTopLevelImpl> _windows = new HashSet<UrhoTopLevelImpl>();

        private readonly MainLoopDispatcher _mainLoopDispatcher;
        readonly HashSet<UrhoTopLevelImpl> _windowsToPaint = new HashSet<UrhoTopLevelImpl>();
        private readonly CoreEventsAdapter _coreEvents;
        private readonly InputEventsAdapter _inputEvents;

        public AvaloniaUrhoContext(Context context)
        {
            Context = context;
            _mainLoopDispatcher = new MainLoopDispatcher(context);
            MouseDevice = new MouseDevice();
            Screen = new UrhoScreenStub(this);

            var eventObject = new Object(context);
            _coreEvents = new CoreEventsAdapter(eventObject);
            _coreEvents.BeginFrame += ProcessWindows;
            _inputEvents = new InputEventsAdapter(eventObject);
            _inputEvents.ExitRequested += OnExitRequested;
        }

        private void OnExitRequested(object sender, InputEventsAdapter.ExitRequestedEventArgs e)
        {
        }

        public Context Context { get; }
        
        public IMouseDevice MouseDevice { get; }

        public IScreenImpl Screen { get; }

        private void ProcessWindows(object sender, CoreEventsAdapter.BeginFrameEventArgs e)
        {
            //Update textures
            if (_windowsToPaint.Count > 0)
            {
                var windowsToPaint = _windowsToPaint.ToList();
                _windowsToPaint.Clear();
                foreach (var window in windowsToPaint)
                {
                    window.PaintImpl();
                }
            }
        }

        public void SchedulePaint(UrhoTopLevelImpl window)
        {
            lock (_windowsCollectionLock)
            {
                _windowsToPaint.Add(window);
            }
        }

        public void EnsureInvokeOnMainThread(Action action)
        {
            _mainLoopDispatcher.InvokeOnMain(action);
        }

        public void Dispose()
        {
            _mainLoopDispatcher.Dispose();
            _coreEvents.Dispose();
            _inputEvents.Dispose();
        }

        public void RunLoop(CancellationToken cancellationToken)
        {
        }

        public void AddWindow(UrhoTopLevelImpl window)
        {
            lock (_windowsCollectionLock)
            {
                _windows.Add(window);
            }
        }

        internal void RemoveWindow(UrhoTopLevelImpl window)
        {
            lock (_windowsCollectionLock)
            {
                _windows.Remove(window);
            }
        }

    }
}
