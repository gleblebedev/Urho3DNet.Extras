using Urho3DNet.MVVM.VisualTree;

namespace Urho3DNet.MVVM.Binding
{
    public class StyledElement : Urho3DNet.MVVM.Animation.Animatable, IDataContextProvider, IStyledElement
    {
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

        private ITemplatedControl? _templatedParent;

        private static void DataContextNotifying(IUrhoObject o, bool updateStarted)
        {
            if (o is StyledElement element)
            {
                DataContextNotifying(element, updateStarted);
            }
        }

        public StyledElement(Object target) : base(target)
        {
        }
    }
}