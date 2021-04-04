namespace Urho3DNet.Actions
{
    public enum ActionTag
    {
        //! Default tag
        Invalid = -1
    }

    public abstract class BaseAction
    {
        #region Constructors

        protected BaseAction()
        {
            Tag = (int) ActionTag.Invalid;
        }

        #endregion Constructor

        public int Tag { get; set; }

        protected internal virtual ActionState StartAction(Object target)
        {
            return null;
        }
    }

    public abstract class ActionState
    {
        protected ActionState(BaseAction action, Object target)
        {
            Action = action;
            Target = target;
            OriginalTarget = target;
        }

        /// <summary>
        ///     Called once per frame.
        /// </summary>
        /// <param name="time">
        ///     A value between 0 and 1
        ///     For example:
        ///     0 means that the action just started
        ///     0.5 means that the action is in the middle
        ///     1 means that the action is over
        /// </param>
        public virtual void Update(float time)
        {
#if DEBUG
            //Log.Log ("[Action State update]. override me");
#endif
        }

        /// <summary>
        ///     Called after the action has finished.
        ///     It will set the 'Target' to null.
        ///     IMPORTANT: You should never call this method manually. Instead, use: "target.StopAction(actionState);"
        /// </summary>
        protected internal virtual void Stop()
        {
            Target = null;
        }

        /// <summary>
        ///     Called every frame with it's delta time.
        ///     DON'T override unless you know what you are doing.
        /// </summary>
        /// <param name="dt">Delta Time</param>
        protected internal virtual void Step(float dt)
        {
#if DEBUG
            //Log.Log ("[Action State step]. override me");
#endif
        }

        /// <summary>
        ///     Gets or sets the target.
        ///     Will be set with the 'StartAction' method of the corresponding Action.
        ///     When the 'Stop' method is called, Target will be set to null.
        /// </summary>
        /// <value>The target.</value>

        #region Properties

        public Object Target { get; protected set; }

        public Object OriginalTarget { get; protected set; }
        public BaseAction Action { get; protected set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is done.
        /// </summary>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        public virtual bool IsDone => true;

        #endregion Properties
    }
}