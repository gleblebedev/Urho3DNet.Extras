using System;

namespace Urho3DNet.InputEvents
{
    public partial class AudioEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public AudioEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region SoundFinished
        // -------------------------------------------- SoundFinished --------------------------------------------

        /// <summary>
        /// Sound playback finished. Sent through the SoundSource's Node.
        /// </summary>
        public class SoundFinishedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.SoundFinished.Node].VoidPtr;
                SoundSource = eventData[E.SoundFinished.SoundSource].VoidPtr;
                Sound = eventData[E.SoundFinished.Sound].VoidPtr;
            }

            public IntPtr Node { get; private set; }

            public IntPtr SoundSource { get; private set; }

            public IntPtr Sound { get; private set; }
        }

        private event EventHandler<SoundFinishedEventArgs> SoundFinishedImpl;

        private readonly SoundFinishedEventArgs _SoundFinishedEventArgs = new SoundFinishedEventArgs();
        
        /// <summary>
        /// Sound playback finished. Sent through the SoundSource's Node.
        /// </summary>
        public event EventHandler<SoundFinishedEventArgs> SoundFinished
        {
            add
            {
                if (SoundFinishedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.SoundFinished, HandleSoundFinished);
                }
                SoundFinishedImpl += value;
            }
            remove
            {
                SoundFinishedImpl -= value;
                if (SoundFinishedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.SoundFinished);
                }
            }
        }

        private void HandleSoundFinished(VariantMap eventData)
        {
            _SoundFinishedEventArgs.Set(eventData);
            SoundFinishedImpl?.Invoke(_urhoObject.Value, _SoundFinishedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (SoundFinishedImpl != null) urhoObject.UnsubscribeFromEvent(E.SoundFinished);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class CoreEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public CoreEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region BeginFrame
        // -------------------------------------------- BeginFrame --------------------------------------------

        /// <summary>
        /// Frame begin event.
        /// </summary>
        public class BeginFrameEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                FrameNumber = eventData[E.BeginFrame.FrameNumber].UInt;
                TimeStep = eventData[E.BeginFrame.TimeStep].Float;
            }

            public uint FrameNumber { get; private set; }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<BeginFrameEventArgs> BeginFrameImpl;

        private readonly BeginFrameEventArgs _BeginFrameEventArgs = new BeginFrameEventArgs();
        
        /// <summary>
        /// Frame begin event.
        /// </summary>
        public event EventHandler<BeginFrameEventArgs> BeginFrame
        {
            add
            {
                if (BeginFrameImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.BeginFrame, HandleBeginFrame);
                }
                BeginFrameImpl += value;
            }
            remove
            {
                BeginFrameImpl -= value;
                if (BeginFrameImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.BeginFrame);
                }
            }
        }

        private void HandleBeginFrame(VariantMap eventData)
        {
            _BeginFrameEventArgs.Set(eventData);
            BeginFrameImpl?.Invoke(_urhoObject.Value, _BeginFrameEventArgs);
        }

        #endregion

        #region Update
        // -------------------------------------------- Update --------------------------------------------

        /// <summary>
        /// Application-wide logic update event.
        /// </summary>
        public class UpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                TimeStep = eventData[E.Update.TimeStep].Float;
            }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<UpdateEventArgs> UpdateImpl;

        private readonly UpdateEventArgs _UpdateEventArgs = new UpdateEventArgs();
        
        /// <summary>
        /// Application-wide logic update event.
        /// </summary>
        public event EventHandler<UpdateEventArgs> Update
        {
            add
            {
                if (UpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.Update, HandleUpdate);
                }
                UpdateImpl += value;
            }
            remove
            {
                UpdateImpl -= value;
                if (UpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.Update);
                }
            }
        }

        private void HandleUpdate(VariantMap eventData)
        {
            _UpdateEventArgs.Set(eventData);
            UpdateImpl?.Invoke(_urhoObject.Value, _UpdateEventArgs);
        }

        #endregion

        #region PostUpdate
        // -------------------------------------------- PostUpdate --------------------------------------------

        /// <summary>
        /// Application-wide logic post-update event.
        /// </summary>
        public class PostUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                TimeStep = eventData[E.PostUpdate.TimeStep].Float;
            }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<PostUpdateEventArgs> PostUpdateImpl;

        private readonly PostUpdateEventArgs _PostUpdateEventArgs = new PostUpdateEventArgs();
        
        /// <summary>
        /// Application-wide logic post-update event.
        /// </summary>
        public event EventHandler<PostUpdateEventArgs> PostUpdate
        {
            add
            {
                if (PostUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PostUpdate, HandlePostUpdate);
                }
                PostUpdateImpl += value;
            }
            remove
            {
                PostUpdateImpl -= value;
                if (PostUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PostUpdate);
                }
            }
        }

        private void HandlePostUpdate(VariantMap eventData)
        {
            _PostUpdateEventArgs.Set(eventData);
            PostUpdateImpl?.Invoke(_urhoObject.Value, _PostUpdateEventArgs);
        }

        #endregion

        #region RenderUpdate
        // -------------------------------------------- RenderUpdate --------------------------------------------

        /// <summary>
        /// Render update event.
        /// </summary>
        public class RenderUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                TimeStep = eventData[E.RenderUpdate.TimeStep].Float;
            }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<RenderUpdateEventArgs> RenderUpdateImpl;

        private readonly RenderUpdateEventArgs _RenderUpdateEventArgs = new RenderUpdateEventArgs();
        
        /// <summary>
        /// Render update event.
        /// </summary>
        public event EventHandler<RenderUpdateEventArgs> RenderUpdate
        {
            add
            {
                if (RenderUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.RenderUpdate, HandleRenderUpdate);
                }
                RenderUpdateImpl += value;
            }
            remove
            {
                RenderUpdateImpl -= value;
                if (RenderUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.RenderUpdate);
                }
            }
        }

        private void HandleRenderUpdate(VariantMap eventData)
        {
            _RenderUpdateEventArgs.Set(eventData);
            RenderUpdateImpl?.Invoke(_urhoObject.Value, _RenderUpdateEventArgs);
        }

        #endregion

        #region PostRenderUpdate
        // -------------------------------------------- PostRenderUpdate --------------------------------------------

        /// <summary>
        /// Post-render update event.
        /// </summary>
        public class PostRenderUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                TimeStep = eventData[E.PostRenderUpdate.TimeStep].Float;
            }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<PostRenderUpdateEventArgs> PostRenderUpdateImpl;

        private readonly PostRenderUpdateEventArgs _PostRenderUpdateEventArgs = new PostRenderUpdateEventArgs();
        
        /// <summary>
        /// Post-render update event.
        /// </summary>
        public event EventHandler<PostRenderUpdateEventArgs> PostRenderUpdate
        {
            add
            {
                if (PostRenderUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PostRenderUpdate, HandlePostRenderUpdate);
                }
                PostRenderUpdateImpl += value;
            }
            remove
            {
                PostRenderUpdateImpl -= value;
                if (PostRenderUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PostRenderUpdate);
                }
            }
        }

        private void HandlePostRenderUpdate(VariantMap eventData)
        {
            _PostRenderUpdateEventArgs.Set(eventData);
            PostRenderUpdateImpl?.Invoke(_urhoObject.Value, _PostRenderUpdateEventArgs);
        }

        #endregion

        #region EndFrame
        // -------------------------------------------- EndFrame --------------------------------------------

        /// <summary>
        /// Frame end event.
        /// </summary>
        public class EndFrameEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<EndFrameEventArgs> EndFrameImpl;

        private readonly EndFrameEventArgs _EndFrameEventArgs = new EndFrameEventArgs();
        
        /// <summary>
        /// Frame end event.
        /// </summary>
        public event EventHandler<EndFrameEventArgs> EndFrame
        {
            add
            {
                if (EndFrameImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.EndFrame, HandleEndFrame);
                }
                EndFrameImpl += value;
            }
            remove
            {
                EndFrameImpl -= value;
                if (EndFrameImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.EndFrame);
                }
            }
        }

        private void HandleEndFrame(VariantMap eventData)
        {
            _EndFrameEventArgs.Set(eventData);
            EndFrameImpl?.Invoke(_urhoObject.Value, _EndFrameEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (BeginFrameImpl != null) urhoObject.UnsubscribeFromEvent(E.BeginFrame);
                if (UpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.Update);
                if (PostUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.PostUpdate);
                if (RenderUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.RenderUpdate);
                if (PostRenderUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.PostRenderUpdate);
                if (EndFrameImpl != null) urhoObject.UnsubscribeFromEvent(E.EndFrame);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class WorkQueueAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public WorkQueueAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region WorkItemCompleted
        // -------------------------------------------- WorkItemCompleted --------------------------------------------

        /// <summary>
        /// Work item completed event.
        /// </summary>
        public class WorkItemCompletedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Item = eventData[E.WorkItemCompleted.Item].VoidPtr;
            }

            public IntPtr Item { get; private set; }
        }

        private event EventHandler<WorkItemCompletedEventArgs> WorkItemCompletedImpl;

        private readonly WorkItemCompletedEventArgs _WorkItemCompletedEventArgs = new WorkItemCompletedEventArgs();
        
        /// <summary>
        /// Work item completed event.
        /// </summary>
        public event EventHandler<WorkItemCompletedEventArgs> WorkItemCompleted
        {
            add
            {
                if (WorkItemCompletedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.WorkItemCompleted, HandleWorkItemCompleted);
                }
                WorkItemCompletedImpl += value;
            }
            remove
            {
                WorkItemCompletedImpl -= value;
                if (WorkItemCompletedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.WorkItemCompleted);
                }
            }
        }

        private void HandleWorkItemCompleted(VariantMap eventData)
        {
            _WorkItemCompletedEventArgs.Set(eventData);
            WorkItemCompletedImpl?.Invoke(_urhoObject.Value, _WorkItemCompletedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (WorkItemCompletedImpl != null) urhoObject.UnsubscribeFromEvent(E.WorkItemCompleted);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class EngineEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public EngineEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region ConsoleCommand
        // -------------------------------------------- ConsoleCommand --------------------------------------------

        /// <summary>
        /// A command has been entered on the console.
        /// </summary>
        public class ConsoleCommandEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Command = eventData[E.ConsoleCommand.Command].String;
                Id = eventData[E.ConsoleCommand.Id].String;
            }

            public String Command { get; private set; }

            public String Id { get; private set; }
        }

        private event EventHandler<ConsoleCommandEventArgs> ConsoleCommandImpl;

        private readonly ConsoleCommandEventArgs _ConsoleCommandEventArgs = new ConsoleCommandEventArgs();
        
        /// <summary>
        /// A command has been entered on the console.
        /// </summary>
        public event EventHandler<ConsoleCommandEventArgs> ConsoleCommand
        {
            add
            {
                if (ConsoleCommandImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ConsoleCommand, HandleConsoleCommand);
                }
                ConsoleCommandImpl += value;
            }
            remove
            {
                ConsoleCommandImpl -= value;
                if (ConsoleCommandImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ConsoleCommand);
                }
            }
        }

        private void HandleConsoleCommand(VariantMap eventData)
        {
            _ConsoleCommandEventArgs.Set(eventData);
            ConsoleCommandImpl?.Invoke(_urhoObject.Value, _ConsoleCommandEventArgs);
        }

        #endregion

        #region ConsoleUriClick
        // -------------------------------------------- ConsoleUriClick --------------------------------------------

        /// <summary>
        /// A command has been entered on the console.
        /// </summary>
        public class ConsoleUriClickEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Address = eventData[E.ConsoleUriClick.Address].String;
                Protocol = eventData[E.ConsoleUriClick.Protocol].String;
            }

            public String Address { get; private set; }

            public String Protocol { get; private set; }
        }

        private event EventHandler<ConsoleUriClickEventArgs> ConsoleUriClickImpl;

        private readonly ConsoleUriClickEventArgs _ConsoleUriClickEventArgs = new ConsoleUriClickEventArgs();
        
        /// <summary>
        /// A command has been entered on the console.
        /// </summary>
        public event EventHandler<ConsoleUriClickEventArgs> ConsoleUriClick
        {
            add
            {
                if (ConsoleUriClickImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ConsoleUriClick, HandleConsoleUriClick);
                }
                ConsoleUriClickImpl += value;
            }
            remove
            {
                ConsoleUriClickImpl -= value;
                if (ConsoleUriClickImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ConsoleUriClick);
                }
            }
        }

        private void HandleConsoleUriClick(VariantMap eventData)
        {
            _ConsoleUriClickEventArgs.Set(eventData);
            ConsoleUriClickImpl?.Invoke(_urhoObject.Value, _ConsoleUriClickEventArgs);
        }

        #endregion

        #region EngineInitialized
        // -------------------------------------------- EngineInitialized --------------------------------------------

        /// <summary>
        /// Engine finished initialization, but Application::Start() was not claled yet.
        /// </summary>
        public class EngineInitializedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<EngineInitializedEventArgs> EngineInitializedImpl;

        private readonly EngineInitializedEventArgs _EngineInitializedEventArgs = new EngineInitializedEventArgs();
        
        /// <summary>
        /// Engine finished initialization, but Application::Start() was not claled yet.
        /// </summary>
        public event EventHandler<EngineInitializedEventArgs> EngineInitialized
        {
            add
            {
                if (EngineInitializedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.EngineInitialized, HandleEngineInitialized);
                }
                EngineInitializedImpl += value;
            }
            remove
            {
                EngineInitializedImpl -= value;
                if (EngineInitializedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.EngineInitialized);
                }
            }
        }

        private void HandleEngineInitialized(VariantMap eventData)
        {
            _EngineInitializedEventArgs.Set(eventData);
            EngineInitializedImpl?.Invoke(_urhoObject.Value, _EngineInitializedEventArgs);
        }

        #endregion

        #region ApplicationStarted
        // -------------------------------------------- ApplicationStarted --------------------------------------------

        /// <summary>
        /// Application started, but first frame was not executed yet.
        /// </summary>
        public class ApplicationStartedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ApplicationStartedEventArgs> ApplicationStartedImpl;

        private readonly ApplicationStartedEventArgs _ApplicationStartedEventArgs = new ApplicationStartedEventArgs();
        
        /// <summary>
        /// Application started, but first frame was not executed yet.
        /// </summary>
        public event EventHandler<ApplicationStartedEventArgs> ApplicationStarted
        {
            add
            {
                if (ApplicationStartedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ApplicationStarted, HandleApplicationStarted);
                }
                ApplicationStartedImpl += value;
            }
            remove
            {
                ApplicationStartedImpl -= value;
                if (ApplicationStartedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ApplicationStarted);
                }
            }
        }

        private void HandleApplicationStarted(VariantMap eventData)
        {
            _ApplicationStartedEventArgs.Set(eventData);
            ApplicationStartedImpl?.Invoke(_urhoObject.Value, _ApplicationStartedEventArgs);
        }

        #endregion

        #region PluginLoad
        // -------------------------------------------- PluginLoad --------------------------------------------

        /// <summary>
        /// Plugin::Load() is about to get called.
        /// </summary>
        public class PluginLoadEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<PluginLoadEventArgs> PluginLoadImpl;

        private readonly PluginLoadEventArgs _PluginLoadEventArgs = new PluginLoadEventArgs();
        
        /// <summary>
        /// Plugin::Load() is about to get called.
        /// </summary>
        public event EventHandler<PluginLoadEventArgs> PluginLoad
        {
            add
            {
                if (PluginLoadImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PluginLoad, HandlePluginLoad);
                }
                PluginLoadImpl += value;
            }
            remove
            {
                PluginLoadImpl -= value;
                if (PluginLoadImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PluginLoad);
                }
            }
        }

        private void HandlePluginLoad(VariantMap eventData)
        {
            _PluginLoadEventArgs.Set(eventData);
            PluginLoadImpl?.Invoke(_urhoObject.Value, _PluginLoadEventArgs);
        }

        #endregion

        #region PluginUnload
        // -------------------------------------------- PluginUnload --------------------------------------------

        /// <summary>
        /// Plugin::Unload() is about to get called.
        /// </summary>
        public class PluginUnloadEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<PluginUnloadEventArgs> PluginUnloadImpl;

        private readonly PluginUnloadEventArgs _PluginUnloadEventArgs = new PluginUnloadEventArgs();
        
        /// <summary>
        /// Plugin::Unload() is about to get called.
        /// </summary>
        public event EventHandler<PluginUnloadEventArgs> PluginUnload
        {
            add
            {
                if (PluginUnloadImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PluginUnload, HandlePluginUnload);
                }
                PluginUnloadImpl += value;
            }
            remove
            {
                PluginUnloadImpl -= value;
                if (PluginUnloadImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PluginUnload);
                }
            }
        }

        private void HandlePluginUnload(VariantMap eventData)
        {
            _PluginUnloadEventArgs.Set(eventData);
            PluginUnloadImpl?.Invoke(_urhoObject.Value, _PluginUnloadEventArgs);
        }

        #endregion

        #region PluginStart
        // -------------------------------------------- PluginStart --------------------------------------------

        /// <summary>
        /// Plugin::Start() is about to get called.
        /// </summary>
        public class PluginStartEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<PluginStartEventArgs> PluginStartImpl;

        private readonly PluginStartEventArgs _PluginStartEventArgs = new PluginStartEventArgs();
        
        /// <summary>
        /// Plugin::Start() is about to get called.
        /// </summary>
        public event EventHandler<PluginStartEventArgs> PluginStart
        {
            add
            {
                if (PluginStartImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PluginStart, HandlePluginStart);
                }
                PluginStartImpl += value;
            }
            remove
            {
                PluginStartImpl -= value;
                if (PluginStartImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PluginStart);
                }
            }
        }

        private void HandlePluginStart(VariantMap eventData)
        {
            _PluginStartEventArgs.Set(eventData);
            PluginStartImpl?.Invoke(_urhoObject.Value, _PluginStartEventArgs);
        }

        #endregion

        #region PluginStop
        // -------------------------------------------- PluginStop --------------------------------------------

        /// <summary>
        /// Plugin::Stop() is about to get called.
        /// </summary>
        public class PluginStopEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<PluginStopEventArgs> PluginStopImpl;

        private readonly PluginStopEventArgs _PluginStopEventArgs = new PluginStopEventArgs();
        
        /// <summary>
        /// Plugin::Stop() is about to get called.
        /// </summary>
        public event EventHandler<PluginStopEventArgs> PluginStop
        {
            add
            {
                if (PluginStopImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PluginStop, HandlePluginStop);
                }
                PluginStopImpl += value;
            }
            remove
            {
                PluginStopImpl -= value;
                if (PluginStopImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PluginStop);
                }
            }
        }

        private void HandlePluginStop(VariantMap eventData)
        {
            _PluginStopEventArgs.Set(eventData);
            PluginStopImpl?.Invoke(_urhoObject.Value, _PluginStopEventArgs);
        }

        #endregion

        #region RegisterStaticPlugins
        // -------------------------------------------- RegisterStaticPlugins --------------------------------------------

        /// <summary>
        /// A request for user to manually register static plugins.
        /// </summary>
        public class RegisterStaticPluginsEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<RegisterStaticPluginsEventArgs> RegisterStaticPluginsImpl;

        private readonly RegisterStaticPluginsEventArgs _RegisterStaticPluginsEventArgs = new RegisterStaticPluginsEventArgs();
        
        /// <summary>
        /// A request for user to manually register static plugins.
        /// </summary>
        public event EventHandler<RegisterStaticPluginsEventArgs> RegisterStaticPlugins
        {
            add
            {
                if (RegisterStaticPluginsImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.RegisterStaticPlugins, HandleRegisterStaticPlugins);
                }
                RegisterStaticPluginsImpl += value;
            }
            remove
            {
                RegisterStaticPluginsImpl -= value;
                if (RegisterStaticPluginsImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.RegisterStaticPlugins);
                }
            }
        }

        private void HandleRegisterStaticPlugins(VariantMap eventData)
        {
            _RegisterStaticPluginsEventArgs.Set(eventData);
            RegisterStaticPluginsImpl?.Invoke(_urhoObject.Value, _RegisterStaticPluginsEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (ConsoleCommandImpl != null) urhoObject.UnsubscribeFromEvent(E.ConsoleCommand);
                if (ConsoleUriClickImpl != null) urhoObject.UnsubscribeFromEvent(E.ConsoleUriClick);
                if (EngineInitializedImpl != null) urhoObject.UnsubscribeFromEvent(E.EngineInitialized);
                if (ApplicationStartedImpl != null) urhoObject.UnsubscribeFromEvent(E.ApplicationStarted);
                if (PluginLoadImpl != null) urhoObject.UnsubscribeFromEvent(E.PluginLoad);
                if (PluginUnloadImpl != null) urhoObject.UnsubscribeFromEvent(E.PluginUnload);
                if (PluginStartImpl != null) urhoObject.UnsubscribeFromEvent(E.PluginStart);
                if (PluginStopImpl != null) urhoObject.UnsubscribeFromEvent(E.PluginStop);
                if (RegisterStaticPluginsImpl != null) urhoObject.UnsubscribeFromEvent(E.RegisterStaticPlugins);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class DrawableEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public DrawableEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region BoneHierarchyCreated
        // -------------------------------------------- BoneHierarchyCreated --------------------------------------------

        /// <summary>
        /// AnimatedModel bone hierarchy created.
        /// </summary>
        public class BoneHierarchyCreatedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.BoneHierarchyCreated.Node].VoidPtr;
            }

            public IntPtr Node { get; private set; }
        }

        private event EventHandler<BoneHierarchyCreatedEventArgs> BoneHierarchyCreatedImpl;

        private readonly BoneHierarchyCreatedEventArgs _BoneHierarchyCreatedEventArgs = new BoneHierarchyCreatedEventArgs();
        
        /// <summary>
        /// AnimatedModel bone hierarchy created.
        /// </summary>
        public event EventHandler<BoneHierarchyCreatedEventArgs> BoneHierarchyCreated
        {
            add
            {
                if (BoneHierarchyCreatedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.BoneHierarchyCreated, HandleBoneHierarchyCreated);
                }
                BoneHierarchyCreatedImpl += value;
            }
            remove
            {
                BoneHierarchyCreatedImpl -= value;
                if (BoneHierarchyCreatedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.BoneHierarchyCreated);
                }
            }
        }

        private void HandleBoneHierarchyCreated(VariantMap eventData)
        {
            _BoneHierarchyCreatedEventArgs.Set(eventData);
            BoneHierarchyCreatedImpl?.Invoke(_urhoObject.Value, _BoneHierarchyCreatedEventArgs);
        }

        #endregion

        #region AnimationTrigger
        // -------------------------------------------- AnimationTrigger --------------------------------------------

        /// <summary>
        /// AnimatedModel animation trigger.
        /// </summary>
        public class AnimationTriggerEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.AnimationTrigger.Node].VoidPtr;
                Animation = eventData[E.AnimationTrigger.Animation].VoidPtr;
                Name = eventData[E.AnimationTrigger.Name].String;
                Time = eventData[E.AnimationTrigger.Time].Float;
                Data = eventData[E.AnimationTrigger.Data];
            }

            public IntPtr Node { get; private set; }

            public IntPtr Animation { get; private set; }

            public String Name { get; private set; }

            public float Time { get; private set; }

            public Variant Data { get; private set; }
        }

        private event EventHandler<AnimationTriggerEventArgs> AnimationTriggerImpl;

        private readonly AnimationTriggerEventArgs _AnimationTriggerEventArgs = new AnimationTriggerEventArgs();
        
        /// <summary>
        /// AnimatedModel animation trigger.
        /// </summary>
        public event EventHandler<AnimationTriggerEventArgs> AnimationTrigger
        {
            add
            {
                if (AnimationTriggerImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AnimationTrigger, HandleAnimationTrigger);
                }
                AnimationTriggerImpl += value;
            }
            remove
            {
                AnimationTriggerImpl -= value;
                if (AnimationTriggerImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AnimationTrigger);
                }
            }
        }

        private void HandleAnimationTrigger(VariantMap eventData)
        {
            _AnimationTriggerEventArgs.Set(eventData);
            AnimationTriggerImpl?.Invoke(_urhoObject.Value, _AnimationTriggerEventArgs);
        }

        #endregion

        #region AnimationFinished
        // -------------------------------------------- AnimationFinished --------------------------------------------

        /// <summary>
        /// AnimatedModel animation finished or looped.
        /// </summary>
        public class AnimationFinishedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.AnimationFinished.Node].VoidPtr;
                Animation = eventData[E.AnimationFinished.Animation].VoidPtr;
                Name = eventData[E.AnimationFinished.Name].String;
                Looped = eventData[E.AnimationFinished.Looped].Bool;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Animation { get; private set; }

            public String Name { get; private set; }

            public bool Looped { get; private set; }
        }

        private event EventHandler<AnimationFinishedEventArgs> AnimationFinishedImpl;

        private readonly AnimationFinishedEventArgs _AnimationFinishedEventArgs = new AnimationFinishedEventArgs();
        
        /// <summary>
        /// AnimatedModel animation finished or looped.
        /// </summary>
        public event EventHandler<AnimationFinishedEventArgs> AnimationFinished
        {
            add
            {
                if (AnimationFinishedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AnimationFinished, HandleAnimationFinished);
                }
                AnimationFinishedImpl += value;
            }
            remove
            {
                AnimationFinishedImpl -= value;
                if (AnimationFinishedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AnimationFinished);
                }
            }
        }

        private void HandleAnimationFinished(VariantMap eventData)
        {
            _AnimationFinishedEventArgs.Set(eventData);
            AnimationFinishedImpl?.Invoke(_urhoObject.Value, _AnimationFinishedEventArgs);
        }

        #endregion

        #region ParticleEffectFinished
        // -------------------------------------------- ParticleEffectFinished --------------------------------------------

        /// <summary>
        /// Particle effect finished.
        /// </summary>
        public class ParticleEffectFinishedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.ParticleEffectFinished.Node].VoidPtr;
                Effect = eventData[E.ParticleEffectFinished.Effect].VoidPtr;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Effect { get; private set; }
        }

        private event EventHandler<ParticleEffectFinishedEventArgs> ParticleEffectFinishedImpl;

        private readonly ParticleEffectFinishedEventArgs _ParticleEffectFinishedEventArgs = new ParticleEffectFinishedEventArgs();
        
        /// <summary>
        /// Particle effect finished.
        /// </summary>
        public event EventHandler<ParticleEffectFinishedEventArgs> ParticleEffectFinished
        {
            add
            {
                if (ParticleEffectFinishedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ParticleEffectFinished, HandleParticleEffectFinished);
                }
                ParticleEffectFinishedImpl += value;
            }
            remove
            {
                ParticleEffectFinishedImpl -= value;
                if (ParticleEffectFinishedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ParticleEffectFinished);
                }
            }
        }

        private void HandleParticleEffectFinished(VariantMap eventData)
        {
            _ParticleEffectFinishedEventArgs.Set(eventData);
            ParticleEffectFinishedImpl?.Invoke(_urhoObject.Value, _ParticleEffectFinishedEventArgs);
        }

        #endregion

        #region TerrainCreated
        // -------------------------------------------- TerrainCreated --------------------------------------------

        /// <summary>
        /// Terrain geometry created.
        /// </summary>
        public class TerrainCreatedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.TerrainCreated.Node].VoidPtr;
            }

            public IntPtr Node { get; private set; }
        }

        private event EventHandler<TerrainCreatedEventArgs> TerrainCreatedImpl;

        private readonly TerrainCreatedEventArgs _TerrainCreatedEventArgs = new TerrainCreatedEventArgs();
        
        /// <summary>
        /// Terrain geometry created.
        /// </summary>
        public event EventHandler<TerrainCreatedEventArgs> TerrainCreated
        {
            add
            {
                if (TerrainCreatedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TerrainCreated, HandleTerrainCreated);
                }
                TerrainCreatedImpl += value;
            }
            remove
            {
                TerrainCreatedImpl -= value;
                if (TerrainCreatedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TerrainCreated);
                }
            }
        }

        private void HandleTerrainCreated(VariantMap eventData)
        {
            _TerrainCreatedEventArgs.Set(eventData);
            TerrainCreatedImpl?.Invoke(_urhoObject.Value, _TerrainCreatedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (BoneHierarchyCreatedImpl != null) urhoObject.UnsubscribeFromEvent(E.BoneHierarchyCreated);
                if (AnimationTriggerImpl != null) urhoObject.UnsubscribeFromEvent(E.AnimationTrigger);
                if (AnimationFinishedImpl != null) urhoObject.UnsubscribeFromEvent(E.AnimationFinished);
                if (ParticleEffectFinishedImpl != null) urhoObject.UnsubscribeFromEvent(E.ParticleEffectFinished);
                if (TerrainCreatedImpl != null) urhoObject.UnsubscribeFromEvent(E.TerrainCreated);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class GraphicsEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public GraphicsEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region ScreenMode
        // -------------------------------------------- ScreenMode --------------------------------------------

        /// <summary>
        /// New screen mode set.
        /// </summary>
        public class ScreenModeEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Width = eventData[E.ScreenMode.Width].Int;
                Height = eventData[E.ScreenMode.Height].Int;
                Fullscreen = eventData[E.ScreenMode.Fullscreen].Bool;
                Borderless = eventData[E.ScreenMode.Borderless].Bool;
                Resizable = eventData[E.ScreenMode.Resizable].Bool;
                HighDPI = eventData[E.ScreenMode.HighDPI].Bool;
                Monitor = eventData[E.ScreenMode.Monitor].Int;
                RefreshRate = eventData[E.ScreenMode.RefreshRate].Int;
            }

            public int Width { get; private set; }

            public int Height { get; private set; }

            public bool Fullscreen { get; private set; }

            public bool Borderless { get; private set; }

            public bool Resizable { get; private set; }

            public bool HighDPI { get; private set; }

            public int Monitor { get; private set; }

            public int RefreshRate { get; private set; }
        }

        private event EventHandler<ScreenModeEventArgs> ScreenModeImpl;

        private readonly ScreenModeEventArgs _ScreenModeEventArgs = new ScreenModeEventArgs();
        
        /// <summary>
        /// New screen mode set.
        /// </summary>
        public event EventHandler<ScreenModeEventArgs> ScreenMode
        {
            add
            {
                if (ScreenModeImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ScreenMode, HandleScreenMode);
                }
                ScreenModeImpl += value;
            }
            remove
            {
                ScreenModeImpl -= value;
                if (ScreenModeImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ScreenMode);
                }
            }
        }

        private void HandleScreenMode(VariantMap eventData)
        {
            _ScreenModeEventArgs.Set(eventData);
            ScreenModeImpl?.Invoke(_urhoObject.Value, _ScreenModeEventArgs);
        }

        #endregion

        #region WindowPos
        // -------------------------------------------- WindowPos --------------------------------------------

        /// <summary>
        /// Window position changed.
        /// </summary>
        public class WindowPosEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                X = eventData[E.WindowPos.X].Int;
                Y = eventData[E.WindowPos.Y].Int;
            }

            public int X { get; private set; }

            public int Y { get; private set; }
        }

        private event EventHandler<WindowPosEventArgs> WindowPosImpl;

        private readonly WindowPosEventArgs _WindowPosEventArgs = new WindowPosEventArgs();
        
        /// <summary>
        /// Window position changed.
        /// </summary>
        public event EventHandler<WindowPosEventArgs> WindowPos
        {
            add
            {
                if (WindowPosImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.WindowPos, HandleWindowPos);
                }
                WindowPosImpl += value;
            }
            remove
            {
                WindowPosImpl -= value;
                if (WindowPosImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.WindowPos);
                }
            }
        }

        private void HandleWindowPos(VariantMap eventData)
        {
            _WindowPosEventArgs.Set(eventData);
            WindowPosImpl?.Invoke(_urhoObject.Value, _WindowPosEventArgs);
        }

        #endregion

        #region RenderSurfaceUpdate
        // -------------------------------------------- RenderSurfaceUpdate --------------------------------------------

        /// <summary>
        /// Request for queuing rendersurfaces either in manual or always-update mode.
        /// </summary>
        public class RenderSurfaceUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<RenderSurfaceUpdateEventArgs> RenderSurfaceUpdateImpl;

        private readonly RenderSurfaceUpdateEventArgs _RenderSurfaceUpdateEventArgs = new RenderSurfaceUpdateEventArgs();
        
        /// <summary>
        /// Request for queuing rendersurfaces either in manual or always-update mode.
        /// </summary>
        public event EventHandler<RenderSurfaceUpdateEventArgs> RenderSurfaceUpdate
        {
            add
            {
                if (RenderSurfaceUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.RenderSurfaceUpdate, HandleRenderSurfaceUpdate);
                }
                RenderSurfaceUpdateImpl += value;
            }
            remove
            {
                RenderSurfaceUpdateImpl -= value;
                if (RenderSurfaceUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.RenderSurfaceUpdate);
                }
            }
        }

        private void HandleRenderSurfaceUpdate(VariantMap eventData)
        {
            _RenderSurfaceUpdateEventArgs.Set(eventData);
            RenderSurfaceUpdateImpl?.Invoke(_urhoObject.Value, _RenderSurfaceUpdateEventArgs);
        }

        #endregion

        #region BeginRendering
        // -------------------------------------------- BeginRendering --------------------------------------------

        /// <summary>
        /// Frame rendering started.
        /// </summary>
        public class BeginRenderingEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<BeginRenderingEventArgs> BeginRenderingImpl;

        private readonly BeginRenderingEventArgs _BeginRenderingEventArgs = new BeginRenderingEventArgs();
        
        /// <summary>
        /// Frame rendering started.
        /// </summary>
        public event EventHandler<BeginRenderingEventArgs> BeginRendering
        {
            add
            {
                if (BeginRenderingImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.BeginRendering, HandleBeginRendering);
                }
                BeginRenderingImpl += value;
            }
            remove
            {
                BeginRenderingImpl -= value;
                if (BeginRenderingImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.BeginRendering);
                }
            }
        }

        private void HandleBeginRendering(VariantMap eventData)
        {
            _BeginRenderingEventArgs.Set(eventData);
            BeginRenderingImpl?.Invoke(_urhoObject.Value, _BeginRenderingEventArgs);
        }

        #endregion

        #region EndRendering
        // -------------------------------------------- EndRendering --------------------------------------------

        /// <summary>
        /// Frame rendering ended.
        /// </summary>
        public class EndRenderingEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<EndRenderingEventArgs> EndRenderingImpl;

        private readonly EndRenderingEventArgs _EndRenderingEventArgs = new EndRenderingEventArgs();
        
        /// <summary>
        /// Frame rendering ended.
        /// </summary>
        public event EventHandler<EndRenderingEventArgs> EndRendering
        {
            add
            {
                if (EndRenderingImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.EndRendering, HandleEndRendering);
                }
                EndRenderingImpl += value;
            }
            remove
            {
                EndRenderingImpl -= value;
                if (EndRenderingImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.EndRendering);
                }
            }
        }

        private void HandleEndRendering(VariantMap eventData)
        {
            _EndRenderingEventArgs.Set(eventData);
            EndRenderingImpl?.Invoke(_urhoObject.Value, _EndRenderingEventArgs);
        }

        #endregion

        #region BeginViewUpdate
        // -------------------------------------------- BeginViewUpdate --------------------------------------------

        /// <summary>
        /// Update of a view started.
        /// </summary>
        public class BeginViewUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                View = eventData[E.BeginViewUpdate.View].VoidPtr;
                Texture = eventData[E.BeginViewUpdate.Texture].VoidPtr;
                Surface = eventData[E.BeginViewUpdate.Surface].VoidPtr;
                Scene = eventData[E.BeginViewUpdate.Scene].VoidPtr;
                Camera = eventData[E.BeginViewUpdate.Camera].VoidPtr;
            }

            public IntPtr View { get; private set; }

            public IntPtr Texture { get; private set; }

            public IntPtr Surface { get; private set; }

            public IntPtr Scene { get; private set; }

            public IntPtr Camera { get; private set; }
        }

        private event EventHandler<BeginViewUpdateEventArgs> BeginViewUpdateImpl;

        private readonly BeginViewUpdateEventArgs _BeginViewUpdateEventArgs = new BeginViewUpdateEventArgs();
        
        /// <summary>
        /// Update of a view started.
        /// </summary>
        public event EventHandler<BeginViewUpdateEventArgs> BeginViewUpdate
        {
            add
            {
                if (BeginViewUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.BeginViewUpdate, HandleBeginViewUpdate);
                }
                BeginViewUpdateImpl += value;
            }
            remove
            {
                BeginViewUpdateImpl -= value;
                if (BeginViewUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.BeginViewUpdate);
                }
            }
        }

        private void HandleBeginViewUpdate(VariantMap eventData)
        {
            _BeginViewUpdateEventArgs.Set(eventData);
            BeginViewUpdateImpl?.Invoke(_urhoObject.Value, _BeginViewUpdateEventArgs);
        }

        #endregion

        #region EndViewUpdate
        // -------------------------------------------- EndViewUpdate --------------------------------------------

        /// <summary>
        /// Update of a view ended.
        /// </summary>
        public class EndViewUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                View = eventData[E.EndViewUpdate.View].VoidPtr;
                Texture = eventData[E.EndViewUpdate.Texture].VoidPtr;
                Surface = eventData[E.EndViewUpdate.Surface].VoidPtr;
                Scene = eventData[E.EndViewUpdate.Scene].VoidPtr;
                Camera = eventData[E.EndViewUpdate.Camera].VoidPtr;
            }

            public IntPtr View { get; private set; }

            public IntPtr Texture { get; private set; }

            public IntPtr Surface { get; private set; }

            public IntPtr Scene { get; private set; }

            public IntPtr Camera { get; private set; }
        }

        private event EventHandler<EndViewUpdateEventArgs> EndViewUpdateImpl;

        private readonly EndViewUpdateEventArgs _EndViewUpdateEventArgs = new EndViewUpdateEventArgs();
        
        /// <summary>
        /// Update of a view ended.
        /// </summary>
        public event EventHandler<EndViewUpdateEventArgs> EndViewUpdate
        {
            add
            {
                if (EndViewUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.EndViewUpdate, HandleEndViewUpdate);
                }
                EndViewUpdateImpl += value;
            }
            remove
            {
                EndViewUpdateImpl -= value;
                if (EndViewUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.EndViewUpdate);
                }
            }
        }

        private void HandleEndViewUpdate(VariantMap eventData)
        {
            _EndViewUpdateEventArgs.Set(eventData);
            EndViewUpdateImpl?.Invoke(_urhoObject.Value, _EndViewUpdateEventArgs);
        }

        #endregion

        #region BeginViewRender
        // -------------------------------------------- BeginViewRender --------------------------------------------

        /// <summary>
        /// Render of a view started.
        /// </summary>
        public class BeginViewRenderEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                View = eventData[E.BeginViewRender.View].VoidPtr;
                Texture = eventData[E.BeginViewRender.Texture].VoidPtr;
                Surface = eventData[E.BeginViewRender.Surface].VoidPtr;
                Scene = eventData[E.BeginViewRender.Scene].VoidPtr;
                Camera = eventData[E.BeginViewRender.Camera].VoidPtr;
            }

            public IntPtr View { get; private set; }

            public IntPtr Texture { get; private set; }

            public IntPtr Surface { get; private set; }

            public IntPtr Scene { get; private set; }

            public IntPtr Camera { get; private set; }
        }

        private event EventHandler<BeginViewRenderEventArgs> BeginViewRenderImpl;

        private readonly BeginViewRenderEventArgs _BeginViewRenderEventArgs = new BeginViewRenderEventArgs();
        
        /// <summary>
        /// Render of a view started.
        /// </summary>
        public event EventHandler<BeginViewRenderEventArgs> BeginViewRender
        {
            add
            {
                if (BeginViewRenderImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.BeginViewRender, HandleBeginViewRender);
                }
                BeginViewRenderImpl += value;
            }
            remove
            {
                BeginViewRenderImpl -= value;
                if (BeginViewRenderImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.BeginViewRender);
                }
            }
        }

        private void HandleBeginViewRender(VariantMap eventData)
        {
            _BeginViewRenderEventArgs.Set(eventData);
            BeginViewRenderImpl?.Invoke(_urhoObject.Value, _BeginViewRenderEventArgs);
        }

        #endregion

        #region ViewBuffersReady
        // -------------------------------------------- ViewBuffersReady --------------------------------------------

        /// <summary>
        /// A view has allocated its screen buffers for rendering. They can be accessed now with View::FindNamedTexture().
        /// </summary>
        public class ViewBuffersReadyEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                View = eventData[E.ViewBuffersReady.View].VoidPtr;
                Texture = eventData[E.ViewBuffersReady.Texture].VoidPtr;
                Surface = eventData[E.ViewBuffersReady.Surface].VoidPtr;
                Scene = eventData[E.ViewBuffersReady.Scene].VoidPtr;
                Camera = eventData[E.ViewBuffersReady.Camera].VoidPtr;
            }

            public IntPtr View { get; private set; }

            public IntPtr Texture { get; private set; }

            public IntPtr Surface { get; private set; }

            public IntPtr Scene { get; private set; }

            public IntPtr Camera { get; private set; }
        }

        private event EventHandler<ViewBuffersReadyEventArgs> ViewBuffersReadyImpl;

        private readonly ViewBuffersReadyEventArgs _ViewBuffersReadyEventArgs = new ViewBuffersReadyEventArgs();
        
        /// <summary>
        /// A view has allocated its screen buffers for rendering. They can be accessed now with View::FindNamedTexture().
        /// </summary>
        public event EventHandler<ViewBuffersReadyEventArgs> ViewBuffersReady
        {
            add
            {
                if (ViewBuffersReadyImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ViewBuffersReady, HandleViewBuffersReady);
                }
                ViewBuffersReadyImpl += value;
            }
            remove
            {
                ViewBuffersReadyImpl -= value;
                if (ViewBuffersReadyImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ViewBuffersReady);
                }
            }
        }

        private void HandleViewBuffersReady(VariantMap eventData)
        {
            _ViewBuffersReadyEventArgs.Set(eventData);
            ViewBuffersReadyImpl?.Invoke(_urhoObject.Value, _ViewBuffersReadyEventArgs);
        }

        #endregion

        #region ViewGlobalShaderParameters
        // -------------------------------------------- ViewGlobalShaderParameters --------------------------------------------

        /// <summary>
        /// A view has set global shader parameters for a new combination of vertex/pixel shaders. Custom global parameters can now be set.
        /// </summary>
        public class ViewGlobalShaderParametersEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                View = eventData[E.ViewGlobalShaderParameters.View].VoidPtr;
                Texture = eventData[E.ViewGlobalShaderParameters.Texture].VoidPtr;
                Surface = eventData[E.ViewGlobalShaderParameters.Surface].VoidPtr;
                Scene = eventData[E.ViewGlobalShaderParameters.Scene].VoidPtr;
                Camera = eventData[E.ViewGlobalShaderParameters.Camera].VoidPtr;
            }

            public IntPtr View { get; private set; }

            public IntPtr Texture { get; private set; }

            public IntPtr Surface { get; private set; }

            public IntPtr Scene { get; private set; }

            public IntPtr Camera { get; private set; }
        }

        private event EventHandler<ViewGlobalShaderParametersEventArgs> ViewGlobalShaderParametersImpl;

        private readonly ViewGlobalShaderParametersEventArgs _ViewGlobalShaderParametersEventArgs = new ViewGlobalShaderParametersEventArgs();
        
        /// <summary>
        /// A view has set global shader parameters for a new combination of vertex/pixel shaders. Custom global parameters can now be set.
        /// </summary>
        public event EventHandler<ViewGlobalShaderParametersEventArgs> ViewGlobalShaderParameters
        {
            add
            {
                if (ViewGlobalShaderParametersImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ViewGlobalShaderParameters, HandleViewGlobalShaderParameters);
                }
                ViewGlobalShaderParametersImpl += value;
            }
            remove
            {
                ViewGlobalShaderParametersImpl -= value;
                if (ViewGlobalShaderParametersImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ViewGlobalShaderParameters);
                }
            }
        }

        private void HandleViewGlobalShaderParameters(VariantMap eventData)
        {
            _ViewGlobalShaderParametersEventArgs.Set(eventData);
            ViewGlobalShaderParametersImpl?.Invoke(_urhoObject.Value, _ViewGlobalShaderParametersEventArgs);
        }

        #endregion

        #region EndViewRender
        // -------------------------------------------- EndViewRender --------------------------------------------

        /// <summary>
        /// Render of a view ended. Its screen buffers are still accessible if needed.
        /// </summary>
        public class EndViewRenderEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                View = eventData[E.EndViewRender.View].VoidPtr;
                Texture = eventData[E.EndViewRender.Texture].VoidPtr;
                Surface = eventData[E.EndViewRender.Surface].VoidPtr;
                Scene = eventData[E.EndViewRender.Scene].VoidPtr;
                Camera = eventData[E.EndViewRender.Camera].VoidPtr;
            }

            public IntPtr View { get; private set; }

            public IntPtr Texture { get; private set; }

            public IntPtr Surface { get; private set; }

            public IntPtr Scene { get; private set; }

            public IntPtr Camera { get; private set; }
        }

        private event EventHandler<EndViewRenderEventArgs> EndViewRenderImpl;

        private readonly EndViewRenderEventArgs _EndViewRenderEventArgs = new EndViewRenderEventArgs();
        
        /// <summary>
        /// Render of a view ended. Its screen buffers are still accessible if needed.
        /// </summary>
        public event EventHandler<EndViewRenderEventArgs> EndViewRender
        {
            add
            {
                if (EndViewRenderImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.EndViewRender, HandleEndViewRender);
                }
                EndViewRenderImpl += value;
            }
            remove
            {
                EndViewRenderImpl -= value;
                if (EndViewRenderImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.EndViewRender);
                }
            }
        }

        private void HandleEndViewRender(VariantMap eventData)
        {
            _EndViewRenderEventArgs.Set(eventData);
            EndViewRenderImpl?.Invoke(_urhoObject.Value, _EndViewRenderEventArgs);
        }

        #endregion

        #region EndAllViewsRender
        // -------------------------------------------- EndAllViewsRender --------------------------------------------

        /// <summary>
        /// Render of all views is finished for the frame.
        /// </summary>
        public class EndAllViewsRenderEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<EndAllViewsRenderEventArgs> EndAllViewsRenderImpl;

        private readonly EndAllViewsRenderEventArgs _EndAllViewsRenderEventArgs = new EndAllViewsRenderEventArgs();
        
        /// <summary>
        /// Render of all views is finished for the frame.
        /// </summary>
        public event EventHandler<EndAllViewsRenderEventArgs> EndAllViewsRender
        {
            add
            {
                if (EndAllViewsRenderImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.EndAllViewsRender, HandleEndAllViewsRender);
                }
                EndAllViewsRenderImpl += value;
            }
            remove
            {
                EndAllViewsRenderImpl -= value;
                if (EndAllViewsRenderImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.EndAllViewsRender);
                }
            }
        }

        private void HandleEndAllViewsRender(VariantMap eventData)
        {
            _EndAllViewsRenderEventArgs.Set(eventData);
            EndAllViewsRenderImpl?.Invoke(_urhoObject.Value, _EndAllViewsRenderEventArgs);
        }

        #endregion

        #region RenderPathEvent
        // -------------------------------------------- RenderPathEvent --------------------------------------------

        /// <summary>
        /// A render path event has occurred.
        /// </summary>
        public class RenderPathEventEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Name = eventData[E.RenderPathEvent.Name].String;
            }

            public String Name { get; private set; }
        }

        private event EventHandler<RenderPathEventEventArgs> RenderPathEventImpl;

        private readonly RenderPathEventEventArgs _RenderPathEventEventArgs = new RenderPathEventEventArgs();
        
        /// <summary>
        /// A render path event has occurred.
        /// </summary>
        public event EventHandler<RenderPathEventEventArgs> RenderPathEvent
        {
            add
            {
                if (RenderPathEventImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.RenderPathEvent, HandleRenderPathEvent);
                }
                RenderPathEventImpl += value;
            }
            remove
            {
                RenderPathEventImpl -= value;
                if (RenderPathEventImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.RenderPathEvent);
                }
            }
        }

        private void HandleRenderPathEvent(VariantMap eventData)
        {
            _RenderPathEventEventArgs.Set(eventData);
            RenderPathEventImpl?.Invoke(_urhoObject.Value, _RenderPathEventEventArgs);
        }

        #endregion

        #region DeviceLost
        // -------------------------------------------- DeviceLost --------------------------------------------

        /// <summary>
        /// Graphics context has been lost. Some or all (depending on the API) GPU objects have lost their contents.
        /// </summary>
        public class DeviceLostEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<DeviceLostEventArgs> DeviceLostImpl;

        private readonly DeviceLostEventArgs _DeviceLostEventArgs = new DeviceLostEventArgs();
        
        /// <summary>
        /// Graphics context has been lost. Some or all (depending on the API) GPU objects have lost their contents.
        /// </summary>
        public event EventHandler<DeviceLostEventArgs> DeviceLost
        {
            add
            {
                if (DeviceLostImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.DeviceLost, HandleDeviceLost);
                }
                DeviceLostImpl += value;
            }
            remove
            {
                DeviceLostImpl -= value;
                if (DeviceLostImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.DeviceLost);
                }
            }
        }

        private void HandleDeviceLost(VariantMap eventData)
        {
            _DeviceLostEventArgs.Set(eventData);
            DeviceLostImpl?.Invoke(_urhoObject.Value, _DeviceLostEventArgs);
        }

        #endregion

        #region DeviceReset
        // -------------------------------------------- DeviceReset --------------------------------------------

        /// <summary>
        /// Graphics context has been recreated after being lost. GPU objects in the "data lost" state can be restored now.
        /// </summary>
        public class DeviceResetEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<DeviceResetEventArgs> DeviceResetImpl;

        private readonly DeviceResetEventArgs _DeviceResetEventArgs = new DeviceResetEventArgs();
        
        /// <summary>
        /// Graphics context has been recreated after being lost. GPU objects in the "data lost" state can be restored now.
        /// </summary>
        public event EventHandler<DeviceResetEventArgs> DeviceReset
        {
            add
            {
                if (DeviceResetImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.DeviceReset, HandleDeviceReset);
                }
                DeviceResetImpl += value;
            }
            remove
            {
                DeviceResetImpl -= value;
                if (DeviceResetImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.DeviceReset);
                }
            }
        }

        private void HandleDeviceReset(VariantMap eventData)
        {
            _DeviceResetEventArgs.Set(eventData);
            DeviceResetImpl?.Invoke(_urhoObject.Value, _DeviceResetEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (ScreenModeImpl != null) urhoObject.UnsubscribeFromEvent(E.ScreenMode);
                if (WindowPosImpl != null) urhoObject.UnsubscribeFromEvent(E.WindowPos);
                if (RenderSurfaceUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.RenderSurfaceUpdate);
                if (BeginRenderingImpl != null) urhoObject.UnsubscribeFromEvent(E.BeginRendering);
                if (EndRenderingImpl != null) urhoObject.UnsubscribeFromEvent(E.EndRendering);
                if (BeginViewUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.BeginViewUpdate);
                if (EndViewUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.EndViewUpdate);
                if (BeginViewRenderImpl != null) urhoObject.UnsubscribeFromEvent(E.BeginViewRender);
                if (ViewBuffersReadyImpl != null) urhoObject.UnsubscribeFromEvent(E.ViewBuffersReady);
                if (ViewGlobalShaderParametersImpl != null) urhoObject.UnsubscribeFromEvent(E.ViewGlobalShaderParameters);
                if (EndViewRenderImpl != null) urhoObject.UnsubscribeFromEvent(E.EndViewRender);
                if (EndAllViewsRenderImpl != null) urhoObject.UnsubscribeFromEvent(E.EndAllViewsRender);
                if (RenderPathEventImpl != null) urhoObject.UnsubscribeFromEvent(E.RenderPathEvent);
                if (DeviceLostImpl != null) urhoObject.UnsubscribeFromEvent(E.DeviceLost);
                if (DeviceResetImpl != null) urhoObject.UnsubscribeFromEvent(E.DeviceReset);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class IKEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public IKEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region IKEffectorTargetChanged
        // -------------------------------------------- IKEffectorTargetChanged --------------------------------------------

        /// <summary>
        /// IKEffectorTargetChanged
        /// </summary>
        public class IKEffectorTargetChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                EffectorNode = eventData[E.IKEffectorTargetChanged.EffectorNode].VoidPtr;
                  TargetNode = eventData[E.IKEffectorTargetChanged.  TargetNode].VoidPtr;
            }

            public IntPtr EffectorNode { get; private set; }

            public IntPtr   TargetNode { get; private set; }
        }

        private event EventHandler<IKEffectorTargetChangedEventArgs> IKEffectorTargetChangedImpl;

        private readonly IKEffectorTargetChangedEventArgs _IKEffectorTargetChangedEventArgs = new IKEffectorTargetChangedEventArgs();
        
        /// <summary>
        /// IKEffectorTargetChanged
        /// </summary>
        public event EventHandler<IKEffectorTargetChangedEventArgs> IKEffectorTargetChanged
        {
            add
            {
                if (IKEffectorTargetChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.IKEffectorTargetChanged, HandleIKEffectorTargetChanged);
                }
                IKEffectorTargetChangedImpl += value;
            }
            remove
            {
                IKEffectorTargetChangedImpl -= value;
                if (IKEffectorTargetChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.IKEffectorTargetChanged);
                }
            }
        }

        private void HandleIKEffectorTargetChanged(VariantMap eventData)
        {
            _IKEffectorTargetChangedEventArgs.Set(eventData);
            IKEffectorTargetChangedImpl?.Invoke(_urhoObject.Value, _IKEffectorTargetChangedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (IKEffectorTargetChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.IKEffectorTargetChanged);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class InputEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public InputEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region MouseButtonDown
        // -------------------------------------------- MouseButtonDown --------------------------------------------

        /// <summary>
        /// Mouse button pressed.
        /// </summary>
        public class MouseButtonDownEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Button = eventData[E.MouseButtonDown.Button].Int;
                Buttons = eventData[E.MouseButtonDown.Buttons].Int;
                Qualifiers = eventData[E.MouseButtonDown.Qualifiers].Int;
                Clicks = eventData[E.MouseButtonDown.Clicks].Int;
            }

            public int Button { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }

            public int Clicks { get; private set; }
        }

        private event EventHandler<MouseButtonDownEventArgs> MouseButtonDownImpl;

        private readonly MouseButtonDownEventArgs _MouseButtonDownEventArgs = new MouseButtonDownEventArgs();
        
        /// <summary>
        /// Mouse button pressed.
        /// </summary>
        public event EventHandler<MouseButtonDownEventArgs> MouseButtonDown
        {
            add
            {
                if (MouseButtonDownImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.MouseButtonDown, HandleMouseButtonDown);
                }
                MouseButtonDownImpl += value;
            }
            remove
            {
                MouseButtonDownImpl -= value;
                if (MouseButtonDownImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.MouseButtonDown);
                }
            }
        }

        private void HandleMouseButtonDown(VariantMap eventData)
        {
            _MouseButtonDownEventArgs.Set(eventData);
            MouseButtonDownImpl?.Invoke(_urhoObject.Value, _MouseButtonDownEventArgs);
        }

        #endregion

        #region MouseButtonUp
        // -------------------------------------------- MouseButtonUp --------------------------------------------

        /// <summary>
        /// Mouse button released.
        /// </summary>
        public class MouseButtonUpEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Button = eventData[E.MouseButtonUp.Button].Int;
                Buttons = eventData[E.MouseButtonUp.Buttons].Int;
                Qualifiers = eventData[E.MouseButtonUp.Qualifiers].Int;
            }

            public int Button { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<MouseButtonUpEventArgs> MouseButtonUpImpl;

        private readonly MouseButtonUpEventArgs _MouseButtonUpEventArgs = new MouseButtonUpEventArgs();
        
        /// <summary>
        /// Mouse button released.
        /// </summary>
        public event EventHandler<MouseButtonUpEventArgs> MouseButtonUp
        {
            add
            {
                if (MouseButtonUpImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.MouseButtonUp, HandleMouseButtonUp);
                }
                MouseButtonUpImpl += value;
            }
            remove
            {
                MouseButtonUpImpl -= value;
                if (MouseButtonUpImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.MouseButtonUp);
                }
            }
        }

        private void HandleMouseButtonUp(VariantMap eventData)
        {
            _MouseButtonUpEventArgs.Set(eventData);
            MouseButtonUpImpl?.Invoke(_urhoObject.Value, _MouseButtonUpEventArgs);
        }

        #endregion

        #region MouseMove
        // -------------------------------------------- MouseMove --------------------------------------------

        /// <summary>
        /// Mouse moved.
        /// </summary>
        public class MouseMoveEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                X = eventData[E.MouseMove.X].Int;
                Y = eventData[E.MouseMove.Y].Int;
                DX = eventData[E.MouseMove.DX].Int;
                DY = eventData[E.MouseMove.DY].Int;
                Buttons = eventData[E.MouseMove.Buttons].Int;
                Qualifiers = eventData[E.MouseMove.Qualifiers].Int;
            }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int DX { get; private set; }

            public int DY { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<MouseMoveEventArgs> MouseMoveImpl;

        private readonly MouseMoveEventArgs _MouseMoveEventArgs = new MouseMoveEventArgs();
        
        /// <summary>
        /// Mouse moved.
        /// </summary>
        public event EventHandler<MouseMoveEventArgs> MouseMove
        {
            add
            {
                if (MouseMoveImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.MouseMove, HandleMouseMove);
                }
                MouseMoveImpl += value;
            }
            remove
            {
                MouseMoveImpl -= value;
                if (MouseMoveImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.MouseMove);
                }
            }
        }

        private void HandleMouseMove(VariantMap eventData)
        {
            _MouseMoveEventArgs.Set(eventData);
            MouseMoveImpl?.Invoke(_urhoObject.Value, _MouseMoveEventArgs);
        }

        #endregion

        #region MouseWheel
        // -------------------------------------------- MouseWheel --------------------------------------------

        /// <summary>
        /// Mouse wheel moved.
        /// </summary>
        public class MouseWheelEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Wheel = eventData[E.MouseWheel.Wheel].Int;
                Buttons = eventData[E.MouseWheel.Buttons].Int;
                Qualifiers = eventData[E.MouseWheel.Qualifiers].Int;
            }

            public int Wheel { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<MouseWheelEventArgs> MouseWheelImpl;

        private readonly MouseWheelEventArgs _MouseWheelEventArgs = new MouseWheelEventArgs();
        
        /// <summary>
        /// Mouse wheel moved.
        /// </summary>
        public event EventHandler<MouseWheelEventArgs> MouseWheel
        {
            add
            {
                if (MouseWheelImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.MouseWheel, HandleMouseWheel);
                }
                MouseWheelImpl += value;
            }
            remove
            {
                MouseWheelImpl -= value;
                if (MouseWheelImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.MouseWheel);
                }
            }
        }

        private void HandleMouseWheel(VariantMap eventData)
        {
            _MouseWheelEventArgs.Set(eventData);
            MouseWheelImpl?.Invoke(_urhoObject.Value, _MouseWheelEventArgs);
        }

        #endregion

        #region KeyDown
        // -------------------------------------------- KeyDown --------------------------------------------

        /// <summary>
        /// Key pressed.
        /// </summary>
        public class KeyDownEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Key = eventData[E.KeyDown.Key].Int;
                Scancode = eventData[E.KeyDown.Scancode].Int;
                Buttons = eventData[E.KeyDown.Buttons].Int;
                Qualifiers = eventData[E.KeyDown.Qualifiers].Int;
                Repeat = eventData[E.KeyDown.Repeat].Bool;
            }

            public int Key { get; private set; }

            public int Scancode { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }

            public bool Repeat { get; private set; }
        }

        private event EventHandler<KeyDownEventArgs> KeyDownImpl;

        private readonly KeyDownEventArgs _KeyDownEventArgs = new KeyDownEventArgs();
        
        /// <summary>
        /// Key pressed.
        /// </summary>
        public event EventHandler<KeyDownEventArgs> KeyDown
        {
            add
            {
                if (KeyDownImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.KeyDown, HandleKeyDown);
                }
                KeyDownImpl += value;
            }
            remove
            {
                KeyDownImpl -= value;
                if (KeyDownImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.KeyDown);
                }
            }
        }

        private void HandleKeyDown(VariantMap eventData)
        {
            _KeyDownEventArgs.Set(eventData);
            KeyDownImpl?.Invoke(_urhoObject.Value, _KeyDownEventArgs);
        }

        #endregion

        #region KeyUp
        // -------------------------------------------- KeyUp --------------------------------------------

        /// <summary>
        /// Key released.
        /// </summary>
        public class KeyUpEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Key = eventData[E.KeyUp.Key].Int;
                Scancode = eventData[E.KeyUp.Scancode].Int;
                Buttons = eventData[E.KeyUp.Buttons].Int;
                Qualifiers = eventData[E.KeyUp.Qualifiers].Int;
            }

            public int Key { get; private set; }

            public int Scancode { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<KeyUpEventArgs> KeyUpImpl;

        private readonly KeyUpEventArgs _KeyUpEventArgs = new KeyUpEventArgs();
        
        /// <summary>
        /// Key released.
        /// </summary>
        public event EventHandler<KeyUpEventArgs> KeyUp
        {
            add
            {
                if (KeyUpImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.KeyUp, HandleKeyUp);
                }
                KeyUpImpl += value;
            }
            remove
            {
                KeyUpImpl -= value;
                if (KeyUpImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.KeyUp);
                }
            }
        }

        private void HandleKeyUp(VariantMap eventData)
        {
            _KeyUpEventArgs.Set(eventData);
            KeyUpImpl?.Invoke(_urhoObject.Value, _KeyUpEventArgs);
        }

        #endregion

        #region TextInput
        // -------------------------------------------- TextInput --------------------------------------------

        /// <summary>
        /// Text input event.
        /// </summary>
        public class TextInputEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Text = eventData[E.TextInput.Text].String;
            }

            public String Text { get; private set; }
        }

        private event EventHandler<TextInputEventArgs> TextInputImpl;

        private readonly TextInputEventArgs _TextInputEventArgs = new TextInputEventArgs();
        
        /// <summary>
        /// Text input event.
        /// </summary>
        public event EventHandler<TextInputEventArgs> TextInput
        {
            add
            {
                if (TextInputImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TextInput, HandleTextInput);
                }
                TextInputImpl += value;
            }
            remove
            {
                TextInputImpl -= value;
                if (TextInputImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TextInput);
                }
            }
        }

        private void HandleTextInput(VariantMap eventData)
        {
            _TextInputEventArgs.Set(eventData);
            TextInputImpl?.Invoke(_urhoObject.Value, _TextInputEventArgs);
        }

        #endregion

        #region TextEditing
        // -------------------------------------------- TextEditing --------------------------------------------

        /// <summary>
        /// Text editing event.
        /// </summary>
        public class TextEditingEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Composition = eventData[E.TextEditing.Composition].String;
                Cursor = eventData[E.TextEditing.Cursor].Int;
                SelectionLength = eventData[E.TextEditing.SelectionLength].Int;
            }

            public String Composition { get; private set; }

            public int Cursor { get; private set; }

            public int SelectionLength { get; private set; }
        }

        private event EventHandler<TextEditingEventArgs> TextEditingImpl;

        private readonly TextEditingEventArgs _TextEditingEventArgs = new TextEditingEventArgs();
        
        /// <summary>
        /// Text editing event.
        /// </summary>
        public event EventHandler<TextEditingEventArgs> TextEditing
        {
            add
            {
                if (TextEditingImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TextEditing, HandleTextEditing);
                }
                TextEditingImpl += value;
            }
            remove
            {
                TextEditingImpl -= value;
                if (TextEditingImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TextEditing);
                }
            }
        }

        private void HandleTextEditing(VariantMap eventData)
        {
            _TextEditingEventArgs.Set(eventData);
            TextEditingImpl?.Invoke(_urhoObject.Value, _TextEditingEventArgs);
        }

        #endregion

        #region JoystickConnected
        // -------------------------------------------- JoystickConnected --------------------------------------------

        /// <summary>
        /// Joystick connected.
        /// </summary>
        public class JoystickConnectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                JoystickID = eventData[E.JoystickConnected.JoystickID].Int;
            }

            public int JoystickID { get; private set; }
        }

        private event EventHandler<JoystickConnectedEventArgs> JoystickConnectedImpl;

        private readonly JoystickConnectedEventArgs _JoystickConnectedEventArgs = new JoystickConnectedEventArgs();
        
        /// <summary>
        /// Joystick connected.
        /// </summary>
        public event EventHandler<JoystickConnectedEventArgs> JoystickConnected
        {
            add
            {
                if (JoystickConnectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.JoystickConnected, HandleJoystickConnected);
                }
                JoystickConnectedImpl += value;
            }
            remove
            {
                JoystickConnectedImpl -= value;
                if (JoystickConnectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.JoystickConnected);
                }
            }
        }

        private void HandleJoystickConnected(VariantMap eventData)
        {
            _JoystickConnectedEventArgs.Set(eventData);
            JoystickConnectedImpl?.Invoke(_urhoObject.Value, _JoystickConnectedEventArgs);
        }

        #endregion

        #region JoystickDisconnected
        // -------------------------------------------- JoystickDisconnected --------------------------------------------

        /// <summary>
        /// Joystick disconnected.
        /// </summary>
        public class JoystickDisconnectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                JoystickID = eventData[E.JoystickDisconnected.JoystickID].Int;
            }

            public int JoystickID { get; private set; }
        }

        private event EventHandler<JoystickDisconnectedEventArgs> JoystickDisconnectedImpl;

        private readonly JoystickDisconnectedEventArgs _JoystickDisconnectedEventArgs = new JoystickDisconnectedEventArgs();
        
        /// <summary>
        /// Joystick disconnected.
        /// </summary>
        public event EventHandler<JoystickDisconnectedEventArgs> JoystickDisconnected
        {
            add
            {
                if (JoystickDisconnectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.JoystickDisconnected, HandleJoystickDisconnected);
                }
                JoystickDisconnectedImpl += value;
            }
            remove
            {
                JoystickDisconnectedImpl -= value;
                if (JoystickDisconnectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.JoystickDisconnected);
                }
            }
        }

        private void HandleJoystickDisconnected(VariantMap eventData)
        {
            _JoystickDisconnectedEventArgs.Set(eventData);
            JoystickDisconnectedImpl?.Invoke(_urhoObject.Value, _JoystickDisconnectedEventArgs);
        }

        #endregion

        #region JoystickButtonDown
        // -------------------------------------------- JoystickButtonDown --------------------------------------------

        /// <summary>
        /// Joystick button pressed.
        /// </summary>
        public class JoystickButtonDownEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                JoystickID = eventData[E.JoystickButtonDown.JoystickID].Int;
                Button = eventData[E.JoystickButtonDown.Button].Int;
            }

            public int JoystickID { get; private set; }

            public int Button { get; private set; }
        }

        private event EventHandler<JoystickButtonDownEventArgs> JoystickButtonDownImpl;

        private readonly JoystickButtonDownEventArgs _JoystickButtonDownEventArgs = new JoystickButtonDownEventArgs();
        
        /// <summary>
        /// Joystick button pressed.
        /// </summary>
        public event EventHandler<JoystickButtonDownEventArgs> JoystickButtonDown
        {
            add
            {
                if (JoystickButtonDownImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.JoystickButtonDown, HandleJoystickButtonDown);
                }
                JoystickButtonDownImpl += value;
            }
            remove
            {
                JoystickButtonDownImpl -= value;
                if (JoystickButtonDownImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.JoystickButtonDown);
                }
            }
        }

        private void HandleJoystickButtonDown(VariantMap eventData)
        {
            _JoystickButtonDownEventArgs.Set(eventData);
            JoystickButtonDownImpl?.Invoke(_urhoObject.Value, _JoystickButtonDownEventArgs);
        }

        #endregion

        #region JoystickButtonUp
        // -------------------------------------------- JoystickButtonUp --------------------------------------------

        /// <summary>
        /// Joystick button released.
        /// </summary>
        public class JoystickButtonUpEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                JoystickID = eventData[E.JoystickButtonUp.JoystickID].Int;
                Button = eventData[E.JoystickButtonUp.Button].Int;
            }

            public int JoystickID { get; private set; }

            public int Button { get; private set; }
        }

        private event EventHandler<JoystickButtonUpEventArgs> JoystickButtonUpImpl;

        private readonly JoystickButtonUpEventArgs _JoystickButtonUpEventArgs = new JoystickButtonUpEventArgs();
        
        /// <summary>
        /// Joystick button released.
        /// </summary>
        public event EventHandler<JoystickButtonUpEventArgs> JoystickButtonUp
        {
            add
            {
                if (JoystickButtonUpImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.JoystickButtonUp, HandleJoystickButtonUp);
                }
                JoystickButtonUpImpl += value;
            }
            remove
            {
                JoystickButtonUpImpl -= value;
                if (JoystickButtonUpImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.JoystickButtonUp);
                }
            }
        }

        private void HandleJoystickButtonUp(VariantMap eventData)
        {
            _JoystickButtonUpEventArgs.Set(eventData);
            JoystickButtonUpImpl?.Invoke(_urhoObject.Value, _JoystickButtonUpEventArgs);
        }

        #endregion

        #region JoystickAxisMove
        // -------------------------------------------- JoystickAxisMove --------------------------------------------

        /// <summary>
        /// Joystick axis moved.
        /// </summary>
        public class JoystickAxisMoveEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                JoystickID = eventData[E.JoystickAxisMove.JoystickID].Int;
                Button = eventData[E.JoystickAxisMove.Button].Int;
                Position = eventData[E.JoystickAxisMove.Position].Float;
            }

            public int JoystickID { get; private set; }

            public int Button { get; private set; }

            public float Position { get; private set; }
        }

        private event EventHandler<JoystickAxisMoveEventArgs> JoystickAxisMoveImpl;

        private readonly JoystickAxisMoveEventArgs _JoystickAxisMoveEventArgs = new JoystickAxisMoveEventArgs();
        
        /// <summary>
        /// Joystick axis moved.
        /// </summary>
        public event EventHandler<JoystickAxisMoveEventArgs> JoystickAxisMove
        {
            add
            {
                if (JoystickAxisMoveImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.JoystickAxisMove, HandleJoystickAxisMove);
                }
                JoystickAxisMoveImpl += value;
            }
            remove
            {
                JoystickAxisMoveImpl -= value;
                if (JoystickAxisMoveImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.JoystickAxisMove);
                }
            }
        }

        private void HandleJoystickAxisMove(VariantMap eventData)
        {
            _JoystickAxisMoveEventArgs.Set(eventData);
            JoystickAxisMoveImpl?.Invoke(_urhoObject.Value, _JoystickAxisMoveEventArgs);
        }

        #endregion

        #region JoystickHatMove
        // -------------------------------------------- JoystickHatMove --------------------------------------------

        /// <summary>
        /// Joystick POV hat moved.
        /// </summary>
        public class JoystickHatMoveEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                JoystickID = eventData[E.JoystickHatMove.JoystickID].Int;
                Button = eventData[E.JoystickHatMove.Button].Int;
                Position = eventData[E.JoystickHatMove.Position].Int;
            }

            public int JoystickID { get; private set; }

            public int Button { get; private set; }

            public int Position { get; private set; }
        }

        private event EventHandler<JoystickHatMoveEventArgs> JoystickHatMoveImpl;

        private readonly JoystickHatMoveEventArgs _JoystickHatMoveEventArgs = new JoystickHatMoveEventArgs();
        
        /// <summary>
        /// Joystick POV hat moved.
        /// </summary>
        public event EventHandler<JoystickHatMoveEventArgs> JoystickHatMove
        {
            add
            {
                if (JoystickHatMoveImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.JoystickHatMove, HandleJoystickHatMove);
                }
                JoystickHatMoveImpl += value;
            }
            remove
            {
                JoystickHatMoveImpl -= value;
                if (JoystickHatMoveImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.JoystickHatMove);
                }
            }
        }

        private void HandleJoystickHatMove(VariantMap eventData)
        {
            _JoystickHatMoveEventArgs.Set(eventData);
            JoystickHatMoveImpl?.Invoke(_urhoObject.Value, _JoystickHatMoveEventArgs);
        }

        #endregion

        #region TouchBegin
        // -------------------------------------------- TouchBegin --------------------------------------------

        /// <summary>
        /// Finger pressed on the screen.
        /// </summary>
        public class TouchBeginEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                TouchID = eventData[E.TouchBegin.TouchID].Int;
                X = eventData[E.TouchBegin.X].Int;
                Y = eventData[E.TouchBegin.Y].Int;
                Pressure = eventData[E.TouchBegin.Pressure].Float;
            }

            public int TouchID { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public float Pressure { get; private set; }
        }

        private event EventHandler<TouchBeginEventArgs> TouchBeginImpl;

        private readonly TouchBeginEventArgs _TouchBeginEventArgs = new TouchBeginEventArgs();
        
        /// <summary>
        /// Finger pressed on the screen.
        /// </summary>
        public event EventHandler<TouchBeginEventArgs> TouchBegin
        {
            add
            {
                if (TouchBeginImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TouchBegin, HandleTouchBegin);
                }
                TouchBeginImpl += value;
            }
            remove
            {
                TouchBeginImpl -= value;
                if (TouchBeginImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TouchBegin);
                }
            }
        }

        private void HandleTouchBegin(VariantMap eventData)
        {
            _TouchBeginEventArgs.Set(eventData);
            TouchBeginImpl?.Invoke(_urhoObject.Value, _TouchBeginEventArgs);
        }

        #endregion

        #region TouchEnd
        // -------------------------------------------- TouchEnd --------------------------------------------

        /// <summary>
        /// Finger released from the screen.
        /// </summary>
        public class TouchEndEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                TouchID = eventData[E.TouchEnd.TouchID].Int;
                X = eventData[E.TouchEnd.X].Int;
                Y = eventData[E.TouchEnd.Y].Int;
            }

            public int TouchID { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }
        }

        private event EventHandler<TouchEndEventArgs> TouchEndImpl;

        private readonly TouchEndEventArgs _TouchEndEventArgs = new TouchEndEventArgs();
        
        /// <summary>
        /// Finger released from the screen.
        /// </summary>
        public event EventHandler<TouchEndEventArgs> TouchEnd
        {
            add
            {
                if (TouchEndImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TouchEnd, HandleTouchEnd);
                }
                TouchEndImpl += value;
            }
            remove
            {
                TouchEndImpl -= value;
                if (TouchEndImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TouchEnd);
                }
            }
        }

        private void HandleTouchEnd(VariantMap eventData)
        {
            _TouchEndEventArgs.Set(eventData);
            TouchEndImpl?.Invoke(_urhoObject.Value, _TouchEndEventArgs);
        }

        #endregion

        #region TouchMove
        // -------------------------------------------- TouchMove --------------------------------------------

        /// <summary>
        /// Finger moved on the screen.
        /// </summary>
        public class TouchMoveEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                TouchID = eventData[E.TouchMove.TouchID].Int;
                X = eventData[E.TouchMove.X].Int;
                Y = eventData[E.TouchMove.Y].Int;
                DX = eventData[E.TouchMove.DX].Int;
                DY = eventData[E.TouchMove.DY].Int;
                Pressure = eventData[E.TouchMove.Pressure].Float;
            }

            public int TouchID { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int DX { get; private set; }

            public int DY { get; private set; }

            public float Pressure { get; private set; }
        }

        private event EventHandler<TouchMoveEventArgs> TouchMoveImpl;

        private readonly TouchMoveEventArgs _TouchMoveEventArgs = new TouchMoveEventArgs();
        
        /// <summary>
        /// Finger moved on the screen.
        /// </summary>
        public event EventHandler<TouchMoveEventArgs> TouchMove
        {
            add
            {
                if (TouchMoveImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TouchMove, HandleTouchMove);
                }
                TouchMoveImpl += value;
            }
            remove
            {
                TouchMoveImpl -= value;
                if (TouchMoveImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TouchMove);
                }
            }
        }

        private void HandleTouchMove(VariantMap eventData)
        {
            _TouchMoveEventArgs.Set(eventData);
            TouchMoveImpl?.Invoke(_urhoObject.Value, _TouchMoveEventArgs);
        }

        #endregion

        #region GestureRecorded
        // -------------------------------------------- GestureRecorded --------------------------------------------

        /// <summary>
        /// A touch gesture finished recording.
        /// </summary>
        public class GestureRecordedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                GestureID = eventData[E.GestureRecorded.GestureID].UInt;
            }

            public uint GestureID { get; private set; }
        }

        private event EventHandler<GestureRecordedEventArgs> GestureRecordedImpl;

        private readonly GestureRecordedEventArgs _GestureRecordedEventArgs = new GestureRecordedEventArgs();
        
        /// <summary>
        /// A touch gesture finished recording.
        /// </summary>
        public event EventHandler<GestureRecordedEventArgs> GestureRecorded
        {
            add
            {
                if (GestureRecordedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.GestureRecorded, HandleGestureRecorded);
                }
                GestureRecordedImpl += value;
            }
            remove
            {
                GestureRecordedImpl -= value;
                if (GestureRecordedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.GestureRecorded);
                }
            }
        }

        private void HandleGestureRecorded(VariantMap eventData)
        {
            _GestureRecordedEventArgs.Set(eventData);
            GestureRecordedImpl?.Invoke(_urhoObject.Value, _GestureRecordedEventArgs);
        }

        #endregion

        #region GestureInput
        // -------------------------------------------- GestureInput --------------------------------------------

        /// <summary>
        /// A recognized touch gesture was input by the user.
        /// </summary>
        public class GestureInputEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                GestureID = eventData[E.GestureInput.GestureID].UInt;
                CenterX = eventData[E.GestureInput.CenterX].Int;
                CenterY = eventData[E.GestureInput.CenterY].Int;
                NumFingers = eventData[E.GestureInput.NumFingers].Int;
                Error = eventData[E.GestureInput.Error].Float;
            }

            public uint GestureID { get; private set; }

            public int CenterX { get; private set; }

            public int CenterY { get; private set; }

            public int NumFingers { get; private set; }

            public float Error { get; private set; }
        }

        private event EventHandler<GestureInputEventArgs> GestureInputImpl;

        private readonly GestureInputEventArgs _GestureInputEventArgs = new GestureInputEventArgs();
        
        /// <summary>
        /// A recognized touch gesture was input by the user.
        /// </summary>
        public event EventHandler<GestureInputEventArgs> GestureInput
        {
            add
            {
                if (GestureInputImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.GestureInput, HandleGestureInput);
                }
                GestureInputImpl += value;
            }
            remove
            {
                GestureInputImpl -= value;
                if (GestureInputImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.GestureInput);
                }
            }
        }

        private void HandleGestureInput(VariantMap eventData)
        {
            _GestureInputEventArgs.Set(eventData);
            GestureInputImpl?.Invoke(_urhoObject.Value, _GestureInputEventArgs);
        }

        #endregion

        #region MultiGesture
        // -------------------------------------------- MultiGesture --------------------------------------------

        /// <summary>
        /// Pinch/rotate multi-finger touch gesture motion update.
        /// </summary>
        public class MultiGestureEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                CenterX = eventData[E.MultiGesture.CenterX].Int;
                CenterY = eventData[E.MultiGesture.CenterY].Int;
                NumFingers = eventData[E.MultiGesture.NumFingers].Int;
                DTheta = eventData[E.MultiGesture.DTheta].Float;
                DDist = eventData[E.MultiGesture.DDist].Float;
            }

            public int CenterX { get; private set; }

            public int CenterY { get; private set; }

            public int NumFingers { get; private set; }

            public float DTheta { get; private set; }

            public float DDist { get; private set; }
        }

        private event EventHandler<MultiGestureEventArgs> MultiGestureImpl;

        private readonly MultiGestureEventArgs _MultiGestureEventArgs = new MultiGestureEventArgs();
        
        /// <summary>
        /// Pinch/rotate multi-finger touch gesture motion update.
        /// </summary>
        public event EventHandler<MultiGestureEventArgs> MultiGesture
        {
            add
            {
                if (MultiGestureImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.MultiGesture, HandleMultiGesture);
                }
                MultiGestureImpl += value;
            }
            remove
            {
                MultiGestureImpl -= value;
                if (MultiGestureImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.MultiGesture);
                }
            }
        }

        private void HandleMultiGesture(VariantMap eventData)
        {
            _MultiGestureEventArgs.Set(eventData);
            MultiGestureImpl?.Invoke(_urhoObject.Value, _MultiGestureEventArgs);
        }

        #endregion

        #region DropFile
        // -------------------------------------------- DropFile --------------------------------------------

        /// <summary>
        /// A file was drag-dropped into the application window.
        /// </summary>
        public class DropFileEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                FileName = eventData[E.DropFile.FileName].String;
            }

            public String FileName { get; private set; }
        }

        private event EventHandler<DropFileEventArgs> DropFileImpl;

        private readonly DropFileEventArgs _DropFileEventArgs = new DropFileEventArgs();
        
        /// <summary>
        /// A file was drag-dropped into the application window.
        /// </summary>
        public event EventHandler<DropFileEventArgs> DropFile
        {
            add
            {
                if (DropFileImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.DropFile, HandleDropFile);
                }
                DropFileImpl += value;
            }
            remove
            {
                DropFileImpl -= value;
                if (DropFileImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.DropFile);
                }
            }
        }

        private void HandleDropFile(VariantMap eventData)
        {
            _DropFileEventArgs.Set(eventData);
            DropFileImpl?.Invoke(_urhoObject.Value, _DropFileEventArgs);
        }

        #endregion

        #region InputFocus
        // -------------------------------------------- InputFocus --------------------------------------------

        /// <summary>
        /// Application input focus or minimization changed.
        /// </summary>
        public class InputFocusEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Focus = eventData[E.InputFocus.Focus].Bool;
                Minimized = eventData[E.InputFocus.Minimized].Bool;
            }

            public bool Focus { get; private set; }

            public bool Minimized { get; private set; }
        }

        private event EventHandler<InputFocusEventArgs> InputFocusImpl;

        private readonly InputFocusEventArgs _InputFocusEventArgs = new InputFocusEventArgs();
        
        /// <summary>
        /// Application input focus or minimization changed.
        /// </summary>
        public event EventHandler<InputFocusEventArgs> InputFocus
        {
            add
            {
                if (InputFocusImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.InputFocus, HandleInputFocus);
                }
                InputFocusImpl += value;
            }
            remove
            {
                InputFocusImpl -= value;
                if (InputFocusImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.InputFocus);
                }
            }
        }

        private void HandleInputFocus(VariantMap eventData)
        {
            _InputFocusEventArgs.Set(eventData);
            InputFocusImpl?.Invoke(_urhoObject.Value, _InputFocusEventArgs);
        }

        #endregion

        #region MouseVisibleChanged
        // -------------------------------------------- MouseVisibleChanged --------------------------------------------

        /// <summary>
        /// OS mouse cursor visibility changed.
        /// </summary>
        public class MouseVisibleChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Visible = eventData[E.MouseVisibleChanged.Visible].Bool;
            }

            public bool Visible { get; private set; }
        }

        private event EventHandler<MouseVisibleChangedEventArgs> MouseVisibleChangedImpl;

        private readonly MouseVisibleChangedEventArgs _MouseVisibleChangedEventArgs = new MouseVisibleChangedEventArgs();
        
        /// <summary>
        /// OS mouse cursor visibility changed.
        /// </summary>
        public event EventHandler<MouseVisibleChangedEventArgs> MouseVisibleChanged
        {
            add
            {
                if (MouseVisibleChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.MouseVisibleChanged, HandleMouseVisibleChanged);
                }
                MouseVisibleChangedImpl += value;
            }
            remove
            {
                MouseVisibleChangedImpl -= value;
                if (MouseVisibleChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.MouseVisibleChanged);
                }
            }
        }

        private void HandleMouseVisibleChanged(VariantMap eventData)
        {
            _MouseVisibleChangedEventArgs.Set(eventData);
            MouseVisibleChangedImpl?.Invoke(_urhoObject.Value, _MouseVisibleChangedEventArgs);
        }

        #endregion

        #region MouseModeChanged
        // -------------------------------------------- MouseModeChanged --------------------------------------------

        /// <summary>
        /// Mouse mode changed.
        /// </summary>
        public class MouseModeChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Mode = eventData[E.MouseModeChanged.Mode].Int;
                MouseLocked = eventData[E.MouseModeChanged.MouseLocked].Bool;
            }

            public int Mode { get; private set; }

            public bool MouseLocked { get; private set; }
        }

        private event EventHandler<MouseModeChangedEventArgs> MouseModeChangedImpl;

        private readonly MouseModeChangedEventArgs _MouseModeChangedEventArgs = new MouseModeChangedEventArgs();
        
        /// <summary>
        /// Mouse mode changed.
        /// </summary>
        public event EventHandler<MouseModeChangedEventArgs> MouseModeChanged
        {
            add
            {
                if (MouseModeChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.MouseModeChanged, HandleMouseModeChanged);
                }
                MouseModeChangedImpl += value;
            }
            remove
            {
                MouseModeChangedImpl -= value;
                if (MouseModeChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.MouseModeChanged);
                }
            }
        }

        private void HandleMouseModeChanged(VariantMap eventData)
        {
            _MouseModeChangedEventArgs.Set(eventData);
            MouseModeChangedImpl?.Invoke(_urhoObject.Value, _MouseModeChangedEventArgs);
        }

        #endregion

        #region ExitRequested
        // -------------------------------------------- ExitRequested --------------------------------------------

        /// <summary>
        /// Application exit requested.
        /// </summary>
        public class ExitRequestedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ExitRequestedEventArgs> ExitRequestedImpl;

        private readonly ExitRequestedEventArgs _ExitRequestedEventArgs = new ExitRequestedEventArgs();
        
        /// <summary>
        /// Application exit requested.
        /// </summary>
        public event EventHandler<ExitRequestedEventArgs> ExitRequested
        {
            add
            {
                if (ExitRequestedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ExitRequested, HandleExitRequested);
                }
                ExitRequestedImpl += value;
            }
            remove
            {
                ExitRequestedImpl -= value;
                if (ExitRequestedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ExitRequested);
                }
            }
        }

        private void HandleExitRequested(VariantMap eventData)
        {
            _ExitRequestedEventArgs.Set(eventData);
            ExitRequestedImpl?.Invoke(_urhoObject.Value, _ExitRequestedEventArgs);
        }

        #endregion

        #region SDLRawInput
        // -------------------------------------------- SDLRawInput --------------------------------------------

        /// <summary>
        /// Raw SDL input event.
        /// </summary>
        public class SDLRawInputEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                SDLEvent = eventData[E.SDLRawInput.SDLEvent].VoidPtr;
                Consumed = eventData[E.SDLRawInput.Consumed].Bool;
            }

            public IntPtr SDLEvent { get; private set; }

            public bool Consumed { get; private set; }
        }

        private event EventHandler<SDLRawInputEventArgs> SDLRawInputImpl;

        private readonly SDLRawInputEventArgs _SDLRawInputEventArgs = new SDLRawInputEventArgs();
        
        /// <summary>
        /// Raw SDL input event.
        /// </summary>
        public event EventHandler<SDLRawInputEventArgs> SDLRawInput
        {
            add
            {
                if (SDLRawInputImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.SDLRawInput, HandleSDLRawInput);
                }
                SDLRawInputImpl += value;
            }
            remove
            {
                SDLRawInputImpl -= value;
                if (SDLRawInputImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.SDLRawInput);
                }
            }
        }

        private void HandleSDLRawInput(VariantMap eventData)
        {
            _SDLRawInputEventArgs.Set(eventData);
            SDLRawInputImpl?.Invoke(_urhoObject.Value, _SDLRawInputEventArgs);
        }

        #endregion

        #region InputBegin
        // -------------------------------------------- InputBegin --------------------------------------------

        /// <summary>
        /// Input handling begins.
        /// </summary>
        public class InputBeginEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<InputBeginEventArgs> InputBeginImpl;

        private readonly InputBeginEventArgs _InputBeginEventArgs = new InputBeginEventArgs();
        
        /// <summary>
        /// Input handling begins.
        /// </summary>
        public event EventHandler<InputBeginEventArgs> InputBegin
        {
            add
            {
                if (InputBeginImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.InputBegin, HandleInputBegin);
                }
                InputBeginImpl += value;
            }
            remove
            {
                InputBeginImpl -= value;
                if (InputBeginImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.InputBegin);
                }
            }
        }

        private void HandleInputBegin(VariantMap eventData)
        {
            _InputBeginEventArgs.Set(eventData);
            InputBeginImpl?.Invoke(_urhoObject.Value, _InputBeginEventArgs);
        }

        #endregion

        #region InputEnd
        // -------------------------------------------- InputEnd --------------------------------------------

        /// <summary>
        /// Input handling ends.
        /// </summary>
        public class InputEndEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<InputEndEventArgs> InputEndImpl;

        private readonly InputEndEventArgs _InputEndEventArgs = new InputEndEventArgs();
        
        /// <summary>
        /// Input handling ends.
        /// </summary>
        public event EventHandler<InputEndEventArgs> InputEnd
        {
            add
            {
                if (InputEndImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.InputEnd, HandleInputEnd);
                }
                InputEndImpl += value;
            }
            remove
            {
                InputEndImpl -= value;
                if (InputEndImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.InputEnd);
                }
            }
        }

        private void HandleInputEnd(VariantMap eventData)
        {
            _InputEndEventArgs.Set(eventData);
            InputEndImpl?.Invoke(_urhoObject.Value, _InputEndEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (MouseButtonDownImpl != null) urhoObject.UnsubscribeFromEvent(E.MouseButtonDown);
                if (MouseButtonUpImpl != null) urhoObject.UnsubscribeFromEvent(E.MouseButtonUp);
                if (MouseMoveImpl != null) urhoObject.UnsubscribeFromEvent(E.MouseMove);
                if (MouseWheelImpl != null) urhoObject.UnsubscribeFromEvent(E.MouseWheel);
                if (KeyDownImpl != null) urhoObject.UnsubscribeFromEvent(E.KeyDown);
                if (KeyUpImpl != null) urhoObject.UnsubscribeFromEvent(E.KeyUp);
                if (TextInputImpl != null) urhoObject.UnsubscribeFromEvent(E.TextInput);
                if (TextEditingImpl != null) urhoObject.UnsubscribeFromEvent(E.TextEditing);
                if (JoystickConnectedImpl != null) urhoObject.UnsubscribeFromEvent(E.JoystickConnected);
                if (JoystickDisconnectedImpl != null) urhoObject.UnsubscribeFromEvent(E.JoystickDisconnected);
                if (JoystickButtonDownImpl != null) urhoObject.UnsubscribeFromEvent(E.JoystickButtonDown);
                if (JoystickButtonUpImpl != null) urhoObject.UnsubscribeFromEvent(E.JoystickButtonUp);
                if (JoystickAxisMoveImpl != null) urhoObject.UnsubscribeFromEvent(E.JoystickAxisMove);
                if (JoystickHatMoveImpl != null) urhoObject.UnsubscribeFromEvent(E.JoystickHatMove);
                if (TouchBeginImpl != null) urhoObject.UnsubscribeFromEvent(E.TouchBegin);
                if (TouchEndImpl != null) urhoObject.UnsubscribeFromEvent(E.TouchEnd);
                if (TouchMoveImpl != null) urhoObject.UnsubscribeFromEvent(E.TouchMove);
                if (GestureRecordedImpl != null) urhoObject.UnsubscribeFromEvent(E.GestureRecorded);
                if (GestureInputImpl != null) urhoObject.UnsubscribeFromEvent(E.GestureInput);
                if (MultiGestureImpl != null) urhoObject.UnsubscribeFromEvent(E.MultiGesture);
                if (DropFileImpl != null) urhoObject.UnsubscribeFromEvent(E.DropFile);
                if (InputFocusImpl != null) urhoObject.UnsubscribeFromEvent(E.InputFocus);
                if (MouseVisibleChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.MouseVisibleChanged);
                if (MouseModeChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.MouseModeChanged);
                if (ExitRequestedImpl != null) urhoObject.UnsubscribeFromEvent(E.ExitRequested);
                if (SDLRawInputImpl != null) urhoObject.UnsubscribeFromEvent(E.SDLRawInput);
                if (InputBeginImpl != null) urhoObject.UnsubscribeFromEvent(E.InputBegin);
                if (InputEndImpl != null) urhoObject.UnsubscribeFromEvent(E.InputEnd);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class IOEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public IOEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region LogMessage
        // -------------------------------------------- LogMessage --------------------------------------------

        /// <summary>
        /// Log message event.
        /// </summary>
        public class LogMessageEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Message = eventData[E.LogMessage.Message].String;
                Logger = eventData[E.LogMessage.Logger].String;
                Level = eventData[E.LogMessage.Level].Int;
                Time = eventData[E.LogMessage.Time].UInt;
            }

            public String Message { get; private set; }

            public String Logger { get; private set; }

            public int Level { get; private set; }

            public uint Time { get; private set; }
        }

        private event EventHandler<LogMessageEventArgs> LogMessageImpl;

        private readonly LogMessageEventArgs _LogMessageEventArgs = new LogMessageEventArgs();
        
        /// <summary>
        /// Log message event.
        /// </summary>
        public event EventHandler<LogMessageEventArgs> LogMessage
        {
            add
            {
                if (LogMessageImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.LogMessage, HandleLogMessage);
                }
                LogMessageImpl += value;
            }
            remove
            {
                LogMessageImpl -= value;
                if (LogMessageImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.LogMessage);
                }
            }
        }

        private void HandleLogMessage(VariantMap eventData)
        {
            _LogMessageEventArgs.Set(eventData);
            LogMessageImpl?.Invoke(_urhoObject.Value, _LogMessageEventArgs);
        }

        #endregion

        #region AsyncExecFinished
        // -------------------------------------------- AsyncExecFinished --------------------------------------------

        /// <summary>
        /// Async system command execution finished.
        /// </summary>
        public class AsyncExecFinishedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                RequestID = eventData[E.AsyncExecFinished.RequestID].UInt;
                ExitCode = eventData[E.AsyncExecFinished.ExitCode].Int;
            }

            public uint RequestID { get; private set; }

            public int ExitCode { get; private set; }
        }

        private event EventHandler<AsyncExecFinishedEventArgs> AsyncExecFinishedImpl;

        private readonly AsyncExecFinishedEventArgs _AsyncExecFinishedEventArgs = new AsyncExecFinishedEventArgs();
        
        /// <summary>
        /// Async system command execution finished.
        /// </summary>
        public event EventHandler<AsyncExecFinishedEventArgs> AsyncExecFinished
        {
            add
            {
                if (AsyncExecFinishedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AsyncExecFinished, HandleAsyncExecFinished);
                }
                AsyncExecFinishedImpl += value;
            }
            remove
            {
                AsyncExecFinishedImpl -= value;
                if (AsyncExecFinishedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AsyncExecFinished);
                }
            }
        }

        private void HandleAsyncExecFinished(VariantMap eventData)
        {
            _AsyncExecFinishedEventArgs.Set(eventData);
            AsyncExecFinishedImpl?.Invoke(_urhoObject.Value, _AsyncExecFinishedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (LogMessageImpl != null) urhoObject.UnsubscribeFromEvent(E.LogMessage);
                if (AsyncExecFinishedImpl != null) urhoObject.UnsubscribeFromEvent(E.AsyncExecFinished);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class NavigationEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public NavigationEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region NavigationMeshRebuilt
        // -------------------------------------------- NavigationMeshRebuilt --------------------------------------------

        /// <summary>
        /// Complete rebuild of navigation mesh.
        /// </summary>
        public class NavigationMeshRebuiltEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.NavigationMeshRebuilt.Node].VoidPtr;
                Mesh = eventData[E.NavigationMeshRebuilt.Mesh].VoidPtr;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Mesh { get; private set; }
        }

        private event EventHandler<NavigationMeshRebuiltEventArgs> NavigationMeshRebuiltImpl;

        private readonly NavigationMeshRebuiltEventArgs _NavigationMeshRebuiltEventArgs = new NavigationMeshRebuiltEventArgs();
        
        /// <summary>
        /// Complete rebuild of navigation mesh.
        /// </summary>
        public event EventHandler<NavigationMeshRebuiltEventArgs> NavigationMeshRebuilt
        {
            add
            {
                if (NavigationMeshRebuiltImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NavigationMeshRebuilt, HandleNavigationMeshRebuilt);
                }
                NavigationMeshRebuiltImpl += value;
            }
            remove
            {
                NavigationMeshRebuiltImpl -= value;
                if (NavigationMeshRebuiltImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NavigationMeshRebuilt);
                }
            }
        }

        private void HandleNavigationMeshRebuilt(VariantMap eventData)
        {
            _NavigationMeshRebuiltEventArgs.Set(eventData);
            NavigationMeshRebuiltImpl?.Invoke(_urhoObject.Value, _NavigationMeshRebuiltEventArgs);
        }

        #endregion

        #region NavigationAreaRebuilt
        // -------------------------------------------- NavigationAreaRebuilt --------------------------------------------

        /// <summary>
        /// Partial bounding box rebuild of navigation mesh.
        /// </summary>
        public class NavigationAreaRebuiltEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.NavigationAreaRebuilt.Node].VoidPtr;
                Mesh = eventData[E.NavigationAreaRebuilt.Mesh].VoidPtr;
                BoundsMin = eventData[E.NavigationAreaRebuilt.BoundsMin].Vector3;
                BoundsMax = eventData[E.NavigationAreaRebuilt.BoundsMax].Vector3;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Mesh { get; private set; }

            public Vector3 BoundsMin { get; private set; }

            public Vector3 BoundsMax { get; private set; }
        }

        private event EventHandler<NavigationAreaRebuiltEventArgs> NavigationAreaRebuiltImpl;

        private readonly NavigationAreaRebuiltEventArgs _NavigationAreaRebuiltEventArgs = new NavigationAreaRebuiltEventArgs();
        
        /// <summary>
        /// Partial bounding box rebuild of navigation mesh.
        /// </summary>
        public event EventHandler<NavigationAreaRebuiltEventArgs> NavigationAreaRebuilt
        {
            add
            {
                if (NavigationAreaRebuiltImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NavigationAreaRebuilt, HandleNavigationAreaRebuilt);
                }
                NavigationAreaRebuiltImpl += value;
            }
            remove
            {
                NavigationAreaRebuiltImpl -= value;
                if (NavigationAreaRebuiltImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NavigationAreaRebuilt);
                }
            }
        }

        private void HandleNavigationAreaRebuilt(VariantMap eventData)
        {
            _NavigationAreaRebuiltEventArgs.Set(eventData);
            NavigationAreaRebuiltImpl?.Invoke(_urhoObject.Value, _NavigationAreaRebuiltEventArgs);
        }

        #endregion

        #region NavigationTileAdded
        // -------------------------------------------- NavigationTileAdded --------------------------------------------

        /// <summary>
        /// Mesh tile is added to navigation mesh.
        /// </summary>
        public class NavigationTileAddedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.NavigationTileAdded.Node].VoidPtr;
                Mesh = eventData[E.NavigationTileAdded.Mesh].VoidPtr;
                Tile = eventData[E.NavigationTileAdded.Tile].Int;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Mesh { get; private set; }

            public int Tile { get; private set; }
        }

        private event EventHandler<NavigationTileAddedEventArgs> NavigationTileAddedImpl;

        private readonly NavigationTileAddedEventArgs _NavigationTileAddedEventArgs = new NavigationTileAddedEventArgs();
        
        /// <summary>
        /// Mesh tile is added to navigation mesh.
        /// </summary>
        public event EventHandler<NavigationTileAddedEventArgs> NavigationTileAdded
        {
            add
            {
                if (NavigationTileAddedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NavigationTileAdded, HandleNavigationTileAdded);
                }
                NavigationTileAddedImpl += value;
            }
            remove
            {
                NavigationTileAddedImpl -= value;
                if (NavigationTileAddedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NavigationTileAdded);
                }
            }
        }

        private void HandleNavigationTileAdded(VariantMap eventData)
        {
            _NavigationTileAddedEventArgs.Set(eventData);
            NavigationTileAddedImpl?.Invoke(_urhoObject.Value, _NavigationTileAddedEventArgs);
        }

        #endregion

        #region NavigationTileRemoved
        // -------------------------------------------- NavigationTileRemoved --------------------------------------------

        /// <summary>
        /// Mesh tile is removed from navigation mesh.
        /// </summary>
        public class NavigationTileRemovedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.NavigationTileRemoved.Node].VoidPtr;
                Mesh = eventData[E.NavigationTileRemoved.Mesh].VoidPtr;
                Tile = eventData[E.NavigationTileRemoved.Tile].Int;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Mesh { get; private set; }

            public int Tile { get; private set; }
        }

        private event EventHandler<NavigationTileRemovedEventArgs> NavigationTileRemovedImpl;

        private readonly NavigationTileRemovedEventArgs _NavigationTileRemovedEventArgs = new NavigationTileRemovedEventArgs();
        
        /// <summary>
        /// Mesh tile is removed from navigation mesh.
        /// </summary>
        public event EventHandler<NavigationTileRemovedEventArgs> NavigationTileRemoved
        {
            add
            {
                if (NavigationTileRemovedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NavigationTileRemoved, HandleNavigationTileRemoved);
                }
                NavigationTileRemovedImpl += value;
            }
            remove
            {
                NavigationTileRemovedImpl -= value;
                if (NavigationTileRemovedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NavigationTileRemoved);
                }
            }
        }

        private void HandleNavigationTileRemoved(VariantMap eventData)
        {
            _NavigationTileRemovedEventArgs.Set(eventData);
            NavigationTileRemovedImpl?.Invoke(_urhoObject.Value, _NavigationTileRemovedEventArgs);
        }

        #endregion

        #region NavigationAllTilesRemoved
        // -------------------------------------------- NavigationAllTilesRemoved --------------------------------------------

        /// <summary>
        /// All mesh tiles are removed from navigation mesh.
        /// </summary>
        public class NavigationAllTilesRemovedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.NavigationAllTilesRemoved.Node].VoidPtr;
                Mesh = eventData[E.NavigationAllTilesRemoved.Mesh].VoidPtr;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Mesh { get; private set; }
        }

        private event EventHandler<NavigationAllTilesRemovedEventArgs> NavigationAllTilesRemovedImpl;

        private readonly NavigationAllTilesRemovedEventArgs _NavigationAllTilesRemovedEventArgs = new NavigationAllTilesRemovedEventArgs();
        
        /// <summary>
        /// All mesh tiles are removed from navigation mesh.
        /// </summary>
        public event EventHandler<NavigationAllTilesRemovedEventArgs> NavigationAllTilesRemoved
        {
            add
            {
                if (NavigationAllTilesRemovedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NavigationAllTilesRemoved, HandleNavigationAllTilesRemoved);
                }
                NavigationAllTilesRemovedImpl += value;
            }
            remove
            {
                NavigationAllTilesRemovedImpl -= value;
                if (NavigationAllTilesRemovedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NavigationAllTilesRemoved);
                }
            }
        }

        private void HandleNavigationAllTilesRemoved(VariantMap eventData)
        {
            _NavigationAllTilesRemovedEventArgs.Set(eventData);
            NavigationAllTilesRemovedImpl?.Invoke(_urhoObject.Value, _NavigationAllTilesRemovedEventArgs);
        }

        #endregion

        #region CrowdAgentFormation
        // -------------------------------------------- CrowdAgentFormation --------------------------------------------

        /// <summary>
        /// Crowd agent formation.
        /// </summary>
        public class CrowdAgentFormationEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.CrowdAgentFormation.Node].VoidPtr;
                CrowdAgent = eventData[E.CrowdAgentFormation.CrowdAgent].VoidPtr;
                Index = eventData[E.CrowdAgentFormation.Index].UInt;
                Size = eventData[E.CrowdAgentFormation.Size].UInt;
                Position = eventData[E.CrowdAgentFormation.Position].Vector3;
            }

            public IntPtr Node { get; private set; }

            public IntPtr CrowdAgent { get; private set; }

            public uint Index { get; private set; }

            public uint Size { get; private set; }

            public Vector3 Position { get; private set; }
        }

        private event EventHandler<CrowdAgentFormationEventArgs> CrowdAgentFormationImpl;

        private readonly CrowdAgentFormationEventArgs _CrowdAgentFormationEventArgs = new CrowdAgentFormationEventArgs();
        
        /// <summary>
        /// Crowd agent formation.
        /// </summary>
        public event EventHandler<CrowdAgentFormationEventArgs> CrowdAgentFormation
        {
            add
            {
                if (CrowdAgentFormationImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.CrowdAgentFormation, HandleCrowdAgentFormation);
                }
                CrowdAgentFormationImpl += value;
            }
            remove
            {
                CrowdAgentFormationImpl -= value;
                if (CrowdAgentFormationImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.CrowdAgentFormation);
                }
            }
        }

        private void HandleCrowdAgentFormation(VariantMap eventData)
        {
            _CrowdAgentFormationEventArgs.Set(eventData);
            CrowdAgentFormationImpl?.Invoke(_urhoObject.Value, _CrowdAgentFormationEventArgs);
        }

        #endregion

        #region CrowdAgentNodeFormation
        // -------------------------------------------- CrowdAgentNodeFormation --------------------------------------------

        /// <summary>
        /// Crowd agent formation specific to a node.
        /// </summary>
        public class CrowdAgentNodeFormationEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.CrowdAgentNodeFormation.Node].VoidPtr;
                CrowdAgent = eventData[E.CrowdAgentNodeFormation.CrowdAgent].VoidPtr;
                Index = eventData[E.CrowdAgentNodeFormation.Index].UInt;
                Size = eventData[E.CrowdAgentNodeFormation.Size].UInt;
                Position = eventData[E.CrowdAgentNodeFormation.Position].Vector3;
            }

            public IntPtr Node { get; private set; }

            public IntPtr CrowdAgent { get; private set; }

            public uint Index { get; private set; }

            public uint Size { get; private set; }

            public Vector3 Position { get; private set; }
        }

        private event EventHandler<CrowdAgentNodeFormationEventArgs> CrowdAgentNodeFormationImpl;

        private readonly CrowdAgentNodeFormationEventArgs _CrowdAgentNodeFormationEventArgs = new CrowdAgentNodeFormationEventArgs();
        
        /// <summary>
        /// Crowd agent formation specific to a node.
        /// </summary>
        public event EventHandler<CrowdAgentNodeFormationEventArgs> CrowdAgentNodeFormation
        {
            add
            {
                if (CrowdAgentNodeFormationImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.CrowdAgentNodeFormation, HandleCrowdAgentNodeFormation);
                }
                CrowdAgentNodeFormationImpl += value;
            }
            remove
            {
                CrowdAgentNodeFormationImpl -= value;
                if (CrowdAgentNodeFormationImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.CrowdAgentNodeFormation);
                }
            }
        }

        private void HandleCrowdAgentNodeFormation(VariantMap eventData)
        {
            _CrowdAgentNodeFormationEventArgs.Set(eventData);
            CrowdAgentNodeFormationImpl?.Invoke(_urhoObject.Value, _CrowdAgentNodeFormationEventArgs);
        }

        #endregion

        #region CrowdAgentReposition
        // -------------------------------------------- CrowdAgentReposition --------------------------------------------

        /// <summary>
        /// Crowd agent has been repositioned.
        /// </summary>
        public class CrowdAgentRepositionEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.CrowdAgentReposition.Node].VoidPtr;
                CrowdAgent = eventData[E.CrowdAgentReposition.CrowdAgent].VoidPtr;
                Position = eventData[E.CrowdAgentReposition.Position].Vector3;
                Velocity = eventData[E.CrowdAgentReposition.Velocity].Vector3;
                Arrived = eventData[E.CrowdAgentReposition.Arrived].Bool;
                TimeStep = eventData[E.CrowdAgentReposition.TimeStep].Float;
            }

            public IntPtr Node { get; private set; }

            public IntPtr CrowdAgent { get; private set; }

            public Vector3 Position { get; private set; }

            public Vector3 Velocity { get; private set; }

            public bool Arrived { get; private set; }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<CrowdAgentRepositionEventArgs> CrowdAgentRepositionImpl;

        private readonly CrowdAgentRepositionEventArgs _CrowdAgentRepositionEventArgs = new CrowdAgentRepositionEventArgs();
        
        /// <summary>
        /// Crowd agent has been repositioned.
        /// </summary>
        public event EventHandler<CrowdAgentRepositionEventArgs> CrowdAgentReposition
        {
            add
            {
                if (CrowdAgentRepositionImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.CrowdAgentReposition, HandleCrowdAgentReposition);
                }
                CrowdAgentRepositionImpl += value;
            }
            remove
            {
                CrowdAgentRepositionImpl -= value;
                if (CrowdAgentRepositionImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.CrowdAgentReposition);
                }
            }
        }

        private void HandleCrowdAgentReposition(VariantMap eventData)
        {
            _CrowdAgentRepositionEventArgs.Set(eventData);
            CrowdAgentRepositionImpl?.Invoke(_urhoObject.Value, _CrowdAgentRepositionEventArgs);
        }

        #endregion

        #region CrowdAgentNodeReposition
        // -------------------------------------------- CrowdAgentNodeReposition --------------------------------------------

        /// <summary>
        /// Crowd agent has been repositioned, specific to a node.
        /// </summary>
        public class CrowdAgentNodeRepositionEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.CrowdAgentNodeReposition.Node].VoidPtr;
                CrowdAgent = eventData[E.CrowdAgentNodeReposition.CrowdAgent].VoidPtr;
                Position = eventData[E.CrowdAgentNodeReposition.Position].Vector3;
                Velocity = eventData[E.CrowdAgentNodeReposition.Velocity].Vector3;
                Arrived = eventData[E.CrowdAgentNodeReposition.Arrived].Bool;
                TimeStep = eventData[E.CrowdAgentNodeReposition.TimeStep].Float;
            }

            public IntPtr Node { get; private set; }

            public IntPtr CrowdAgent { get; private set; }

            public Vector3 Position { get; private set; }

            public Vector3 Velocity { get; private set; }

            public bool Arrived { get; private set; }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<CrowdAgentNodeRepositionEventArgs> CrowdAgentNodeRepositionImpl;

        private readonly CrowdAgentNodeRepositionEventArgs _CrowdAgentNodeRepositionEventArgs = new CrowdAgentNodeRepositionEventArgs();
        
        /// <summary>
        /// Crowd agent has been repositioned, specific to a node.
        /// </summary>
        public event EventHandler<CrowdAgentNodeRepositionEventArgs> CrowdAgentNodeReposition
        {
            add
            {
                if (CrowdAgentNodeRepositionImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.CrowdAgentNodeReposition, HandleCrowdAgentNodeReposition);
                }
                CrowdAgentNodeRepositionImpl += value;
            }
            remove
            {
                CrowdAgentNodeRepositionImpl -= value;
                if (CrowdAgentNodeRepositionImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.CrowdAgentNodeReposition);
                }
            }
        }

        private void HandleCrowdAgentNodeReposition(VariantMap eventData)
        {
            _CrowdAgentNodeRepositionEventArgs.Set(eventData);
            CrowdAgentNodeRepositionImpl?.Invoke(_urhoObject.Value, _CrowdAgentNodeRepositionEventArgs);
        }

        #endregion

        #region CrowdAgentFailure
        // -------------------------------------------- CrowdAgentFailure --------------------------------------------

        /// <summary>
        /// Crowd agent's internal state has become invalidated. This is a special case of CrowdAgentStateChanged event.
        /// </summary>
        public class CrowdAgentFailureEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.CrowdAgentFailure.Node].VoidPtr;
                CrowdAgent = eventData[E.CrowdAgentFailure.CrowdAgent].VoidPtr;
                Position = eventData[E.CrowdAgentFailure.Position].Vector3;
                Velocity = eventData[E.CrowdAgentFailure.Velocity].Vector3;
                CrowdAgentState = eventData[E.CrowdAgentFailure.CrowdAgentState].Int;
                CrowdTargetState = eventData[E.CrowdAgentFailure.CrowdTargetState].Int;
            }

            public IntPtr Node { get; private set; }

            public IntPtr CrowdAgent { get; private set; }

            public Vector3 Position { get; private set; }

            public Vector3 Velocity { get; private set; }

            public int CrowdAgentState { get; private set; }

            public int CrowdTargetState { get; private set; }
        }

        private event EventHandler<CrowdAgentFailureEventArgs> CrowdAgentFailureImpl;

        private readonly CrowdAgentFailureEventArgs _CrowdAgentFailureEventArgs = new CrowdAgentFailureEventArgs();
        
        /// <summary>
        /// Crowd agent's internal state has become invalidated. This is a special case of CrowdAgentStateChanged event.
        /// </summary>
        public event EventHandler<CrowdAgentFailureEventArgs> CrowdAgentFailure
        {
            add
            {
                if (CrowdAgentFailureImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.CrowdAgentFailure, HandleCrowdAgentFailure);
                }
                CrowdAgentFailureImpl += value;
            }
            remove
            {
                CrowdAgentFailureImpl -= value;
                if (CrowdAgentFailureImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.CrowdAgentFailure);
                }
            }
        }

        private void HandleCrowdAgentFailure(VariantMap eventData)
        {
            _CrowdAgentFailureEventArgs.Set(eventData);
            CrowdAgentFailureImpl?.Invoke(_urhoObject.Value, _CrowdAgentFailureEventArgs);
        }

        #endregion

        #region CrowdAgentNodeFailure
        // -------------------------------------------- CrowdAgentNodeFailure --------------------------------------------

        /// <summary>
        /// Crowd agent's internal state has become invalidated. This is a special case of CrowdAgentStateChanged event.
        /// </summary>
        public class CrowdAgentNodeFailureEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.CrowdAgentNodeFailure.Node].VoidPtr;
                CrowdAgent = eventData[E.CrowdAgentNodeFailure.CrowdAgent].VoidPtr;
                Position = eventData[E.CrowdAgentNodeFailure.Position].Vector3;
                Velocity = eventData[E.CrowdAgentNodeFailure.Velocity].Vector3;
                CrowdAgentState = eventData[E.CrowdAgentNodeFailure.CrowdAgentState].Int;
                CrowdTargetState = eventData[E.CrowdAgentNodeFailure.CrowdTargetState].Int;
            }

            public IntPtr Node { get; private set; }

            public IntPtr CrowdAgent { get; private set; }

            public Vector3 Position { get; private set; }

            public Vector3 Velocity { get; private set; }

            public int CrowdAgentState { get; private set; }

            public int CrowdTargetState { get; private set; }
        }

        private event EventHandler<CrowdAgentNodeFailureEventArgs> CrowdAgentNodeFailureImpl;

        private readonly CrowdAgentNodeFailureEventArgs _CrowdAgentNodeFailureEventArgs = new CrowdAgentNodeFailureEventArgs();
        
        /// <summary>
        /// Crowd agent's internal state has become invalidated. This is a special case of CrowdAgentStateChanged event.
        /// </summary>
        public event EventHandler<CrowdAgentNodeFailureEventArgs> CrowdAgentNodeFailure
        {
            add
            {
                if (CrowdAgentNodeFailureImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.CrowdAgentNodeFailure, HandleCrowdAgentNodeFailure);
                }
                CrowdAgentNodeFailureImpl += value;
            }
            remove
            {
                CrowdAgentNodeFailureImpl -= value;
                if (CrowdAgentNodeFailureImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.CrowdAgentNodeFailure);
                }
            }
        }

        private void HandleCrowdAgentNodeFailure(VariantMap eventData)
        {
            _CrowdAgentNodeFailureEventArgs.Set(eventData);
            CrowdAgentNodeFailureImpl?.Invoke(_urhoObject.Value, _CrowdAgentNodeFailureEventArgs);
        }

        #endregion

        #region CrowdAgentStateChanged
        // -------------------------------------------- CrowdAgentStateChanged --------------------------------------------

        /// <summary>
        /// Crowd agent's state has been changed.
        /// </summary>
        public class CrowdAgentStateChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.CrowdAgentStateChanged.Node].VoidPtr;
                CrowdAgent = eventData[E.CrowdAgentStateChanged.CrowdAgent].VoidPtr;
                Position = eventData[E.CrowdAgentStateChanged.Position].Vector3;
                Velocity = eventData[E.CrowdAgentStateChanged.Velocity].Vector3;
                CrowdAgentState = eventData[E.CrowdAgentStateChanged.CrowdAgentState].Int;
                CrowdTargetState = eventData[E.CrowdAgentStateChanged.CrowdTargetState].Int;
            }

            public IntPtr Node { get; private set; }

            public IntPtr CrowdAgent { get; private set; }

            public Vector3 Position { get; private set; }

            public Vector3 Velocity { get; private set; }

            public int CrowdAgentState { get; private set; }

            public int CrowdTargetState { get; private set; }
        }

        private event EventHandler<CrowdAgentStateChangedEventArgs> CrowdAgentStateChangedImpl;

        private readonly CrowdAgentStateChangedEventArgs _CrowdAgentStateChangedEventArgs = new CrowdAgentStateChangedEventArgs();
        
        /// <summary>
        /// Crowd agent's state has been changed.
        /// </summary>
        public event EventHandler<CrowdAgentStateChangedEventArgs> CrowdAgentStateChanged
        {
            add
            {
                if (CrowdAgentStateChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.CrowdAgentStateChanged, HandleCrowdAgentStateChanged);
                }
                CrowdAgentStateChangedImpl += value;
            }
            remove
            {
                CrowdAgentStateChangedImpl -= value;
                if (CrowdAgentStateChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.CrowdAgentStateChanged);
                }
            }
        }

        private void HandleCrowdAgentStateChanged(VariantMap eventData)
        {
            _CrowdAgentStateChangedEventArgs.Set(eventData);
            CrowdAgentStateChangedImpl?.Invoke(_urhoObject.Value, _CrowdAgentStateChangedEventArgs);
        }

        #endregion

        #region CrowdAgentNodeStateChanged
        // -------------------------------------------- CrowdAgentNodeStateChanged --------------------------------------------

        /// <summary>
        /// Crowd agent's state has been changed.
        /// </summary>
        public class CrowdAgentNodeStateChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.CrowdAgentNodeStateChanged.Node].VoidPtr;
                CrowdAgent = eventData[E.CrowdAgentNodeStateChanged.CrowdAgent].VoidPtr;
                Position = eventData[E.CrowdAgentNodeStateChanged.Position].Vector3;
                Velocity = eventData[E.CrowdAgentNodeStateChanged.Velocity].Vector3;
                CrowdAgentState = eventData[E.CrowdAgentNodeStateChanged.CrowdAgentState].Int;
                CrowdTargetState = eventData[E.CrowdAgentNodeStateChanged.CrowdTargetState].Int;
            }

            public IntPtr Node { get; private set; }

            public IntPtr CrowdAgent { get; private set; }

            public Vector3 Position { get; private set; }

            public Vector3 Velocity { get; private set; }

            public int CrowdAgentState { get; private set; }

            public int CrowdTargetState { get; private set; }
        }

        private event EventHandler<CrowdAgentNodeStateChangedEventArgs> CrowdAgentNodeStateChangedImpl;

        private readonly CrowdAgentNodeStateChangedEventArgs _CrowdAgentNodeStateChangedEventArgs = new CrowdAgentNodeStateChangedEventArgs();
        
        /// <summary>
        /// Crowd agent's state has been changed.
        /// </summary>
        public event EventHandler<CrowdAgentNodeStateChangedEventArgs> CrowdAgentNodeStateChanged
        {
            add
            {
                if (CrowdAgentNodeStateChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.CrowdAgentNodeStateChanged, HandleCrowdAgentNodeStateChanged);
                }
                CrowdAgentNodeStateChangedImpl += value;
            }
            remove
            {
                CrowdAgentNodeStateChangedImpl -= value;
                if (CrowdAgentNodeStateChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.CrowdAgentNodeStateChanged);
                }
            }
        }

        private void HandleCrowdAgentNodeStateChanged(VariantMap eventData)
        {
            _CrowdAgentNodeStateChangedEventArgs.Set(eventData);
            CrowdAgentNodeStateChangedImpl?.Invoke(_urhoObject.Value, _CrowdAgentNodeStateChangedEventArgs);
        }

        #endregion

        #region NavigationObstacleAdded
        // -------------------------------------------- NavigationObstacleAdded --------------------------------------------

        /// <summary>
        /// Addition of obstacle to dynamic navigation mesh.
        /// </summary>
        public class NavigationObstacleAddedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.NavigationObstacleAdded.Node].VoidPtr;
                Obstacle = eventData[E.NavigationObstacleAdded.Obstacle].VoidPtr;
                Position = eventData[E.NavigationObstacleAdded.Position].Vector3;
                Radius = eventData[E.NavigationObstacleAdded.Radius].Float;
                Height = eventData[E.NavigationObstacleAdded.Height].Float;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Obstacle { get; private set; }

            public Vector3 Position { get; private set; }

            public float Radius { get; private set; }

            public float Height { get; private set; }
        }

        private event EventHandler<NavigationObstacleAddedEventArgs> NavigationObstacleAddedImpl;

        private readonly NavigationObstacleAddedEventArgs _NavigationObstacleAddedEventArgs = new NavigationObstacleAddedEventArgs();
        
        /// <summary>
        /// Addition of obstacle to dynamic navigation mesh.
        /// </summary>
        public event EventHandler<NavigationObstacleAddedEventArgs> NavigationObstacleAdded
        {
            add
            {
                if (NavigationObstacleAddedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NavigationObstacleAdded, HandleNavigationObstacleAdded);
                }
                NavigationObstacleAddedImpl += value;
            }
            remove
            {
                NavigationObstacleAddedImpl -= value;
                if (NavigationObstacleAddedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NavigationObstacleAdded);
                }
            }
        }

        private void HandleNavigationObstacleAdded(VariantMap eventData)
        {
            _NavigationObstacleAddedEventArgs.Set(eventData);
            NavigationObstacleAddedImpl?.Invoke(_urhoObject.Value, _NavigationObstacleAddedEventArgs);
        }

        #endregion

        #region NavigationObstacleRemoved
        // -------------------------------------------- NavigationObstacleRemoved --------------------------------------------

        /// <summary>
        /// Removal of obstacle from dynamic navigation mesh.
        /// </summary>
        public class NavigationObstacleRemovedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.NavigationObstacleRemoved.Node].VoidPtr;
                Obstacle = eventData[E.NavigationObstacleRemoved.Obstacle].VoidPtr;
                Position = eventData[E.NavigationObstacleRemoved.Position].Vector3;
                Radius = eventData[E.NavigationObstacleRemoved.Radius].Float;
                Height = eventData[E.NavigationObstacleRemoved.Height].Float;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Obstacle { get; private set; }

            public Vector3 Position { get; private set; }

            public float Radius { get; private set; }

            public float Height { get; private set; }
        }

        private event EventHandler<NavigationObstacleRemovedEventArgs> NavigationObstacleRemovedImpl;

        private readonly NavigationObstacleRemovedEventArgs _NavigationObstacleRemovedEventArgs = new NavigationObstacleRemovedEventArgs();
        
        /// <summary>
        /// Removal of obstacle from dynamic navigation mesh.
        /// </summary>
        public event EventHandler<NavigationObstacleRemovedEventArgs> NavigationObstacleRemoved
        {
            add
            {
                if (NavigationObstacleRemovedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NavigationObstacleRemoved, HandleNavigationObstacleRemoved);
                }
                NavigationObstacleRemovedImpl += value;
            }
            remove
            {
                NavigationObstacleRemovedImpl -= value;
                if (NavigationObstacleRemovedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NavigationObstacleRemoved);
                }
            }
        }

        private void HandleNavigationObstacleRemoved(VariantMap eventData)
        {
            _NavigationObstacleRemovedEventArgs.Set(eventData);
            NavigationObstacleRemovedImpl?.Invoke(_urhoObject.Value, _NavigationObstacleRemovedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (NavigationMeshRebuiltImpl != null) urhoObject.UnsubscribeFromEvent(E.NavigationMeshRebuilt);
                if (NavigationAreaRebuiltImpl != null) urhoObject.UnsubscribeFromEvent(E.NavigationAreaRebuilt);
                if (NavigationTileAddedImpl != null) urhoObject.UnsubscribeFromEvent(E.NavigationTileAdded);
                if (NavigationTileRemovedImpl != null) urhoObject.UnsubscribeFromEvent(E.NavigationTileRemoved);
                if (NavigationAllTilesRemovedImpl != null) urhoObject.UnsubscribeFromEvent(E.NavigationAllTilesRemoved);
                if (CrowdAgentFormationImpl != null) urhoObject.UnsubscribeFromEvent(E.CrowdAgentFormation);
                if (CrowdAgentNodeFormationImpl != null) urhoObject.UnsubscribeFromEvent(E.CrowdAgentNodeFormation);
                if (CrowdAgentRepositionImpl != null) urhoObject.UnsubscribeFromEvent(E.CrowdAgentReposition);
                if (CrowdAgentNodeRepositionImpl != null) urhoObject.UnsubscribeFromEvent(E.CrowdAgentNodeReposition);
                if (CrowdAgentFailureImpl != null) urhoObject.UnsubscribeFromEvent(E.CrowdAgentFailure);
                if (CrowdAgentNodeFailureImpl != null) urhoObject.UnsubscribeFromEvent(E.CrowdAgentNodeFailure);
                if (CrowdAgentStateChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.CrowdAgentStateChanged);
                if (CrowdAgentNodeStateChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.CrowdAgentNodeStateChanged);
                if (NavigationObstacleAddedImpl != null) urhoObject.UnsubscribeFromEvent(E.NavigationObstacleAdded);
                if (NavigationObstacleRemovedImpl != null) urhoObject.UnsubscribeFromEvent(E.NavigationObstacleRemoved);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class NetworkEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public NetworkEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region ServerConnected
        // -------------------------------------------- ServerConnected --------------------------------------------

        /// <summary>
        /// Server connection established.
        /// </summary>
        public class ServerConnectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ServerConnectedEventArgs> ServerConnectedImpl;

        private readonly ServerConnectedEventArgs _ServerConnectedEventArgs = new ServerConnectedEventArgs();
        
        /// <summary>
        /// Server connection established.
        /// </summary>
        public event EventHandler<ServerConnectedEventArgs> ServerConnected
        {
            add
            {
                if (ServerConnectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ServerConnected, HandleServerConnected);
                }
                ServerConnectedImpl += value;
            }
            remove
            {
                ServerConnectedImpl -= value;
                if (ServerConnectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ServerConnected);
                }
            }
        }

        private void HandleServerConnected(VariantMap eventData)
        {
            _ServerConnectedEventArgs.Set(eventData);
            ServerConnectedImpl?.Invoke(_urhoObject.Value, _ServerConnectedEventArgs);
        }

        #endregion

        #region ServerDisconnected
        // -------------------------------------------- ServerDisconnected --------------------------------------------

        /// <summary>
        /// Server connection disconnected.
        /// </summary>
        public class ServerDisconnectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ServerDisconnectedEventArgs> ServerDisconnectedImpl;

        private readonly ServerDisconnectedEventArgs _ServerDisconnectedEventArgs = new ServerDisconnectedEventArgs();
        
        /// <summary>
        /// Server connection disconnected.
        /// </summary>
        public event EventHandler<ServerDisconnectedEventArgs> ServerDisconnected
        {
            add
            {
                if (ServerDisconnectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ServerDisconnected, HandleServerDisconnected);
                }
                ServerDisconnectedImpl += value;
            }
            remove
            {
                ServerDisconnectedImpl -= value;
                if (ServerDisconnectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ServerDisconnected);
                }
            }
        }

        private void HandleServerDisconnected(VariantMap eventData)
        {
            _ServerDisconnectedEventArgs.Set(eventData);
            ServerDisconnectedImpl?.Invoke(_urhoObject.Value, _ServerDisconnectedEventArgs);
        }

        #endregion

        #region ConnectFailed
        // -------------------------------------------- ConnectFailed --------------------------------------------

        /// <summary>
        /// Server connection failed.
        /// </summary>
        public class ConnectFailedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ConnectFailedEventArgs> ConnectFailedImpl;

        private readonly ConnectFailedEventArgs _ConnectFailedEventArgs = new ConnectFailedEventArgs();
        
        /// <summary>
        /// Server connection failed.
        /// </summary>
        public event EventHandler<ConnectFailedEventArgs> ConnectFailed
        {
            add
            {
                if (ConnectFailedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ConnectFailed, HandleConnectFailed);
                }
                ConnectFailedImpl += value;
            }
            remove
            {
                ConnectFailedImpl -= value;
                if (ConnectFailedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ConnectFailed);
                }
            }
        }

        private void HandleConnectFailed(VariantMap eventData)
        {
            _ConnectFailedEventArgs.Set(eventData);
            ConnectFailedImpl?.Invoke(_urhoObject.Value, _ConnectFailedEventArgs);
        }

        #endregion

        #region ConnectionInProgress
        // -------------------------------------------- ConnectionInProgress --------------------------------------------

        /// <summary>
        /// Server connection failed because its already connected or tries to connect already.
        /// </summary>
        public class ConnectionInProgressEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ConnectionInProgressEventArgs> ConnectionInProgressImpl;

        private readonly ConnectionInProgressEventArgs _ConnectionInProgressEventArgs = new ConnectionInProgressEventArgs();
        
        /// <summary>
        /// Server connection failed because its already connected or tries to connect already.
        /// </summary>
        public event EventHandler<ConnectionInProgressEventArgs> ConnectionInProgress
        {
            add
            {
                if (ConnectionInProgressImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ConnectionInProgress, HandleConnectionInProgress);
                }
                ConnectionInProgressImpl += value;
            }
            remove
            {
                ConnectionInProgressImpl -= value;
                if (ConnectionInProgressImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ConnectionInProgress);
                }
            }
        }

        private void HandleConnectionInProgress(VariantMap eventData)
        {
            _ConnectionInProgressEventArgs.Set(eventData);
            ConnectionInProgressImpl?.Invoke(_urhoObject.Value, _ConnectionInProgressEventArgs);
        }

        #endregion

        #region ClientConnected
        // -------------------------------------------- ClientConnected --------------------------------------------

        /// <summary>
        /// New client connection established.
        /// </summary>
        public class ClientConnectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Connection = eventData[E.ClientConnected.Connection].VoidPtr;
            }

            public IntPtr Connection { get; private set; }
        }

        private event EventHandler<ClientConnectedEventArgs> ClientConnectedImpl;

        private readonly ClientConnectedEventArgs _ClientConnectedEventArgs = new ClientConnectedEventArgs();
        
        /// <summary>
        /// New client connection established.
        /// </summary>
        public event EventHandler<ClientConnectedEventArgs> ClientConnected
        {
            add
            {
                if (ClientConnectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ClientConnected, HandleClientConnected);
                }
                ClientConnectedImpl += value;
            }
            remove
            {
                ClientConnectedImpl -= value;
                if (ClientConnectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ClientConnected);
                }
            }
        }

        private void HandleClientConnected(VariantMap eventData)
        {
            _ClientConnectedEventArgs.Set(eventData);
            ClientConnectedImpl?.Invoke(_urhoObject.Value, _ClientConnectedEventArgs);
        }

        #endregion

        #region ClientDisconnected
        // -------------------------------------------- ClientDisconnected --------------------------------------------

        /// <summary>
        /// Client connection disconnected.
        /// </summary>
        public class ClientDisconnectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Connection = eventData[E.ClientDisconnected.Connection].VoidPtr;
            }

            public IntPtr Connection { get; private set; }
        }

        private event EventHandler<ClientDisconnectedEventArgs> ClientDisconnectedImpl;

        private readonly ClientDisconnectedEventArgs _ClientDisconnectedEventArgs = new ClientDisconnectedEventArgs();
        
        /// <summary>
        /// Client connection disconnected.
        /// </summary>
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected
        {
            add
            {
                if (ClientDisconnectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ClientDisconnected, HandleClientDisconnected);
                }
                ClientDisconnectedImpl += value;
            }
            remove
            {
                ClientDisconnectedImpl -= value;
                if (ClientDisconnectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ClientDisconnected);
                }
            }
        }

        private void HandleClientDisconnected(VariantMap eventData)
        {
            _ClientDisconnectedEventArgs.Set(eventData);
            ClientDisconnectedImpl?.Invoke(_urhoObject.Value, _ClientDisconnectedEventArgs);
        }

        #endregion

        #region ClientIdentity
        // -------------------------------------------- ClientIdentity --------------------------------------------

        /// <summary>
        /// Client has sent identity: identity map is in the event data.
        /// </summary>
        public class ClientIdentityEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Connection = eventData[E.ClientIdentity.Connection].VoidPtr;
                Allow = eventData[E.ClientIdentity.Allow].Bool;
            }

            public IntPtr Connection { get; private set; }

            public bool Allow { get; private set; }
        }

        private event EventHandler<ClientIdentityEventArgs> ClientIdentityImpl;

        private readonly ClientIdentityEventArgs _ClientIdentityEventArgs = new ClientIdentityEventArgs();
        
        /// <summary>
        /// Client has sent identity: identity map is in the event data.
        /// </summary>
        public event EventHandler<ClientIdentityEventArgs> ClientIdentity
        {
            add
            {
                if (ClientIdentityImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ClientIdentity, HandleClientIdentity);
                }
                ClientIdentityImpl += value;
            }
            remove
            {
                ClientIdentityImpl -= value;
                if (ClientIdentityImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ClientIdentity);
                }
            }
        }

        private void HandleClientIdentity(VariantMap eventData)
        {
            _ClientIdentityEventArgs.Set(eventData);
            ClientIdentityImpl?.Invoke(_urhoObject.Value, _ClientIdentityEventArgs);
        }

        #endregion

        #region ClientSceneLoaded
        // -------------------------------------------- ClientSceneLoaded --------------------------------------------

        /// <summary>
        /// Client has informed to have loaded the scene.
        /// </summary>
        public class ClientSceneLoadedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Connection = eventData[E.ClientSceneLoaded.Connection].VoidPtr;
            }

            public IntPtr Connection { get; private set; }
        }

        private event EventHandler<ClientSceneLoadedEventArgs> ClientSceneLoadedImpl;

        private readonly ClientSceneLoadedEventArgs _ClientSceneLoadedEventArgs = new ClientSceneLoadedEventArgs();
        
        /// <summary>
        /// Client has informed to have loaded the scene.
        /// </summary>
        public event EventHandler<ClientSceneLoadedEventArgs> ClientSceneLoaded
        {
            add
            {
                if (ClientSceneLoadedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ClientSceneLoaded, HandleClientSceneLoaded);
                }
                ClientSceneLoadedImpl += value;
            }
            remove
            {
                ClientSceneLoadedImpl -= value;
                if (ClientSceneLoadedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ClientSceneLoaded);
                }
            }
        }

        private void HandleClientSceneLoaded(VariantMap eventData)
        {
            _ClientSceneLoadedEventArgs.Set(eventData);
            ClientSceneLoadedImpl?.Invoke(_urhoObject.Value, _ClientSceneLoadedEventArgs);
        }

        #endregion

        #region NetworkMessage
        // -------------------------------------------- NetworkMessage --------------------------------------------

        /// <summary>
        /// Unhandled network message received.
        /// </summary>
        public class NetworkMessageEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Connection = eventData[E.NetworkMessage.Connection].VoidPtr;
                MessageID = eventData[E.NetworkMessage.MessageID].Int;
            }

            public IntPtr Connection { get; private set; }

            public int MessageID { get; private set; }
        }

        private event EventHandler<NetworkMessageEventArgs> NetworkMessageImpl;

        private readonly NetworkMessageEventArgs _NetworkMessageEventArgs = new NetworkMessageEventArgs();
        
        /// <summary>
        /// Unhandled network message received.
        /// </summary>
        public event EventHandler<NetworkMessageEventArgs> NetworkMessage
        {
            add
            {
                if (NetworkMessageImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkMessage, HandleNetworkMessage);
                }
                NetworkMessageImpl += value;
            }
            remove
            {
                NetworkMessageImpl -= value;
                if (NetworkMessageImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkMessage);
                }
            }
        }

        private void HandleNetworkMessage(VariantMap eventData)
        {
            _NetworkMessageEventArgs.Set(eventData);
            NetworkMessageImpl?.Invoke(_urhoObject.Value, _NetworkMessageEventArgs);
        }

        #endregion

        #region NetworkUpdate
        // -------------------------------------------- NetworkUpdate --------------------------------------------

        /// <summary>
        /// About to send network update on the client or server.
        /// </summary>
        public class NetworkUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<NetworkUpdateEventArgs> NetworkUpdateImpl;

        private readonly NetworkUpdateEventArgs _NetworkUpdateEventArgs = new NetworkUpdateEventArgs();
        
        /// <summary>
        /// About to send network update on the client or server.
        /// </summary>
        public event EventHandler<NetworkUpdateEventArgs> NetworkUpdate
        {
            add
            {
                if (NetworkUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkUpdate, HandleNetworkUpdate);
                }
                NetworkUpdateImpl += value;
            }
            remove
            {
                NetworkUpdateImpl -= value;
                if (NetworkUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkUpdate);
                }
            }
        }

        private void HandleNetworkUpdate(VariantMap eventData)
        {
            _NetworkUpdateEventArgs.Set(eventData);
            NetworkUpdateImpl?.Invoke(_urhoObject.Value, _NetworkUpdateEventArgs);
        }

        #endregion

        #region NetworkUpdateSent
        // -------------------------------------------- NetworkUpdateSent --------------------------------------------

        /// <summary>
        /// Network update has been sent on the client or server.
        /// </summary>
        public class NetworkUpdateSentEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<NetworkUpdateSentEventArgs> NetworkUpdateSentImpl;

        private readonly NetworkUpdateSentEventArgs _NetworkUpdateSentEventArgs = new NetworkUpdateSentEventArgs();
        
        /// <summary>
        /// Network update has been sent on the client or server.
        /// </summary>
        public event EventHandler<NetworkUpdateSentEventArgs> NetworkUpdateSent
        {
            add
            {
                if (NetworkUpdateSentImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkUpdateSent, HandleNetworkUpdateSent);
                }
                NetworkUpdateSentImpl += value;
            }
            remove
            {
                NetworkUpdateSentImpl -= value;
                if (NetworkUpdateSentImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkUpdateSent);
                }
            }
        }

        private void HandleNetworkUpdateSent(VariantMap eventData)
        {
            _NetworkUpdateSentEventArgs.Set(eventData);
            NetworkUpdateSentImpl?.Invoke(_urhoObject.Value, _NetworkUpdateSentEventArgs);
        }

        #endregion

        #region NetworkSceneLoadFailed
        // -------------------------------------------- NetworkSceneLoadFailed --------------------------------------------

        /// <summary>
        /// Scene load failed, either due to file not found or checksum error.
        /// </summary>
        public class NetworkSceneLoadFailedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Connection = eventData[E.NetworkSceneLoadFailed.Connection].VoidPtr;
            }

            public IntPtr Connection { get; private set; }
        }

        private event EventHandler<NetworkSceneLoadFailedEventArgs> NetworkSceneLoadFailedImpl;

        private readonly NetworkSceneLoadFailedEventArgs _NetworkSceneLoadFailedEventArgs = new NetworkSceneLoadFailedEventArgs();
        
        /// <summary>
        /// Scene load failed, either due to file not found or checksum error.
        /// </summary>
        public event EventHandler<NetworkSceneLoadFailedEventArgs> NetworkSceneLoadFailed
        {
            add
            {
                if (NetworkSceneLoadFailedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkSceneLoadFailed, HandleNetworkSceneLoadFailed);
                }
                NetworkSceneLoadFailedImpl += value;
            }
            remove
            {
                NetworkSceneLoadFailedImpl -= value;
                if (NetworkSceneLoadFailedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkSceneLoadFailed);
                }
            }
        }

        private void HandleNetworkSceneLoadFailed(VariantMap eventData)
        {
            _NetworkSceneLoadFailedEventArgs.Set(eventData);
            NetworkSceneLoadFailedImpl?.Invoke(_urhoObject.Value, _NetworkSceneLoadFailedEventArgs);
        }

        #endregion

        #region RemoteEventData
        // -------------------------------------------- RemoteEventData --------------------------------------------

        /// <summary>
        /// Remote event: adds Connection parameter to the event data.
        /// </summary>
        public class RemoteEventDataEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Connection = eventData[E.RemoteEventData.Connection].VoidPtr;
            }

            public IntPtr Connection { get; private set; }
        }

        private event EventHandler<RemoteEventDataEventArgs> RemoteEventDataImpl;

        private readonly RemoteEventDataEventArgs _RemoteEventDataEventArgs = new RemoteEventDataEventArgs();
        
        /// <summary>
        /// Remote event: adds Connection parameter to the event data.
        /// </summary>
        public event EventHandler<RemoteEventDataEventArgs> RemoteEventData
        {
            add
            {
                if (RemoteEventDataImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.RemoteEventData, HandleRemoteEventData);
                }
                RemoteEventDataImpl += value;
            }
            remove
            {
                RemoteEventDataImpl -= value;
                if (RemoteEventDataImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.RemoteEventData);
                }
            }
        }

        private void HandleRemoteEventData(VariantMap eventData)
        {
            _RemoteEventDataEventArgs.Set(eventData);
            RemoteEventDataImpl?.Invoke(_urhoObject.Value, _RemoteEventDataEventArgs);
        }

        #endregion

        #region NetworkBanned
        // -------------------------------------------- NetworkBanned --------------------------------------------

        /// <summary>
        /// Server refuses client connection because of the ban.
        /// </summary>
        public class NetworkBannedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<NetworkBannedEventArgs> NetworkBannedImpl;

        private readonly NetworkBannedEventArgs _NetworkBannedEventArgs = new NetworkBannedEventArgs();
        
        /// <summary>
        /// Server refuses client connection because of the ban.
        /// </summary>
        public event EventHandler<NetworkBannedEventArgs> NetworkBanned
        {
            add
            {
                if (NetworkBannedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkBanned, HandleNetworkBanned);
                }
                NetworkBannedImpl += value;
            }
            remove
            {
                NetworkBannedImpl -= value;
                if (NetworkBannedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkBanned);
                }
            }
        }

        private void HandleNetworkBanned(VariantMap eventData)
        {
            _NetworkBannedEventArgs.Set(eventData);
            NetworkBannedImpl?.Invoke(_urhoObject.Value, _NetworkBannedEventArgs);
        }

        #endregion

        #region NetworkInvalidPassword
        // -------------------------------------------- NetworkInvalidPassword --------------------------------------------

        /// <summary>
        /// Server refuses connection because of invalid password.
        /// </summary>
        public class NetworkInvalidPasswordEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<NetworkInvalidPasswordEventArgs> NetworkInvalidPasswordImpl;

        private readonly NetworkInvalidPasswordEventArgs _NetworkInvalidPasswordEventArgs = new NetworkInvalidPasswordEventArgs();
        
        /// <summary>
        /// Server refuses connection because of invalid password.
        /// </summary>
        public event EventHandler<NetworkInvalidPasswordEventArgs> NetworkInvalidPassword
        {
            add
            {
                if (NetworkInvalidPasswordImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkInvalidPassword, HandleNetworkInvalidPassword);
                }
                NetworkInvalidPasswordImpl += value;
            }
            remove
            {
                NetworkInvalidPasswordImpl -= value;
                if (NetworkInvalidPasswordImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkInvalidPassword);
                }
            }
        }

        private void HandleNetworkInvalidPassword(VariantMap eventData)
        {
            _NetworkInvalidPasswordEventArgs.Set(eventData);
            NetworkInvalidPasswordImpl?.Invoke(_urhoObject.Value, _NetworkInvalidPasswordEventArgs);
        }

        #endregion

        #region NetworkHostDiscovered
        // -------------------------------------------- NetworkHostDiscovered --------------------------------------------

        /// <summary>
        /// When LAN discovery found hosted server.
        /// </summary>
        public class NetworkHostDiscoveredEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Address = eventData[E.NetworkHostDiscovered.Address].String;
                Port = eventData[E.NetworkHostDiscovered.Port].Int;
                Beacon = eventData[E.NetworkHostDiscovered.Beacon].VariantMap;
            }

            public String Address { get; private set; }

            public int Port { get; private set; }

            public VariantMap Beacon { get; private set; }
        }

        private event EventHandler<NetworkHostDiscoveredEventArgs> NetworkHostDiscoveredImpl;

        private readonly NetworkHostDiscoveredEventArgs _NetworkHostDiscoveredEventArgs = new NetworkHostDiscoveredEventArgs();
        
        /// <summary>
        /// When LAN discovery found hosted server.
        /// </summary>
        public event EventHandler<NetworkHostDiscoveredEventArgs> NetworkHostDiscovered
        {
            add
            {
                if (NetworkHostDiscoveredImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkHostDiscovered, HandleNetworkHostDiscovered);
                }
                NetworkHostDiscoveredImpl += value;
            }
            remove
            {
                NetworkHostDiscoveredImpl -= value;
                if (NetworkHostDiscoveredImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkHostDiscovered);
                }
            }
        }

        private void HandleNetworkHostDiscovered(VariantMap eventData)
        {
            _NetworkHostDiscoveredEventArgs.Set(eventData);
            NetworkHostDiscoveredImpl?.Invoke(_urhoObject.Value, _NetworkHostDiscoveredEventArgs);
        }

        #endregion

        #region NetworkNatPunchtroughSucceeded
        // -------------------------------------------- NetworkNatPunchtroughSucceeded --------------------------------------------

        /// <summary>
        /// NAT punchtrough succeeds.
        /// </summary>
        public class NetworkNatPunchtroughSucceededEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Address = eventData[E.NetworkNatPunchtroughSucceeded.Address].String;
                Port = eventData[E.NetworkNatPunchtroughSucceeded.Port].Int;
            }

            public String Address { get; private set; }

            public int Port { get; private set; }
        }

        private event EventHandler<NetworkNatPunchtroughSucceededEventArgs> NetworkNatPunchtroughSucceededImpl;

        private readonly NetworkNatPunchtroughSucceededEventArgs _NetworkNatPunchtroughSucceededEventArgs = new NetworkNatPunchtroughSucceededEventArgs();
        
        /// <summary>
        /// NAT punchtrough succeeds.
        /// </summary>
        public event EventHandler<NetworkNatPunchtroughSucceededEventArgs> NetworkNatPunchtroughSucceeded
        {
            add
            {
                if (NetworkNatPunchtroughSucceededImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkNatPunchtroughSucceeded, HandleNetworkNatPunchtroughSucceeded);
                }
                NetworkNatPunchtroughSucceededImpl += value;
            }
            remove
            {
                NetworkNatPunchtroughSucceededImpl -= value;
                if (NetworkNatPunchtroughSucceededImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkNatPunchtroughSucceeded);
                }
            }
        }

        private void HandleNetworkNatPunchtroughSucceeded(VariantMap eventData)
        {
            _NetworkNatPunchtroughSucceededEventArgs.Set(eventData);
            NetworkNatPunchtroughSucceededImpl?.Invoke(_urhoObject.Value, _NetworkNatPunchtroughSucceededEventArgs);
        }

        #endregion

        #region NetworkNatPunchtroughFailed
        // -------------------------------------------- NetworkNatPunchtroughFailed --------------------------------------------

        /// <summary>
        /// NAT punchtrough fails.
        /// </summary>
        public class NetworkNatPunchtroughFailedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Address = eventData[E.NetworkNatPunchtroughFailed.Address].String;
                Port = eventData[E.NetworkNatPunchtroughFailed.Port].Int;
            }

            public String Address { get; private set; }

            public int Port { get; private set; }
        }

        private event EventHandler<NetworkNatPunchtroughFailedEventArgs> NetworkNatPunchtroughFailedImpl;

        private readonly NetworkNatPunchtroughFailedEventArgs _NetworkNatPunchtroughFailedEventArgs = new NetworkNatPunchtroughFailedEventArgs();
        
        /// <summary>
        /// NAT punchtrough fails.
        /// </summary>
        public event EventHandler<NetworkNatPunchtroughFailedEventArgs> NetworkNatPunchtroughFailed
        {
            add
            {
                if (NetworkNatPunchtroughFailedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkNatPunchtroughFailed, HandleNetworkNatPunchtroughFailed);
                }
                NetworkNatPunchtroughFailedImpl += value;
            }
            remove
            {
                NetworkNatPunchtroughFailedImpl -= value;
                if (NetworkNatPunchtroughFailedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkNatPunchtroughFailed);
                }
            }
        }

        private void HandleNetworkNatPunchtroughFailed(VariantMap eventData)
        {
            _NetworkNatPunchtroughFailedEventArgs.Set(eventData);
            NetworkNatPunchtroughFailedImpl?.Invoke(_urhoObject.Value, _NetworkNatPunchtroughFailedEventArgs);
        }

        #endregion

        #region NetworkNatMasterConnectionFailed
        // -------------------------------------------- NetworkNatMasterConnectionFailed --------------------------------------------

        /// <summary>
        /// Connecting to NAT master server failed.
        /// </summary>
        public class NetworkNatMasterConnectionFailedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<NetworkNatMasterConnectionFailedEventArgs> NetworkNatMasterConnectionFailedImpl;

        private readonly NetworkNatMasterConnectionFailedEventArgs _NetworkNatMasterConnectionFailedEventArgs = new NetworkNatMasterConnectionFailedEventArgs();
        
        /// <summary>
        /// Connecting to NAT master server failed.
        /// </summary>
        public event EventHandler<NetworkNatMasterConnectionFailedEventArgs> NetworkNatMasterConnectionFailed
        {
            add
            {
                if (NetworkNatMasterConnectionFailedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkNatMasterConnectionFailed, HandleNetworkNatMasterConnectionFailed);
                }
                NetworkNatMasterConnectionFailedImpl += value;
            }
            remove
            {
                NetworkNatMasterConnectionFailedImpl -= value;
                if (NetworkNatMasterConnectionFailedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkNatMasterConnectionFailed);
                }
            }
        }

        private void HandleNetworkNatMasterConnectionFailed(VariantMap eventData)
        {
            _NetworkNatMasterConnectionFailedEventArgs.Set(eventData);
            NetworkNatMasterConnectionFailedImpl?.Invoke(_urhoObject.Value, _NetworkNatMasterConnectionFailedEventArgs);
        }

        #endregion

        #region NetworkNatMasterConnectionSucceeded
        // -------------------------------------------- NetworkNatMasterConnectionSucceeded --------------------------------------------

        /// <summary>
        /// Connecting to NAT master server succeeded.
        /// </summary>
        public class NetworkNatMasterConnectionSucceededEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<NetworkNatMasterConnectionSucceededEventArgs> NetworkNatMasterConnectionSucceededImpl;

        private readonly NetworkNatMasterConnectionSucceededEventArgs _NetworkNatMasterConnectionSucceededEventArgs = new NetworkNatMasterConnectionSucceededEventArgs();
        
        /// <summary>
        /// Connecting to NAT master server succeeded.
        /// </summary>
        public event EventHandler<NetworkNatMasterConnectionSucceededEventArgs> NetworkNatMasterConnectionSucceeded
        {
            add
            {
                if (NetworkNatMasterConnectionSucceededImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkNatMasterConnectionSucceeded, HandleNetworkNatMasterConnectionSucceeded);
                }
                NetworkNatMasterConnectionSucceededImpl += value;
            }
            remove
            {
                NetworkNatMasterConnectionSucceededImpl -= value;
                if (NetworkNatMasterConnectionSucceededImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkNatMasterConnectionSucceeded);
                }
            }
        }

        private void HandleNetworkNatMasterConnectionSucceeded(VariantMap eventData)
        {
            _NetworkNatMasterConnectionSucceededEventArgs.Set(eventData);
            NetworkNatMasterConnectionSucceededImpl?.Invoke(_urhoObject.Value, _NetworkNatMasterConnectionSucceededEventArgs);
        }

        #endregion

        #region NetworkNatMasterDisconnected
        // -------------------------------------------- NetworkNatMasterDisconnected --------------------------------------------

        /// <summary>
        /// Disconnected from NAT master server.
        /// </summary>
        public class NetworkNatMasterDisconnectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<NetworkNatMasterDisconnectedEventArgs> NetworkNatMasterDisconnectedImpl;

        private readonly NetworkNatMasterDisconnectedEventArgs _NetworkNatMasterDisconnectedEventArgs = new NetworkNatMasterDisconnectedEventArgs();
        
        /// <summary>
        /// Disconnected from NAT master server.
        /// </summary>
        public event EventHandler<NetworkNatMasterDisconnectedEventArgs> NetworkNatMasterDisconnected
        {
            add
            {
                if (NetworkNatMasterDisconnectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NetworkNatMasterDisconnected, HandleNetworkNatMasterDisconnected);
                }
                NetworkNatMasterDisconnectedImpl += value;
            }
            remove
            {
                NetworkNatMasterDisconnectedImpl -= value;
                if (NetworkNatMasterDisconnectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NetworkNatMasterDisconnected);
                }
            }
        }

        private void HandleNetworkNatMasterDisconnected(VariantMap eventData)
        {
            _NetworkNatMasterDisconnectedEventArgs.Set(eventData);
            NetworkNatMasterDisconnectedImpl?.Invoke(_urhoObject.Value, _NetworkNatMasterDisconnectedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (ServerConnectedImpl != null) urhoObject.UnsubscribeFromEvent(E.ServerConnected);
                if (ServerDisconnectedImpl != null) urhoObject.UnsubscribeFromEvent(E.ServerDisconnected);
                if (ConnectFailedImpl != null) urhoObject.UnsubscribeFromEvent(E.ConnectFailed);
                if (ConnectionInProgressImpl != null) urhoObject.UnsubscribeFromEvent(E.ConnectionInProgress);
                if (ClientConnectedImpl != null) urhoObject.UnsubscribeFromEvent(E.ClientConnected);
                if (ClientDisconnectedImpl != null) urhoObject.UnsubscribeFromEvent(E.ClientDisconnected);
                if (ClientIdentityImpl != null) urhoObject.UnsubscribeFromEvent(E.ClientIdentity);
                if (ClientSceneLoadedImpl != null) urhoObject.UnsubscribeFromEvent(E.ClientSceneLoaded);
                if (NetworkMessageImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkMessage);
                if (NetworkUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkUpdate);
                if (NetworkUpdateSentImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkUpdateSent);
                if (NetworkSceneLoadFailedImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkSceneLoadFailed);
                if (RemoteEventDataImpl != null) urhoObject.UnsubscribeFromEvent(E.RemoteEventData);
                if (NetworkBannedImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkBanned);
                if (NetworkInvalidPasswordImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkInvalidPassword);
                if (NetworkHostDiscoveredImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkHostDiscovered);
                if (NetworkNatPunchtroughSucceededImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkNatPunchtroughSucceeded);
                if (NetworkNatPunchtroughFailedImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkNatPunchtroughFailed);
                if (NetworkNatMasterConnectionFailedImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkNatMasterConnectionFailed);
                if (NetworkNatMasterConnectionSucceededImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkNatMasterConnectionSucceeded);
                if (NetworkNatMasterDisconnectedImpl != null) urhoObject.UnsubscribeFromEvent(E.NetworkNatMasterDisconnected);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class PhysicsEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public PhysicsEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region PhysicsPreStep
        // -------------------------------------------- PhysicsPreStep --------------------------------------------

        /// <summary>
        /// Physics world is about to be stepped.
        /// </summary>
        public class PhysicsPreStepEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                World = eventData[E.PhysicsPreStep.World].VoidPtr;
                TimeStep = eventData[E.PhysicsPreStep.TimeStep].Float;
            }

            public IntPtr World { get; private set; }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<PhysicsPreStepEventArgs> PhysicsPreStepImpl;

        private readonly PhysicsPreStepEventArgs _PhysicsPreStepEventArgs = new PhysicsPreStepEventArgs();
        
        /// <summary>
        /// Physics world is about to be stepped.
        /// </summary>
        public event EventHandler<PhysicsPreStepEventArgs> PhysicsPreStep
        {
            add
            {
                if (PhysicsPreStepImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PhysicsPreStep, HandlePhysicsPreStep);
                }
                PhysicsPreStepImpl += value;
            }
            remove
            {
                PhysicsPreStepImpl -= value;
                if (PhysicsPreStepImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PhysicsPreStep);
                }
            }
        }

        private void HandlePhysicsPreStep(VariantMap eventData)
        {
            _PhysicsPreStepEventArgs.Set(eventData);
            PhysicsPreStepImpl?.Invoke(_urhoObject.Value, _PhysicsPreStepEventArgs);
        }

        #endregion

        #region PhysicsPostStep
        // -------------------------------------------- PhysicsPostStep --------------------------------------------

        /// <summary>
        /// Physics world has been stepped.
        /// </summary>
        public class PhysicsPostStepEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                World = eventData[E.PhysicsPostStep.World].VoidPtr;
                TimeStep = eventData[E.PhysicsPostStep.TimeStep].Float;
            }

            public IntPtr World { get; private set; }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<PhysicsPostStepEventArgs> PhysicsPostStepImpl;

        private readonly PhysicsPostStepEventArgs _PhysicsPostStepEventArgs = new PhysicsPostStepEventArgs();
        
        /// <summary>
        /// Physics world has been stepped.
        /// </summary>
        public event EventHandler<PhysicsPostStepEventArgs> PhysicsPostStep
        {
            add
            {
                if (PhysicsPostStepImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PhysicsPostStep, HandlePhysicsPostStep);
                }
                PhysicsPostStepImpl += value;
            }
            remove
            {
                PhysicsPostStepImpl -= value;
                if (PhysicsPostStepImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PhysicsPostStep);
                }
            }
        }

        private void HandlePhysicsPostStep(VariantMap eventData)
        {
            _PhysicsPostStepEventArgs.Set(eventData);
            PhysicsPostStepImpl?.Invoke(_urhoObject.Value, _PhysicsPostStepEventArgs);
        }

        #endregion

        #region PhysicsCollisionStart
        // -------------------------------------------- PhysicsCollisionStart --------------------------------------------

        /// <summary>
        /// Physics collision started. Global event sent by the PhysicsWorld.
        /// </summary>
        public class PhysicsCollisionStartEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                World = eventData[E.PhysicsCollisionStart.World].VoidPtr;
                NodeA = eventData[E.PhysicsCollisionStart.NodeA].VoidPtr;
                NodeB = eventData[E.PhysicsCollisionStart.NodeB].VoidPtr;
                BodyA = eventData[E.PhysicsCollisionStart.BodyA].VoidPtr;
                BodyB = eventData[E.PhysicsCollisionStart.BodyB].VoidPtr;
                Trigger = eventData[E.PhysicsCollisionStart.Trigger].Bool;
            }

            public IntPtr World { get; private set; }

            public IntPtr NodeA { get; private set; }

            public IntPtr NodeB { get; private set; }

            public IntPtr BodyA { get; private set; }

            public IntPtr BodyB { get; private set; }

            public bool Trigger { get; private set; }
        }

        private event EventHandler<PhysicsCollisionStartEventArgs> PhysicsCollisionStartImpl;

        private readonly PhysicsCollisionStartEventArgs _PhysicsCollisionStartEventArgs = new PhysicsCollisionStartEventArgs();
        
        /// <summary>
        /// Physics collision started. Global event sent by the PhysicsWorld.
        /// </summary>
        public event EventHandler<PhysicsCollisionStartEventArgs> PhysicsCollisionStart
        {
            add
            {
                if (PhysicsCollisionStartImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PhysicsCollisionStart, HandlePhysicsCollisionStart);
                }
                PhysicsCollisionStartImpl += value;
            }
            remove
            {
                PhysicsCollisionStartImpl -= value;
                if (PhysicsCollisionStartImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PhysicsCollisionStart);
                }
            }
        }

        private void HandlePhysicsCollisionStart(VariantMap eventData)
        {
            _PhysicsCollisionStartEventArgs.Set(eventData);
            PhysicsCollisionStartImpl?.Invoke(_urhoObject.Value, _PhysicsCollisionStartEventArgs);
        }

        #endregion

        #region PhysicsCollision
        // -------------------------------------------- PhysicsCollision --------------------------------------------

        /// <summary>
        /// Physics collision ongoing. Global event sent by the PhysicsWorld.
        /// </summary>
        public class PhysicsCollisionEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                World = eventData[E.PhysicsCollision.World].VoidPtr;
                NodeA = eventData[E.PhysicsCollision.NodeA].VoidPtr;
                NodeB = eventData[E.PhysicsCollision.NodeB].VoidPtr;
                BodyA = eventData[E.PhysicsCollision.BodyA].VoidPtr;
                BodyB = eventData[E.PhysicsCollision.BodyB].VoidPtr;
                Trigger = eventData[E.PhysicsCollision.Trigger].Bool;
            }

            public IntPtr World { get; private set; }

            public IntPtr NodeA { get; private set; }

            public IntPtr NodeB { get; private set; }

            public IntPtr BodyA { get; private set; }

            public IntPtr BodyB { get; private set; }

            public bool Trigger { get; private set; }
        }

        private event EventHandler<PhysicsCollisionEventArgs> PhysicsCollisionImpl;

        private readonly PhysicsCollisionEventArgs _PhysicsCollisionEventArgs = new PhysicsCollisionEventArgs();
        
        /// <summary>
        /// Physics collision ongoing. Global event sent by the PhysicsWorld.
        /// </summary>
        public event EventHandler<PhysicsCollisionEventArgs> PhysicsCollision
        {
            add
            {
                if (PhysicsCollisionImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PhysicsCollision, HandlePhysicsCollision);
                }
                PhysicsCollisionImpl += value;
            }
            remove
            {
                PhysicsCollisionImpl -= value;
                if (PhysicsCollisionImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PhysicsCollision);
                }
            }
        }

        private void HandlePhysicsCollision(VariantMap eventData)
        {
            _PhysicsCollisionEventArgs.Set(eventData);
            PhysicsCollisionImpl?.Invoke(_urhoObject.Value, _PhysicsCollisionEventArgs);
        }

        #endregion

        #region PhysicsCollisionEnd
        // -------------------------------------------- PhysicsCollisionEnd --------------------------------------------

        /// <summary>
        /// Physics collision ended. Global event sent by the PhysicsWorld.
        /// </summary>
        public class PhysicsCollisionEndEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                World = eventData[E.PhysicsCollisionEnd.World].VoidPtr;
                NodeA = eventData[E.PhysicsCollisionEnd.NodeA].VoidPtr;
                NodeB = eventData[E.PhysicsCollisionEnd.NodeB].VoidPtr;
                BodyA = eventData[E.PhysicsCollisionEnd.BodyA].VoidPtr;
                BodyB = eventData[E.PhysicsCollisionEnd.BodyB].VoidPtr;
                Trigger = eventData[E.PhysicsCollisionEnd.Trigger].Bool;
            }

            public IntPtr World { get; private set; }

            public IntPtr NodeA { get; private set; }

            public IntPtr NodeB { get; private set; }

            public IntPtr BodyA { get; private set; }

            public IntPtr BodyB { get; private set; }

            public bool Trigger { get; private set; }
        }

        private event EventHandler<PhysicsCollisionEndEventArgs> PhysicsCollisionEndImpl;

        private readonly PhysicsCollisionEndEventArgs _PhysicsCollisionEndEventArgs = new PhysicsCollisionEndEventArgs();
        
        /// <summary>
        /// Physics collision ended. Global event sent by the PhysicsWorld.
        /// </summary>
        public event EventHandler<PhysicsCollisionEndEventArgs> PhysicsCollisionEnd
        {
            add
            {
                if (PhysicsCollisionEndImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PhysicsCollisionEnd, HandlePhysicsCollisionEnd);
                }
                PhysicsCollisionEndImpl += value;
            }
            remove
            {
                PhysicsCollisionEndImpl -= value;
                if (PhysicsCollisionEndImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PhysicsCollisionEnd);
                }
            }
        }

        private void HandlePhysicsCollisionEnd(VariantMap eventData)
        {
            _PhysicsCollisionEndEventArgs.Set(eventData);
            PhysicsCollisionEndImpl?.Invoke(_urhoObject.Value, _PhysicsCollisionEndEventArgs);
        }

        #endregion

        #region NodeCollisionStart
        // -------------------------------------------- NodeCollisionStart --------------------------------------------

        /// <summary>
        /// Node's physics collision started. Sent by scene nodes participating in a collision.
        /// </summary>
        public class NodeCollisionStartEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Body = eventData[E.NodeCollisionStart.Body].VoidPtr;
                OtherNode = eventData[E.NodeCollisionStart.OtherNode].VoidPtr;
                OtherBody = eventData[E.NodeCollisionStart.OtherBody].VoidPtr;
                Trigger = eventData[E.NodeCollisionStart.Trigger].Bool;
            }

            public IntPtr Body { get; private set; }

            public IntPtr OtherNode { get; private set; }

            public IntPtr OtherBody { get; private set; }

            public bool Trigger { get; private set; }
        }

        private event EventHandler<NodeCollisionStartEventArgs> NodeCollisionStartImpl;

        private readonly NodeCollisionStartEventArgs _NodeCollisionStartEventArgs = new NodeCollisionStartEventArgs();
        
        /// <summary>
        /// Node's physics collision started. Sent by scene nodes participating in a collision.
        /// </summary>
        public event EventHandler<NodeCollisionStartEventArgs> NodeCollisionStart
        {
            add
            {
                if (NodeCollisionStartImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeCollisionStart, HandleNodeCollisionStart);
                }
                NodeCollisionStartImpl += value;
            }
            remove
            {
                NodeCollisionStartImpl -= value;
                if (NodeCollisionStartImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeCollisionStart);
                }
            }
        }

        private void HandleNodeCollisionStart(VariantMap eventData)
        {
            _NodeCollisionStartEventArgs.Set(eventData);
            NodeCollisionStartImpl?.Invoke(_urhoObject.Value, _NodeCollisionStartEventArgs);
        }

        #endregion

        #region NodeCollision
        // -------------------------------------------- NodeCollision --------------------------------------------

        /// <summary>
        /// Node's physics collision ongoing. Sent by scene nodes participating in a collision.
        /// </summary>
        public class NodeCollisionEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Body = eventData[E.NodeCollision.Body].VoidPtr;
                OtherNode = eventData[E.NodeCollision.OtherNode].VoidPtr;
                OtherBody = eventData[E.NodeCollision.OtherBody].VoidPtr;
                Trigger = eventData[E.NodeCollision.Trigger].Bool;
            }

            public IntPtr Body { get; private set; }

            public IntPtr OtherNode { get; private set; }

            public IntPtr OtherBody { get; private set; }

            public bool Trigger { get; private set; }
        }

        private event EventHandler<NodeCollisionEventArgs> NodeCollisionImpl;

        private readonly NodeCollisionEventArgs _NodeCollisionEventArgs = new NodeCollisionEventArgs();
        
        /// <summary>
        /// Node's physics collision ongoing. Sent by scene nodes participating in a collision.
        /// </summary>
        public event EventHandler<NodeCollisionEventArgs> NodeCollision
        {
            add
            {
                if (NodeCollisionImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeCollision, HandleNodeCollision);
                }
                NodeCollisionImpl += value;
            }
            remove
            {
                NodeCollisionImpl -= value;
                if (NodeCollisionImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeCollision);
                }
            }
        }

        private void HandleNodeCollision(VariantMap eventData)
        {
            _NodeCollisionEventArgs.Set(eventData);
            NodeCollisionImpl?.Invoke(_urhoObject.Value, _NodeCollisionEventArgs);
        }

        #endregion

        #region NodeCollisionEnd
        // -------------------------------------------- NodeCollisionEnd --------------------------------------------

        /// <summary>
        /// Node's physics collision ended. Sent by scene nodes participating in a collision.
        /// </summary>
        public class NodeCollisionEndEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Body = eventData[E.NodeCollisionEnd.Body].VoidPtr;
                OtherNode = eventData[E.NodeCollisionEnd.OtherNode].VoidPtr;
                OtherBody = eventData[E.NodeCollisionEnd.OtherBody].VoidPtr;
                Trigger = eventData[E.NodeCollisionEnd.Trigger].Bool;
            }

            public IntPtr Body { get; private set; }

            public IntPtr OtherNode { get; private set; }

            public IntPtr OtherBody { get; private set; }

            public bool Trigger { get; private set; }
        }

        private event EventHandler<NodeCollisionEndEventArgs> NodeCollisionEndImpl;

        private readonly NodeCollisionEndEventArgs _NodeCollisionEndEventArgs = new NodeCollisionEndEventArgs();
        
        /// <summary>
        /// Node's physics collision ended. Sent by scene nodes participating in a collision.
        /// </summary>
        public event EventHandler<NodeCollisionEndEventArgs> NodeCollisionEnd
        {
            add
            {
                if (NodeCollisionEndImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeCollisionEnd, HandleNodeCollisionEnd);
                }
                NodeCollisionEndImpl += value;
            }
            remove
            {
                NodeCollisionEndImpl -= value;
                if (NodeCollisionEndImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeCollisionEnd);
                }
            }
        }

        private void HandleNodeCollisionEnd(VariantMap eventData)
        {
            _NodeCollisionEndEventArgs.Set(eventData);
            NodeCollisionEndImpl?.Invoke(_urhoObject.Value, _NodeCollisionEndEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (PhysicsPreStepImpl != null) urhoObject.UnsubscribeFromEvent(E.PhysicsPreStep);
                if (PhysicsPostStepImpl != null) urhoObject.UnsubscribeFromEvent(E.PhysicsPostStep);
                if (PhysicsCollisionStartImpl != null) urhoObject.UnsubscribeFromEvent(E.PhysicsCollisionStart);
                if (PhysicsCollisionImpl != null) urhoObject.UnsubscribeFromEvent(E.PhysicsCollision);
                if (PhysicsCollisionEndImpl != null) urhoObject.UnsubscribeFromEvent(E.PhysicsCollisionEnd);
                if (NodeCollisionStartImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeCollisionStart);
                if (NodeCollisionImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeCollision);
                if (NodeCollisionEndImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeCollisionEnd);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class ResourceEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public ResourceEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region ReloadStarted
        // -------------------------------------------- ReloadStarted --------------------------------------------

        /// <summary>
        /// Resource reloading started.
        /// </summary>
        public class ReloadStartedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ReloadStartedEventArgs> ReloadStartedImpl;

        private readonly ReloadStartedEventArgs _ReloadStartedEventArgs = new ReloadStartedEventArgs();
        
        /// <summary>
        /// Resource reloading started.
        /// </summary>
        public event EventHandler<ReloadStartedEventArgs> ReloadStarted
        {
            add
            {
                if (ReloadStartedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ReloadStarted, HandleReloadStarted);
                }
                ReloadStartedImpl += value;
            }
            remove
            {
                ReloadStartedImpl -= value;
                if (ReloadStartedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ReloadStarted);
                }
            }
        }

        private void HandleReloadStarted(VariantMap eventData)
        {
            _ReloadStartedEventArgs.Set(eventData);
            ReloadStartedImpl?.Invoke(_urhoObject.Value, _ReloadStartedEventArgs);
        }

        #endregion

        #region ReloadFinished
        // -------------------------------------------- ReloadFinished --------------------------------------------

        /// <summary>
        /// Resource reloading finished successfully.
        /// </summary>
        public class ReloadFinishedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ReloadFinishedEventArgs> ReloadFinishedImpl;

        private readonly ReloadFinishedEventArgs _ReloadFinishedEventArgs = new ReloadFinishedEventArgs();
        
        /// <summary>
        /// Resource reloading finished successfully.
        /// </summary>
        public event EventHandler<ReloadFinishedEventArgs> ReloadFinished
        {
            add
            {
                if (ReloadFinishedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ReloadFinished, HandleReloadFinished);
                }
                ReloadFinishedImpl += value;
            }
            remove
            {
                ReloadFinishedImpl -= value;
                if (ReloadFinishedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ReloadFinished);
                }
            }
        }

        private void HandleReloadFinished(VariantMap eventData)
        {
            _ReloadFinishedEventArgs.Set(eventData);
            ReloadFinishedImpl?.Invoke(_urhoObject.Value, _ReloadFinishedEventArgs);
        }

        #endregion

        #region ReloadFailed
        // -------------------------------------------- ReloadFailed --------------------------------------------

        /// <summary>
        /// Resource reloading failed.
        /// </summary>
        public class ReloadFailedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ReloadFailedEventArgs> ReloadFailedImpl;

        private readonly ReloadFailedEventArgs _ReloadFailedEventArgs = new ReloadFailedEventArgs();
        
        /// <summary>
        /// Resource reloading failed.
        /// </summary>
        public event EventHandler<ReloadFailedEventArgs> ReloadFailed
        {
            add
            {
                if (ReloadFailedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ReloadFailed, HandleReloadFailed);
                }
                ReloadFailedImpl += value;
            }
            remove
            {
                ReloadFailedImpl -= value;
                if (ReloadFailedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ReloadFailed);
                }
            }
        }

        private void HandleReloadFailed(VariantMap eventData)
        {
            _ReloadFailedEventArgs.Set(eventData);
            ReloadFailedImpl?.Invoke(_urhoObject.Value, _ReloadFailedEventArgs);
        }

        #endregion

        #region FileChanged
        // -------------------------------------------- FileChanged --------------------------------------------

        /// <summary>
        /// Tracked file changed in the resource directories.
        /// </summary>
        public class FileChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                FileName = eventData[E.FileChanged.FileName].String;
                ResourceName = eventData[E.FileChanged.ResourceName].String;
            }

            public String FileName { get; private set; }

            public String ResourceName { get; private set; }
        }

        private event EventHandler<FileChangedEventArgs> FileChangedImpl;

        private readonly FileChangedEventArgs _FileChangedEventArgs = new FileChangedEventArgs();
        
        /// <summary>
        /// Tracked file changed in the resource directories.
        /// </summary>
        public event EventHandler<FileChangedEventArgs> FileChanged
        {
            add
            {
                if (FileChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.FileChanged, HandleFileChanged);
                }
                FileChangedImpl += value;
            }
            remove
            {
                FileChangedImpl -= value;
                if (FileChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.FileChanged);
                }
            }
        }

        private void HandleFileChanged(VariantMap eventData)
        {
            _FileChangedEventArgs.Set(eventData);
            FileChangedImpl?.Invoke(_urhoObject.Value, _FileChangedEventArgs);
        }

        #endregion

        #region LoadFailed
        // -------------------------------------------- LoadFailed --------------------------------------------

        /// <summary>
        /// Resource loading failed.
        /// </summary>
        public class LoadFailedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                ResourceName = eventData[E.LoadFailed.ResourceName].String;
            }

            public String ResourceName { get; private set; }
        }

        private event EventHandler<LoadFailedEventArgs> LoadFailedImpl;

        private readonly LoadFailedEventArgs _LoadFailedEventArgs = new LoadFailedEventArgs();
        
        /// <summary>
        /// Resource loading failed.
        /// </summary>
        public event EventHandler<LoadFailedEventArgs> LoadFailed
        {
            add
            {
                if (LoadFailedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.LoadFailed, HandleLoadFailed);
                }
                LoadFailedImpl += value;
            }
            remove
            {
                LoadFailedImpl -= value;
                if (LoadFailedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.LoadFailed);
                }
            }
        }

        private void HandleLoadFailed(VariantMap eventData)
        {
            _LoadFailedEventArgs.Set(eventData);
            LoadFailedImpl?.Invoke(_urhoObject.Value, _LoadFailedEventArgs);
        }

        #endregion

        #region ResourceNotFound
        // -------------------------------------------- ResourceNotFound --------------------------------------------

        /// <summary>
        /// Resource not found.
        /// </summary>
        public class ResourceNotFoundEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                ResourceName = eventData[E.ResourceNotFound.ResourceName].String;
            }

            public String ResourceName { get; private set; }
        }

        private event EventHandler<ResourceNotFoundEventArgs> ResourceNotFoundImpl;

        private readonly ResourceNotFoundEventArgs _ResourceNotFoundEventArgs = new ResourceNotFoundEventArgs();
        
        /// <summary>
        /// Resource not found.
        /// </summary>
        public event EventHandler<ResourceNotFoundEventArgs> ResourceNotFound
        {
            add
            {
                if (ResourceNotFoundImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ResourceNotFound, HandleResourceNotFound);
                }
                ResourceNotFoundImpl += value;
            }
            remove
            {
                ResourceNotFoundImpl -= value;
                if (ResourceNotFoundImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ResourceNotFound);
                }
            }
        }

        private void HandleResourceNotFound(VariantMap eventData)
        {
            _ResourceNotFoundEventArgs.Set(eventData);
            ResourceNotFoundImpl?.Invoke(_urhoObject.Value, _ResourceNotFoundEventArgs);
        }

        #endregion

        #region UnknownResourceType
        // -------------------------------------------- UnknownResourceType --------------------------------------------

        /// <summary>
        /// Unknown resource type.
        /// </summary>
        public class UnknownResourceTypeEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                ResourceType = eventData[E.UnknownResourceType.ResourceType].StringHash;
            }

            public StringHash ResourceType { get; private set; }
        }

        private event EventHandler<UnknownResourceTypeEventArgs> UnknownResourceTypeImpl;

        private readonly UnknownResourceTypeEventArgs _UnknownResourceTypeEventArgs = new UnknownResourceTypeEventArgs();
        
        /// <summary>
        /// Unknown resource type.
        /// </summary>
        public event EventHandler<UnknownResourceTypeEventArgs> UnknownResourceType
        {
            add
            {
                if (UnknownResourceTypeImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.UnknownResourceType, HandleUnknownResourceType);
                }
                UnknownResourceTypeImpl += value;
            }
            remove
            {
                UnknownResourceTypeImpl -= value;
                if (UnknownResourceTypeImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.UnknownResourceType);
                }
            }
        }

        private void HandleUnknownResourceType(VariantMap eventData)
        {
            _UnknownResourceTypeEventArgs.Set(eventData);
            UnknownResourceTypeImpl?.Invoke(_urhoObject.Value, _UnknownResourceTypeEventArgs);
        }

        #endregion

        #region ResourceBackgroundLoaded
        // -------------------------------------------- ResourceBackgroundLoaded --------------------------------------------

        /// <summary>
        /// Resource background loading finished.
        /// </summary>
        public class ResourceBackgroundLoadedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                ResourceName = eventData[E.ResourceBackgroundLoaded.ResourceName].String;
                Success = eventData[E.ResourceBackgroundLoaded.Success].Bool;
                Resource = eventData[E.ResourceBackgroundLoaded.Resource].VoidPtr;
            }

            public String ResourceName { get; private set; }

            public bool Success { get; private set; }

            public IntPtr Resource { get; private set; }
        }

        private event EventHandler<ResourceBackgroundLoadedEventArgs> ResourceBackgroundLoadedImpl;

        private readonly ResourceBackgroundLoadedEventArgs _ResourceBackgroundLoadedEventArgs = new ResourceBackgroundLoadedEventArgs();
        
        /// <summary>
        /// Resource background loading finished.
        /// </summary>
        public event EventHandler<ResourceBackgroundLoadedEventArgs> ResourceBackgroundLoaded
        {
            add
            {
                if (ResourceBackgroundLoadedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ResourceBackgroundLoaded, HandleResourceBackgroundLoaded);
                }
                ResourceBackgroundLoadedImpl += value;
            }
            remove
            {
                ResourceBackgroundLoadedImpl -= value;
                if (ResourceBackgroundLoadedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ResourceBackgroundLoaded);
                }
            }
        }

        private void HandleResourceBackgroundLoaded(VariantMap eventData)
        {
            _ResourceBackgroundLoadedEventArgs.Set(eventData);
            ResourceBackgroundLoadedImpl?.Invoke(_urhoObject.Value, _ResourceBackgroundLoadedEventArgs);
        }

        #endregion

        #region ChangeLanguage
        // -------------------------------------------- ChangeLanguage --------------------------------------------

        /// <summary>
        /// Language changed.
        /// </summary>
        public class ChangeLanguageEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ChangeLanguageEventArgs> ChangeLanguageImpl;

        private readonly ChangeLanguageEventArgs _ChangeLanguageEventArgs = new ChangeLanguageEventArgs();
        
        /// <summary>
        /// Language changed.
        /// </summary>
        public event EventHandler<ChangeLanguageEventArgs> ChangeLanguage
        {
            add
            {
                if (ChangeLanguageImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ChangeLanguage, HandleChangeLanguage);
                }
                ChangeLanguageImpl += value;
            }
            remove
            {
                ChangeLanguageImpl -= value;
                if (ChangeLanguageImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ChangeLanguage);
                }
            }
        }

        private void HandleChangeLanguage(VariantMap eventData)
        {
            _ChangeLanguageEventArgs.Set(eventData);
            ChangeLanguageImpl?.Invoke(_urhoObject.Value, _ChangeLanguageEventArgs);
        }

        #endregion

        #region ResourceRenamed
        // -------------------------------------------- ResourceRenamed --------------------------------------------

        /// <summary>
        /// Resource renamed
        /// </summary>
        public class ResourceRenamedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                From = eventData[E.ResourceRenamed.From].String;
                To = eventData[E.ResourceRenamed.To].String;
            }

            public String From { get; private set; }

            public String To { get; private set; }
        }

        private event EventHandler<ResourceRenamedEventArgs> ResourceRenamedImpl;

        private readonly ResourceRenamedEventArgs _ResourceRenamedEventArgs = new ResourceRenamedEventArgs();
        
        /// <summary>
        /// Resource renamed
        /// </summary>
        public event EventHandler<ResourceRenamedEventArgs> ResourceRenamed
        {
            add
            {
                if (ResourceRenamedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ResourceRenamed, HandleResourceRenamed);
                }
                ResourceRenamedImpl += value;
            }
            remove
            {
                ResourceRenamedImpl -= value;
                if (ResourceRenamedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ResourceRenamed);
                }
            }
        }

        private void HandleResourceRenamed(VariantMap eventData)
        {
            _ResourceRenamedEventArgs.Set(eventData);
            ResourceRenamedImpl?.Invoke(_urhoObject.Value, _ResourceRenamedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (ReloadStartedImpl != null) urhoObject.UnsubscribeFromEvent(E.ReloadStarted);
                if (ReloadFinishedImpl != null) urhoObject.UnsubscribeFromEvent(E.ReloadFinished);
                if (ReloadFailedImpl != null) urhoObject.UnsubscribeFromEvent(E.ReloadFailed);
                if (FileChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.FileChanged);
                if (LoadFailedImpl != null) urhoObject.UnsubscribeFromEvent(E.LoadFailed);
                if (ResourceNotFoundImpl != null) urhoObject.UnsubscribeFromEvent(E.ResourceNotFound);
                if (UnknownResourceTypeImpl != null) urhoObject.UnsubscribeFromEvent(E.UnknownResourceType);
                if (ResourceBackgroundLoadedImpl != null) urhoObject.UnsubscribeFromEvent(E.ResourceBackgroundLoaded);
                if (ChangeLanguageImpl != null) urhoObject.UnsubscribeFromEvent(E.ChangeLanguage);
                if (ResourceRenamedImpl != null) urhoObject.UnsubscribeFromEvent(E.ResourceRenamed);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class CameraViewportAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public CameraViewportAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region CameraViewportResized
        // -------------------------------------------- CameraViewportResized --------------------------------------------

        /// <summary>
        /// CameraViewportResized
        /// </summary>
        public class CameraViewportResizedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Camera = eventData[E.CameraViewportResized.Camera].VoidPtr;
                Viewport = eventData[E.CameraViewportResized.Viewport].VoidPtr;
                Size = eventData[E.CameraViewportResized.Size].Int;
            }

            public IntPtr Camera { get; private set; }

            public IntPtr Viewport { get; private set; }

            public int Size { get; private set; }
        }

        private event EventHandler<CameraViewportResizedEventArgs> CameraViewportResizedImpl;

        private readonly CameraViewportResizedEventArgs _CameraViewportResizedEventArgs = new CameraViewportResizedEventArgs();
        
        /// <summary>
        /// CameraViewportResized
        /// </summary>
        public event EventHandler<CameraViewportResizedEventArgs> CameraViewportResized
        {
            add
            {
                if (CameraViewportResizedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.CameraViewportResized, HandleCameraViewportResized);
                }
                CameraViewportResizedImpl += value;
            }
            remove
            {
                CameraViewportResizedImpl -= value;
                if (CameraViewportResizedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.CameraViewportResized);
                }
            }
        }

        private void HandleCameraViewportResized(VariantMap eventData)
        {
            _CameraViewportResizedEventArgs.Set(eventData);
            CameraViewportResizedImpl?.Invoke(_urhoObject.Value, _CameraViewportResizedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (CameraViewportResizedImpl != null) urhoObject.UnsubscribeFromEvent(E.CameraViewportResized);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class SceneEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public SceneEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region SceneUpdate
        // -------------------------------------------- SceneUpdate --------------------------------------------

        /// <summary>
        /// Variable timestep scene update.
        /// </summary>
        public class SceneUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.SceneUpdate.Scene].VoidPtr;
                TimeStep = eventData[E.SceneUpdate.TimeStep].Float;
            }

            public IntPtr Scene { get; private set; }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<SceneUpdateEventArgs> SceneUpdateImpl;

        private readonly SceneUpdateEventArgs _SceneUpdateEventArgs = new SceneUpdateEventArgs();
        
        /// <summary>
        /// Variable timestep scene update.
        /// </summary>
        public event EventHandler<SceneUpdateEventArgs> SceneUpdate
        {
            add
            {
                if (SceneUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.SceneUpdate, HandleSceneUpdate);
                }
                SceneUpdateImpl += value;
            }
            remove
            {
                SceneUpdateImpl -= value;
                if (SceneUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.SceneUpdate);
                }
            }
        }

        private void HandleSceneUpdate(VariantMap eventData)
        {
            _SceneUpdateEventArgs.Set(eventData);
            SceneUpdateImpl?.Invoke(_urhoObject.Value, _SceneUpdateEventArgs);
        }

        #endregion

        #region SceneSubsystemUpdate
        // -------------------------------------------- SceneSubsystemUpdate --------------------------------------------

        /// <summary>
        /// Scene subsystem update.
        /// </summary>
        public class SceneSubsystemUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.SceneSubsystemUpdate.Scene].VoidPtr;
                TimeStep = eventData[E.SceneSubsystemUpdate.TimeStep].Float;
            }

            public IntPtr Scene { get; private set; }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<SceneSubsystemUpdateEventArgs> SceneSubsystemUpdateImpl;

        private readonly SceneSubsystemUpdateEventArgs _SceneSubsystemUpdateEventArgs = new SceneSubsystemUpdateEventArgs();
        
        /// <summary>
        /// Scene subsystem update.
        /// </summary>
        public event EventHandler<SceneSubsystemUpdateEventArgs> SceneSubsystemUpdate
        {
            add
            {
                if (SceneSubsystemUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.SceneSubsystemUpdate, HandleSceneSubsystemUpdate);
                }
                SceneSubsystemUpdateImpl += value;
            }
            remove
            {
                SceneSubsystemUpdateImpl -= value;
                if (SceneSubsystemUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.SceneSubsystemUpdate);
                }
            }
        }

        private void HandleSceneSubsystemUpdate(VariantMap eventData)
        {
            _SceneSubsystemUpdateEventArgs.Set(eventData);
            SceneSubsystemUpdateImpl?.Invoke(_urhoObject.Value, _SceneSubsystemUpdateEventArgs);
        }

        #endregion

        #region UpdateSmoothing
        // -------------------------------------------- UpdateSmoothing --------------------------------------------

        /// <summary>
        /// Scene transform smoothing update.
        /// </summary>
        public class UpdateSmoothingEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Constant = eventData[E.UpdateSmoothing.Constant].Float;
                SquaredSnapThreshold = eventData[E.UpdateSmoothing.SquaredSnapThreshold].Float;
            }

            public float Constant { get; private set; }

            public float SquaredSnapThreshold { get; private set; }
        }

        private event EventHandler<UpdateSmoothingEventArgs> UpdateSmoothingImpl;

        private readonly UpdateSmoothingEventArgs _UpdateSmoothingEventArgs = new UpdateSmoothingEventArgs();
        
        /// <summary>
        /// Scene transform smoothing update.
        /// </summary>
        public event EventHandler<UpdateSmoothingEventArgs> UpdateSmoothing
        {
            add
            {
                if (UpdateSmoothingImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.UpdateSmoothing, HandleUpdateSmoothing);
                }
                UpdateSmoothingImpl += value;
            }
            remove
            {
                UpdateSmoothingImpl -= value;
                if (UpdateSmoothingImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.UpdateSmoothing);
                }
            }
        }

        private void HandleUpdateSmoothing(VariantMap eventData)
        {
            _UpdateSmoothingEventArgs.Set(eventData);
            UpdateSmoothingImpl?.Invoke(_urhoObject.Value, _UpdateSmoothingEventArgs);
        }

        #endregion

        #region SceneDrawableUpdateFinished
        // -------------------------------------------- SceneDrawableUpdateFinished --------------------------------------------

        /// <summary>
        /// Scene drawable update finished. Custom animation (eg. IK) can be done at this point.
        /// </summary>
        public class SceneDrawableUpdateFinishedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.SceneDrawableUpdateFinished.Scene].VoidPtr;
                TimeStep = eventData[E.SceneDrawableUpdateFinished.TimeStep].Float;
            }

            public IntPtr Scene { get; private set; }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<SceneDrawableUpdateFinishedEventArgs> SceneDrawableUpdateFinishedImpl;

        private readonly SceneDrawableUpdateFinishedEventArgs _SceneDrawableUpdateFinishedEventArgs = new SceneDrawableUpdateFinishedEventArgs();
        
        /// <summary>
        /// Scene drawable update finished. Custom animation (eg. IK) can be done at this point.
        /// </summary>
        public event EventHandler<SceneDrawableUpdateFinishedEventArgs> SceneDrawableUpdateFinished
        {
            add
            {
                if (SceneDrawableUpdateFinishedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.SceneDrawableUpdateFinished, HandleSceneDrawableUpdateFinished);
                }
                SceneDrawableUpdateFinishedImpl += value;
            }
            remove
            {
                SceneDrawableUpdateFinishedImpl -= value;
                if (SceneDrawableUpdateFinishedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.SceneDrawableUpdateFinished);
                }
            }
        }

        private void HandleSceneDrawableUpdateFinished(VariantMap eventData)
        {
            _SceneDrawableUpdateFinishedEventArgs.Set(eventData);
            SceneDrawableUpdateFinishedImpl?.Invoke(_urhoObject.Value, _SceneDrawableUpdateFinishedEventArgs);
        }

        #endregion

        #region TargetPositionChanged
        // -------------------------------------------- TargetPositionChanged --------------------------------------------

        /// <summary>
        /// SmoothedTransform target position changed.
        /// </summary>
        public class TargetPositionChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<TargetPositionChangedEventArgs> TargetPositionChangedImpl;

        private readonly TargetPositionChangedEventArgs _TargetPositionChangedEventArgs = new TargetPositionChangedEventArgs();
        
        /// <summary>
        /// SmoothedTransform target position changed.
        /// </summary>
        public event EventHandler<TargetPositionChangedEventArgs> TargetPositionChanged
        {
            add
            {
                if (TargetPositionChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TargetPositionChanged, HandleTargetPositionChanged);
                }
                TargetPositionChangedImpl += value;
            }
            remove
            {
                TargetPositionChangedImpl -= value;
                if (TargetPositionChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TargetPositionChanged);
                }
            }
        }

        private void HandleTargetPositionChanged(VariantMap eventData)
        {
            _TargetPositionChangedEventArgs.Set(eventData);
            TargetPositionChangedImpl?.Invoke(_urhoObject.Value, _TargetPositionChangedEventArgs);
        }

        #endregion

        #region TargetRotationChanged
        // -------------------------------------------- TargetRotationChanged --------------------------------------------

        /// <summary>
        /// SmoothedTransform target position changed.
        /// </summary>
        public class TargetRotationChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<TargetRotationChangedEventArgs> TargetRotationChangedImpl;

        private readonly TargetRotationChangedEventArgs _TargetRotationChangedEventArgs = new TargetRotationChangedEventArgs();
        
        /// <summary>
        /// SmoothedTransform target position changed.
        /// </summary>
        public event EventHandler<TargetRotationChangedEventArgs> TargetRotationChanged
        {
            add
            {
                if (TargetRotationChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TargetRotationChanged, HandleTargetRotationChanged);
                }
                TargetRotationChangedImpl += value;
            }
            remove
            {
                TargetRotationChangedImpl -= value;
                if (TargetRotationChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TargetRotationChanged);
                }
            }
        }

        private void HandleTargetRotationChanged(VariantMap eventData)
        {
            _TargetRotationChangedEventArgs.Set(eventData);
            TargetRotationChangedImpl?.Invoke(_urhoObject.Value, _TargetRotationChangedEventArgs);
        }

        #endregion

        #region AttributeAnimationUpdate
        // -------------------------------------------- AttributeAnimationUpdate --------------------------------------------

        /// <summary>
        /// Scene attribute animation update.
        /// </summary>
        public class AttributeAnimationUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.AttributeAnimationUpdate.Scene].VoidPtr;
                TimeStep = eventData[E.AttributeAnimationUpdate.TimeStep].Float;
            }

            public IntPtr Scene { get; private set; }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<AttributeAnimationUpdateEventArgs> AttributeAnimationUpdateImpl;

        private readonly AttributeAnimationUpdateEventArgs _AttributeAnimationUpdateEventArgs = new AttributeAnimationUpdateEventArgs();
        
        /// <summary>
        /// Scene attribute animation update.
        /// </summary>
        public event EventHandler<AttributeAnimationUpdateEventArgs> AttributeAnimationUpdate
        {
            add
            {
                if (AttributeAnimationUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AttributeAnimationUpdate, HandleAttributeAnimationUpdate);
                }
                AttributeAnimationUpdateImpl += value;
            }
            remove
            {
                AttributeAnimationUpdateImpl -= value;
                if (AttributeAnimationUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AttributeAnimationUpdate);
                }
            }
        }

        private void HandleAttributeAnimationUpdate(VariantMap eventData)
        {
            _AttributeAnimationUpdateEventArgs.Set(eventData);
            AttributeAnimationUpdateImpl?.Invoke(_urhoObject.Value, _AttributeAnimationUpdateEventArgs);
        }

        #endregion

        #region AttributeAnimationAdded
        // -------------------------------------------- AttributeAnimationAdded --------------------------------------------

        /// <summary>
        /// Attribute animation added to object animation.
        /// </summary>
        public class AttributeAnimationAddedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                ObjectAnimation = eventData[E.AttributeAnimationAdded.ObjectAnimation].VoidPtr;
                AttributeAnimationName = eventData[E.AttributeAnimationAdded.AttributeAnimationName].String;
            }

            public IntPtr ObjectAnimation { get; private set; }

            public String AttributeAnimationName { get; private set; }
        }

        private event EventHandler<AttributeAnimationAddedEventArgs> AttributeAnimationAddedImpl;

        private readonly AttributeAnimationAddedEventArgs _AttributeAnimationAddedEventArgs = new AttributeAnimationAddedEventArgs();
        
        /// <summary>
        /// Attribute animation added to object animation.
        /// </summary>
        public event EventHandler<AttributeAnimationAddedEventArgs> AttributeAnimationAdded
        {
            add
            {
                if (AttributeAnimationAddedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AttributeAnimationAdded, HandleAttributeAnimationAdded);
                }
                AttributeAnimationAddedImpl += value;
            }
            remove
            {
                AttributeAnimationAddedImpl -= value;
                if (AttributeAnimationAddedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AttributeAnimationAdded);
                }
            }
        }

        private void HandleAttributeAnimationAdded(VariantMap eventData)
        {
            _AttributeAnimationAddedEventArgs.Set(eventData);
            AttributeAnimationAddedImpl?.Invoke(_urhoObject.Value, _AttributeAnimationAddedEventArgs);
        }

        #endregion

        #region AttributeAnimationRemoved
        // -------------------------------------------- AttributeAnimationRemoved --------------------------------------------

        /// <summary>
        /// Attribute animation removed from object animation.
        /// </summary>
        public class AttributeAnimationRemovedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                ObjectAnimation = eventData[E.AttributeAnimationRemoved.ObjectAnimation].VoidPtr;
                AttributeAnimationName = eventData[E.AttributeAnimationRemoved.AttributeAnimationName].String;
            }

            public IntPtr ObjectAnimation { get; private set; }

            public String AttributeAnimationName { get; private set; }
        }

        private event EventHandler<AttributeAnimationRemovedEventArgs> AttributeAnimationRemovedImpl;

        private readonly AttributeAnimationRemovedEventArgs _AttributeAnimationRemovedEventArgs = new AttributeAnimationRemovedEventArgs();
        
        /// <summary>
        /// Attribute animation removed from object animation.
        /// </summary>
        public event EventHandler<AttributeAnimationRemovedEventArgs> AttributeAnimationRemoved
        {
            add
            {
                if (AttributeAnimationRemovedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AttributeAnimationRemoved, HandleAttributeAnimationRemoved);
                }
                AttributeAnimationRemovedImpl += value;
            }
            remove
            {
                AttributeAnimationRemovedImpl -= value;
                if (AttributeAnimationRemovedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AttributeAnimationRemoved);
                }
            }
        }

        private void HandleAttributeAnimationRemoved(VariantMap eventData)
        {
            _AttributeAnimationRemovedEventArgs.Set(eventData);
            AttributeAnimationRemovedImpl?.Invoke(_urhoObject.Value, _AttributeAnimationRemovedEventArgs);
        }

        #endregion

        #region ScenePostUpdate
        // -------------------------------------------- ScenePostUpdate --------------------------------------------

        /// <summary>
        /// Variable timestep scene post-update.
        /// </summary>
        public class ScenePostUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.ScenePostUpdate.Scene].VoidPtr;
                TimeStep = eventData[E.ScenePostUpdate.TimeStep].Float;
            }

            public IntPtr Scene { get; private set; }

            public float TimeStep { get; private set; }
        }

        private event EventHandler<ScenePostUpdateEventArgs> ScenePostUpdateImpl;

        private readonly ScenePostUpdateEventArgs _ScenePostUpdateEventArgs = new ScenePostUpdateEventArgs();
        
        /// <summary>
        /// Variable timestep scene post-update.
        /// </summary>
        public event EventHandler<ScenePostUpdateEventArgs> ScenePostUpdate
        {
            add
            {
                if (ScenePostUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ScenePostUpdate, HandleScenePostUpdate);
                }
                ScenePostUpdateImpl += value;
            }
            remove
            {
                ScenePostUpdateImpl -= value;
                if (ScenePostUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ScenePostUpdate);
                }
            }
        }

        private void HandleScenePostUpdate(VariantMap eventData)
        {
            _ScenePostUpdateEventArgs.Set(eventData);
            ScenePostUpdateImpl?.Invoke(_urhoObject.Value, _ScenePostUpdateEventArgs);
        }

        #endregion

        #region AsyncLoadProgress
        // -------------------------------------------- AsyncLoadProgress --------------------------------------------

        /// <summary>
        /// Asynchronous scene loading progress.
        /// </summary>
        public class AsyncLoadProgressEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.AsyncLoadProgress.Scene].VoidPtr;
                Progress = eventData[E.AsyncLoadProgress.Progress].Float;
                LoadedNodes = eventData[E.AsyncLoadProgress.LoadedNodes].Int;
                TotalNodes = eventData[E.AsyncLoadProgress.TotalNodes].Int;
                LoadedResources = eventData[E.AsyncLoadProgress.LoadedResources].Int;
                TotalResources = eventData[E.AsyncLoadProgress.TotalResources].Int;
            }

            public IntPtr Scene { get; private set; }

            public float Progress { get; private set; }

            public int LoadedNodes { get; private set; }

            public int TotalNodes { get; private set; }

            public int LoadedResources { get; private set; }

            public int TotalResources { get; private set; }
        }

        private event EventHandler<AsyncLoadProgressEventArgs> AsyncLoadProgressImpl;

        private readonly AsyncLoadProgressEventArgs _AsyncLoadProgressEventArgs = new AsyncLoadProgressEventArgs();
        
        /// <summary>
        /// Asynchronous scene loading progress.
        /// </summary>
        public event EventHandler<AsyncLoadProgressEventArgs> AsyncLoadProgress
        {
            add
            {
                if (AsyncLoadProgressImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AsyncLoadProgress, HandleAsyncLoadProgress);
                }
                AsyncLoadProgressImpl += value;
            }
            remove
            {
                AsyncLoadProgressImpl -= value;
                if (AsyncLoadProgressImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AsyncLoadProgress);
                }
            }
        }

        private void HandleAsyncLoadProgress(VariantMap eventData)
        {
            _AsyncLoadProgressEventArgs.Set(eventData);
            AsyncLoadProgressImpl?.Invoke(_urhoObject.Value, _AsyncLoadProgressEventArgs);
        }

        #endregion

        #region AsyncLoadFinished
        // -------------------------------------------- AsyncLoadFinished --------------------------------------------

        /// <summary>
        /// Asynchronous scene loading finished.
        /// </summary>
        public class AsyncLoadFinishedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.AsyncLoadFinished.Scene].VoidPtr;
            }

            public IntPtr Scene { get; private set; }
        }

        private event EventHandler<AsyncLoadFinishedEventArgs> AsyncLoadFinishedImpl;

        private readonly AsyncLoadFinishedEventArgs _AsyncLoadFinishedEventArgs = new AsyncLoadFinishedEventArgs();
        
        /// <summary>
        /// Asynchronous scene loading finished.
        /// </summary>
        public event EventHandler<AsyncLoadFinishedEventArgs> AsyncLoadFinished
        {
            add
            {
                if (AsyncLoadFinishedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AsyncLoadFinished, HandleAsyncLoadFinished);
                }
                AsyncLoadFinishedImpl += value;
            }
            remove
            {
                AsyncLoadFinishedImpl -= value;
                if (AsyncLoadFinishedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AsyncLoadFinished);
                }
            }
        }

        private void HandleAsyncLoadFinished(VariantMap eventData)
        {
            _AsyncLoadFinishedEventArgs.Set(eventData);
            AsyncLoadFinishedImpl?.Invoke(_urhoObject.Value, _AsyncLoadFinishedEventArgs);
        }

        #endregion

        #region NodeAdded
        // -------------------------------------------- NodeAdded --------------------------------------------

        /// <summary>
        /// A child node has been added to a parent node.
        /// </summary>
        public class NodeAddedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.NodeAdded.Scene].VoidPtr;
                Parent = eventData[E.NodeAdded.Parent].VoidPtr;
                Node = eventData[E.NodeAdded.Node].VoidPtr;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Parent { get; private set; }

            public IntPtr Node { get; private set; }
        }

        private event EventHandler<NodeAddedEventArgs> NodeAddedImpl;

        private readonly NodeAddedEventArgs _NodeAddedEventArgs = new NodeAddedEventArgs();
        
        /// <summary>
        /// A child node has been added to a parent node.
        /// </summary>
        public event EventHandler<NodeAddedEventArgs> NodeAdded
        {
            add
            {
                if (NodeAddedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeAdded, HandleNodeAdded);
                }
                NodeAddedImpl += value;
            }
            remove
            {
                NodeAddedImpl -= value;
                if (NodeAddedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeAdded);
                }
            }
        }

        private void HandleNodeAdded(VariantMap eventData)
        {
            _NodeAddedEventArgs.Set(eventData);
            NodeAddedImpl?.Invoke(_urhoObject.Value, _NodeAddedEventArgs);
        }

        #endregion

        #region NodeRemoved
        // -------------------------------------------- NodeRemoved --------------------------------------------

        /// <summary>
        /// A child node is about to be removed from a parent node. Note that individual component removal events will not be sent.
        /// </summary>
        public class NodeRemovedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.NodeRemoved.Scene].VoidPtr;
                Parent = eventData[E.NodeRemoved.Parent].VoidPtr;
                Node = eventData[E.NodeRemoved.Node].VoidPtr;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Parent { get; private set; }

            public IntPtr Node { get; private set; }
        }

        private event EventHandler<NodeRemovedEventArgs> NodeRemovedImpl;

        private readonly NodeRemovedEventArgs _NodeRemovedEventArgs = new NodeRemovedEventArgs();
        
        /// <summary>
        /// A child node is about to be removed from a parent node. Note that individual component removal events will not be sent.
        /// </summary>
        public event EventHandler<NodeRemovedEventArgs> NodeRemoved
        {
            add
            {
                if (NodeRemovedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeRemoved, HandleNodeRemoved);
                }
                NodeRemovedImpl += value;
            }
            remove
            {
                NodeRemovedImpl -= value;
                if (NodeRemovedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeRemoved);
                }
            }
        }

        private void HandleNodeRemoved(VariantMap eventData)
        {
            _NodeRemovedEventArgs.Set(eventData);
            NodeRemovedImpl?.Invoke(_urhoObject.Value, _NodeRemovedEventArgs);
        }

        #endregion

        #region ComponentAdded
        // -------------------------------------------- ComponentAdded --------------------------------------------

        /// <summary>
        /// A component has been created to a node.
        /// </summary>
        public class ComponentAddedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.ComponentAdded.Scene].VoidPtr;
                Node = eventData[E.ComponentAdded.Node].VoidPtr;
                Component = eventData[E.ComponentAdded.Component].VoidPtr;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Node { get; private set; }

            public IntPtr Component { get; private set; }
        }

        private event EventHandler<ComponentAddedEventArgs> ComponentAddedImpl;

        private readonly ComponentAddedEventArgs _ComponentAddedEventArgs = new ComponentAddedEventArgs();
        
        /// <summary>
        /// A component has been created to a node.
        /// </summary>
        public event EventHandler<ComponentAddedEventArgs> ComponentAdded
        {
            add
            {
                if (ComponentAddedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ComponentAdded, HandleComponentAdded);
                }
                ComponentAddedImpl += value;
            }
            remove
            {
                ComponentAddedImpl -= value;
                if (ComponentAddedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ComponentAdded);
                }
            }
        }

        private void HandleComponentAdded(VariantMap eventData)
        {
            _ComponentAddedEventArgs.Set(eventData);
            ComponentAddedImpl?.Invoke(_urhoObject.Value, _ComponentAddedEventArgs);
        }

        #endregion

        #region ComponentRemoved
        // -------------------------------------------- ComponentRemoved --------------------------------------------

        /// <summary>
        /// A component is about to be removed from a node.
        /// </summary>
        public class ComponentRemovedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.ComponentRemoved.Scene].VoidPtr;
                Node = eventData[E.ComponentRemoved.Node].VoidPtr;
                Component = eventData[E.ComponentRemoved.Component].VoidPtr;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Node { get; private set; }

            public IntPtr Component { get; private set; }
        }

        private event EventHandler<ComponentRemovedEventArgs> ComponentRemovedImpl;

        private readonly ComponentRemovedEventArgs _ComponentRemovedEventArgs = new ComponentRemovedEventArgs();
        
        /// <summary>
        /// A component is about to be removed from a node.
        /// </summary>
        public event EventHandler<ComponentRemovedEventArgs> ComponentRemoved
        {
            add
            {
                if (ComponentRemovedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ComponentRemoved, HandleComponentRemoved);
                }
                ComponentRemovedImpl += value;
            }
            remove
            {
                ComponentRemovedImpl -= value;
                if (ComponentRemovedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ComponentRemoved);
                }
            }
        }

        private void HandleComponentRemoved(VariantMap eventData)
        {
            _ComponentRemovedEventArgs.Set(eventData);
            ComponentRemovedImpl?.Invoke(_urhoObject.Value, _ComponentRemovedEventArgs);
        }

        #endregion

        #region NodeNameChanged
        // -------------------------------------------- NodeNameChanged --------------------------------------------

        /// <summary>
        /// A node's name has changed.
        /// </summary>
        public class NodeNameChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.NodeNameChanged.Scene].VoidPtr;
                Node = eventData[E.NodeNameChanged.Node].VoidPtr;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Node { get; private set; }
        }

        private event EventHandler<NodeNameChangedEventArgs> NodeNameChangedImpl;

        private readonly NodeNameChangedEventArgs _NodeNameChangedEventArgs = new NodeNameChangedEventArgs();
        
        /// <summary>
        /// A node's name has changed.
        /// </summary>
        public event EventHandler<NodeNameChangedEventArgs> NodeNameChanged
        {
            add
            {
                if (NodeNameChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeNameChanged, HandleNodeNameChanged);
                }
                NodeNameChangedImpl += value;
            }
            remove
            {
                NodeNameChangedImpl -= value;
                if (NodeNameChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeNameChanged);
                }
            }
        }

        private void HandleNodeNameChanged(VariantMap eventData)
        {
            _NodeNameChangedEventArgs.Set(eventData);
            NodeNameChangedImpl?.Invoke(_urhoObject.Value, _NodeNameChangedEventArgs);
        }

        #endregion

        #region NodeEnabledChanged
        // -------------------------------------------- NodeEnabledChanged --------------------------------------------

        /// <summary>
        /// A node's enabled state has changed.
        /// </summary>
        public class NodeEnabledChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.NodeEnabledChanged.Scene].VoidPtr;
                Node = eventData[E.NodeEnabledChanged.Node].VoidPtr;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Node { get; private set; }
        }

        private event EventHandler<NodeEnabledChangedEventArgs> NodeEnabledChangedImpl;

        private readonly NodeEnabledChangedEventArgs _NodeEnabledChangedEventArgs = new NodeEnabledChangedEventArgs();
        
        /// <summary>
        /// A node's enabled state has changed.
        /// </summary>
        public event EventHandler<NodeEnabledChangedEventArgs> NodeEnabledChanged
        {
            add
            {
                if (NodeEnabledChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeEnabledChanged, HandleNodeEnabledChanged);
                }
                NodeEnabledChangedImpl += value;
            }
            remove
            {
                NodeEnabledChangedImpl -= value;
                if (NodeEnabledChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeEnabledChanged);
                }
            }
        }

        private void HandleNodeEnabledChanged(VariantMap eventData)
        {
            _NodeEnabledChangedEventArgs.Set(eventData);
            NodeEnabledChangedImpl?.Invoke(_urhoObject.Value, _NodeEnabledChangedEventArgs);
        }

        #endregion

        #region NodeTagAdded
        // -------------------------------------------- NodeTagAdded --------------------------------------------

        /// <summary>
        /// A node's tag has been added.
        /// </summary>
        public class NodeTagAddedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.NodeTagAdded.Scene].VoidPtr;
                Node = eventData[E.NodeTagAdded.Node].VoidPtr;
                Tag = eventData[E.NodeTagAdded.Tag].String;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Node { get; private set; }

            public String Tag { get; private set; }
        }

        private event EventHandler<NodeTagAddedEventArgs> NodeTagAddedImpl;

        private readonly NodeTagAddedEventArgs _NodeTagAddedEventArgs = new NodeTagAddedEventArgs();
        
        /// <summary>
        /// A node's tag has been added.
        /// </summary>
        public event EventHandler<NodeTagAddedEventArgs> NodeTagAdded
        {
            add
            {
                if (NodeTagAddedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeTagAdded, HandleNodeTagAdded);
                }
                NodeTagAddedImpl += value;
            }
            remove
            {
                NodeTagAddedImpl -= value;
                if (NodeTagAddedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeTagAdded);
                }
            }
        }

        private void HandleNodeTagAdded(VariantMap eventData)
        {
            _NodeTagAddedEventArgs.Set(eventData);
            NodeTagAddedImpl?.Invoke(_urhoObject.Value, _NodeTagAddedEventArgs);
        }

        #endregion

        #region NodeTagRemoved
        // -------------------------------------------- NodeTagRemoved --------------------------------------------

        /// <summary>
        /// A node's tag has been removed.
        /// </summary>
        public class NodeTagRemovedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.NodeTagRemoved.Scene].VoidPtr;
                Node = eventData[E.NodeTagRemoved.Node].VoidPtr;
                Tag = eventData[E.NodeTagRemoved.Tag].String;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Node { get; private set; }

            public String Tag { get; private set; }
        }

        private event EventHandler<NodeTagRemovedEventArgs> NodeTagRemovedImpl;

        private readonly NodeTagRemovedEventArgs _NodeTagRemovedEventArgs = new NodeTagRemovedEventArgs();
        
        /// <summary>
        /// A node's tag has been removed.
        /// </summary>
        public event EventHandler<NodeTagRemovedEventArgs> NodeTagRemoved
        {
            add
            {
                if (NodeTagRemovedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeTagRemoved, HandleNodeTagRemoved);
                }
                NodeTagRemovedImpl += value;
            }
            remove
            {
                NodeTagRemovedImpl -= value;
                if (NodeTagRemovedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeTagRemoved);
                }
            }
        }

        private void HandleNodeTagRemoved(VariantMap eventData)
        {
            _NodeTagRemovedEventArgs.Set(eventData);
            NodeTagRemovedImpl?.Invoke(_urhoObject.Value, _NodeTagRemovedEventArgs);
        }

        #endregion

        #region ComponentEnabledChanged
        // -------------------------------------------- ComponentEnabledChanged --------------------------------------------

        /// <summary>
        /// A component's enabled state has changed.
        /// </summary>
        public class ComponentEnabledChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.ComponentEnabledChanged.Scene].VoidPtr;
                Node = eventData[E.ComponentEnabledChanged.Node].VoidPtr;
                Component = eventData[E.ComponentEnabledChanged.Component].VoidPtr;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Node { get; private set; }

            public IntPtr Component { get; private set; }
        }

        private event EventHandler<ComponentEnabledChangedEventArgs> ComponentEnabledChangedImpl;

        private readonly ComponentEnabledChangedEventArgs _ComponentEnabledChangedEventArgs = new ComponentEnabledChangedEventArgs();
        
        /// <summary>
        /// A component's enabled state has changed.
        /// </summary>
        public event EventHandler<ComponentEnabledChangedEventArgs> ComponentEnabledChanged
        {
            add
            {
                if (ComponentEnabledChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ComponentEnabledChanged, HandleComponentEnabledChanged);
                }
                ComponentEnabledChangedImpl += value;
            }
            remove
            {
                ComponentEnabledChangedImpl -= value;
                if (ComponentEnabledChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ComponentEnabledChanged);
                }
            }
        }

        private void HandleComponentEnabledChanged(VariantMap eventData)
        {
            _ComponentEnabledChangedEventArgs.Set(eventData);
            ComponentEnabledChangedImpl?.Invoke(_urhoObject.Value, _ComponentEnabledChangedEventArgs);
        }

        #endregion

        #region TemporaryChanged
        // -------------------------------------------- TemporaryChanged --------------------------------------------

        /// <summary>
        /// A serializable's temporary state has changed.
        /// </summary>
        public class TemporaryChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Serializable = eventData[E.TemporaryChanged.Serializable].VoidPtr;
            }

            public IntPtr Serializable { get; private set; }
        }

        private event EventHandler<TemporaryChangedEventArgs> TemporaryChangedImpl;

        private readonly TemporaryChangedEventArgs _TemporaryChangedEventArgs = new TemporaryChangedEventArgs();
        
        /// <summary>
        /// A serializable's temporary state has changed.
        /// </summary>
        public event EventHandler<TemporaryChangedEventArgs> TemporaryChanged
        {
            add
            {
                if (TemporaryChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TemporaryChanged, HandleTemporaryChanged);
                }
                TemporaryChangedImpl += value;
            }
            remove
            {
                TemporaryChangedImpl -= value;
                if (TemporaryChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TemporaryChanged);
                }
            }
        }

        private void HandleTemporaryChanged(VariantMap eventData)
        {
            _TemporaryChangedEventArgs.Set(eventData);
            TemporaryChangedImpl?.Invoke(_urhoObject.Value, _TemporaryChangedEventArgs);
        }

        #endregion

        #region NodeCloned
        // -------------------------------------------- NodeCloned --------------------------------------------

        /// <summary>
        /// A node (and its children and components) has been cloned.
        /// </summary>
        public class NodeClonedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.NodeCloned.Scene].VoidPtr;
                Node = eventData[E.NodeCloned.Node].VoidPtr;
                CloneNode = eventData[E.NodeCloned.CloneNode].VoidPtr;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Node { get; private set; }

            public IntPtr CloneNode { get; private set; }
        }

        private event EventHandler<NodeClonedEventArgs> NodeClonedImpl;

        private readonly NodeClonedEventArgs _NodeClonedEventArgs = new NodeClonedEventArgs();
        
        /// <summary>
        /// A node (and its children and components) has been cloned.
        /// </summary>
        public event EventHandler<NodeClonedEventArgs> NodeCloned
        {
            add
            {
                if (NodeClonedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeCloned, HandleNodeCloned);
                }
                NodeClonedImpl += value;
            }
            remove
            {
                NodeClonedImpl -= value;
                if (NodeClonedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeCloned);
                }
            }
        }

        private void HandleNodeCloned(VariantMap eventData)
        {
            _NodeClonedEventArgs.Set(eventData);
            NodeClonedImpl?.Invoke(_urhoObject.Value, _NodeClonedEventArgs);
        }

        #endregion

        #region ComponentCloned
        // -------------------------------------------- ComponentCloned --------------------------------------------

        /// <summary>
        /// A component has been cloned.
        /// </summary>
        public class ComponentClonedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Scene = eventData[E.ComponentCloned.Scene].VoidPtr;
                Component = eventData[E.ComponentCloned.Component].VoidPtr;
                CloneComponent = eventData[E.ComponentCloned.CloneComponent].VoidPtr;
            }

            public IntPtr Scene { get; private set; }

            public IntPtr Component { get; private set; }

            public IntPtr CloneComponent { get; private set; }
        }

        private event EventHandler<ComponentClonedEventArgs> ComponentClonedImpl;

        private readonly ComponentClonedEventArgs _ComponentClonedEventArgs = new ComponentClonedEventArgs();
        
        /// <summary>
        /// A component has been cloned.
        /// </summary>
        public event EventHandler<ComponentClonedEventArgs> ComponentCloned
        {
            add
            {
                if (ComponentClonedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ComponentCloned, HandleComponentCloned);
                }
                ComponentClonedImpl += value;
            }
            remove
            {
                ComponentClonedImpl -= value;
                if (ComponentClonedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ComponentCloned);
                }
            }
        }

        private void HandleComponentCloned(VariantMap eventData)
        {
            _ComponentClonedEventArgs.Set(eventData);
            ComponentClonedImpl?.Invoke(_urhoObject.Value, _ComponentClonedEventArgs);
        }

        #endregion

        #region InterceptNetworkUpdate
        // -------------------------------------------- InterceptNetworkUpdate --------------------------------------------

        /// <summary>
        /// A network attribute update from the server has been intercepted.
        /// </summary>
        public class InterceptNetworkUpdateEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Serializable = eventData[E.InterceptNetworkUpdate.Serializable].VoidPtr;
                TimeStamp = eventData[E.InterceptNetworkUpdate.TimeStamp].UInt;
                Index = eventData[E.InterceptNetworkUpdate.Index].UInt;
                Name = eventData[E.InterceptNetworkUpdate.Name].String;
                Value = eventData[E.InterceptNetworkUpdate.Value];
            }

            public IntPtr Serializable { get; private set; }

            public uint TimeStamp { get; private set; }

            public uint Index { get; private set; }

            public String Name { get; private set; }

            public Variant Value { get; private set; }
        }

        private event EventHandler<InterceptNetworkUpdateEventArgs> InterceptNetworkUpdateImpl;

        private readonly InterceptNetworkUpdateEventArgs _InterceptNetworkUpdateEventArgs = new InterceptNetworkUpdateEventArgs();
        
        /// <summary>
        /// A network attribute update from the server has been intercepted.
        /// </summary>
        public event EventHandler<InterceptNetworkUpdateEventArgs> InterceptNetworkUpdate
        {
            add
            {
                if (InterceptNetworkUpdateImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.InterceptNetworkUpdate, HandleInterceptNetworkUpdate);
                }
                InterceptNetworkUpdateImpl += value;
            }
            remove
            {
                InterceptNetworkUpdateImpl -= value;
                if (InterceptNetworkUpdateImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.InterceptNetworkUpdate);
                }
            }
        }

        private void HandleInterceptNetworkUpdate(VariantMap eventData)
        {
            _InterceptNetworkUpdateEventArgs.Set(eventData);
            InterceptNetworkUpdateImpl?.Invoke(_urhoObject.Value, _InterceptNetworkUpdateEventArgs);
        }

        #endregion

        #region SceneActivated
        // -------------------------------------------- SceneActivated --------------------------------------------

        /// <summary>
        /// Scene manager has activated a new scene.
        /// </summary>
        public class SceneActivatedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                OldScene = eventData[E.SceneActivated.OldScene].VoidPtr;
                NewScene = eventData[E.SceneActivated.NewScene].VoidPtr;
            }

            public IntPtr OldScene { get; private set; }

            public IntPtr NewScene { get; private set; }
        }

        private event EventHandler<SceneActivatedEventArgs> SceneActivatedImpl;

        private readonly SceneActivatedEventArgs _SceneActivatedEventArgs = new SceneActivatedEventArgs();
        
        /// <summary>
        /// Scene manager has activated a new scene.
        /// </summary>
        public event EventHandler<SceneActivatedEventArgs> SceneActivated
        {
            add
            {
                if (SceneActivatedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.SceneActivated, HandleSceneActivated);
                }
                SceneActivatedImpl += value;
            }
            remove
            {
                SceneActivatedImpl -= value;
                if (SceneActivatedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.SceneActivated);
                }
            }
        }

        private void HandleSceneActivated(VariantMap eventData)
        {
            _SceneActivatedEventArgs.Set(eventData);
            SceneActivatedImpl?.Invoke(_urhoObject.Value, _SceneActivatedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (SceneUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.SceneUpdate);
                if (SceneSubsystemUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.SceneSubsystemUpdate);
                if (UpdateSmoothingImpl != null) urhoObject.UnsubscribeFromEvent(E.UpdateSmoothing);
                if (SceneDrawableUpdateFinishedImpl != null) urhoObject.UnsubscribeFromEvent(E.SceneDrawableUpdateFinished);
                if (TargetPositionChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.TargetPositionChanged);
                if (TargetRotationChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.TargetRotationChanged);
                if (AttributeAnimationUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.AttributeAnimationUpdate);
                if (AttributeAnimationAddedImpl != null) urhoObject.UnsubscribeFromEvent(E.AttributeAnimationAdded);
                if (AttributeAnimationRemovedImpl != null) urhoObject.UnsubscribeFromEvent(E.AttributeAnimationRemoved);
                if (ScenePostUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.ScenePostUpdate);
                if (AsyncLoadProgressImpl != null) urhoObject.UnsubscribeFromEvent(E.AsyncLoadProgress);
                if (AsyncLoadFinishedImpl != null) urhoObject.UnsubscribeFromEvent(E.AsyncLoadFinished);
                if (NodeAddedImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeAdded);
                if (NodeRemovedImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeRemoved);
                if (ComponentAddedImpl != null) urhoObject.UnsubscribeFromEvent(E.ComponentAdded);
                if (ComponentRemovedImpl != null) urhoObject.UnsubscribeFromEvent(E.ComponentRemoved);
                if (NodeNameChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeNameChanged);
                if (NodeEnabledChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeEnabledChanged);
                if (NodeTagAddedImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeTagAdded);
                if (NodeTagRemovedImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeTagRemoved);
                if (ComponentEnabledChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.ComponentEnabledChanged);
                if (TemporaryChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.TemporaryChanged);
                if (NodeClonedImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeCloned);
                if (ComponentClonedImpl != null) urhoObject.UnsubscribeFromEvent(E.ComponentCloned);
                if (InterceptNetworkUpdateImpl != null) urhoObject.UnsubscribeFromEvent(E.InterceptNetworkUpdate);
                if (SceneActivatedImpl != null) urhoObject.UnsubscribeFromEvent(E.SceneActivated);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class SystemUIEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public SystemUIEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region EndRenderingSystemUI
        // -------------------------------------------- EndRenderingSystemUI --------------------------------------------

        /// <summary>
        /// EndRenderingSystemUI
        /// </summary>
        public class EndRenderingSystemUIEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<EndRenderingSystemUIEventArgs> EndRenderingSystemUIImpl;

        private readonly EndRenderingSystemUIEventArgs _EndRenderingSystemUIEventArgs = new EndRenderingSystemUIEventArgs();
        
        /// <summary>
        /// EndRenderingSystemUI
        /// </summary>
        public event EventHandler<EndRenderingSystemUIEventArgs> EndRenderingSystemUI
        {
            add
            {
                if (EndRenderingSystemUIImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.EndRenderingSystemUI, HandleEndRenderingSystemUI);
                }
                EndRenderingSystemUIImpl += value;
            }
            remove
            {
                EndRenderingSystemUIImpl -= value;
                if (EndRenderingSystemUIImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.EndRenderingSystemUI);
                }
            }
        }

        private void HandleEndRenderingSystemUI(VariantMap eventData)
        {
            _EndRenderingSystemUIEventArgs.Set(eventData);
            EndRenderingSystemUIImpl?.Invoke(_urhoObject.Value, _EndRenderingSystemUIEventArgs);
        }

        #endregion

        #region ConsoleClosed
        // -------------------------------------------- ConsoleClosed --------------------------------------------

        /// <summary>
        /// ConsoleClosed
        /// </summary>
        public class ConsoleClosedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<ConsoleClosedEventArgs> ConsoleClosedImpl;

        private readonly ConsoleClosedEventArgs _ConsoleClosedEventArgs = new ConsoleClosedEventArgs();
        
        /// <summary>
        /// ConsoleClosed
        /// </summary>
        public event EventHandler<ConsoleClosedEventArgs> ConsoleClosed
        {
            add
            {
                if (ConsoleClosedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ConsoleClosed, HandleConsoleClosed);
                }
                ConsoleClosedImpl += value;
            }
            remove
            {
                ConsoleClosedImpl -= value;
                if (ConsoleClosedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ConsoleClosed);
                }
            }
        }

        private void HandleConsoleClosed(VariantMap eventData)
        {
            _ConsoleClosedEventArgs.Set(eventData);
            ConsoleClosedImpl?.Invoke(_urhoObject.Value, _ConsoleClosedEventArgs);
        }

        #endregion

        #region AttributeInspectorMenu
        // -------------------------------------------- AttributeInspectorMenu --------------------------------------------

        /// <summary>
        /// AttributeInspectorMenu
        /// </summary>
        public class AttributeInspectorMenuEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Serializable = eventData[E.AttributeInspectorMenu.Serializable].VoidPtr;
                AttributeInfo = eventData[E.AttributeInspectorMenu.AttributeInfo].VoidPtr;
            }

            public IntPtr Serializable { get; private set; }

            public IntPtr AttributeInfo { get; private set; }
        }

        private event EventHandler<AttributeInspectorMenuEventArgs> AttributeInspectorMenuImpl;

        private readonly AttributeInspectorMenuEventArgs _AttributeInspectorMenuEventArgs = new AttributeInspectorMenuEventArgs();
        
        /// <summary>
        /// AttributeInspectorMenu
        /// </summary>
        public event EventHandler<AttributeInspectorMenuEventArgs> AttributeInspectorMenu
        {
            add
            {
                if (AttributeInspectorMenuImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AttributeInspectorMenu, HandleAttributeInspectorMenu);
                }
                AttributeInspectorMenuImpl += value;
            }
            remove
            {
                AttributeInspectorMenuImpl -= value;
                if (AttributeInspectorMenuImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AttributeInspectorMenu);
                }
            }
        }

        private void HandleAttributeInspectorMenu(VariantMap eventData)
        {
            _AttributeInspectorMenuEventArgs.Set(eventData);
            AttributeInspectorMenuImpl?.Invoke(_urhoObject.Value, _AttributeInspectorMenuEventArgs);
        }

        #endregion

        #region AttributeInspectorValueModified
        // -------------------------------------------- AttributeInspectorValueModified --------------------------------------------

        /// <summary>
        /// AttributeInspectorValueModified
        /// </summary>
        public class AttributeInspectorValueModifiedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Serializable = eventData[E.AttributeInspectorValueModified.Serializable].VoidPtr;
                AttributeInfo = eventData[E.AttributeInspectorValueModified.AttributeInfo].VoidPtr;
                OldValue = eventData[E.AttributeInspectorValueModified.OldValue];
                NewValue = eventData[E.AttributeInspectorValueModified.NewValue];
                Reason = eventData[E.AttributeInspectorValueModified.Reason].UInt;
            }

            public IntPtr Serializable { get; private set; }

            public IntPtr AttributeInfo { get; private set; }

            public Variant OldValue { get; private set; }

            public Variant NewValue { get; private set; }

            public uint Reason { get; private set; }
        }

        private event EventHandler<AttributeInspectorValueModifiedEventArgs> AttributeInspectorValueModifiedImpl;

        private readonly AttributeInspectorValueModifiedEventArgs _AttributeInspectorValueModifiedEventArgs = new AttributeInspectorValueModifiedEventArgs();
        
        /// <summary>
        /// AttributeInspectorValueModified
        /// </summary>
        public event EventHandler<AttributeInspectorValueModifiedEventArgs> AttributeInspectorValueModified
        {
            add
            {
                if (AttributeInspectorValueModifiedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AttributeInspectorValueModified, HandleAttributeInspectorValueModified);
                }
                AttributeInspectorValueModifiedImpl += value;
            }
            remove
            {
                AttributeInspectorValueModifiedImpl -= value;
                if (AttributeInspectorValueModifiedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AttributeInspectorValueModified);
                }
            }
        }

        private void HandleAttributeInspectorValueModified(VariantMap eventData)
        {
            _AttributeInspectorValueModifiedEventArgs.Set(eventData);
            AttributeInspectorValueModifiedImpl?.Invoke(_urhoObject.Value, _AttributeInspectorValueModifiedEventArgs);
        }

        #endregion

        #region AttributeInspectorAttribute
        // -------------------------------------------- AttributeInspectorAttribute --------------------------------------------

        /// <summary>
        /// AttributeInspectorAttribute
        /// </summary>
        public class AttributeInspectorAttributeEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Serializable = eventData[E.AttributeInspectorAttribute.Serializable].VoidPtr;
                AttributeInfo = eventData[E.AttributeInspectorAttribute.AttributeInfo].VoidPtr;
                Color = eventData[E.AttributeInspectorAttribute.Color].Color;
                Hidden = eventData[E.AttributeInspectorAttribute.Hidden].Bool;
                Tooltip = eventData[E.AttributeInspectorAttribute.Tooltip].String;
                ValueKind = eventData[E.AttributeInspectorAttribute.ValueKind].Int;
            }

            public IntPtr Serializable { get; private set; }

            public IntPtr AttributeInfo { get; private set; }

            public Color Color { get; private set; }

            public bool Hidden { get; private set; }

            public String Tooltip { get; private set; }

            public int ValueKind { get; private set; }
        }

        private event EventHandler<AttributeInspectorAttributeEventArgs> AttributeInspectorAttributeImpl;

        private readonly AttributeInspectorAttributeEventArgs _AttributeInspectorAttributeEventArgs = new AttributeInspectorAttributeEventArgs();
        
        /// <summary>
        /// AttributeInspectorAttribute
        /// </summary>
        public event EventHandler<AttributeInspectorAttributeEventArgs> AttributeInspectorAttribute
        {
            add
            {
                if (AttributeInspectorAttributeImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.AttributeInspectorAttribute, HandleAttributeInspectorAttribute);
                }
                AttributeInspectorAttributeImpl += value;
            }
            remove
            {
                AttributeInspectorAttributeImpl -= value;
                if (AttributeInspectorAttributeImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.AttributeInspectorAttribute);
                }
            }
        }

        private void HandleAttributeInspectorAttribute(VariantMap eventData)
        {
            _AttributeInspectorAttributeEventArgs.Set(eventData);
            AttributeInspectorAttributeImpl?.Invoke(_urhoObject.Value, _AttributeInspectorAttributeEventArgs);
        }

        #endregion

        #region GizmoNodeModified
        // -------------------------------------------- GizmoNodeModified --------------------------------------------

        /// <summary>
        /// GizmoNodeModified
        /// </summary>
        public class GizmoNodeModifiedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.GizmoNodeModified.Node].VoidPtr;
                OldTransform = eventData[E.GizmoNodeModified.OldTransform].Matrix3x4;
                NewTransform = eventData[E.GizmoNodeModified.NewTransform].Matrix3x4;
            }

            public IntPtr Node { get; private set; }

            public Matrix3x4 OldTransform { get; private set; }

            public Matrix3x4 NewTransform { get; private set; }
        }

        private event EventHandler<GizmoNodeModifiedEventArgs> GizmoNodeModifiedImpl;

        private readonly GizmoNodeModifiedEventArgs _GizmoNodeModifiedEventArgs = new GizmoNodeModifiedEventArgs();
        
        /// <summary>
        /// GizmoNodeModified
        /// </summary>
        public event EventHandler<GizmoNodeModifiedEventArgs> GizmoNodeModified
        {
            add
            {
                if (GizmoNodeModifiedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.GizmoNodeModified, HandleGizmoNodeModified);
                }
                GizmoNodeModifiedImpl += value;
            }
            remove
            {
                GizmoNodeModifiedImpl -= value;
                if (GizmoNodeModifiedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.GizmoNodeModified);
                }
            }
        }

        private void HandleGizmoNodeModified(VariantMap eventData)
        {
            _GizmoNodeModifiedEventArgs.Set(eventData);
            GizmoNodeModifiedImpl?.Invoke(_urhoObject.Value, _GizmoNodeModifiedEventArgs);
        }

        #endregion

        #region GizmoSelectionChanged
        // -------------------------------------------- GizmoSelectionChanged --------------------------------------------

        /// <summary>
        /// GizmoSelectionChanged
        /// </summary>
        public class GizmoSelectionChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
            }
        }

        private event EventHandler<GizmoSelectionChangedEventArgs> GizmoSelectionChangedImpl;

        private readonly GizmoSelectionChangedEventArgs _GizmoSelectionChangedEventArgs = new GizmoSelectionChangedEventArgs();
        
        /// <summary>
        /// GizmoSelectionChanged
        /// </summary>
        public event EventHandler<GizmoSelectionChangedEventArgs> GizmoSelectionChanged
        {
            add
            {
                if (GizmoSelectionChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.GizmoSelectionChanged, HandleGizmoSelectionChanged);
                }
                GizmoSelectionChangedImpl += value;
            }
            remove
            {
                GizmoSelectionChangedImpl -= value;
                if (GizmoSelectionChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.GizmoSelectionChanged);
                }
            }
        }

        private void HandleGizmoSelectionChanged(VariantMap eventData)
        {
            _GizmoSelectionChangedEventArgs.Set(eventData);
            GizmoSelectionChangedImpl?.Invoke(_urhoObject.Value, _GizmoSelectionChangedEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (EndRenderingSystemUIImpl != null) urhoObject.UnsubscribeFromEvent(E.EndRenderingSystemUI);
                if (ConsoleClosedImpl != null) urhoObject.UnsubscribeFromEvent(E.ConsoleClosed);
                if (AttributeInspectorMenuImpl != null) urhoObject.UnsubscribeFromEvent(E.AttributeInspectorMenu);
                if (AttributeInspectorValueModifiedImpl != null) urhoObject.UnsubscribeFromEvent(E.AttributeInspectorValueModified);
                if (AttributeInspectorAttributeImpl != null) urhoObject.UnsubscribeFromEvent(E.AttributeInspectorAttribute);
                if (GizmoNodeModifiedImpl != null) urhoObject.UnsubscribeFromEvent(E.GizmoNodeModified);
                if (GizmoSelectionChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.GizmoSelectionChanged);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class UIEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public UIEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region UIMouseClick
        // -------------------------------------------- UIMouseClick --------------------------------------------

        /// <summary>
        /// Global mouse click in the UI. Sent by the UI subsystem.
        /// </summary>
        public class UIMouseClickEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.UIMouseClick.Element].VoidPtr;
                X = eventData[E.UIMouseClick.X].Int;
                Y = eventData[E.UIMouseClick.Y].Int;
                Button = eventData[E.UIMouseClick.Button].Int;
                Buttons = eventData[E.UIMouseClick.Buttons].Int;
                Qualifiers = eventData[E.UIMouseClick.Qualifiers].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int Button { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<UIMouseClickEventArgs> UIMouseClickImpl;

        private readonly UIMouseClickEventArgs _UIMouseClickEventArgs = new UIMouseClickEventArgs();
        
        /// <summary>
        /// Global mouse click in the UI. Sent by the UI subsystem.
        /// </summary>
        public event EventHandler<UIMouseClickEventArgs> UIMouseClick
        {
            add
            {
                if (UIMouseClickImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.UIMouseClick, HandleUIMouseClick);
                }
                UIMouseClickImpl += value;
            }
            remove
            {
                UIMouseClickImpl -= value;
                if (UIMouseClickImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.UIMouseClick);
                }
            }
        }

        private void HandleUIMouseClick(VariantMap eventData)
        {
            _UIMouseClickEventArgs.Set(eventData);
            UIMouseClickImpl?.Invoke(_urhoObject.Value, _UIMouseClickEventArgs);
        }

        #endregion

        #region UIMouseClickEnd
        // -------------------------------------------- UIMouseClickEnd --------------------------------------------

        /// <summary>
        /// Global mouse click end in the UI. Sent by the UI subsystem.
        /// </summary>
        public class UIMouseClickEndEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.UIMouseClickEnd.Element].VoidPtr;
                BeginElement = eventData[E.UIMouseClickEnd.BeginElement].VoidPtr;
                X = eventData[E.UIMouseClickEnd.X].Int;
                Y = eventData[E.UIMouseClickEnd.Y].Int;
                Button = eventData[E.UIMouseClickEnd.Button].Int;
                Buttons = eventData[E.UIMouseClickEnd.Buttons].Int;
                Qualifiers = eventData[E.UIMouseClickEnd.Qualifiers].Int;
            }

            public IntPtr Element { get; private set; }

            public IntPtr BeginElement { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int Button { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<UIMouseClickEndEventArgs> UIMouseClickEndImpl;

        private readonly UIMouseClickEndEventArgs _UIMouseClickEndEventArgs = new UIMouseClickEndEventArgs();
        
        /// <summary>
        /// Global mouse click end in the UI. Sent by the UI subsystem.
        /// </summary>
        public event EventHandler<UIMouseClickEndEventArgs> UIMouseClickEnd
        {
            add
            {
                if (UIMouseClickEndImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.UIMouseClickEnd, HandleUIMouseClickEnd);
                }
                UIMouseClickEndImpl += value;
            }
            remove
            {
                UIMouseClickEndImpl -= value;
                if (UIMouseClickEndImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.UIMouseClickEnd);
                }
            }
        }

        private void HandleUIMouseClickEnd(VariantMap eventData)
        {
            _UIMouseClickEndEventArgs.Set(eventData);
            UIMouseClickEndImpl?.Invoke(_urhoObject.Value, _UIMouseClickEndEventArgs);
        }

        #endregion

        #region UIMouseDoubleClick
        // -------------------------------------------- UIMouseDoubleClick --------------------------------------------

        /// <summary>
        /// Global mouse double click in the UI. Sent by the UI subsystem.
        /// </summary>
        public class UIMouseDoubleClickEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.UIMouseDoubleClick.Element].VoidPtr;
                X = eventData[E.UIMouseDoubleClick.X].Int;
                Y = eventData[E.UIMouseDoubleClick.Y].Int;
                XBegin = eventData[E.UIMouseDoubleClick.XBegin].Int;
                YBegin = eventData[E.UIMouseDoubleClick.YBegin].Int;
                Button = eventData[E.UIMouseDoubleClick.Button].Int;
                Buttons = eventData[E.UIMouseDoubleClick.Buttons].Int;
                Qualifiers = eventData[E.UIMouseDoubleClick.Qualifiers].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int XBegin { get; private set; }

            public int YBegin { get; private set; }

            public int Button { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<UIMouseDoubleClickEventArgs> UIMouseDoubleClickImpl;

        private readonly UIMouseDoubleClickEventArgs _UIMouseDoubleClickEventArgs = new UIMouseDoubleClickEventArgs();
        
        /// <summary>
        /// Global mouse double click in the UI. Sent by the UI subsystem.
        /// </summary>
        public event EventHandler<UIMouseDoubleClickEventArgs> UIMouseDoubleClick
        {
            add
            {
                if (UIMouseDoubleClickImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.UIMouseDoubleClick, HandleUIMouseDoubleClick);
                }
                UIMouseDoubleClickImpl += value;
            }
            remove
            {
                UIMouseDoubleClickImpl -= value;
                if (UIMouseDoubleClickImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.UIMouseDoubleClick);
                }
            }
        }

        private void HandleUIMouseDoubleClick(VariantMap eventData)
        {
            _UIMouseDoubleClickEventArgs.Set(eventData);
            UIMouseDoubleClickImpl?.Invoke(_urhoObject.Value, _UIMouseDoubleClickEventArgs);
        }

        #endregion

        #region Click
        // -------------------------------------------- Click --------------------------------------------

        /// <summary>
        /// Mouse click on a UI element. Parameters are same as in UIMouseClick event, but is sent by the element.
        /// </summary>
        public class ClickEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.Click.Element].VoidPtr;
                X = eventData[E.Click.X].Int;
                Y = eventData[E.Click.Y].Int;
                Button = eventData[E.Click.Button].Int;
                Buttons = eventData[E.Click.Buttons].Int;
                Qualifiers = eventData[E.Click.Qualifiers].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int Button { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<ClickEventArgs> ClickImpl;

        private readonly ClickEventArgs _ClickEventArgs = new ClickEventArgs();
        
        /// <summary>
        /// Mouse click on a UI element. Parameters are same as in UIMouseClick event, but is sent by the element.
        /// </summary>
        public event EventHandler<ClickEventArgs> Click
        {
            add
            {
                if (ClickImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.Click, HandleClick);
                }
                ClickImpl += value;
            }
            remove
            {
                ClickImpl -= value;
                if (ClickImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.Click);
                }
            }
        }

        private void HandleClick(VariantMap eventData)
        {
            _ClickEventArgs.Set(eventData);
            ClickImpl?.Invoke(_urhoObject.Value, _ClickEventArgs);
        }

        #endregion

        #region ClickEnd
        // -------------------------------------------- ClickEnd --------------------------------------------

        /// <summary>
        /// Mouse click end on a UI element. Parameters are same as in UIMouseClickEnd event, but is sent by the element.
        /// </summary>
        public class ClickEndEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.ClickEnd.Element].VoidPtr;
                BeginElement = eventData[E.ClickEnd.BeginElement].VoidPtr;
                X = eventData[E.ClickEnd.X].Int;
                Y = eventData[E.ClickEnd.Y].Int;
                Button = eventData[E.ClickEnd.Button].Int;
                Buttons = eventData[E.ClickEnd.Buttons].Int;
                Qualifiers = eventData[E.ClickEnd.Qualifiers].Int;
            }

            public IntPtr Element { get; private set; }

            public IntPtr BeginElement { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int Button { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<ClickEndEventArgs> ClickEndImpl;

        private readonly ClickEndEventArgs _ClickEndEventArgs = new ClickEndEventArgs();
        
        /// <summary>
        /// Mouse click end on a UI element. Parameters are same as in UIMouseClickEnd event, but is sent by the element.
        /// </summary>
        public event EventHandler<ClickEndEventArgs> ClickEnd
        {
            add
            {
                if (ClickEndImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ClickEnd, HandleClickEnd);
                }
                ClickEndImpl += value;
            }
            remove
            {
                ClickEndImpl -= value;
                if (ClickEndImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ClickEnd);
                }
            }
        }

        private void HandleClickEnd(VariantMap eventData)
        {
            _ClickEndEventArgs.Set(eventData);
            ClickEndImpl?.Invoke(_urhoObject.Value, _ClickEndEventArgs);
        }

        #endregion

        #region DoubleClick
        // -------------------------------------------- DoubleClick --------------------------------------------

        /// <summary>
        /// Mouse double click on a UI element. Parameters are same as in UIMouseDoubleClick event, but is sent by the element.
        /// </summary>
        public class DoubleClickEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.DoubleClick.Element].VoidPtr;
                X = eventData[E.DoubleClick.X].Int;
                Y = eventData[E.DoubleClick.Y].Int;
                XBegin = eventData[E.DoubleClick.XBegin].Int;
                YBegin = eventData[E.DoubleClick.YBegin].Int;
                Button = eventData[E.DoubleClick.Button].Int;
                Buttons = eventData[E.DoubleClick.Buttons].Int;
                Qualifiers = eventData[E.DoubleClick.Qualifiers].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int XBegin { get; private set; }

            public int YBegin { get; private set; }

            public int Button { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<DoubleClickEventArgs> DoubleClickImpl;

        private readonly DoubleClickEventArgs _DoubleClickEventArgs = new DoubleClickEventArgs();
        
        /// <summary>
        /// Mouse double click on a UI element. Parameters are same as in UIMouseDoubleClick event, but is sent by the element.
        /// </summary>
        public event EventHandler<DoubleClickEventArgs> DoubleClick
        {
            add
            {
                if (DoubleClickImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.DoubleClick, HandleDoubleClick);
                }
                DoubleClickImpl += value;
            }
            remove
            {
                DoubleClickImpl -= value;
                if (DoubleClickImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.DoubleClick);
                }
            }
        }

        private void HandleDoubleClick(VariantMap eventData)
        {
            _DoubleClickEventArgs.Set(eventData);
            DoubleClickImpl?.Invoke(_urhoObject.Value, _DoubleClickEventArgs);
        }

        #endregion

        #region DragDropTest
        // -------------------------------------------- DragDropTest --------------------------------------------

        /// <summary>
        /// Drag and drop test.
        /// </summary>
        public class DragDropTestEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Source = eventData[E.DragDropTest.Source].VoidPtr;
                Target = eventData[E.DragDropTest.Target].VoidPtr;
                Accept = eventData[E.DragDropTest.Accept].Bool;
            }

            public IntPtr Source { get; private set; }

            public IntPtr Target { get; private set; }

            public bool Accept { get; private set; }
        }

        private event EventHandler<DragDropTestEventArgs> DragDropTestImpl;

        private readonly DragDropTestEventArgs _DragDropTestEventArgs = new DragDropTestEventArgs();
        
        /// <summary>
        /// Drag and drop test.
        /// </summary>
        public event EventHandler<DragDropTestEventArgs> DragDropTest
        {
            add
            {
                if (DragDropTestImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.DragDropTest, HandleDragDropTest);
                }
                DragDropTestImpl += value;
            }
            remove
            {
                DragDropTestImpl -= value;
                if (DragDropTestImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.DragDropTest);
                }
            }
        }

        private void HandleDragDropTest(VariantMap eventData)
        {
            _DragDropTestEventArgs.Set(eventData);
            DragDropTestImpl?.Invoke(_urhoObject.Value, _DragDropTestEventArgs);
        }

        #endregion

        #region DragDropFinish
        // -------------------------------------------- DragDropFinish --------------------------------------------

        /// <summary>
        /// Drag and drop finish.
        /// </summary>
        public class DragDropFinishEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Source = eventData[E.DragDropFinish.Source].VoidPtr;
                Target = eventData[E.DragDropFinish.Target].VoidPtr;
                Accept = eventData[E.DragDropFinish.Accept].Bool;
            }

            public IntPtr Source { get; private set; }

            public IntPtr Target { get; private set; }

            public bool Accept { get; private set; }
        }

        private event EventHandler<DragDropFinishEventArgs> DragDropFinishImpl;

        private readonly DragDropFinishEventArgs _DragDropFinishEventArgs = new DragDropFinishEventArgs();
        
        /// <summary>
        /// Drag and drop finish.
        /// </summary>
        public event EventHandler<DragDropFinishEventArgs> DragDropFinish
        {
            add
            {
                if (DragDropFinishImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.DragDropFinish, HandleDragDropFinish);
                }
                DragDropFinishImpl += value;
            }
            remove
            {
                DragDropFinishImpl -= value;
                if (DragDropFinishImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.DragDropFinish);
                }
            }
        }

        private void HandleDragDropFinish(VariantMap eventData)
        {
            _DragDropFinishEventArgs.Set(eventData);
            DragDropFinishImpl?.Invoke(_urhoObject.Value, _DragDropFinishEventArgs);
        }

        #endregion

        #region FocusChanged
        // -------------------------------------------- FocusChanged --------------------------------------------

        /// <summary>
        /// Focus element changed.
        /// </summary>
        public class FocusChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.FocusChanged.Element].VoidPtr;
                ClickedElement = eventData[E.FocusChanged.ClickedElement].VoidPtr;
            }

            public IntPtr Element { get; private set; }

            public IntPtr ClickedElement { get; private set; }
        }

        private event EventHandler<FocusChangedEventArgs> FocusChangedImpl;

        private readonly FocusChangedEventArgs _FocusChangedEventArgs = new FocusChangedEventArgs();
        
        /// <summary>
        /// Focus element changed.
        /// </summary>
        public event EventHandler<FocusChangedEventArgs> FocusChanged
        {
            add
            {
                if (FocusChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.FocusChanged, HandleFocusChanged);
                }
                FocusChangedImpl += value;
            }
            remove
            {
                FocusChangedImpl -= value;
                if (FocusChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.FocusChanged);
                }
            }
        }

        private void HandleFocusChanged(VariantMap eventData)
        {
            _FocusChangedEventArgs.Set(eventData);
            FocusChangedImpl?.Invoke(_urhoObject.Value, _FocusChangedEventArgs);
        }

        #endregion

        #region NameChanged
        // -------------------------------------------- NameChanged --------------------------------------------

        /// <summary>
        /// UI element name changed.
        /// </summary>
        public class NameChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.NameChanged.Element].VoidPtr;
            }

            public IntPtr Element { get; private set; }
        }

        private event EventHandler<NameChangedEventArgs> NameChangedImpl;

        private readonly NameChangedEventArgs _NameChangedEventArgs = new NameChangedEventArgs();
        
        /// <summary>
        /// UI element name changed.
        /// </summary>
        public event EventHandler<NameChangedEventArgs> NameChanged
        {
            add
            {
                if (NameChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NameChanged, HandleNameChanged);
                }
                NameChangedImpl += value;
            }
            remove
            {
                NameChangedImpl -= value;
                if (NameChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NameChanged);
                }
            }
        }

        private void HandleNameChanged(VariantMap eventData)
        {
            _NameChangedEventArgs.Set(eventData);
            NameChangedImpl?.Invoke(_urhoObject.Value, _NameChangedEventArgs);
        }

        #endregion

        #region Resized
        // -------------------------------------------- Resized --------------------------------------------

        /// <summary>
        /// UI element resized.
        /// </summary>
        public class ResizedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.Resized.Element].VoidPtr;
                Width = eventData[E.Resized.Width].Int;
                Height = eventData[E.Resized.Height].Int;
                DX = eventData[E.Resized.DX].Int;
                DY = eventData[E.Resized.DY].Int;
            }

            public IntPtr Element { get; private set; }

            public int Width { get; private set; }

            public int Height { get; private set; }

            public int DX { get; private set; }

            public int DY { get; private set; }
        }

        private event EventHandler<ResizedEventArgs> ResizedImpl;

        private readonly ResizedEventArgs _ResizedEventArgs = new ResizedEventArgs();
        
        /// <summary>
        /// UI element resized.
        /// </summary>
        public event EventHandler<ResizedEventArgs> Resized
        {
            add
            {
                if (ResizedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.Resized, HandleResized);
                }
                ResizedImpl += value;
            }
            remove
            {
                ResizedImpl -= value;
                if (ResizedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.Resized);
                }
            }
        }

        private void HandleResized(VariantMap eventData)
        {
            _ResizedEventArgs.Set(eventData);
            ResizedImpl?.Invoke(_urhoObject.Value, _ResizedEventArgs);
        }

        #endregion

        #region Positioned
        // -------------------------------------------- Positioned --------------------------------------------

        /// <summary>
        /// UI element positioned.
        /// </summary>
        public class PositionedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.Positioned.Element].VoidPtr;
                X = eventData[E.Positioned.X].Int;
                Y = eventData[E.Positioned.Y].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }
        }

        private event EventHandler<PositionedEventArgs> PositionedImpl;

        private readonly PositionedEventArgs _PositionedEventArgs = new PositionedEventArgs();
        
        /// <summary>
        /// UI element positioned.
        /// </summary>
        public event EventHandler<PositionedEventArgs> Positioned
        {
            add
            {
                if (PositionedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.Positioned, HandlePositioned);
                }
                PositionedImpl += value;
            }
            remove
            {
                PositionedImpl -= value;
                if (PositionedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.Positioned);
                }
            }
        }

        private void HandlePositioned(VariantMap eventData)
        {
            _PositionedEventArgs.Set(eventData);
            PositionedImpl?.Invoke(_urhoObject.Value, _PositionedEventArgs);
        }

        #endregion

        #region VisibleChanged
        // -------------------------------------------- VisibleChanged --------------------------------------------

        /// <summary>
        /// UI element visibility changed.
        /// </summary>
        public class VisibleChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.VisibleChanged.Element].VoidPtr;
                Visible = eventData[E.VisibleChanged.Visible].Bool;
            }

            public IntPtr Element { get; private set; }

            public bool Visible { get; private set; }
        }

        private event EventHandler<VisibleChangedEventArgs> VisibleChangedImpl;

        private readonly VisibleChangedEventArgs _VisibleChangedEventArgs = new VisibleChangedEventArgs();
        
        /// <summary>
        /// UI element visibility changed.
        /// </summary>
        public event EventHandler<VisibleChangedEventArgs> VisibleChanged
        {
            add
            {
                if (VisibleChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.VisibleChanged, HandleVisibleChanged);
                }
                VisibleChangedImpl += value;
            }
            remove
            {
                VisibleChangedImpl -= value;
                if (VisibleChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.VisibleChanged);
                }
            }
        }

        private void HandleVisibleChanged(VariantMap eventData)
        {
            _VisibleChangedEventArgs.Set(eventData);
            VisibleChangedImpl?.Invoke(_urhoObject.Value, _VisibleChangedEventArgs);
        }

        #endregion

        #region Focused
        // -------------------------------------------- Focused --------------------------------------------

        /// <summary>
        /// UI element focused.
        /// </summary>
        public class FocusedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.Focused.Element].VoidPtr;
                ByKey = eventData[E.Focused.ByKey].Bool;
            }

            public IntPtr Element { get; private set; }

            public bool ByKey { get; private set; }
        }

        private event EventHandler<FocusedEventArgs> FocusedImpl;

        private readonly FocusedEventArgs _FocusedEventArgs = new FocusedEventArgs();
        
        /// <summary>
        /// UI element focused.
        /// </summary>
        public event EventHandler<FocusedEventArgs> Focused
        {
            add
            {
                if (FocusedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.Focused, HandleFocused);
                }
                FocusedImpl += value;
            }
            remove
            {
                FocusedImpl -= value;
                if (FocusedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.Focused);
                }
            }
        }

        private void HandleFocused(VariantMap eventData)
        {
            _FocusedEventArgs.Set(eventData);
            FocusedImpl?.Invoke(_urhoObject.Value, _FocusedEventArgs);
        }

        #endregion

        #region Defocused
        // -------------------------------------------- Defocused --------------------------------------------

        /// <summary>
        /// UI element defocused.
        /// </summary>
        public class DefocusedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.Defocused.Element].VoidPtr;
            }

            public IntPtr Element { get; private set; }
        }

        private event EventHandler<DefocusedEventArgs> DefocusedImpl;

        private readonly DefocusedEventArgs _DefocusedEventArgs = new DefocusedEventArgs();
        
        /// <summary>
        /// UI element defocused.
        /// </summary>
        public event EventHandler<DefocusedEventArgs> Defocused
        {
            add
            {
                if (DefocusedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.Defocused, HandleDefocused);
                }
                DefocusedImpl += value;
            }
            remove
            {
                DefocusedImpl -= value;
                if (DefocusedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.Defocused);
                }
            }
        }

        private void HandleDefocused(VariantMap eventData)
        {
            _DefocusedEventArgs.Set(eventData);
            DefocusedImpl?.Invoke(_urhoObject.Value, _DefocusedEventArgs);
        }

        #endregion

        #region LayoutUpdated
        // -------------------------------------------- LayoutUpdated --------------------------------------------

        /// <summary>
        /// UI element layout updated.
        /// </summary>
        public class LayoutUpdatedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.LayoutUpdated.Element].VoidPtr;
            }

            public IntPtr Element { get; private set; }
        }

        private event EventHandler<LayoutUpdatedEventArgs> LayoutUpdatedImpl;

        private readonly LayoutUpdatedEventArgs _LayoutUpdatedEventArgs = new LayoutUpdatedEventArgs();
        
        /// <summary>
        /// UI element layout updated.
        /// </summary>
        public event EventHandler<LayoutUpdatedEventArgs> LayoutUpdated
        {
            add
            {
                if (LayoutUpdatedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.LayoutUpdated, HandleLayoutUpdated);
                }
                LayoutUpdatedImpl += value;
            }
            remove
            {
                LayoutUpdatedImpl -= value;
                if (LayoutUpdatedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.LayoutUpdated);
                }
            }
        }

        private void HandleLayoutUpdated(VariantMap eventData)
        {
            _LayoutUpdatedEventArgs.Set(eventData);
            LayoutUpdatedImpl?.Invoke(_urhoObject.Value, _LayoutUpdatedEventArgs);
        }

        #endregion

        #region Pressed
        // -------------------------------------------- Pressed --------------------------------------------

        /// <summary>
        /// UI button pressed.
        /// </summary>
        public class PressedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.Pressed.Element].VoidPtr;
            }

            public IntPtr Element { get; private set; }
        }

        private event EventHandler<PressedEventArgs> PressedImpl;

        private readonly PressedEventArgs _PressedEventArgs = new PressedEventArgs();
        
        /// <summary>
        /// UI button pressed.
        /// </summary>
        public event EventHandler<PressedEventArgs> Pressed
        {
            add
            {
                if (PressedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.Pressed, HandlePressed);
                }
                PressedImpl += value;
            }
            remove
            {
                PressedImpl -= value;
                if (PressedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.Pressed);
                }
            }
        }

        private void HandlePressed(VariantMap eventData)
        {
            _PressedEventArgs.Set(eventData);
            PressedImpl?.Invoke(_urhoObject.Value, _PressedEventArgs);
        }

        #endregion

        #region Released
        // -------------------------------------------- Released --------------------------------------------

        /// <summary>
        /// UI button was pressed, then released.
        /// </summary>
        public class ReleasedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.Released.Element].VoidPtr;
            }

            public IntPtr Element { get; private set; }
        }

        private event EventHandler<ReleasedEventArgs> ReleasedImpl;

        private readonly ReleasedEventArgs _ReleasedEventArgs = new ReleasedEventArgs();
        
        /// <summary>
        /// UI button was pressed, then released.
        /// </summary>
        public event EventHandler<ReleasedEventArgs> Released
        {
            add
            {
                if (ReleasedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.Released, HandleReleased);
                }
                ReleasedImpl += value;
            }
            remove
            {
                ReleasedImpl -= value;
                if (ReleasedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.Released);
                }
            }
        }

        private void HandleReleased(VariantMap eventData)
        {
            _ReleasedEventArgs.Set(eventData);
            ReleasedImpl?.Invoke(_urhoObject.Value, _ReleasedEventArgs);
        }

        #endregion

        #region Toggled
        // -------------------------------------------- Toggled --------------------------------------------

        /// <summary>
        /// UI checkbox toggled.
        /// </summary>
        public class ToggledEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.Toggled.Element].VoidPtr;
                State = eventData[E.Toggled.State].Bool;
            }

            public IntPtr Element { get; private set; }

            public bool State { get; private set; }
        }

        private event EventHandler<ToggledEventArgs> ToggledImpl;

        private readonly ToggledEventArgs _ToggledEventArgs = new ToggledEventArgs();
        
        /// <summary>
        /// UI checkbox toggled.
        /// </summary>
        public event EventHandler<ToggledEventArgs> Toggled
        {
            add
            {
                if (ToggledImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.Toggled, HandleToggled);
                }
                ToggledImpl += value;
            }
            remove
            {
                ToggledImpl -= value;
                if (ToggledImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.Toggled);
                }
            }
        }

        private void HandleToggled(VariantMap eventData)
        {
            _ToggledEventArgs.Set(eventData);
            ToggledImpl?.Invoke(_urhoObject.Value, _ToggledEventArgs);
        }

        #endregion

        #region SliderChanged
        // -------------------------------------------- SliderChanged --------------------------------------------

        /// <summary>
        /// UI slider value changed.
        /// </summary>
        public class SliderChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.SliderChanged.Element].VoidPtr;
                Value = eventData[E.SliderChanged.Value].Float;
            }

            public IntPtr Element { get; private set; }

            public float Value { get; private set; }
        }

        private event EventHandler<SliderChangedEventArgs> SliderChangedImpl;

        private readonly SliderChangedEventArgs _SliderChangedEventArgs = new SliderChangedEventArgs();
        
        /// <summary>
        /// UI slider value changed.
        /// </summary>
        public event EventHandler<SliderChangedEventArgs> SliderChanged
        {
            add
            {
                if (SliderChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.SliderChanged, HandleSliderChanged);
                }
                SliderChangedImpl += value;
            }
            remove
            {
                SliderChangedImpl -= value;
                if (SliderChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.SliderChanged);
                }
            }
        }

        private void HandleSliderChanged(VariantMap eventData)
        {
            _SliderChangedEventArgs.Set(eventData);
            SliderChangedImpl?.Invoke(_urhoObject.Value, _SliderChangedEventArgs);
        }

        #endregion

        #region SliderPaged
        // -------------------------------------------- SliderPaged --------------------------------------------

        /// <summary>
        /// UI slider being paged.
        /// </summary>
        public class SliderPagedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.SliderPaged.Element].VoidPtr;
                Offset = eventData[E.SliderPaged.Offset].Int;
                Pressed = eventData[E.SliderPaged.Pressed].Bool;
            }

            public IntPtr Element { get; private set; }

            public int Offset { get; private set; }

            public bool Pressed { get; private set; }
        }

        private event EventHandler<SliderPagedEventArgs> SliderPagedImpl;

        private readonly SliderPagedEventArgs _SliderPagedEventArgs = new SliderPagedEventArgs();
        
        /// <summary>
        /// UI slider being paged.
        /// </summary>
        public event EventHandler<SliderPagedEventArgs> SliderPaged
        {
            add
            {
                if (SliderPagedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.SliderPaged, HandleSliderPaged);
                }
                SliderPagedImpl += value;
            }
            remove
            {
                SliderPagedImpl -= value;
                if (SliderPagedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.SliderPaged);
                }
            }
        }

        private void HandleSliderPaged(VariantMap eventData)
        {
            _SliderPagedEventArgs.Set(eventData);
            SliderPagedImpl?.Invoke(_urhoObject.Value, _SliderPagedEventArgs);
        }

        #endregion

        #region ProgressBarChanged
        // -------------------------------------------- ProgressBarChanged --------------------------------------------

        /// <summary>
        /// UI progressbar value changed.
        /// </summary>
        public class ProgressBarChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.ProgressBarChanged.Element].VoidPtr;
                Value = eventData[E.ProgressBarChanged.Value].Float;
            }

            public IntPtr Element { get; private set; }

            public float Value { get; private set; }
        }

        private event EventHandler<ProgressBarChangedEventArgs> ProgressBarChangedImpl;

        private readonly ProgressBarChangedEventArgs _ProgressBarChangedEventArgs = new ProgressBarChangedEventArgs();
        
        /// <summary>
        /// UI progressbar value changed.
        /// </summary>
        public event EventHandler<ProgressBarChangedEventArgs> ProgressBarChanged
        {
            add
            {
                if (ProgressBarChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ProgressBarChanged, HandleProgressBarChanged);
                }
                ProgressBarChangedImpl += value;
            }
            remove
            {
                ProgressBarChangedImpl -= value;
                if (ProgressBarChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ProgressBarChanged);
                }
            }
        }

        private void HandleProgressBarChanged(VariantMap eventData)
        {
            _ProgressBarChangedEventArgs.Set(eventData);
            ProgressBarChangedImpl?.Invoke(_urhoObject.Value, _ProgressBarChangedEventArgs);
        }

        #endregion

        #region ScrollBarChanged
        // -------------------------------------------- ScrollBarChanged --------------------------------------------

        /// <summary>
        /// UI scrollbar value changed.
        /// </summary>
        public class ScrollBarChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.ScrollBarChanged.Element].VoidPtr;
                Value = eventData[E.ScrollBarChanged.Value].Float;
            }

            public IntPtr Element { get; private set; }

            public float Value { get; private set; }
        }

        private event EventHandler<ScrollBarChangedEventArgs> ScrollBarChangedImpl;

        private readonly ScrollBarChangedEventArgs _ScrollBarChangedEventArgs = new ScrollBarChangedEventArgs();
        
        /// <summary>
        /// UI scrollbar value changed.
        /// </summary>
        public event EventHandler<ScrollBarChangedEventArgs> ScrollBarChanged
        {
            add
            {
                if (ScrollBarChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ScrollBarChanged, HandleScrollBarChanged);
                }
                ScrollBarChangedImpl += value;
            }
            remove
            {
                ScrollBarChangedImpl -= value;
                if (ScrollBarChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ScrollBarChanged);
                }
            }
        }

        private void HandleScrollBarChanged(VariantMap eventData)
        {
            _ScrollBarChangedEventArgs.Set(eventData);
            ScrollBarChangedImpl?.Invoke(_urhoObject.Value, _ScrollBarChangedEventArgs);
        }

        #endregion

        #region ViewChanged
        // -------------------------------------------- ViewChanged --------------------------------------------

        /// <summary>
        /// UI scrollview position changed.
        /// </summary>
        public class ViewChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.ViewChanged.Element].VoidPtr;
                X = eventData[E.ViewChanged.X].Int;
                Y = eventData[E.ViewChanged.Y].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }
        }

        private event EventHandler<ViewChangedEventArgs> ViewChangedImpl;

        private readonly ViewChangedEventArgs _ViewChangedEventArgs = new ViewChangedEventArgs();
        
        /// <summary>
        /// UI scrollview position changed.
        /// </summary>
        public event EventHandler<ViewChangedEventArgs> ViewChanged
        {
            add
            {
                if (ViewChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ViewChanged, HandleViewChanged);
                }
                ViewChangedImpl += value;
            }
            remove
            {
                ViewChangedImpl -= value;
                if (ViewChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ViewChanged);
                }
            }
        }

        private void HandleViewChanged(VariantMap eventData)
        {
            _ViewChangedEventArgs.Set(eventData);
            ViewChangedImpl?.Invoke(_urhoObject.Value, _ViewChangedEventArgs);
        }

        #endregion

        #region ModalChanged
        // -------------------------------------------- ModalChanged --------------------------------------------

        /// <summary>
        /// UI modal changed (currently only Window has modal flag).
        /// </summary>
        public class ModalChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.ModalChanged.Element].VoidPtr;
                Modal = eventData[E.ModalChanged.Modal].Bool;
            }

            public IntPtr Element { get; private set; }

            public bool Modal { get; private set; }
        }

        private event EventHandler<ModalChangedEventArgs> ModalChangedImpl;

        private readonly ModalChangedEventArgs _ModalChangedEventArgs = new ModalChangedEventArgs();
        
        /// <summary>
        /// UI modal changed (currently only Window has modal flag).
        /// </summary>
        public event EventHandler<ModalChangedEventArgs> ModalChanged
        {
            add
            {
                if (ModalChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ModalChanged, HandleModalChanged);
                }
                ModalChangedImpl += value;
            }
            remove
            {
                ModalChangedImpl -= value;
                if (ModalChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ModalChanged);
                }
            }
        }

        private void HandleModalChanged(VariantMap eventData)
        {
            _ModalChangedEventArgs.Set(eventData);
            ModalChangedImpl?.Invoke(_urhoObject.Value, _ModalChangedEventArgs);
        }

        #endregion

        #region TextEntry
        // -------------------------------------------- TextEntry --------------------------------------------

        /// <summary>
        /// Text entry into a LineEdit. The text can be modified in the event data.
        /// </summary>
        public class TextEntryEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.TextEntry.Element].VoidPtr;
                Text = eventData[E.TextEntry.Text].String;
            }

            public IntPtr Element { get; private set; }

            public String Text { get; private set; }
        }

        private event EventHandler<TextEntryEventArgs> TextEntryImpl;

        private readonly TextEntryEventArgs _TextEntryEventArgs = new TextEntryEventArgs();
        
        /// <summary>
        /// Text entry into a LineEdit. The text can be modified in the event data.
        /// </summary>
        public event EventHandler<TextEntryEventArgs> TextEntry
        {
            add
            {
                if (TextEntryImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TextEntry, HandleTextEntry);
                }
                TextEntryImpl += value;
            }
            remove
            {
                TextEntryImpl -= value;
                if (TextEntryImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TextEntry);
                }
            }
        }

        private void HandleTextEntry(VariantMap eventData)
        {
            _TextEntryEventArgs.Set(eventData);
            TextEntryImpl?.Invoke(_urhoObject.Value, _TextEntryEventArgs);
        }

        #endregion

        #region TextChanged
        // -------------------------------------------- TextChanged --------------------------------------------

        /// <summary>
        /// Editable text changed.
        /// </summary>
        public class TextChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.TextChanged.Element].VoidPtr;
                Text = eventData[E.TextChanged.Text].String;
            }

            public IntPtr Element { get; private set; }

            public String Text { get; private set; }
        }

        private event EventHandler<TextChangedEventArgs> TextChangedImpl;

        private readonly TextChangedEventArgs _TextChangedEventArgs = new TextChangedEventArgs();
        
        /// <summary>
        /// Editable text changed.
        /// </summary>
        public event EventHandler<TextChangedEventArgs> TextChanged
        {
            add
            {
                if (TextChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TextChanged, HandleTextChanged);
                }
                TextChangedImpl += value;
            }
            remove
            {
                TextChangedImpl -= value;
                if (TextChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TextChanged);
                }
            }
        }

        private void HandleTextChanged(VariantMap eventData)
        {
            _TextChangedEventArgs.Set(eventData);
            TextChangedImpl?.Invoke(_urhoObject.Value, _TextChangedEventArgs);
        }

        #endregion

        #region TextFinished
        // -------------------------------------------- TextFinished --------------------------------------------

        /// <summary>
        /// Text editing finished (enter pressed on a LineEdit).
        /// </summary>
        public class TextFinishedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.TextFinished.Element].VoidPtr;
                Text = eventData[E.TextFinished.Text].String;
                Value = eventData[E.TextFinished.Value].Float;
            }

            public IntPtr Element { get; private set; }

            public String Text { get; private set; }

            public float Value { get; private set; }
        }

        private event EventHandler<TextFinishedEventArgs> TextFinishedImpl;

        private readonly TextFinishedEventArgs _TextFinishedEventArgs = new TextFinishedEventArgs();
        
        /// <summary>
        /// Text editing finished (enter pressed on a LineEdit).
        /// </summary>
        public event EventHandler<TextFinishedEventArgs> TextFinished
        {
            add
            {
                if (TextFinishedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.TextFinished, HandleTextFinished);
                }
                TextFinishedImpl += value;
            }
            remove
            {
                TextFinishedImpl -= value;
                if (TextFinishedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.TextFinished);
                }
            }
        }

        private void HandleTextFinished(VariantMap eventData)
        {
            _TextFinishedEventArgs.Set(eventData);
            TextFinishedImpl?.Invoke(_urhoObject.Value, _TextFinishedEventArgs);
        }

        #endregion

        #region MenuSelected
        // -------------------------------------------- MenuSelected --------------------------------------------

        /// <summary>
        /// Menu selected.
        /// </summary>
        public class MenuSelectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.MenuSelected.Element].VoidPtr;
            }

            public IntPtr Element { get; private set; }
        }

        private event EventHandler<MenuSelectedEventArgs> MenuSelectedImpl;

        private readonly MenuSelectedEventArgs _MenuSelectedEventArgs = new MenuSelectedEventArgs();
        
        /// <summary>
        /// Menu selected.
        /// </summary>
        public event EventHandler<MenuSelectedEventArgs> MenuSelected
        {
            add
            {
                if (MenuSelectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.MenuSelected, HandleMenuSelected);
                }
                MenuSelectedImpl += value;
            }
            remove
            {
                MenuSelectedImpl -= value;
                if (MenuSelectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.MenuSelected);
                }
            }
        }

        private void HandleMenuSelected(VariantMap eventData)
        {
            _MenuSelectedEventArgs.Set(eventData);
            MenuSelectedImpl?.Invoke(_urhoObject.Value, _MenuSelectedEventArgs);
        }

        #endregion

        #region ItemSelected
        // -------------------------------------------- ItemSelected --------------------------------------------

        /// <summary>
        /// Listview or DropDownList item selected.
        /// </summary>
        public class ItemSelectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.ItemSelected.Element].VoidPtr;
                Selection = eventData[E.ItemSelected.Selection].Int;
            }

            public IntPtr Element { get; private set; }

            public int Selection { get; private set; }
        }

        private event EventHandler<ItemSelectedEventArgs> ItemSelectedImpl;

        private readonly ItemSelectedEventArgs _ItemSelectedEventArgs = new ItemSelectedEventArgs();
        
        /// <summary>
        /// Listview or DropDownList item selected.
        /// </summary>
        public event EventHandler<ItemSelectedEventArgs> ItemSelected
        {
            add
            {
                if (ItemSelectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ItemSelected, HandleItemSelected);
                }
                ItemSelectedImpl += value;
            }
            remove
            {
                ItemSelectedImpl -= value;
                if (ItemSelectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ItemSelected);
                }
            }
        }

        private void HandleItemSelected(VariantMap eventData)
        {
            _ItemSelectedEventArgs.Set(eventData);
            ItemSelectedImpl?.Invoke(_urhoObject.Value, _ItemSelectedEventArgs);
        }

        #endregion

        #region ItemDeselected
        // -------------------------------------------- ItemDeselected --------------------------------------------

        /// <summary>
        /// Listview item deselected.
        /// </summary>
        public class ItemDeselectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.ItemDeselected.Element].VoidPtr;
                Selection = eventData[E.ItemDeselected.Selection].Int;
            }

            public IntPtr Element { get; private set; }

            public int Selection { get; private set; }
        }

        private event EventHandler<ItemDeselectedEventArgs> ItemDeselectedImpl;

        private readonly ItemDeselectedEventArgs _ItemDeselectedEventArgs = new ItemDeselectedEventArgs();
        
        /// <summary>
        /// Listview item deselected.
        /// </summary>
        public event EventHandler<ItemDeselectedEventArgs> ItemDeselected
        {
            add
            {
                if (ItemDeselectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ItemDeselected, HandleItemDeselected);
                }
                ItemDeselectedImpl += value;
            }
            remove
            {
                ItemDeselectedImpl -= value;
                if (ItemDeselectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ItemDeselected);
                }
            }
        }

        private void HandleItemDeselected(VariantMap eventData)
        {
            _ItemDeselectedEventArgs.Set(eventData);
            ItemDeselectedImpl?.Invoke(_urhoObject.Value, _ItemDeselectedEventArgs);
        }

        #endregion

        #region SelectionChanged
        // -------------------------------------------- SelectionChanged --------------------------------------------

        /// <summary>
        /// Listview selection change finished.
        /// </summary>
        public class SelectionChangedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.SelectionChanged.Element].VoidPtr;
            }

            public IntPtr Element { get; private set; }
        }

        private event EventHandler<SelectionChangedEventArgs> SelectionChangedImpl;

        private readonly SelectionChangedEventArgs _SelectionChangedEventArgs = new SelectionChangedEventArgs();
        
        /// <summary>
        /// Listview selection change finished.
        /// </summary>
        public event EventHandler<SelectionChangedEventArgs> SelectionChanged
        {
            add
            {
                if (SelectionChangedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.SelectionChanged, HandleSelectionChanged);
                }
                SelectionChangedImpl += value;
            }
            remove
            {
                SelectionChangedImpl -= value;
                if (SelectionChangedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.SelectionChanged);
                }
            }
        }

        private void HandleSelectionChanged(VariantMap eventData)
        {
            _SelectionChangedEventArgs.Set(eventData);
            SelectionChangedImpl?.Invoke(_urhoObject.Value, _SelectionChangedEventArgs);
        }

        #endregion

        #region ItemClicked
        // -------------------------------------------- ItemClicked --------------------------------------------

        /// <summary>
        /// Listview item clicked. If this is a left-click, also ItemSelected event will be sent. If this is a right-click, only this event is sent.
        /// </summary>
        public class ItemClickedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.ItemClicked.Element].VoidPtr;
                Item = eventData[E.ItemClicked.Item].VoidPtr;
                Selection = eventData[E.ItemClicked.Selection].Int;
                Button = eventData[E.ItemClicked.Button].Int;
                Buttons = eventData[E.ItemClicked.Buttons].Int;
                Qualifiers = eventData[E.ItemClicked.Qualifiers].Int;
            }

            public IntPtr Element { get; private set; }

            public IntPtr Item { get; private set; }

            public int Selection { get; private set; }

            public int Button { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<ItemClickedEventArgs> ItemClickedImpl;

        private readonly ItemClickedEventArgs _ItemClickedEventArgs = new ItemClickedEventArgs();
        
        /// <summary>
        /// Listview item clicked. If this is a left-click, also ItemSelected event will be sent. If this is a right-click, only this event is sent.
        /// </summary>
        public event EventHandler<ItemClickedEventArgs> ItemClicked
        {
            add
            {
                if (ItemClickedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ItemClicked, HandleItemClicked);
                }
                ItemClickedImpl += value;
            }
            remove
            {
                ItemClickedImpl -= value;
                if (ItemClickedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ItemClicked);
                }
            }
        }

        private void HandleItemClicked(VariantMap eventData)
        {
            _ItemClickedEventArgs.Set(eventData);
            ItemClickedImpl?.Invoke(_urhoObject.Value, _ItemClickedEventArgs);
        }

        #endregion

        #region ItemDoubleClicked
        // -------------------------------------------- ItemDoubleClicked --------------------------------------------

        /// <summary>
        /// Listview item double clicked.
        /// </summary>
        public class ItemDoubleClickedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.ItemDoubleClicked.Element].VoidPtr;
                Item = eventData[E.ItemDoubleClicked.Item].VoidPtr;
                Selection = eventData[E.ItemDoubleClicked.Selection].Int;
                Button = eventData[E.ItemDoubleClicked.Button].Int;
                Buttons = eventData[E.ItemDoubleClicked.Buttons].Int;
                Qualifiers = eventData[E.ItemDoubleClicked.Qualifiers].Int;
            }

            public IntPtr Element { get; private set; }

            public IntPtr Item { get; private set; }

            public int Selection { get; private set; }

            public int Button { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<ItemDoubleClickedEventArgs> ItemDoubleClickedImpl;

        private readonly ItemDoubleClickedEventArgs _ItemDoubleClickedEventArgs = new ItemDoubleClickedEventArgs();
        
        /// <summary>
        /// Listview item double clicked.
        /// </summary>
        public event EventHandler<ItemDoubleClickedEventArgs> ItemDoubleClicked
        {
            add
            {
                if (ItemDoubleClickedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ItemDoubleClicked, HandleItemDoubleClicked);
                }
                ItemDoubleClickedImpl += value;
            }
            remove
            {
                ItemDoubleClickedImpl -= value;
                if (ItemDoubleClickedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ItemDoubleClicked);
                }
            }
        }

        private void HandleItemDoubleClicked(VariantMap eventData)
        {
            _ItemDoubleClickedEventArgs.Set(eventData);
            ItemDoubleClickedImpl?.Invoke(_urhoObject.Value, _ItemDoubleClickedEventArgs);
        }

        #endregion

        #region UnhandledKey
        // -------------------------------------------- UnhandledKey --------------------------------------------

        /// <summary>
        /// LineEdit or ListView unhandled key pressed.
        /// </summary>
        public class UnhandledKeyEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.UnhandledKey.Element].VoidPtr;
                Key = eventData[E.UnhandledKey.Key].Int;
                Buttons = eventData[E.UnhandledKey.Buttons].Int;
                Qualifiers = eventData[E.UnhandledKey.Qualifiers].Int;
            }

            public IntPtr Element { get; private set; }

            public int Key { get; private set; }

            public int Buttons { get; private set; }

            public int Qualifiers { get; private set; }
        }

        private event EventHandler<UnhandledKeyEventArgs> UnhandledKeyImpl;

        private readonly UnhandledKeyEventArgs _UnhandledKeyEventArgs = new UnhandledKeyEventArgs();
        
        /// <summary>
        /// LineEdit or ListView unhandled key pressed.
        /// </summary>
        public event EventHandler<UnhandledKeyEventArgs> UnhandledKey
        {
            add
            {
                if (UnhandledKeyImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.UnhandledKey, HandleUnhandledKey);
                }
                UnhandledKeyImpl += value;
            }
            remove
            {
                UnhandledKeyImpl -= value;
                if (UnhandledKeyImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.UnhandledKey);
                }
            }
        }

        private void HandleUnhandledKey(VariantMap eventData)
        {
            _UnhandledKeyEventArgs.Set(eventData);
            UnhandledKeyImpl?.Invoke(_urhoObject.Value, _UnhandledKeyEventArgs);
        }

        #endregion

        #region FileSelected
        // -------------------------------------------- FileSelected --------------------------------------------

        /// <summary>
        /// Fileselector choice.
        /// </summary>
        public class FileSelectedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                FileName = eventData[E.FileSelected.FileName].String;
                Filter = eventData[E.FileSelected.Filter].String;
                OK = eventData[E.FileSelected.OK].Bool;
            }

            public String FileName { get; private set; }

            public String Filter { get; private set; }

            public bool OK { get; private set; }
        }

        private event EventHandler<FileSelectedEventArgs> FileSelectedImpl;

        private readonly FileSelectedEventArgs _FileSelectedEventArgs = new FileSelectedEventArgs();
        
        /// <summary>
        /// Fileselector choice.
        /// </summary>
        public event EventHandler<FileSelectedEventArgs> FileSelected
        {
            add
            {
                if (FileSelectedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.FileSelected, HandleFileSelected);
                }
                FileSelectedImpl += value;
            }
            remove
            {
                FileSelectedImpl -= value;
                if (FileSelectedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.FileSelected);
                }
            }
        }

        private void HandleFileSelected(VariantMap eventData)
        {
            _FileSelectedEventArgs.Set(eventData);
            FileSelectedImpl?.Invoke(_urhoObject.Value, _FileSelectedEventArgs);
        }

        #endregion

        #region MessageACK
        // -------------------------------------------- MessageACK --------------------------------------------

        /// <summary>
        /// MessageBox acknowlegement.
        /// </summary>
        public class MessageACKEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                OK = eventData[E.MessageACK.OK].Bool;
            }

            public bool OK { get; private set; }
        }

        private event EventHandler<MessageACKEventArgs> MessageACKImpl;

        private readonly MessageACKEventArgs _MessageACKEventArgs = new MessageACKEventArgs();
        
        /// <summary>
        /// MessageBox acknowlegement.
        /// </summary>
        public event EventHandler<MessageACKEventArgs> MessageACK
        {
            add
            {
                if (MessageACKImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.MessageACK, HandleMessageACK);
                }
                MessageACKImpl += value;
            }
            remove
            {
                MessageACKImpl -= value;
                if (MessageACKImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.MessageACK);
                }
            }
        }

        private void HandleMessageACK(VariantMap eventData)
        {
            _MessageACKEventArgs.Set(eventData);
            MessageACKImpl?.Invoke(_urhoObject.Value, _MessageACKEventArgs);
        }

        #endregion

        #region ElementAdded
        // -------------------------------------------- ElementAdded --------------------------------------------

        /// <summary>
        /// A child element has been added to an element. Sent by the UI root element, or element-event-sender if set.
        /// </summary>
        public class ElementAddedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Root = eventData[E.ElementAdded.Root].VoidPtr;
                Parent = eventData[E.ElementAdded.Parent].VoidPtr;
                Element = eventData[E.ElementAdded.Element].VoidPtr;
            }

            public IntPtr Root { get; private set; }

            public IntPtr Parent { get; private set; }

            public IntPtr Element { get; private set; }
        }

        private event EventHandler<ElementAddedEventArgs> ElementAddedImpl;

        private readonly ElementAddedEventArgs _ElementAddedEventArgs = new ElementAddedEventArgs();
        
        /// <summary>
        /// A child element has been added to an element. Sent by the UI root element, or element-event-sender if set.
        /// </summary>
        public event EventHandler<ElementAddedEventArgs> ElementAdded
        {
            add
            {
                if (ElementAddedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ElementAdded, HandleElementAdded);
                }
                ElementAddedImpl += value;
            }
            remove
            {
                ElementAddedImpl -= value;
                if (ElementAddedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ElementAdded);
                }
            }
        }

        private void HandleElementAdded(VariantMap eventData)
        {
            _ElementAddedEventArgs.Set(eventData);
            ElementAddedImpl?.Invoke(_urhoObject.Value, _ElementAddedEventArgs);
        }

        #endregion

        #region ElementRemoved
        // -------------------------------------------- ElementRemoved --------------------------------------------

        /// <summary>
        /// A child element is about to be removed from an element. Sent by the UI root element, or element-event-sender if set.
        /// </summary>
        public class ElementRemovedEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Root = eventData[E.ElementRemoved.Root].VoidPtr;
                Parent = eventData[E.ElementRemoved.Parent].VoidPtr;
                Element = eventData[E.ElementRemoved.Element].VoidPtr;
            }

            public IntPtr Root { get; private set; }

            public IntPtr Parent { get; private set; }

            public IntPtr Element { get; private set; }
        }

        private event EventHandler<ElementRemovedEventArgs> ElementRemovedImpl;

        private readonly ElementRemovedEventArgs _ElementRemovedEventArgs = new ElementRemovedEventArgs();
        
        /// <summary>
        /// A child element is about to be removed from an element. Sent by the UI root element, or element-event-sender if set.
        /// </summary>
        public event EventHandler<ElementRemovedEventArgs> ElementRemoved
        {
            add
            {
                if (ElementRemovedImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ElementRemoved, HandleElementRemoved);
                }
                ElementRemovedImpl += value;
            }
            remove
            {
                ElementRemovedImpl -= value;
                if (ElementRemovedImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ElementRemoved);
                }
            }
        }

        private void HandleElementRemoved(VariantMap eventData)
        {
            _ElementRemovedEventArgs.Set(eventData);
            ElementRemovedImpl?.Invoke(_urhoObject.Value, _ElementRemovedEventArgs);
        }

        #endregion

        #region HoverBegin
        // -------------------------------------------- HoverBegin --------------------------------------------

        /// <summary>
        /// Hovering on an UI element has started.
        /// </summary>
        public class HoverBeginEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.HoverBegin.Element].VoidPtr;
                X = eventData[E.HoverBegin.X].Int;
                Y = eventData[E.HoverBegin.Y].Int;
                ElementX = eventData[E.HoverBegin.ElementX].Int;
                ElementY = eventData[E.HoverBegin.ElementY].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int ElementX { get; private set; }

            public int ElementY { get; private set; }
        }

        private event EventHandler<HoverBeginEventArgs> HoverBeginImpl;

        private readonly HoverBeginEventArgs _HoverBeginEventArgs = new HoverBeginEventArgs();
        
        /// <summary>
        /// Hovering on an UI element has started.
        /// </summary>
        public event EventHandler<HoverBeginEventArgs> HoverBegin
        {
            add
            {
                if (HoverBeginImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.HoverBegin, HandleHoverBegin);
                }
                HoverBeginImpl += value;
            }
            remove
            {
                HoverBeginImpl -= value;
                if (HoverBeginImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.HoverBegin);
                }
            }
        }

        private void HandleHoverBegin(VariantMap eventData)
        {
            _HoverBeginEventArgs.Set(eventData);
            HoverBeginImpl?.Invoke(_urhoObject.Value, _HoverBeginEventArgs);
        }

        #endregion

        #region HoverEnd
        // -------------------------------------------- HoverEnd --------------------------------------------

        /// <summary>
        /// Hovering on an UI element has ended.
        /// </summary>
        public class HoverEndEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.HoverEnd.Element].VoidPtr;
            }

            public IntPtr Element { get; private set; }
        }

        private event EventHandler<HoverEndEventArgs> HoverEndImpl;

        private readonly HoverEndEventArgs _HoverEndEventArgs = new HoverEndEventArgs();
        
        /// <summary>
        /// Hovering on an UI element has ended.
        /// </summary>
        public event EventHandler<HoverEndEventArgs> HoverEnd
        {
            add
            {
                if (HoverEndImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.HoverEnd, HandleHoverEnd);
                }
                HoverEndImpl += value;
            }
            remove
            {
                HoverEndImpl -= value;
                if (HoverEndImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.HoverEnd);
                }
            }
        }

        private void HandleHoverEnd(VariantMap eventData)
        {
            _HoverEndEventArgs.Set(eventData);
            HoverEndImpl?.Invoke(_urhoObject.Value, _HoverEndEventArgs);
        }

        #endregion

        #region DragBegin
        // -------------------------------------------- DragBegin --------------------------------------------

        /// <summary>
        /// Drag behavior of a UI Element has started.
        /// </summary>
        public class DragBeginEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.DragBegin.Element].VoidPtr;
                X = eventData[E.DragBegin.X].Int;
                Y = eventData[E.DragBegin.Y].Int;
                ElementX = eventData[E.DragBegin.ElementX].Int;
                ElementY = eventData[E.DragBegin.ElementY].Int;
                Buttons = eventData[E.DragBegin.Buttons].Int;
                NumButtons = eventData[E.DragBegin.NumButtons].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int ElementX { get; private set; }

            public int ElementY { get; private set; }

            public int Buttons { get; private set; }

            public int NumButtons { get; private set; }
        }

        private event EventHandler<DragBeginEventArgs> DragBeginImpl;

        private readonly DragBeginEventArgs _DragBeginEventArgs = new DragBeginEventArgs();
        
        /// <summary>
        /// Drag behavior of a UI Element has started.
        /// </summary>
        public event EventHandler<DragBeginEventArgs> DragBegin
        {
            add
            {
                if (DragBeginImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.DragBegin, HandleDragBegin);
                }
                DragBeginImpl += value;
            }
            remove
            {
                DragBeginImpl -= value;
                if (DragBeginImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.DragBegin);
                }
            }
        }

        private void HandleDragBegin(VariantMap eventData)
        {
            _DragBeginEventArgs.Set(eventData);
            DragBeginImpl?.Invoke(_urhoObject.Value, _DragBeginEventArgs);
        }

        #endregion

        #region DragMove
        // -------------------------------------------- DragMove --------------------------------------------

        /// <summary>
        /// Drag behavior of a UI Element when the input device has moved.
        /// </summary>
        public class DragMoveEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.DragMove.Element].VoidPtr;
                X = eventData[E.DragMove.X].Int;
                Y = eventData[E.DragMove.Y].Int;
                DX = eventData[E.DragMove.DX].Int;
                DY = eventData[E.DragMove.DY].Int;
                ElementX = eventData[E.DragMove.ElementX].Int;
                ElementY = eventData[E.DragMove.ElementY].Int;
                Buttons = eventData[E.DragMove.Buttons].Int;
                NumButtons = eventData[E.DragMove.NumButtons].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int DX { get; private set; }

            public int DY { get; private set; }

            public int ElementX { get; private set; }

            public int ElementY { get; private set; }

            public int Buttons { get; private set; }

            public int NumButtons { get; private set; }
        }

        private event EventHandler<DragMoveEventArgs> DragMoveImpl;

        private readonly DragMoveEventArgs _DragMoveEventArgs = new DragMoveEventArgs();
        
        /// <summary>
        /// Drag behavior of a UI Element when the input device has moved.
        /// </summary>
        public event EventHandler<DragMoveEventArgs> DragMove
        {
            add
            {
                if (DragMoveImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.DragMove, HandleDragMove);
                }
                DragMoveImpl += value;
            }
            remove
            {
                DragMoveImpl -= value;
                if (DragMoveImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.DragMove);
                }
            }
        }

        private void HandleDragMove(VariantMap eventData)
        {
            _DragMoveEventArgs.Set(eventData);
            DragMoveImpl?.Invoke(_urhoObject.Value, _DragMoveEventArgs);
        }

        #endregion

        #region DragEnd
        // -------------------------------------------- DragEnd --------------------------------------------

        /// <summary>
        /// Drag behavior of a UI Element has finished.
        /// </summary>
        public class DragEndEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.DragEnd.Element].VoidPtr;
                X = eventData[E.DragEnd.X].Int;
                Y = eventData[E.DragEnd.Y].Int;
                ElementX = eventData[E.DragEnd.ElementX].Int;
                ElementY = eventData[E.DragEnd.ElementY].Int;
                Buttons = eventData[E.DragEnd.Buttons].Int;
                NumButtons = eventData[E.DragEnd.NumButtons].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int ElementX { get; private set; }

            public int ElementY { get; private set; }

            public int Buttons { get; private set; }

            public int NumButtons { get; private set; }
        }

        private event EventHandler<DragEndEventArgs> DragEndImpl;

        private readonly DragEndEventArgs _DragEndEventArgs = new DragEndEventArgs();
        
        /// <summary>
        /// Drag behavior of a UI Element has finished.
        /// </summary>
        public event EventHandler<DragEndEventArgs> DragEnd
        {
            add
            {
                if (DragEndImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.DragEnd, HandleDragEnd);
                }
                DragEndImpl += value;
            }
            remove
            {
                DragEndImpl -= value;
                if (DragEndImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.DragEnd);
                }
            }
        }

        private void HandleDragEnd(VariantMap eventData)
        {
            _DragEndEventArgs.Set(eventData);
            DragEndImpl?.Invoke(_urhoObject.Value, _DragEndEventArgs);
        }

        #endregion

        #region DragCancel
        // -------------------------------------------- DragCancel --------------------------------------------

        /// <summary>
        /// Drag of a UI Element was canceled by pressing ESC.
        /// </summary>
        public class DragCancelEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Element = eventData[E.DragCancel.Element].VoidPtr;
                X = eventData[E.DragCancel.X].Int;
                Y = eventData[E.DragCancel.Y].Int;
                ElementX = eventData[E.DragCancel.ElementX].Int;
                ElementY = eventData[E.DragCancel.ElementY].Int;
                Buttons = eventData[E.DragCancel.Buttons].Int;
                NumButtons = eventData[E.DragCancel.NumButtons].Int;
            }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int ElementX { get; private set; }

            public int ElementY { get; private set; }

            public int Buttons { get; private set; }

            public int NumButtons { get; private set; }
        }

        private event EventHandler<DragCancelEventArgs> DragCancelImpl;

        private readonly DragCancelEventArgs _DragCancelEventArgs = new DragCancelEventArgs();
        
        /// <summary>
        /// Drag of a UI Element was canceled by pressing ESC.
        /// </summary>
        public event EventHandler<DragCancelEventArgs> DragCancel
        {
            add
            {
                if (DragCancelImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.DragCancel, HandleDragCancel);
                }
                DragCancelImpl += value;
            }
            remove
            {
                DragCancelImpl -= value;
                if (DragCancelImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.DragCancel);
                }
            }
        }

        private void HandleDragCancel(VariantMap eventData)
        {
            _DragCancelEventArgs.Set(eventData);
            DragCancelImpl?.Invoke(_urhoObject.Value, _DragCancelEventArgs);
        }

        #endregion

        #region UIDropFile
        // -------------------------------------------- UIDropFile --------------------------------------------

        /// <summary>
        /// A file was drag-dropped into the application window. Includes also coordinates and UI element if applicable.
        /// </summary>
        public class UIDropFileEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                FileName = eventData[E.UIDropFile.FileName].String;
                Element = eventData[E.UIDropFile.Element].VoidPtr;
                X = eventData[E.UIDropFile.X].Int;
                Y = eventData[E.UIDropFile.Y].Int;
                ElementX = eventData[E.UIDropFile.ElementX].Int;
                ElementY = eventData[E.UIDropFile.ElementY].Int;
            }

            public String FileName { get; private set; }

            public IntPtr Element { get; private set; }

            public int X { get; private set; }

            public int Y { get; private set; }

            public int ElementX { get; private set; }

            public int ElementY { get; private set; }
        }

        private event EventHandler<UIDropFileEventArgs> UIDropFileImpl;

        private readonly UIDropFileEventArgs _UIDropFileEventArgs = new UIDropFileEventArgs();
        
        /// <summary>
        /// A file was drag-dropped into the application window. Includes also coordinates and UI element if applicable.
        /// </summary>
        public event EventHandler<UIDropFileEventArgs> UIDropFile
        {
            add
            {
                if (UIDropFileImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.UIDropFile, HandleUIDropFile);
                }
                UIDropFileImpl += value;
            }
            remove
            {
                UIDropFileImpl -= value;
                if (UIDropFileImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.UIDropFile);
                }
            }
        }

        private void HandleUIDropFile(VariantMap eventData)
        {
            _UIDropFileEventArgs.Set(eventData);
            UIDropFileImpl?.Invoke(_urhoObject.Value, _UIDropFileEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (UIMouseClickImpl != null) urhoObject.UnsubscribeFromEvent(E.UIMouseClick);
                if (UIMouseClickEndImpl != null) urhoObject.UnsubscribeFromEvent(E.UIMouseClickEnd);
                if (UIMouseDoubleClickImpl != null) urhoObject.UnsubscribeFromEvent(E.UIMouseDoubleClick);
                if (ClickImpl != null) urhoObject.UnsubscribeFromEvent(E.Click);
                if (ClickEndImpl != null) urhoObject.UnsubscribeFromEvent(E.ClickEnd);
                if (DoubleClickImpl != null) urhoObject.UnsubscribeFromEvent(E.DoubleClick);
                if (DragDropTestImpl != null) urhoObject.UnsubscribeFromEvent(E.DragDropTest);
                if (DragDropFinishImpl != null) urhoObject.UnsubscribeFromEvent(E.DragDropFinish);
                if (FocusChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.FocusChanged);
                if (NameChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.NameChanged);
                if (ResizedImpl != null) urhoObject.UnsubscribeFromEvent(E.Resized);
                if (PositionedImpl != null) urhoObject.UnsubscribeFromEvent(E.Positioned);
                if (VisibleChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.VisibleChanged);
                if (FocusedImpl != null) urhoObject.UnsubscribeFromEvent(E.Focused);
                if (DefocusedImpl != null) urhoObject.UnsubscribeFromEvent(E.Defocused);
                if (LayoutUpdatedImpl != null) urhoObject.UnsubscribeFromEvent(E.LayoutUpdated);
                if (PressedImpl != null) urhoObject.UnsubscribeFromEvent(E.Pressed);
                if (ReleasedImpl != null) urhoObject.UnsubscribeFromEvent(E.Released);
                if (ToggledImpl != null) urhoObject.UnsubscribeFromEvent(E.Toggled);
                if (SliderChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.SliderChanged);
                if (SliderPagedImpl != null) urhoObject.UnsubscribeFromEvent(E.SliderPaged);
                if (ProgressBarChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.ProgressBarChanged);
                if (ScrollBarChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.ScrollBarChanged);
                if (ViewChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.ViewChanged);
                if (ModalChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.ModalChanged);
                if (TextEntryImpl != null) urhoObject.UnsubscribeFromEvent(E.TextEntry);
                if (TextChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.TextChanged);
                if (TextFinishedImpl != null) urhoObject.UnsubscribeFromEvent(E.TextFinished);
                if (MenuSelectedImpl != null) urhoObject.UnsubscribeFromEvent(E.MenuSelected);
                if (ItemSelectedImpl != null) urhoObject.UnsubscribeFromEvent(E.ItemSelected);
                if (ItemDeselectedImpl != null) urhoObject.UnsubscribeFromEvent(E.ItemDeselected);
                if (SelectionChangedImpl != null) urhoObject.UnsubscribeFromEvent(E.SelectionChanged);
                if (ItemClickedImpl != null) urhoObject.UnsubscribeFromEvent(E.ItemClicked);
                if (ItemDoubleClickedImpl != null) urhoObject.UnsubscribeFromEvent(E.ItemDoubleClicked);
                if (UnhandledKeyImpl != null) urhoObject.UnsubscribeFromEvent(E.UnhandledKey);
                if (FileSelectedImpl != null) urhoObject.UnsubscribeFromEvent(E.FileSelected);
                if (MessageACKImpl != null) urhoObject.UnsubscribeFromEvent(E.MessageACK);
                if (ElementAddedImpl != null) urhoObject.UnsubscribeFromEvent(E.ElementAdded);
                if (ElementRemovedImpl != null) urhoObject.UnsubscribeFromEvent(E.ElementRemoved);
                if (HoverBeginImpl != null) urhoObject.UnsubscribeFromEvent(E.HoverBegin);
                if (HoverEndImpl != null) urhoObject.UnsubscribeFromEvent(E.HoverEnd);
                if (DragBeginImpl != null) urhoObject.UnsubscribeFromEvent(E.DragBegin);
                if (DragMoveImpl != null) urhoObject.UnsubscribeFromEvent(E.DragMove);
                if (DragEndImpl != null) urhoObject.UnsubscribeFromEvent(E.DragEnd);
                if (DragCancelImpl != null) urhoObject.UnsubscribeFromEvent(E.DragCancel);
                if (UIDropFileImpl != null) urhoObject.UnsubscribeFromEvent(E.UIDropFile);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class PhysicsEvents2DAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public PhysicsEvents2DAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region PhysicsUpdateContact2D
        // -------------------------------------------- PhysicsUpdateContact2D --------------------------------------------

        /// <summary>
        /// Physics update contact. Global event sent by PhysicsWorld2D.
        /// </summary>
        public class PhysicsUpdateContact2DEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                World = eventData[E.PhysicsUpdateContact2D.World].VoidPtr;
                BodyA = eventData[E.PhysicsUpdateContact2D.BodyA].VoidPtr;
                BodyB = eventData[E.PhysicsUpdateContact2D.BodyB].VoidPtr;
                NodeA = eventData[E.PhysicsUpdateContact2D.NodeA].VoidPtr;
                NodeB = eventData[E.PhysicsUpdateContact2D.NodeB].VoidPtr;
                ShapeA = eventData[E.PhysicsUpdateContact2D.ShapeA].VoidPtr;
                ShapeB = eventData[E.PhysicsUpdateContact2D.ShapeB].VoidPtr;
                Enabled = eventData[E.PhysicsUpdateContact2D.Enabled].Bool;
            }

            public IntPtr World { get; private set; }

            public IntPtr BodyA { get; private set; }

            public IntPtr BodyB { get; private set; }

            public IntPtr NodeA { get; private set; }

            public IntPtr NodeB { get; private set; }

            public IntPtr ShapeA { get; private set; }

            public IntPtr ShapeB { get; private set; }

            public bool Enabled { get; private set; }
        }

        private event EventHandler<PhysicsUpdateContact2DEventArgs> PhysicsUpdateContact2DImpl;

        private readonly PhysicsUpdateContact2DEventArgs _PhysicsUpdateContact2DEventArgs = new PhysicsUpdateContact2DEventArgs();
        
        /// <summary>
        /// Physics update contact. Global event sent by PhysicsWorld2D.
        /// </summary>
        public event EventHandler<PhysicsUpdateContact2DEventArgs> PhysicsUpdateContact2D
        {
            add
            {
                if (PhysicsUpdateContact2DImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PhysicsUpdateContact2D, HandlePhysicsUpdateContact2D);
                }
                PhysicsUpdateContact2DImpl += value;
            }
            remove
            {
                PhysicsUpdateContact2DImpl -= value;
                if (PhysicsUpdateContact2DImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PhysicsUpdateContact2D);
                }
            }
        }

        private void HandlePhysicsUpdateContact2D(VariantMap eventData)
        {
            _PhysicsUpdateContact2DEventArgs.Set(eventData);
            PhysicsUpdateContact2DImpl?.Invoke(_urhoObject.Value, _PhysicsUpdateContact2DEventArgs);
        }

        #endregion

        #region PhysicsBeginContact2D
        // -------------------------------------------- PhysicsBeginContact2D --------------------------------------------

        /// <summary>
        /// Physics begin contact. Global event sent by PhysicsWorld2D.
        /// </summary>
        public class PhysicsBeginContact2DEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                World = eventData[E.PhysicsBeginContact2D.World].VoidPtr;
                BodyA = eventData[E.PhysicsBeginContact2D.BodyA].VoidPtr;
                BodyB = eventData[E.PhysicsBeginContact2D.BodyB].VoidPtr;
                NodeA = eventData[E.PhysicsBeginContact2D.NodeA].VoidPtr;
                NodeB = eventData[E.PhysicsBeginContact2D.NodeB].VoidPtr;
                ShapeA = eventData[E.PhysicsBeginContact2D.ShapeA].VoidPtr;
                ShapeB = eventData[E.PhysicsBeginContact2D.ShapeB].VoidPtr;
            }

            public IntPtr World { get; private set; }

            public IntPtr BodyA { get; private set; }

            public IntPtr BodyB { get; private set; }

            public IntPtr NodeA { get; private set; }

            public IntPtr NodeB { get; private set; }

            public IntPtr ShapeA { get; private set; }

            public IntPtr ShapeB { get; private set; }
        }

        private event EventHandler<PhysicsBeginContact2DEventArgs> PhysicsBeginContact2DImpl;

        private readonly PhysicsBeginContact2DEventArgs _PhysicsBeginContact2DEventArgs = new PhysicsBeginContact2DEventArgs();
        
        /// <summary>
        /// Physics begin contact. Global event sent by PhysicsWorld2D.
        /// </summary>
        public event EventHandler<PhysicsBeginContact2DEventArgs> PhysicsBeginContact2D
        {
            add
            {
                if (PhysicsBeginContact2DImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PhysicsBeginContact2D, HandlePhysicsBeginContact2D);
                }
                PhysicsBeginContact2DImpl += value;
            }
            remove
            {
                PhysicsBeginContact2DImpl -= value;
                if (PhysicsBeginContact2DImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PhysicsBeginContact2D);
                }
            }
        }

        private void HandlePhysicsBeginContact2D(VariantMap eventData)
        {
            _PhysicsBeginContact2DEventArgs.Set(eventData);
            PhysicsBeginContact2DImpl?.Invoke(_urhoObject.Value, _PhysicsBeginContact2DEventArgs);
        }

        #endregion

        #region PhysicsEndContact2D
        // -------------------------------------------- PhysicsEndContact2D --------------------------------------------

        /// <summary>
        /// Physics end contact. Global event sent by PhysicsWorld2D.
        /// </summary>
        public class PhysicsEndContact2DEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                World = eventData[E.PhysicsEndContact2D.World].VoidPtr;
                BodyA = eventData[E.PhysicsEndContact2D.BodyA].VoidPtr;
                BodyB = eventData[E.PhysicsEndContact2D.BodyB].VoidPtr;
                NodeA = eventData[E.PhysicsEndContact2D.NodeA].VoidPtr;
                NodeB = eventData[E.PhysicsEndContact2D.NodeB].VoidPtr;
                ShapeA = eventData[E.PhysicsEndContact2D.ShapeA].VoidPtr;
                ShapeB = eventData[E.PhysicsEndContact2D.ShapeB].VoidPtr;
            }

            public IntPtr World { get; private set; }

            public IntPtr BodyA { get; private set; }

            public IntPtr BodyB { get; private set; }

            public IntPtr NodeA { get; private set; }

            public IntPtr NodeB { get; private set; }

            public IntPtr ShapeA { get; private set; }

            public IntPtr ShapeB { get; private set; }
        }

        private event EventHandler<PhysicsEndContact2DEventArgs> PhysicsEndContact2DImpl;

        private readonly PhysicsEndContact2DEventArgs _PhysicsEndContact2DEventArgs = new PhysicsEndContact2DEventArgs();
        
        /// <summary>
        /// Physics end contact. Global event sent by PhysicsWorld2D.
        /// </summary>
        public event EventHandler<PhysicsEndContact2DEventArgs> PhysicsEndContact2D
        {
            add
            {
                if (PhysicsEndContact2DImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.PhysicsEndContact2D, HandlePhysicsEndContact2D);
                }
                PhysicsEndContact2DImpl += value;
            }
            remove
            {
                PhysicsEndContact2DImpl -= value;
                if (PhysicsEndContact2DImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.PhysicsEndContact2D);
                }
            }
        }

        private void HandlePhysicsEndContact2D(VariantMap eventData)
        {
            _PhysicsEndContact2DEventArgs.Set(eventData);
            PhysicsEndContact2DImpl?.Invoke(_urhoObject.Value, _PhysicsEndContact2DEventArgs);
        }

        #endregion

        #region NodeUpdateContact2D
        // -------------------------------------------- NodeUpdateContact2D --------------------------------------------

        /// <summary>
        /// Node update contact. Sent by scene nodes participating in a collision.
        /// </summary>
        public class NodeUpdateContact2DEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Body = eventData[E.NodeUpdateContact2D.Body].VoidPtr;
                OtherNode = eventData[E.NodeUpdateContact2D.OtherNode].VoidPtr;
                OtherBody = eventData[E.NodeUpdateContact2D.OtherBody].VoidPtr;
                Shape = eventData[E.NodeUpdateContact2D.Shape].VoidPtr;
                OtherShape = eventData[E.NodeUpdateContact2D.OtherShape].VoidPtr;
                Enabled = eventData[E.NodeUpdateContact2D.Enabled].Bool;
            }

            public IntPtr Body { get; private set; }

            public IntPtr OtherNode { get; private set; }

            public IntPtr OtherBody { get; private set; }

            public IntPtr Shape { get; private set; }

            public IntPtr OtherShape { get; private set; }

            public bool Enabled { get; private set; }
        }

        private event EventHandler<NodeUpdateContact2DEventArgs> NodeUpdateContact2DImpl;

        private readonly NodeUpdateContact2DEventArgs _NodeUpdateContact2DEventArgs = new NodeUpdateContact2DEventArgs();
        
        /// <summary>
        /// Node update contact. Sent by scene nodes participating in a collision.
        /// </summary>
        public event EventHandler<NodeUpdateContact2DEventArgs> NodeUpdateContact2D
        {
            add
            {
                if (NodeUpdateContact2DImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeUpdateContact2D, HandleNodeUpdateContact2D);
                }
                NodeUpdateContact2DImpl += value;
            }
            remove
            {
                NodeUpdateContact2DImpl -= value;
                if (NodeUpdateContact2DImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeUpdateContact2D);
                }
            }
        }

        private void HandleNodeUpdateContact2D(VariantMap eventData)
        {
            _NodeUpdateContact2DEventArgs.Set(eventData);
            NodeUpdateContact2DImpl?.Invoke(_urhoObject.Value, _NodeUpdateContact2DEventArgs);
        }

        #endregion

        #region NodeBeginContact2D
        // -------------------------------------------- NodeBeginContact2D --------------------------------------------

        /// <summary>
        /// Node begin contact. Sent by scene nodes participating in a collision.
        /// </summary>
        public class NodeBeginContact2DEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Body = eventData[E.NodeBeginContact2D.Body].VoidPtr;
                OtherNode = eventData[E.NodeBeginContact2D.OtherNode].VoidPtr;
                OtherBody = eventData[E.NodeBeginContact2D.OtherBody].VoidPtr;
                Shape = eventData[E.NodeBeginContact2D.Shape].VoidPtr;
                OtherShape = eventData[E.NodeBeginContact2D.OtherShape].VoidPtr;
            }

            public IntPtr Body { get; private set; }

            public IntPtr OtherNode { get; private set; }

            public IntPtr OtherBody { get; private set; }

            public IntPtr Shape { get; private set; }

            public IntPtr OtherShape { get; private set; }
        }

        private event EventHandler<NodeBeginContact2DEventArgs> NodeBeginContact2DImpl;

        private readonly NodeBeginContact2DEventArgs _NodeBeginContact2DEventArgs = new NodeBeginContact2DEventArgs();
        
        /// <summary>
        /// Node begin contact. Sent by scene nodes participating in a collision.
        /// </summary>
        public event EventHandler<NodeBeginContact2DEventArgs> NodeBeginContact2D
        {
            add
            {
                if (NodeBeginContact2DImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeBeginContact2D, HandleNodeBeginContact2D);
                }
                NodeBeginContact2DImpl += value;
            }
            remove
            {
                NodeBeginContact2DImpl -= value;
                if (NodeBeginContact2DImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeBeginContact2D);
                }
            }
        }

        private void HandleNodeBeginContact2D(VariantMap eventData)
        {
            _NodeBeginContact2DEventArgs.Set(eventData);
            NodeBeginContact2DImpl?.Invoke(_urhoObject.Value, _NodeBeginContact2DEventArgs);
        }

        #endregion

        #region NodeEndContact2D
        // -------------------------------------------- NodeEndContact2D --------------------------------------------

        /// <summary>
        /// Node end contact. Sent by scene nodes participating in a collision.
        /// </summary>
        public class NodeEndContact2DEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Body = eventData[E.NodeEndContact2D.Body].VoidPtr;
                OtherNode = eventData[E.NodeEndContact2D.OtherNode].VoidPtr;
                OtherBody = eventData[E.NodeEndContact2D.OtherBody].VoidPtr;
                Shape = eventData[E.NodeEndContact2D.Shape].VoidPtr;
                OtherShape = eventData[E.NodeEndContact2D.OtherShape].VoidPtr;
            }

            public IntPtr Body { get; private set; }

            public IntPtr OtherNode { get; private set; }

            public IntPtr OtherBody { get; private set; }

            public IntPtr Shape { get; private set; }

            public IntPtr OtherShape { get; private set; }
        }

        private event EventHandler<NodeEndContact2DEventArgs> NodeEndContact2DImpl;

        private readonly NodeEndContact2DEventArgs _NodeEndContact2DEventArgs = new NodeEndContact2DEventArgs();
        
        /// <summary>
        /// Node end contact. Sent by scene nodes participating in a collision.
        /// </summary>
        public event EventHandler<NodeEndContact2DEventArgs> NodeEndContact2D
        {
            add
            {
                if (NodeEndContact2DImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.NodeEndContact2D, HandleNodeEndContact2D);
                }
                NodeEndContact2DImpl += value;
            }
            remove
            {
                NodeEndContact2DImpl -= value;
                if (NodeEndContact2DImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.NodeEndContact2D);
                }
            }
        }

        private void HandleNodeEndContact2D(VariantMap eventData)
        {
            _NodeEndContact2DEventArgs.Set(eventData);
            NodeEndContact2DImpl?.Invoke(_urhoObject.Value, _NodeEndContact2DEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (PhysicsUpdateContact2DImpl != null) urhoObject.UnsubscribeFromEvent(E.PhysicsUpdateContact2D);
                if (PhysicsBeginContact2DImpl != null) urhoObject.UnsubscribeFromEvent(E.PhysicsBeginContact2D);
                if (PhysicsEndContact2DImpl != null) urhoObject.UnsubscribeFromEvent(E.PhysicsEndContact2D);
                if (NodeUpdateContact2DImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeUpdateContact2D);
                if (NodeBeginContact2DImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeBeginContact2D);
                if (NodeEndContact2DImpl != null) urhoObject.UnsubscribeFromEvent(E.NodeEndContact2D);
            }
            _urhoObject.Dispose();
        }
    }
    public partial class Urho2DEventsAdapter: IDisposable
    {
        private readonly SharedPtr<Object> _urhoObject;

        public Urho2DEventsAdapter(Urho3DNet.Object urhoObject)
        {
            _urhoObject = urhoObject ?? throw new ArgumentNullException(nameof(urhoObject));
        }

        #region ParticlesEnd
        // -------------------------------------------- ParticlesEnd --------------------------------------------

        /// <summary>
        /// Emitting ParticleEmitter2D particles stopped.
        /// </summary>
        public class ParticlesEndEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.ParticlesEnd.Node].VoidPtr;
                Effect = eventData[E.ParticlesEnd.Effect].VoidPtr;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Effect { get; private set; }
        }

        private event EventHandler<ParticlesEndEventArgs> ParticlesEndImpl;

        private readonly ParticlesEndEventArgs _ParticlesEndEventArgs = new ParticlesEndEventArgs();
        
        /// <summary>
        /// Emitting ParticleEmitter2D particles stopped.
        /// </summary>
        public event EventHandler<ParticlesEndEventArgs> ParticlesEnd
        {
            add
            {
                if (ParticlesEndImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ParticlesEnd, HandleParticlesEnd);
                }
                ParticlesEndImpl += value;
            }
            remove
            {
                ParticlesEndImpl -= value;
                if (ParticlesEndImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ParticlesEnd);
                }
            }
        }

        private void HandleParticlesEnd(VariantMap eventData)
        {
            _ParticlesEndEventArgs.Set(eventData);
            ParticlesEndImpl?.Invoke(_urhoObject.Value, _ParticlesEndEventArgs);
        }

        #endregion

        #region ParticlesDuration
        // -------------------------------------------- ParticlesDuration --------------------------------------------

        /// <summary>
        /// All ParticleEmitter2D particles have been removed.
        /// </summary>
        public class ParticlesDurationEventArgs : EventArgs
        {
            public void Set(VariantMap eventData)
            {
                Node = eventData[E.ParticlesDuration.Node].VoidPtr;
                Effect = eventData[E.ParticlesDuration.Effect].VoidPtr;
            }

            public IntPtr Node { get; private set; }

            public IntPtr Effect { get; private set; }
        }

        private event EventHandler<ParticlesDurationEventArgs> ParticlesDurationImpl;

        private readonly ParticlesDurationEventArgs _ParticlesDurationEventArgs = new ParticlesDurationEventArgs();
        
        /// <summary>
        /// All ParticleEmitter2D particles have been removed.
        /// </summary>
        public event EventHandler<ParticlesDurationEventArgs> ParticlesDuration
        {
            add
            {
                if (ParticlesDurationImpl == null)
                {
                    _urhoObject.Value?.SubscribeToEvent(E.ParticlesDuration, HandleParticlesDuration);
                }
                ParticlesDurationImpl += value;
            }
            remove
            {
                ParticlesDurationImpl -= value;
                if (ParticlesDurationImpl == null)
                {
                    _urhoObject.Value?.UnsubscribeFromEvent(E.ParticlesDuration);
                }
            }
        }

        private void HandleParticlesDuration(VariantMap eventData)
        {
            _ParticlesDurationEventArgs.Set(eventData);
            ParticlesDurationImpl?.Invoke(_urhoObject.Value, _ParticlesDurationEventArgs);
        }

        #endregion
        public void Dispose()
        {
            var urhoObject = _urhoObject.Value;
            if (urhoObject != null)
            {
                if (ParticlesEndImpl != null) urhoObject.UnsubscribeFromEvent(E.ParticlesEnd);
                if (ParticlesDurationImpl != null) urhoObject.UnsubscribeFromEvent(E.ParticlesDuration);
            }
            _urhoObject.Dispose();
        }
    }
}