using System;
using System.Collections;
using System.Collections.Specialized;
using Urho3DNet.MVVM.Collections;
using Urho3DNet.MVVM.Controls;
using Urho3DNet.MVVM.LogicalTree;
using Urho3DNet.MVVM.VisualTree;

#nullable enable

namespace Urho3DNet.MVVM.Binding
{
    public class StyledElement : Urho3DNet.MVVM.Animation.Animatable, IDataContextProvider, IStyledElement
    {
        private bool _dataContextUpdating;
        private IUrhoList<ILogical> _logicalChildren;

        /// <summary>
        /// Raised when the styled element is attached to a rooted logical tree.
        /// </summary>
        public event EventHandler<LogicalTreeAttachmentEventArgs>? AttachedToLogicalTree;

        /// <summary>
        /// Raised when the styled element is detached from a rooted logical tree.
        /// </summary>
        public event EventHandler<LogicalTreeAttachmentEventArgs>? DetachedFromLogicalTree;

        /// <summary>
        /// Defines the <see cref="DataContext"/> property.
        /// </summary>
        public static readonly StyledProperty<object?> DataContextProperty =
            UrhoProperty.Register<StyledElement, object?>(
                nameof(DataContext),
                inherits: true,
                notifying: DataContextNotifying);

        /// <summary>
        /// Defines the <see cref="TemplatedParent"/> property.
        /// </summary>
        public static readonly DirectProperty<StyledElement, ITemplatedControl?> TemplatedParentProperty =
            UrhoProperty.RegisterDirect<StyledElement, ITemplatedControl?>(
                nameof(TemplatedParent),
                o => o.TemplatedParent,
                (o, v) => o.TemplatedParent = v);

        /// <summary>
        /// Gets the styled element's logical parent.
        /// </summary>
        ILogical? ILogical.LogicalParent => Parent;

        /// <summary>
        /// Gets the styled element's logical parent.
        /// </summary>
        public IStyledElement? Parent { get; private set; }

        /// <summary>
        /// Gets or sets the control's data context.
        /// </summary>
        /// <remarks>
        /// The data context is an inherited property that specifies the default object that will
        /// be used for data binding.
        /// </remarks>
        public object? DataContext
        {
            get { return GetValue(DataContextProperty); }
            set { SetValue(DataContextProperty, value); }
        }

        /// <summary>
        /// Gets the styled element whose lookless template this styled element is part of.
        /// </summary>
        public ITemplatedControl? TemplatedParent
        {
            get => _templatedParent;
            internal set => SetAndRaise(TemplatedParentProperty, ref _templatedParent, value);
        }

        /// <summary>
        /// Called when the <see cref="DataContext"/> begins updating.
        /// </summary>
        protected virtual void OnDataContextBeginUpdate()
        {
        }

        /// <summary>
        /// Called when the <see cref="DataContext"/> finishes updating.
        /// </summary>
        protected virtual void OnDataContextEndUpdate()
        {
        }

        /// <summary>
        /// Gets the styled element's logical children.
        /// </summary>
        protected IUrhoList<ILogical> LogicalChildren
        {
            get
            {
                if (_logicalChildren == null)
                {
                    var list = new UrhoList<ILogical>
                    {
                        ResetBehavior = ResetBehavior.Remove,
                        Validate = logical => ValidateLogicalChild(logical)
                    };
                    list.CollectionChanged += LogicalChildrenCollectionChanged;
                    _logicalChildren = list;
                }

                return _logicalChildren;
            }
        }

        protected virtual void LogicalChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SetLogicalParent(e.NewItems);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    ClearLogicalParent(e.OldItems);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    ClearLogicalParent(e.OldItems);
                    SetLogicalParent(e.NewItems);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    throw new NotSupportedException("Reset should not be signaled on LogicalChildren collection");
            }
        }
        private void SetLogicalParent(IList children)
        {
            var count = children.Count;

            for (var i = 0; i < count; i++)
            {
                var logical = (ILogical)children[i];

                if (logical.LogicalParent is null)
                {
                    ((ISetLogicalParent)logical).SetParent(this);
                }
            }
        }

        private void ClearLogicalParent(IList children)
        {
            var count = children.Count;

            for (var i = 0; i < count; i++)
            {
                var logical = (ILogical)children[i];

                if (logical.LogicalParent == this)
                {
                    ((ISetLogicalParent)logical).SetParent(null);
                }
            }
        }
        private static void ValidateLogicalChild(ILogical c)
        {
            if (c == null)
            {
                throw new ArgumentException("Cannot add null to LogicalChildren.");
            }
        }

        /// <summary>
        /// Gets the styled element's logical children.
        /// </summary>
        IUrhoReadOnlyList<ILogical> ILogical.LogicalChildren => LogicalChildren;

        private ITemplatedControl? _templatedParent;

        private static void DataContextNotifying(IUrhoObject o, bool updateStarted)
        {
            if (o is StyledElement element)
            {
                DataContextNotifying(element, updateStarted);
            }
        }

        private static void DataContextNotifying(StyledElement element, bool updateStarted)
        {
            if (updateStarted)
            {
                if (!element._dataContextUpdating)
                {
                    element._dataContextUpdating = true;
                    element.OnDataContextBeginUpdate();

                    var logicalChildren = element.LogicalChildren;
                    var logicalChildrenCount = logicalChildren.Count;

                    for (var i = 0; i < logicalChildrenCount; i++)
                    {
                        if (element.LogicalChildren[i] is StyledElement s &&
                            s.InheritanceParent == element &&
                            !s.IsSet(DataContextProperty))
                        {
                            DataContextNotifying(s, updateStarted);
                        }
                    }
                }
            }
            else
            {
                if (element._dataContextUpdating)
                {
                    element.OnDataContextEndUpdate();
                    element._dataContextUpdating = false;
                }
            }
        }
        public StyledElement(Object target) : base(target)
        {
        }
    }
}