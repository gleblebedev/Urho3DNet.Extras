using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Layout;

namespace Urho3DNet.MVVM
{
    public partial class UIElementView : Layoutable
    {
        private UIElement _target;

        public UIElementView(UIElement target): base(target)
        {
            _target = target;
            _lastKnownName = target.Name;
            _lastKnownPosition = target.Position;
            _lastKnownSize = target.Size;
            _lastKnownWidth = target.Width;
            _lastKnownHeight = target.Height;
            _lastKnownMinSize = target.MinSize;
            _lastKnownMinWidth = target.MinWidth;
            _lastKnownMinHeight = target.MinHeight;
            _lastKnownMaxSize = target.MaxSize;
            _lastKnownMaxWidth = target.MaxWidth;
            _lastKnownMaxHeight = target.MaxHeight;
            _lastKnownChildOffset = target.ChildOffset;
            _lastKnownEnableAnchor = target.EnableAnchor;
            _lastKnownMinAnchor = target.MinAnchor;
            _lastKnownMaxAnchor = target.MaxAnchor;
            _lastKnownMinOffset = target.MinOffset;
            _lastKnownMaxOffset = target.MaxOffset;
            _lastKnownPivot = target.Pivot;
            _lastKnownClipBorder = target.ClipBorder;
            _lastKnownPriority = target.Priority;
            _lastKnownOpacity = target.Opacity;
            _lastKnownBringToBack = target.BringToBack;
            _lastKnownClipChildren = target.ClipChildren;
            _lastKnownUseDerivedOpacity = target.UseDerivedOpacity;
            _lastKnownIsEnabled = target.IsEnabled;
            _lastKnownIsEditable = target.IsEditable;
            _lastKnownIsSelected = target.IsSelected;
            _lastKnownIsVisible = target.IsVisible;
            _lastKnownIsHovering = target.IsHovering;
            _lastKnownIsInternal = target.IsInternal;
            _lastKnownFocusMode = target.FocusMode;
            _lastKnownDragDropMode = target.DragDropMode;
            _lastKnownLayoutMode = target.LayoutMode;
            _lastKnownLayoutSpacing = target.LayoutSpacing;
            _lastKnownLayoutBorder = target.LayoutBorder;
            _lastKnownLayoutFlexScale = target.LayoutFlexScale;
            _lastKnownIndent = target.Indent;
            _lastKnownIndentSpacing = target.IndentSpacing;
            _lastKnownTraversalMode = target.TraversalMode;
            _lastKnownIsElementEventSender = target.IsElementEventSender;
            PartialInit(target);
        }

        partial void PartialInit(UIElement target);

        public new UIElement Target => _target;

        #region Property Name

        private string _lastKnownName;

        public static readonly DirectProperty<UIElementView, string> NameProperty =
            UrhoProperty.RegisterDirect<UIElementView, string>(
                nameof(Name),
                o => o.Name,
                (o, v) => o.Name = v);

        public string Name
        {
            get => _target.Name;

            set
            {
                SetAndRaise(NameProperty, _lastKnownName, value, _ =>
                {
                    _lastKnownName = value;
                    _target.Name = value;
                });
            }
        }

        #endregion Property Name
        #region Property Position

        private IntVector2 _lastKnownPosition;

        public static readonly DirectProperty<UIElementView, IntVector2> PositionProperty =
            UrhoProperty.RegisterDirect<UIElementView, IntVector2>(
                nameof(Position),
                o => o.Position,
                (o, v) => o.Position = v);

        public IntVector2 Position
        {
            get => _target.Position;

            set
            {
                SetAndRaise(PositionProperty, _lastKnownPosition, value, _ =>
                {
                    _lastKnownPosition = value;
                    _target.Position = value;
                });
            }
        }

        #endregion Property Position
        #region Property Size

        private IntVector2 _lastKnownSize;

        public static readonly DirectProperty<UIElementView, IntVector2> SizeProperty =
            UrhoProperty.RegisterDirect<UIElementView, IntVector2>(
                nameof(Size),
                o => o.Size,
                (o, v) => o.Size = v);

        public IntVector2 Size
        {
            get => _target.Size;

            set
            {
                SetAndRaise(SizeProperty, _lastKnownSize, value, _ =>
                {
                    _lastKnownSize = value;
                    _target.Size = value;
                });
            }
        }

        #endregion Property Size
        #region Property Width

        private int _lastKnownWidth;

        public static readonly DirectProperty<UIElementView, int> WidthProperty =
            UrhoProperty.RegisterDirect<UIElementView, int>(
                nameof(Width),
                o => o.Width,
                (o, v) => o.Width = v);

        public int Width
        {
            get => _target.Width;

            set
            {
                SetAndRaise(WidthProperty, _lastKnownWidth, value, _ =>
                {
                    _lastKnownWidth = value;
                    _target.Width = value;
                });
            }
        }

        #endregion Property Width
        #region Property Height

        private int _lastKnownHeight;

        public static readonly DirectProperty<UIElementView, int> HeightProperty =
            UrhoProperty.RegisterDirect<UIElementView, int>(
                nameof(Height),
                o => o.Height,
                (o, v) => o.Height = v);

        public int Height
        {
            get => _target.Height;

            set
            {
                SetAndRaise(HeightProperty, _lastKnownHeight, value, _ =>
                {
                    _lastKnownHeight = value;
                    _target.Height = value;
                });
            }
        }

        #endregion Property Height
        #region Property MinSize

        private IntVector2 _lastKnownMinSize;

        public static readonly DirectProperty<UIElementView, IntVector2> MinSizeProperty =
            UrhoProperty.RegisterDirect<UIElementView, IntVector2>(
                nameof(MinSize),
                o => o.MinSize,
                (o, v) => o.MinSize = v);

        public IntVector2 MinSize
        {
            get => _target.MinSize;

            set
            {
                SetAndRaise(MinSizeProperty, _lastKnownMinSize, value, _ =>
                {
                    _lastKnownMinSize = value;
                    _target.MinSize = value;
                });
            }
        }

        #endregion Property MinSize
        #region Property MinWidth

        private int _lastKnownMinWidth;

        public static readonly DirectProperty<UIElementView, int> MinWidthProperty =
            UrhoProperty.RegisterDirect<UIElementView, int>(
                nameof(MinWidth),
                o => o.MinWidth,
                (o, v) => o.MinWidth = v);

        public int MinWidth
        {
            get => _target.MinWidth;

            set
            {
                SetAndRaise(MinWidthProperty, _lastKnownMinWidth, value, _ =>
                {
                    _lastKnownMinWidth = value;
                    _target.MinWidth = value;
                });
            }
        }

        #endregion Property MinWidth
        #region Property MinHeight

        private int _lastKnownMinHeight;

        public static readonly DirectProperty<UIElementView, int> MinHeightProperty =
            UrhoProperty.RegisterDirect<UIElementView, int>(
                nameof(MinHeight),
                o => o.MinHeight,
                (o, v) => o.MinHeight = v);

        public int MinHeight
        {
            get => _target.MinHeight;

            set
            {
                SetAndRaise(MinHeightProperty, _lastKnownMinHeight, value, _ =>
                {
                    _lastKnownMinHeight = value;
                    _target.MinHeight = value;
                });
            }
        }

        #endregion Property MinHeight
        #region Property MaxSize

        private IntVector2 _lastKnownMaxSize;

        public static readonly DirectProperty<UIElementView, IntVector2> MaxSizeProperty =
            UrhoProperty.RegisterDirect<UIElementView, IntVector2>(
                nameof(MaxSize),
                o => o.MaxSize,
                (o, v) => o.MaxSize = v);

        public IntVector2 MaxSize
        {
            get => _target.MaxSize;

            set
            {
                SetAndRaise(MaxSizeProperty, _lastKnownMaxSize, value, _ =>
                {
                    _lastKnownMaxSize = value;
                    _target.MaxSize = value;
                });
            }
        }

        #endregion Property MaxSize
        #region Property MaxWidth

        private int _lastKnownMaxWidth;

        public static readonly DirectProperty<UIElementView, int> MaxWidthProperty =
            UrhoProperty.RegisterDirect<UIElementView, int>(
                nameof(MaxWidth),
                o => o.MaxWidth,
                (o, v) => o.MaxWidth = v);

        public int MaxWidth
        {
            get => _target.MaxWidth;

            set
            {
                SetAndRaise(MaxWidthProperty, _lastKnownMaxWidth, value, _ =>
                {
                    _lastKnownMaxWidth = value;
                    _target.MaxWidth = value;
                });
            }
        }

        #endregion Property MaxWidth
        #region Property MaxHeight

        private int _lastKnownMaxHeight;

        public static readonly DirectProperty<UIElementView, int> MaxHeightProperty =
            UrhoProperty.RegisterDirect<UIElementView, int>(
                nameof(MaxHeight),
                o => o.MaxHeight,
                (o, v) => o.MaxHeight = v);

        public int MaxHeight
        {
            get => _target.MaxHeight;

            set
            {
                SetAndRaise(MaxHeightProperty, _lastKnownMaxHeight, value, _ =>
                {
                    _lastKnownMaxHeight = value;
                    _target.MaxHeight = value;
                });
            }
        }

        #endregion Property MaxHeight
        #region Property ChildOffset

        private IntVector2 _lastKnownChildOffset;

        public static readonly DirectProperty<UIElementView, IntVector2> ChildOffsetProperty =
            UrhoProperty.RegisterDirect<UIElementView, IntVector2>(
                nameof(ChildOffset),
                o => o.ChildOffset,
                (o, v) => o.ChildOffset = v);

        public IntVector2 ChildOffset
        {
            get => _target.ChildOffset;

            set
            {
                SetAndRaise(ChildOffsetProperty, _lastKnownChildOffset, value, _ =>
                {
                    _lastKnownChildOffset = value;
                    _target.ChildOffset = value;
                });
            }
        }

        #endregion Property ChildOffset
        #region Property EnableAnchor

        private bool _lastKnownEnableAnchor;

        public static readonly DirectProperty<UIElementView, bool> EnableAnchorProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(EnableAnchor),
                o => o.EnableAnchor,
                (o, v) => o.EnableAnchor = v);

        public bool EnableAnchor
        {
            get => _target.EnableAnchor;

            set
            {
                SetAndRaise(EnableAnchorProperty, _lastKnownEnableAnchor, value, _ =>
                {
                    _lastKnownEnableAnchor = value;
                    _target.EnableAnchor = value;
                });
            }
        }

        #endregion Property EnableAnchor
        #region Property MinAnchor

        private Vector2 _lastKnownMinAnchor;

        public static readonly DirectProperty<UIElementView, Vector2> MinAnchorProperty =
            UrhoProperty.RegisterDirect<UIElementView, Vector2>(
                nameof(MinAnchor),
                o => o.MinAnchor,
                (o, v) => o.MinAnchor = v);

        public Vector2 MinAnchor
        {
            get => _target.MinAnchor;

            set
            {
                SetAndRaise(MinAnchorProperty, _lastKnownMinAnchor, value, _ =>
                {
                    _lastKnownMinAnchor = value;
                    _target.MinAnchor = value;
                });
            }
        }

        #endregion Property MinAnchor
        #region Property MaxAnchor

        private Vector2 _lastKnownMaxAnchor;

        public static readonly DirectProperty<UIElementView, Vector2> MaxAnchorProperty =
            UrhoProperty.RegisterDirect<UIElementView, Vector2>(
                nameof(MaxAnchor),
                o => o.MaxAnchor,
                (o, v) => o.MaxAnchor = v);

        public Vector2 MaxAnchor
        {
            get => _target.MaxAnchor;

            set
            {
                SetAndRaise(MaxAnchorProperty, _lastKnownMaxAnchor, value, _ =>
                {
                    _lastKnownMaxAnchor = value;
                    _target.MaxAnchor = value;
                });
            }
        }

        #endregion Property MaxAnchor
        #region Property MinOffset

        private IntVector2 _lastKnownMinOffset;

        public static readonly DirectProperty<UIElementView, IntVector2> MinOffsetProperty =
            UrhoProperty.RegisterDirect<UIElementView, IntVector2>(
                nameof(MinOffset),
                o => o.MinOffset,
                (o, v) => o.MinOffset = v);

        public IntVector2 MinOffset
        {
            get => _target.MinOffset;

            set
            {
                SetAndRaise(MinOffsetProperty, _lastKnownMinOffset, value, _ =>
                {
                    _lastKnownMinOffset = value;
                    _target.MinOffset = value;
                });
            }
        }

        #endregion Property MinOffset
        #region Property MaxOffset

        private IntVector2 _lastKnownMaxOffset;

        public static readonly DirectProperty<UIElementView, IntVector2> MaxOffsetProperty =
            UrhoProperty.RegisterDirect<UIElementView, IntVector2>(
                nameof(MaxOffset),
                o => o.MaxOffset,
                (o, v) => o.MaxOffset = v);

        public IntVector2 MaxOffset
        {
            get => _target.MaxOffset;

            set
            {
                SetAndRaise(MaxOffsetProperty, _lastKnownMaxOffset, value, _ =>
                {
                    _lastKnownMaxOffset = value;
                    _target.MaxOffset = value;
                });
            }
        }

        #endregion Property MaxOffset
        #region Property Pivot

        private Vector2 _lastKnownPivot;

        public static readonly DirectProperty<UIElementView, Vector2> PivotProperty =
            UrhoProperty.RegisterDirect<UIElementView, Vector2>(
                nameof(Pivot),
                o => o.Pivot,
                (o, v) => o.Pivot = v);

        public Vector2 Pivot
        {
            get => _target.Pivot;

            set
            {
                SetAndRaise(PivotProperty, _lastKnownPivot, value, _ =>
                {
                    _lastKnownPivot = value;
                    _target.Pivot = value;
                });
            }
        }

        #endregion Property Pivot
        #region Property ClipBorder

        private IntRect _lastKnownClipBorder;

        public static readonly DirectProperty<UIElementView, IntRect> ClipBorderProperty =
            UrhoProperty.RegisterDirect<UIElementView, IntRect>(
                nameof(ClipBorder),
                o => o.ClipBorder,
                (o, v) => o.ClipBorder = v);

        public IntRect ClipBorder
        {
            get => _target.ClipBorder;

            set
            {
                SetAndRaise(ClipBorderProperty, _lastKnownClipBorder, value, _ =>
                {
                    _lastKnownClipBorder = value;
                    _target.ClipBorder = value;
                });
            }
        }

        #endregion Property ClipBorder
        #region Property Priority

        private int _lastKnownPriority;

        public static readonly DirectProperty<UIElementView, int> PriorityProperty =
            UrhoProperty.RegisterDirect<UIElementView, int>(
                nameof(Priority),
                o => o.Priority,
                (o, v) => o.Priority = v);

        public int Priority
        {
            get => _target.Priority;

            set
            {
                SetAndRaise(PriorityProperty, _lastKnownPriority, value, _ =>
                {
                    _lastKnownPriority = value;
                    _target.Priority = value;
                });
            }
        }

        #endregion Property Priority
        #region Property Opacity

        private float _lastKnownOpacity;

        public static readonly DirectProperty<UIElementView, float> OpacityProperty =
            UrhoProperty.RegisterDirect<UIElementView, float>(
                nameof(Opacity),
                o => o.Opacity,
                (o, v) => o.Opacity = v);

        public float Opacity
        {
            get => _target.Opacity;

            set
            {
                SetAndRaise(OpacityProperty, _lastKnownOpacity, value, _ =>
                {
                    _lastKnownOpacity = value;
                    _target.Opacity = value;
                });
            }
        }

        #endregion Property Opacity
        #region Property BringToBack

        private bool _lastKnownBringToBack;

        public static readonly DirectProperty<UIElementView, bool> BringToBackProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(BringToBack),
                o => o.BringToBack,
                (o, v) => o.BringToBack = v);

        public bool BringToBack
        {
            get => _target.BringToBack;

            set
            {
                SetAndRaise(BringToBackProperty, _lastKnownBringToBack, value, _ =>
                {
                    _lastKnownBringToBack = value;
                    _target.BringToBack = value;
                });
            }
        }

        #endregion Property BringToBack
        #region Property ClipChildren

        private bool _lastKnownClipChildren;

        public static readonly DirectProperty<UIElementView, bool> ClipChildrenProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(ClipChildren),
                o => o.ClipChildren,
                (o, v) => o.ClipChildren = v);

        public bool ClipChildren
        {
            get => _target.ClipChildren;

            set
            {
                SetAndRaise(ClipChildrenProperty, _lastKnownClipChildren, value, _ =>
                {
                    _lastKnownClipChildren = value;
                    _target.ClipChildren = value;
                });
            }
        }

        #endregion Property ClipChildren
        #region Property UseDerivedOpacity

        private bool _lastKnownUseDerivedOpacity;

        public static readonly DirectProperty<UIElementView, bool> UseDerivedOpacityProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(UseDerivedOpacity),
                o => o.UseDerivedOpacity,
                (o, v) => o.UseDerivedOpacity = v);

        public bool UseDerivedOpacity
        {
            get => _target.UseDerivedOpacity;

            set
            {
                SetAndRaise(UseDerivedOpacityProperty, _lastKnownUseDerivedOpacity, value, _ =>
                {
                    _lastKnownUseDerivedOpacity = value;
                    _target.UseDerivedOpacity = value;
                });
            }
        }

        #endregion Property UseDerivedOpacity
        #region Property IsEnabled

        private bool _lastKnownIsEnabled;

        public static readonly DirectProperty<UIElementView, bool> IsEnabledProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(IsEnabled),
                o => o.IsEnabled,
                (o, v) => o.IsEnabled = v);

        public bool IsEnabled
        {
            get => _target.IsEnabled;

            set
            {
                SetAndRaise(IsEnabledProperty, _lastKnownIsEnabled, value, _ =>
                {
                    _lastKnownIsEnabled = value;
                    _target.IsEnabled = value;
                });
            }
        }

        #endregion Property IsEnabled
        #region Property IsEditable

        private bool _lastKnownIsEditable;

        public static readonly DirectProperty<UIElementView, bool> IsEditableProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(IsEditable),
                o => o.IsEditable,
                (o, v) => o.IsEditable = v);

        public bool IsEditable
        {
            get => _target.IsEditable;

            set
            {
                SetAndRaise(IsEditableProperty, _lastKnownIsEditable, value, _ =>
                {
                    _lastKnownIsEditable = value;
                    _target.IsEditable = value;
                });
            }
        }

        #endregion Property IsEditable
        #region Property IsSelected

        private bool _lastKnownIsSelected;

        public static readonly DirectProperty<UIElementView, bool> IsSelectedProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(IsSelected),
                o => o.IsSelected,
                (o, v) => o.IsSelected = v);

        public bool IsSelected
        {
            get => _target.IsSelected;

            set
            {
                SetAndRaise(IsSelectedProperty, _lastKnownIsSelected, value, _ =>
                {
                    _lastKnownIsSelected = value;
                    _target.IsSelected = value;
                });
            }
        }

        #endregion Property IsSelected
        #region Property IsVisible

        private bool _lastKnownIsVisible;

        public static readonly DirectProperty<UIElementView, bool> IsVisibleProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(IsVisible),
                o => o.IsVisible,
                (o, v) => o.IsVisible = v);

        public override bool IsVisible
        {
            get => _target.IsVisible;

            set
            {
                SetAndRaise(IsVisibleProperty, _lastKnownIsVisible, value, _ =>
                {
                    _lastKnownIsVisible = value;
                    _target.IsVisible = value;
                });
            }
        }

        #endregion Property IsVisible
        #region Property IsHovering

        private bool _lastKnownIsHovering;

        public static readonly DirectProperty<UIElementView, bool> IsHoveringProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(IsHovering),
                o => o.IsHovering,
                (o, v) => o.IsHovering = v);

        public bool IsHovering
        {
            get => _target.IsHovering;

            set
            {
                SetAndRaise(IsHoveringProperty, _lastKnownIsHovering, value, _ =>
                {
                    _lastKnownIsHovering = value;
                    _target.IsHovering = value;
                });
            }
        }

        #endregion Property IsHovering
        #region Property IsInternal

        private bool _lastKnownIsInternal;

        public static readonly DirectProperty<UIElementView, bool> IsInternalProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(IsInternal),
                o => o.IsInternal,
                (o, v) => o.IsInternal = v);

        public bool IsInternal
        {
            get => _target.IsInternal;

            set
            {
                SetAndRaise(IsInternalProperty, _lastKnownIsInternal, value, _ =>
                {
                    _lastKnownIsInternal = value;
                    _target.IsInternal = value;
                });
            }
        }

        #endregion Property IsInternal
        #region Property FocusMode

        private FocusMode _lastKnownFocusMode;

        public static readonly DirectProperty<UIElementView, FocusMode> FocusModeProperty =
            UrhoProperty.RegisterDirect<UIElementView, FocusMode>(
                nameof(FocusMode),
                o => o.FocusMode,
                (o, v) => o.FocusMode = v);

        public FocusMode FocusMode
        {
            get => _target.FocusMode;

            set
            {
                SetAndRaise(FocusModeProperty, _lastKnownFocusMode, value, _ =>
                {
                    _lastKnownFocusMode = value;
                    _target.FocusMode = value;
                });
            }
        }

        #endregion Property FocusMode
        #region Property DragDropMode

        private DragAndDropMode _lastKnownDragDropMode;

        public static readonly DirectProperty<UIElementView, DragAndDropMode> DragDropModeProperty =
            UrhoProperty.RegisterDirect<UIElementView, DragAndDropMode>(
                nameof(DragDropMode),
                o => o.DragDropMode,
                (o, v) => o.DragDropMode = v);

        public DragAndDropMode DragDropMode
        {
            get => _target.DragDropMode;

            set
            {
                SetAndRaise(DragDropModeProperty, _lastKnownDragDropMode, value, _ =>
                {
                    _lastKnownDragDropMode = value;
                    _target.DragDropMode = value;
                });
            }
        }

        #endregion Property DragDropMode
        #region Property LayoutMode

        private LayoutMode _lastKnownLayoutMode;

        public static readonly DirectProperty<UIElementView, LayoutMode> LayoutModeProperty =
            UrhoProperty.RegisterDirect<UIElementView, LayoutMode>(
                nameof(LayoutMode),
                o => o.LayoutMode,
                (o, v) => o.LayoutMode = v);

        public LayoutMode LayoutMode
        {
            get => _target.LayoutMode;

            set
            {
                SetAndRaise(LayoutModeProperty, _lastKnownLayoutMode, value, _ =>
                {
                    _lastKnownLayoutMode = value;
                    _target.LayoutMode = value;
                });
            }
        }

        #endregion Property LayoutMode
        #region Property LayoutSpacing

        private int _lastKnownLayoutSpacing;

        public static readonly DirectProperty<UIElementView, int> LayoutSpacingProperty =
            UrhoProperty.RegisterDirect<UIElementView, int>(
                nameof(LayoutSpacing),
                o => o.LayoutSpacing,
                (o, v) => o.LayoutSpacing = v);

        public int LayoutSpacing
        {
            get => _target.LayoutSpacing;

            set
            {
                SetAndRaise(LayoutSpacingProperty, _lastKnownLayoutSpacing, value, _ =>
                {
                    _lastKnownLayoutSpacing = value;
                    _target.LayoutSpacing = value;
                });
            }
        }

        #endregion Property LayoutSpacing
        #region Property LayoutBorder

        private IntRect _lastKnownLayoutBorder;

        public static readonly DirectProperty<UIElementView, IntRect> LayoutBorderProperty =
            UrhoProperty.RegisterDirect<UIElementView, IntRect>(
                nameof(LayoutBorder),
                o => o.LayoutBorder,
                (o, v) => o.LayoutBorder = v);

        public IntRect LayoutBorder
        {
            get => _target.LayoutBorder;

            set
            {
                SetAndRaise(LayoutBorderProperty, _lastKnownLayoutBorder, value, _ =>
                {
                    _lastKnownLayoutBorder = value;
                    _target.LayoutBorder = value;
                });
            }
        }

        #endregion Property LayoutBorder
        #region Property LayoutFlexScale

        private Vector2 _lastKnownLayoutFlexScale;

        public static readonly DirectProperty<UIElementView, Vector2> LayoutFlexScaleProperty =
            UrhoProperty.RegisterDirect<UIElementView, Vector2>(
                nameof(LayoutFlexScale),
                o => o.LayoutFlexScale,
                (o, v) => o.LayoutFlexScale = v);

        public Vector2 LayoutFlexScale
        {
            get => _target.LayoutFlexScale;

            set
            {
                SetAndRaise(LayoutFlexScaleProperty, _lastKnownLayoutFlexScale, value, _ =>
                {
                    _lastKnownLayoutFlexScale = value;
                    _target.LayoutFlexScale = value;
                });
            }
        }

        #endregion Property LayoutFlexScale
        #region Property Indent

        private int _lastKnownIndent;

        public static readonly DirectProperty<UIElementView, int> IndentProperty =
            UrhoProperty.RegisterDirect<UIElementView, int>(
                nameof(Indent),
                o => o.Indent,
                (o, v) => o.Indent = v);

        public int Indent
        {
            get => _target.Indent;

            set
            {
                SetAndRaise(IndentProperty, _lastKnownIndent, value, _ =>
                {
                    _lastKnownIndent = value;
                    _target.Indent = value;
                });
            }
        }

        #endregion Property Indent
        #region Property IndentSpacing

        private int _lastKnownIndentSpacing;

        public static readonly DirectProperty<UIElementView, int> IndentSpacingProperty =
            UrhoProperty.RegisterDirect<UIElementView, int>(
                nameof(IndentSpacing),
                o => o.IndentSpacing,
                (o, v) => o.IndentSpacing = v);

        public int IndentSpacing
        {
            get => _target.IndentSpacing;

            set
            {
                SetAndRaise(IndentSpacingProperty, _lastKnownIndentSpacing, value, _ =>
                {
                    _lastKnownIndentSpacing = value;
                    _target.IndentSpacing = value;
                });
            }
        }

        #endregion Property IndentSpacing
        #region Property TraversalMode

        private TraversalMode _lastKnownTraversalMode;

        public static readonly DirectProperty<UIElementView, TraversalMode> TraversalModeProperty =
            UrhoProperty.RegisterDirect<UIElementView, TraversalMode>(
                nameof(TraversalMode),
                o => o.TraversalMode,
                (o, v) => o.TraversalMode = v);

        public TraversalMode TraversalMode
        {
            get => _target.TraversalMode;

            set
            {
                SetAndRaise(TraversalModeProperty, _lastKnownTraversalMode, value, _ =>
                {
                    _lastKnownTraversalMode = value;
                    _target.TraversalMode = value;
                });
            }
        }

        #endregion Property TraversalMode
        #region Property IsElementEventSender

        private bool _lastKnownIsElementEventSender;

        public static readonly DirectProperty<UIElementView, bool> IsElementEventSenderProperty =
            UrhoProperty.RegisterDirect<UIElementView, bool>(
                nameof(IsElementEventSender),
                o => o.IsElementEventSender,
                (o, v) => o.IsElementEventSender = v);

        public bool IsElementEventSender
        {
            get => _target.IsElementEventSender;

            set
            {
                SetAndRaise(IsElementEventSenderProperty, _lastKnownIsElementEventSender, value, _ =>
                {
                    _lastKnownIsElementEventSender = value;
                    _target.IsElementEventSender = value;
                });
            }
        }

        #endregion Property IsElementEventSender
    }
    public partial class AnimatableView : SerializableView
    {
        private Animatable _target;

        public AnimatableView(Animatable target): base(target)
        {
            _target = target;
            _lastKnownAnimationEnabled = target.AnimationEnabled;
        }

        public new Animatable Target => _target;

        #region Property AnimationEnabled

        private bool _lastKnownAnimationEnabled;

        public static readonly DirectProperty<AnimatableView, bool> AnimationEnabledProperty =
            UrhoProperty.RegisterDirect<AnimatableView, bool>(
                nameof(AnimationEnabled),
                o => o.AnimationEnabled,
                (o, v) => o.AnimationEnabled = v);

        public bool AnimationEnabled
        {
            get => _target.AnimationEnabled;

            set
            {
                SetAndRaise(AnimationEnabledProperty, _lastKnownAnimationEnabled, value, _ =>
                {
                    _lastKnownAnimationEnabled = value;
                    _target.AnimationEnabled = value;
                });
            }
        }

        #endregion Property AnimationEnabled
    }
    public partial class SerializableView : ObjectView
    {
        private Serializable _target;

        public SerializableView(Serializable target): base(target)
        {
            _target = target;
            _lastKnownIsTemporary = target.IsTemporary;
        }

        public new Serializable Target => _target;

        #region Property IsTemporary

        private bool _lastKnownIsTemporary;

        public static readonly DirectProperty<SerializableView, bool> IsTemporaryProperty =
            UrhoProperty.RegisterDirect<SerializableView, bool>(
                nameof(IsTemporary),
                o => o.IsTemporary,
                (o, v) => o.IsTemporary = v);

        public bool IsTemporary
        {
            get => _target.IsTemporary;

            set
            {
                SetAndRaise(IsTemporaryProperty, _lastKnownIsTemporary, value, _ =>
                {
                    _lastKnownIsTemporary = value;
                    _target.IsTemporary = value;
                });
            }
        }

        #endregion Property IsTemporary
    }
    public partial class BorderImageView : UIElementView
    {
        private BorderImage _target;

        public BorderImageView(BorderImage target): base(target)
        {
            _target = target;
            _lastKnownImageRect = target.ImageRect;
            _lastKnownBorder = target.Border;
            _lastKnownImageBorder = target.ImageBorder;
            _lastKnownHoverOffset = target.HoverOffset;
            _lastKnownDisabledOffset = target.DisabledOffset;
            _lastKnownBlendMode = target.BlendMode;
            _lastKnownIsTiled = target.IsTiled;
            _lastKnownMaterial = target.Material;
        }

        public new BorderImage Target => _target;

        #region Property ImageRect

        private IntRect _lastKnownImageRect;

        public static readonly DirectProperty<BorderImageView, IntRect> ImageRectProperty =
            UrhoProperty.RegisterDirect<BorderImageView, IntRect>(
                nameof(ImageRect),
                o => o.ImageRect,
                (o, v) => o.ImageRect = v);

        public IntRect ImageRect
        {
            get => _target.ImageRect;

            set
            {
                SetAndRaise(ImageRectProperty, _lastKnownImageRect, value, _ =>
                {
                    _lastKnownImageRect = value;
                    _target.ImageRect = value;
                });
            }
        }

        #endregion Property ImageRect
        #region Property Border

        private IntRect _lastKnownBorder;

        public static readonly DirectProperty<BorderImageView, IntRect> BorderProperty =
            UrhoProperty.RegisterDirect<BorderImageView, IntRect>(
                nameof(Border),
                o => o.Border,
                (o, v) => o.Border = v);

        public IntRect Border
        {
            get => _target.Border;

            set
            {
                SetAndRaise(BorderProperty, _lastKnownBorder, value, _ =>
                {
                    _lastKnownBorder = value;
                    _target.Border = value;
                });
            }
        }

        #endregion Property Border
        #region Property ImageBorder

        private IntRect _lastKnownImageBorder;

        public static readonly DirectProperty<BorderImageView, IntRect> ImageBorderProperty =
            UrhoProperty.RegisterDirect<BorderImageView, IntRect>(
                nameof(ImageBorder),
                o => o.ImageBorder,
                (o, v) => o.ImageBorder = v);

        public IntRect ImageBorder
        {
            get => _target.ImageBorder;

            set
            {
                SetAndRaise(ImageBorderProperty, _lastKnownImageBorder, value, _ =>
                {
                    _lastKnownImageBorder = value;
                    _target.ImageBorder = value;
                });
            }
        }

        #endregion Property ImageBorder
        #region Property HoverOffset

        private IntVector2 _lastKnownHoverOffset;

        public static readonly DirectProperty<BorderImageView, IntVector2> HoverOffsetProperty =
            UrhoProperty.RegisterDirect<BorderImageView, IntVector2>(
                nameof(HoverOffset),
                o => o.HoverOffset,
                (o, v) => o.HoverOffset = v);

        public IntVector2 HoverOffset
        {
            get => _target.HoverOffset;

            set
            {
                SetAndRaise(HoverOffsetProperty, _lastKnownHoverOffset, value, _ =>
                {
                    _lastKnownHoverOffset = value;
                    _target.HoverOffset = value;
                });
            }
        }

        #endregion Property HoverOffset
        #region Property DisabledOffset

        private IntVector2 _lastKnownDisabledOffset;

        public static readonly DirectProperty<BorderImageView, IntVector2> DisabledOffsetProperty =
            UrhoProperty.RegisterDirect<BorderImageView, IntVector2>(
                nameof(DisabledOffset),
                o => o.DisabledOffset,
                (o, v) => o.DisabledOffset = v);

        public IntVector2 DisabledOffset
        {
            get => _target.DisabledOffset;

            set
            {
                SetAndRaise(DisabledOffsetProperty, _lastKnownDisabledOffset, value, _ =>
                {
                    _lastKnownDisabledOffset = value;
                    _target.DisabledOffset = value;
                });
            }
        }

        #endregion Property DisabledOffset
        #region Property BlendMode

        private BlendMode _lastKnownBlendMode;

        public static readonly DirectProperty<BorderImageView, BlendMode> BlendModeProperty =
            UrhoProperty.RegisterDirect<BorderImageView, BlendMode>(
                nameof(BlendMode),
                o => o.BlendMode,
                (o, v) => o.BlendMode = v);

        public BlendMode BlendMode
        {
            get => _target.BlendMode;

            set
            {
                SetAndRaise(BlendModeProperty, _lastKnownBlendMode, value, _ =>
                {
                    _lastKnownBlendMode = value;
                    _target.BlendMode = value;
                });
            }
        }

        #endregion Property BlendMode
        #region Property IsTiled

        private bool _lastKnownIsTiled;

        public static readonly DirectProperty<BorderImageView, bool> IsTiledProperty =
            UrhoProperty.RegisterDirect<BorderImageView, bool>(
                nameof(IsTiled),
                o => o.IsTiled,
                (o, v) => o.IsTiled = v);

        public bool IsTiled
        {
            get => _target.IsTiled;

            set
            {
                SetAndRaise(IsTiledProperty, _lastKnownIsTiled, value, _ =>
                {
                    _lastKnownIsTiled = value;
                    _target.IsTiled = value;
                });
            }
        }

        #endregion Property IsTiled
        #region Property Material

        private Material _lastKnownMaterial;

        public static readonly DirectProperty<BorderImageView, Material> MaterialProperty =
            UrhoProperty.RegisterDirect<BorderImageView, Material>(
                nameof(Material),
                o => o.Material,
                (o, v) => o.Material = v);

        public Material Material
        {
            get => _target.Material;

            set
            {
                SetAndRaise(MaterialProperty, _lastKnownMaterial, value, _ =>
                {
                    _lastKnownMaterial = value;
                    _target.Material = value;
                });
            }
        }

        #endregion Property Material
    }
    public partial class ButtonView : BorderImageView
    {
        private Button _target;

        public ButtonView(Button target): base(target)
        {
            _target = target;
            _lastKnownPressedOffset = target.PressedOffset;
            _lastKnownPressedChildOffset = target.PressedChildOffset;
            _lastKnownRepeatDelay = target.RepeatDelay;
            _lastKnownRepeatRate = target.RepeatRate;
        }

        public new Button Target => _target;

        #region Property PressedOffset

        private IntVector2 _lastKnownPressedOffset;

        public static readonly DirectProperty<ButtonView, IntVector2> PressedOffsetProperty =
            UrhoProperty.RegisterDirect<ButtonView, IntVector2>(
                nameof(PressedOffset),
                o => o.PressedOffset,
                (o, v) => o.PressedOffset = v);

        public IntVector2 PressedOffset
        {
            get => _target.PressedOffset;

            set
            {
                SetAndRaise(PressedOffsetProperty, _lastKnownPressedOffset, value, _ =>
                {
                    _lastKnownPressedOffset = value;
                    _target.PressedOffset = value;
                });
            }
        }

        #endregion Property PressedOffset
        #region Property PressedChildOffset

        private IntVector2 _lastKnownPressedChildOffset;

        public static readonly DirectProperty<ButtonView, IntVector2> PressedChildOffsetProperty =
            UrhoProperty.RegisterDirect<ButtonView, IntVector2>(
                nameof(PressedChildOffset),
                o => o.PressedChildOffset,
                (o, v) => o.PressedChildOffset = v);

        public IntVector2 PressedChildOffset
        {
            get => _target.PressedChildOffset;

            set
            {
                SetAndRaise(PressedChildOffsetProperty, _lastKnownPressedChildOffset, value, _ =>
                {
                    _lastKnownPressedChildOffset = value;
                    _target.PressedChildOffset = value;
                });
            }
        }

        #endregion Property PressedChildOffset
        #region Property RepeatDelay

        private float _lastKnownRepeatDelay;

        public static readonly DirectProperty<ButtonView, float> RepeatDelayProperty =
            UrhoProperty.RegisterDirect<ButtonView, float>(
                nameof(RepeatDelay),
                o => o.RepeatDelay,
                (o, v) => o.RepeatDelay = v);

        public float RepeatDelay
        {
            get => _target.RepeatDelay;

            set
            {
                SetAndRaise(RepeatDelayProperty, _lastKnownRepeatDelay, value, _ =>
                {
                    _lastKnownRepeatDelay = value;
                    _target.RepeatDelay = value;
                });
            }
        }

        #endregion Property RepeatDelay
        #region Property RepeatRate

        private float _lastKnownRepeatRate;

        public static readonly DirectProperty<ButtonView, float> RepeatRateProperty =
            UrhoProperty.RegisterDirect<ButtonView, float>(
                nameof(RepeatRate),
                o => o.RepeatRate,
                (o, v) => o.RepeatRate = v);

        public float RepeatRate
        {
            get => _target.RepeatRate;

            set
            {
                SetAndRaise(RepeatRateProperty, _lastKnownRepeatRate, value, _ =>
                {
                    _lastKnownRepeatRate = value;
                    _target.RepeatRate = value;
                });
            }
        }

        #endregion Property RepeatRate
    }
    public partial class CheckBoxView : BorderImageView
    {
        private CheckBox _target;

        public CheckBoxView(CheckBox target): base(target)
        {
            _target = target;
            _lastKnownIsChecked = target.IsChecked;
            _lastKnownCheckedOffset = target.CheckedOffset;
        }

        public new CheckBox Target => _target;

        #region Property IsChecked

        private bool _lastKnownIsChecked;

        public static readonly DirectProperty<CheckBoxView, bool> IsCheckedProperty =
            UrhoProperty.RegisterDirect<CheckBoxView, bool>(
                nameof(IsChecked),
                o => o.IsChecked,
                (o, v) => o.IsChecked = v);

        public bool IsChecked
        {
            get => _target.IsChecked;

            set
            {
                SetAndRaise(IsCheckedProperty, _lastKnownIsChecked, value, _ =>
                {
                    _lastKnownIsChecked = value;
                    _target.IsChecked = value;
                });
            }
        }

        #endregion Property IsChecked
        #region Property CheckedOffset

        private IntVector2 _lastKnownCheckedOffset;

        public static readonly DirectProperty<CheckBoxView, IntVector2> CheckedOffsetProperty =
            UrhoProperty.RegisterDirect<CheckBoxView, IntVector2>(
                nameof(CheckedOffset),
                o => o.CheckedOffset,
                (o, v) => o.CheckedOffset = v);

        public IntVector2 CheckedOffset
        {
            get => _target.CheckedOffset;

            set
            {
                SetAndRaise(CheckedOffsetProperty, _lastKnownCheckedOffset, value, _ =>
                {
                    _lastKnownCheckedOffset = value;
                    _target.CheckedOffset = value;
                });
            }
        }

        #endregion Property CheckedOffset
    }
    public partial class CursorView : BorderImageView
    {
        private Cursor _target;

        public CursorView(Cursor target): base(target)
        {
            _target = target;
            _lastKnownShape = target.Shape;
            _lastKnownUseSystemShapes = target.UseSystemShapes;
        }

        public new Cursor Target => _target;

        #region Property Shape

        private string _lastKnownShape;

        public static readonly DirectProperty<CursorView, string> ShapeProperty =
            UrhoProperty.RegisterDirect<CursorView, string>(
                nameof(Shape),
                o => o.Shape,
                (o, v) => o.Shape = v);

        public string Shape
        {
            get => _target.Shape;

            set
            {
                SetAndRaise(ShapeProperty, _lastKnownShape, value, _ =>
                {
                    _lastKnownShape = value;
                    _target.Shape = value;
                });
            }
        }

        #endregion Property Shape
        #region Property UseSystemShapes

        private bool _lastKnownUseSystemShapes;

        public static readonly DirectProperty<CursorView, bool> UseSystemShapesProperty =
            UrhoProperty.RegisterDirect<CursorView, bool>(
                nameof(UseSystemShapes),
                o => o.UseSystemShapes,
                (o, v) => o.UseSystemShapes = v);

        public bool UseSystemShapes
        {
            get => _target.UseSystemShapes;

            set
            {
                SetAndRaise(UseSystemShapesProperty, _lastKnownUseSystemShapes, value, _ =>
                {
                    _lastKnownUseSystemShapes = value;
                    _target.UseSystemShapes = value;
                });
            }
        }

        #endregion Property UseSystemShapes
    }
    public partial class DropDownListView : MenuView
    {
        private DropDownList _target;

        public DropDownListView(DropDownList target): base(target)
        {
            _target = target;
            _lastKnownSelection = target.Selection;
            _lastKnownPlaceholderText = target.PlaceholderText;
            _lastKnownResizePopup = target.ResizePopup;
        }

        public new DropDownList Target => _target;

        #region Property Selection

        private uint _lastKnownSelection;

        public static readonly DirectProperty<DropDownListView, uint> SelectionProperty =
            UrhoProperty.RegisterDirect<DropDownListView, uint>(
                nameof(Selection),
                o => o.Selection,
                (o, v) => o.Selection = v);

        public uint Selection
        {
            get => _target.Selection;

            set
            {
                SetAndRaise(SelectionProperty, _lastKnownSelection, value, _ =>
                {
                    _lastKnownSelection = value;
                    _target.Selection = value;
                });
            }
        }

        #endregion Property Selection
        #region Property PlaceholderText

        private string _lastKnownPlaceholderText;

        public static readonly DirectProperty<DropDownListView, string> PlaceholderTextProperty =
            UrhoProperty.RegisterDirect<DropDownListView, string>(
                nameof(PlaceholderText),
                o => o.PlaceholderText,
                (o, v) => o.PlaceholderText = v);

        public string PlaceholderText
        {
            get => _target.PlaceholderText;

            set
            {
                SetAndRaise(PlaceholderTextProperty, _lastKnownPlaceholderText, value, _ =>
                {
                    _lastKnownPlaceholderText = value;
                    _target.PlaceholderText = value;
                });
            }
        }

        #endregion Property PlaceholderText
        #region Property ResizePopup

        private bool _lastKnownResizePopup;

        public static readonly DirectProperty<DropDownListView, bool> ResizePopupProperty =
            UrhoProperty.RegisterDirect<DropDownListView, bool>(
                nameof(ResizePopup),
                o => o.ResizePopup,
                (o, v) => o.ResizePopup = v);

        public bool ResizePopup
        {
            get => _target.ResizePopup;

            set
            {
                SetAndRaise(ResizePopupProperty, _lastKnownResizePopup, value, _ =>
                {
                    _lastKnownResizePopup = value;
                    _target.ResizePopup = value;
                });
            }
        }

        #endregion Property ResizePopup
    }
    public partial class MenuView : ButtonView
    {
        private Menu _target;

        public MenuView(Menu target): base(target)
        {
            _target = target;
            _lastKnownPopupOffset = target.PopupOffset;
        }

        public new Menu Target => _target;

        #region Property PopupOffset

        private IntVector2 _lastKnownPopupOffset;

        public static readonly DirectProperty<MenuView, IntVector2> PopupOffsetProperty =
            UrhoProperty.RegisterDirect<MenuView, IntVector2>(
                nameof(PopupOffset),
                o => o.PopupOffset,
                (o, v) => o.PopupOffset = v);

        public IntVector2 PopupOffset
        {
            get => _target.PopupOffset;

            set
            {
                SetAndRaise(PopupOffsetProperty, _lastKnownPopupOffset, value, _ =>
                {
                    _lastKnownPopupOffset = value;
                    _target.PopupOffset = value;
                });
            }
        }

        #endregion Property PopupOffset
    }
    public partial class LineEditView : BorderImageView
    {
        private LineEdit _target;

        public LineEditView(LineEdit target): base(target)
        {
            _target = target;
            _lastKnownText = target.Text;
            _lastKnownCursorPosition = target.CursorPosition;
            _lastKnownCursorBlinkRate = target.CursorBlinkRate;
            _lastKnownMaxLength = target.MaxLength;
            _lastKnownEchoCharacter = target.EchoCharacter;
            _lastKnownIsCursorMovable = target.IsCursorMovable;
            _lastKnownIsTextSelectable = target.IsTextSelectable;
            _lastKnownIsTextCopyable = target.IsTextCopyable;
        }

        public new LineEdit Target => _target;

        #region Property Text

        private string _lastKnownText;

        public static readonly DirectProperty<LineEditView, string> TextProperty =
            UrhoProperty.RegisterDirect<LineEditView, string>(
                nameof(Text),
                o => o.Text,
                (o, v) => o.Text = v);

        public string Text
        {
            get => _target.Text;

            set
            {
                SetAndRaise(TextProperty, _lastKnownText, value, _ =>
                {
                    _lastKnownText = value;
                    _target.Text = value;
                });
            }
        }

        #endregion Property Text
        #region Property CursorPosition

        private uint _lastKnownCursorPosition;

        public static readonly DirectProperty<LineEditView, uint> CursorPositionProperty =
            UrhoProperty.RegisterDirect<LineEditView, uint>(
                nameof(CursorPosition),
                o => o.CursorPosition,
                (o, v) => o.CursorPosition = v);

        public uint CursorPosition
        {
            get => _target.CursorPosition;

            set
            {
                SetAndRaise(CursorPositionProperty, _lastKnownCursorPosition, value, _ =>
                {
                    _lastKnownCursorPosition = value;
                    _target.CursorPosition = value;
                });
            }
        }

        #endregion Property CursorPosition
        #region Property CursorBlinkRate

        private float _lastKnownCursorBlinkRate;

        public static readonly DirectProperty<LineEditView, float> CursorBlinkRateProperty =
            UrhoProperty.RegisterDirect<LineEditView, float>(
                nameof(CursorBlinkRate),
                o => o.CursorBlinkRate,
                (o, v) => o.CursorBlinkRate = v);

        public float CursorBlinkRate
        {
            get => _target.CursorBlinkRate;

            set
            {
                SetAndRaise(CursorBlinkRateProperty, _lastKnownCursorBlinkRate, value, _ =>
                {
                    _lastKnownCursorBlinkRate = value;
                    _target.CursorBlinkRate = value;
                });
            }
        }

        #endregion Property CursorBlinkRate
        #region Property MaxLength

        private uint _lastKnownMaxLength;

        public static readonly DirectProperty<LineEditView, uint> MaxLengthProperty =
            UrhoProperty.RegisterDirect<LineEditView, uint>(
                nameof(MaxLength),
                o => o.MaxLength,
                (o, v) => o.MaxLength = v);

        public uint MaxLength
        {
            get => _target.MaxLength;

            set
            {
                SetAndRaise(MaxLengthProperty, _lastKnownMaxLength, value, _ =>
                {
                    _lastKnownMaxLength = value;
                    _target.MaxLength = value;
                });
            }
        }

        #endregion Property MaxLength
        #region Property EchoCharacter

        private uint _lastKnownEchoCharacter;

        public static readonly DirectProperty<LineEditView, uint> EchoCharacterProperty =
            UrhoProperty.RegisterDirect<LineEditView, uint>(
                nameof(EchoCharacter),
                o => o.EchoCharacter,
                (o, v) => o.EchoCharacter = v);

        public uint EchoCharacter
        {
            get => _target.EchoCharacter;

            set
            {
                SetAndRaise(EchoCharacterProperty, _lastKnownEchoCharacter, value, _ =>
                {
                    _lastKnownEchoCharacter = value;
                    _target.EchoCharacter = value;
                });
            }
        }

        #endregion Property EchoCharacter
        #region Property IsCursorMovable

        private bool _lastKnownIsCursorMovable;

        public static readonly DirectProperty<LineEditView, bool> IsCursorMovableProperty =
            UrhoProperty.RegisterDirect<LineEditView, bool>(
                nameof(IsCursorMovable),
                o => o.IsCursorMovable,
                (o, v) => o.IsCursorMovable = v);

        public bool IsCursorMovable
        {
            get => _target.IsCursorMovable;

            set
            {
                SetAndRaise(IsCursorMovableProperty, _lastKnownIsCursorMovable, value, _ =>
                {
                    _lastKnownIsCursorMovable = value;
                    _target.IsCursorMovable = value;
                });
            }
        }

        #endregion Property IsCursorMovable
        #region Property IsTextSelectable

        private bool _lastKnownIsTextSelectable;

        public static readonly DirectProperty<LineEditView, bool> IsTextSelectableProperty =
            UrhoProperty.RegisterDirect<LineEditView, bool>(
                nameof(IsTextSelectable),
                o => o.IsTextSelectable,
                (o, v) => o.IsTextSelectable = v);

        public bool IsTextSelectable
        {
            get => _target.IsTextSelectable;

            set
            {
                SetAndRaise(IsTextSelectableProperty, _lastKnownIsTextSelectable, value, _ =>
                {
                    _lastKnownIsTextSelectable = value;
                    _target.IsTextSelectable = value;
                });
            }
        }

        #endregion Property IsTextSelectable
        #region Property IsTextCopyable

        private bool _lastKnownIsTextCopyable;

        public static readonly DirectProperty<LineEditView, bool> IsTextCopyableProperty =
            UrhoProperty.RegisterDirect<LineEditView, bool>(
                nameof(IsTextCopyable),
                o => o.IsTextCopyable,
                (o, v) => o.IsTextCopyable = v);

        public bool IsTextCopyable
        {
            get => _target.IsTextCopyable;

            set
            {
                SetAndRaise(IsTextCopyableProperty, _lastKnownIsTextCopyable, value, _ =>
                {
                    _lastKnownIsTextCopyable = value;
                    _target.IsTextCopyable = value;
                });
            }
        }

        #endregion Property IsTextCopyable
    }
    public partial class ListViewView : ScrollViewView
    {
        private ListView _target;

        public ListViewView(ListView target): base(target)
        {
            _target = target;
            _lastKnownSelection = target.Selection;
            _lastKnownMultiselect = target.Multiselect;
            _lastKnownClearSelectionOnDefocus = target.ClearSelectionOnDefocus;
            _lastKnownSelectOnClickEnd = target.SelectOnClickEnd;
            _lastKnownHierarchyMode = target.HierarchyMode;
            _lastKnownBaseIndent = target.BaseIndent;
        }

        public new ListView Target => _target;

        #region Property Selection

        private uint _lastKnownSelection;

        public static readonly DirectProperty<ListViewView, uint> SelectionProperty =
            UrhoProperty.RegisterDirect<ListViewView, uint>(
                nameof(Selection),
                o => o.Selection,
                (o, v) => o.Selection = v);

        public uint Selection
        {
            get => _target.Selection;

            set
            {
                SetAndRaise(SelectionProperty, _lastKnownSelection, value, _ =>
                {
                    _lastKnownSelection = value;
                    _target.Selection = value;
                });
            }
        }

        #endregion Property Selection
        #region Property Multiselect

        private bool _lastKnownMultiselect;

        public static readonly DirectProperty<ListViewView, bool> MultiselectProperty =
            UrhoProperty.RegisterDirect<ListViewView, bool>(
                nameof(Multiselect),
                o => o.Multiselect,
                (o, v) => o.Multiselect = v);

        public bool Multiselect
        {
            get => _target.Multiselect;

            set
            {
                SetAndRaise(MultiselectProperty, _lastKnownMultiselect, value, _ =>
                {
                    _lastKnownMultiselect = value;
                    _target.Multiselect = value;
                });
            }
        }

        #endregion Property Multiselect
        #region Property ClearSelectionOnDefocus

        private bool _lastKnownClearSelectionOnDefocus;

        public static readonly DirectProperty<ListViewView, bool> ClearSelectionOnDefocusProperty =
            UrhoProperty.RegisterDirect<ListViewView, bool>(
                nameof(ClearSelectionOnDefocus),
                o => o.ClearSelectionOnDefocus,
                (o, v) => o.ClearSelectionOnDefocus = v);

        public bool ClearSelectionOnDefocus
        {
            get => _target.ClearSelectionOnDefocus;

            set
            {
                SetAndRaise(ClearSelectionOnDefocusProperty, _lastKnownClearSelectionOnDefocus, value, _ =>
                {
                    _lastKnownClearSelectionOnDefocus = value;
                    _target.ClearSelectionOnDefocus = value;
                });
            }
        }

        #endregion Property ClearSelectionOnDefocus
        #region Property SelectOnClickEnd

        private bool _lastKnownSelectOnClickEnd;

        public static readonly DirectProperty<ListViewView, bool> SelectOnClickEndProperty =
            UrhoProperty.RegisterDirect<ListViewView, bool>(
                nameof(SelectOnClickEnd),
                o => o.SelectOnClickEnd,
                (o, v) => o.SelectOnClickEnd = v);

        public bool SelectOnClickEnd
        {
            get => _target.SelectOnClickEnd;

            set
            {
                SetAndRaise(SelectOnClickEndProperty, _lastKnownSelectOnClickEnd, value, _ =>
                {
                    _lastKnownSelectOnClickEnd = value;
                    _target.SelectOnClickEnd = value;
                });
            }
        }

        #endregion Property SelectOnClickEnd
        #region Property HierarchyMode

        private bool _lastKnownHierarchyMode;

        public static readonly DirectProperty<ListViewView, bool> HierarchyModeProperty =
            UrhoProperty.RegisterDirect<ListViewView, bool>(
                nameof(HierarchyMode),
                o => o.HierarchyMode,
                (o, v) => o.HierarchyMode = v);

        public bool HierarchyMode
        {
            get => _target.HierarchyMode;

            set
            {
                SetAndRaise(HierarchyModeProperty, _lastKnownHierarchyMode, value, _ =>
                {
                    _lastKnownHierarchyMode = value;
                    _target.HierarchyMode = value;
                });
            }
        }

        #endregion Property HierarchyMode
        #region Property BaseIndent

        private int _lastKnownBaseIndent;

        public static readonly DirectProperty<ListViewView, int> BaseIndentProperty =
            UrhoProperty.RegisterDirect<ListViewView, int>(
                nameof(BaseIndent),
                o => o.BaseIndent,
                (o, v) => o.BaseIndent = v);

        public int BaseIndent
        {
            get => _target.BaseIndent;

            set
            {
                SetAndRaise(BaseIndentProperty, _lastKnownBaseIndent, value, _ =>
                {
                    _lastKnownBaseIndent = value;
                    _target.BaseIndent = value;
                });
            }
        }

        #endregion Property BaseIndent
    }
    public partial class ScrollViewView : UIElementView
    {
        private ScrollView _target;

        public ScrollViewView(ScrollView target): base(target)
        {
            _target = target;
            _lastKnownViewPosition = target.ViewPosition;
            _lastKnownScrollBarsAutoVisible = target.ScrollBarsAutoVisible;
            _lastKnownHorizontalScrollBarVisible = target.HorizontalScrollBarVisible;
            _lastKnownVerticalScrollBarVisible = target.VerticalScrollBarVisible;
            _lastKnownScrollStep = target.ScrollStep;
            _lastKnownPageStep = target.PageStep;
            _lastKnownScrollDeceleration = target.ScrollDeceleration;
            _lastKnownScrollSnapEpsilon = target.ScrollSnapEpsilon;
            _lastKnownAutoDisableChildren = target.AutoDisableChildren;
            _lastKnownAutoDisableThreshold = target.AutoDisableThreshold;
        }

        public new ScrollView Target => _target;

        #region Property ViewPosition

        private IntVector2 _lastKnownViewPosition;

        public static readonly DirectProperty<ScrollViewView, IntVector2> ViewPositionProperty =
            UrhoProperty.RegisterDirect<ScrollViewView, IntVector2>(
                nameof(ViewPosition),
                o => o.ViewPosition,
                (o, v) => o.ViewPosition = v);

        public IntVector2 ViewPosition
        {
            get => _target.ViewPosition;

            set
            {
                SetAndRaise(ViewPositionProperty, _lastKnownViewPosition, value, _ =>
                {
                    _lastKnownViewPosition = value;
                    _target.ViewPosition = value;
                });
            }
        }

        #endregion Property ViewPosition
        #region Property ScrollBarsAutoVisible

        private bool _lastKnownScrollBarsAutoVisible;

        public static readonly DirectProperty<ScrollViewView, bool> ScrollBarsAutoVisibleProperty =
            UrhoProperty.RegisterDirect<ScrollViewView, bool>(
                nameof(ScrollBarsAutoVisible),
                o => o.ScrollBarsAutoVisible,
                (o, v) => o.ScrollBarsAutoVisible = v);

        public bool ScrollBarsAutoVisible
        {
            get => _target.ScrollBarsAutoVisible;

            set
            {
                SetAndRaise(ScrollBarsAutoVisibleProperty, _lastKnownScrollBarsAutoVisible, value, _ =>
                {
                    _lastKnownScrollBarsAutoVisible = value;
                    _target.ScrollBarsAutoVisible = value;
                });
            }
        }

        #endregion Property ScrollBarsAutoVisible
        #region Property HorizontalScrollBarVisible

        private bool _lastKnownHorizontalScrollBarVisible;

        public static readonly DirectProperty<ScrollViewView, bool> HorizontalScrollBarVisibleProperty =
            UrhoProperty.RegisterDirect<ScrollViewView, bool>(
                nameof(HorizontalScrollBarVisible),
                o => o.HorizontalScrollBarVisible,
                (o, v) => o.HorizontalScrollBarVisible = v);

        public bool HorizontalScrollBarVisible
        {
            get => _target.HorizontalScrollBarVisible;

            set
            {
                SetAndRaise(HorizontalScrollBarVisibleProperty, _lastKnownHorizontalScrollBarVisible, value, _ =>
                {
                    _lastKnownHorizontalScrollBarVisible = value;
                    _target.HorizontalScrollBarVisible = value;
                });
            }
        }

        #endregion Property HorizontalScrollBarVisible
        #region Property VerticalScrollBarVisible

        private bool _lastKnownVerticalScrollBarVisible;

        public static readonly DirectProperty<ScrollViewView, bool> VerticalScrollBarVisibleProperty =
            UrhoProperty.RegisterDirect<ScrollViewView, bool>(
                nameof(VerticalScrollBarVisible),
                o => o.VerticalScrollBarVisible,
                (o, v) => o.VerticalScrollBarVisible = v);

        public bool VerticalScrollBarVisible
        {
            get => _target.VerticalScrollBarVisible;

            set
            {
                SetAndRaise(VerticalScrollBarVisibleProperty, _lastKnownVerticalScrollBarVisible, value, _ =>
                {
                    _lastKnownVerticalScrollBarVisible = value;
                    _target.VerticalScrollBarVisible = value;
                });
            }
        }

        #endregion Property VerticalScrollBarVisible
        #region Property ScrollStep

        private float _lastKnownScrollStep;

        public static readonly DirectProperty<ScrollViewView, float> ScrollStepProperty =
            UrhoProperty.RegisterDirect<ScrollViewView, float>(
                nameof(ScrollStep),
                o => o.ScrollStep,
                (o, v) => o.ScrollStep = v);

        public float ScrollStep
        {
            get => _target.ScrollStep;

            set
            {
                SetAndRaise(ScrollStepProperty, _lastKnownScrollStep, value, _ =>
                {
                    _lastKnownScrollStep = value;
                    _target.ScrollStep = value;
                });
            }
        }

        #endregion Property ScrollStep
        #region Property PageStep

        private float _lastKnownPageStep;

        public static readonly DirectProperty<ScrollViewView, float> PageStepProperty =
            UrhoProperty.RegisterDirect<ScrollViewView, float>(
                nameof(PageStep),
                o => o.PageStep,
                (o, v) => o.PageStep = v);

        public float PageStep
        {
            get => _target.PageStep;

            set
            {
                SetAndRaise(PageStepProperty, _lastKnownPageStep, value, _ =>
                {
                    _lastKnownPageStep = value;
                    _target.PageStep = value;
                });
            }
        }

        #endregion Property PageStep
        #region Property ScrollDeceleration

        private float _lastKnownScrollDeceleration;

        public static readonly DirectProperty<ScrollViewView, float> ScrollDecelerationProperty =
            UrhoProperty.RegisterDirect<ScrollViewView, float>(
                nameof(ScrollDeceleration),
                o => o.ScrollDeceleration,
                (o, v) => o.ScrollDeceleration = v);

        public float ScrollDeceleration
        {
            get => _target.ScrollDeceleration;

            set
            {
                SetAndRaise(ScrollDecelerationProperty, _lastKnownScrollDeceleration, value, _ =>
                {
                    _lastKnownScrollDeceleration = value;
                    _target.ScrollDeceleration = value;
                });
            }
        }

        #endregion Property ScrollDeceleration
        #region Property ScrollSnapEpsilon

        private float _lastKnownScrollSnapEpsilon;

        public static readonly DirectProperty<ScrollViewView, float> ScrollSnapEpsilonProperty =
            UrhoProperty.RegisterDirect<ScrollViewView, float>(
                nameof(ScrollSnapEpsilon),
                o => o.ScrollSnapEpsilon,
                (o, v) => o.ScrollSnapEpsilon = v);

        public float ScrollSnapEpsilon
        {
            get => _target.ScrollSnapEpsilon;

            set
            {
                SetAndRaise(ScrollSnapEpsilonProperty, _lastKnownScrollSnapEpsilon, value, _ =>
                {
                    _lastKnownScrollSnapEpsilon = value;
                    _target.ScrollSnapEpsilon = value;
                });
            }
        }

        #endregion Property ScrollSnapEpsilon
        #region Property AutoDisableChildren

        private bool _lastKnownAutoDisableChildren;

        public static readonly DirectProperty<ScrollViewView, bool> AutoDisableChildrenProperty =
            UrhoProperty.RegisterDirect<ScrollViewView, bool>(
                nameof(AutoDisableChildren),
                o => o.AutoDisableChildren,
                (o, v) => o.AutoDisableChildren = v);

        public bool AutoDisableChildren
        {
            get => _target.AutoDisableChildren;

            set
            {
                SetAndRaise(AutoDisableChildrenProperty, _lastKnownAutoDisableChildren, value, _ =>
                {
                    _lastKnownAutoDisableChildren = value;
                    _target.AutoDisableChildren = value;
                });
            }
        }

        #endregion Property AutoDisableChildren
        #region Property AutoDisableThreshold

        private float _lastKnownAutoDisableThreshold;

        public static readonly DirectProperty<ScrollViewView, float> AutoDisableThresholdProperty =
            UrhoProperty.RegisterDirect<ScrollViewView, float>(
                nameof(AutoDisableThreshold),
                o => o.AutoDisableThreshold,
                (o, v) => o.AutoDisableThreshold = v);

        public float AutoDisableThreshold
        {
            get => _target.AutoDisableThreshold;

            set
            {
                SetAndRaise(AutoDisableThresholdProperty, _lastKnownAutoDisableThreshold, value, _ =>
                {
                    _lastKnownAutoDisableThreshold = value;
                    _target.AutoDisableThreshold = value;
                });
            }
        }

        #endregion Property AutoDisableThreshold
    }
    public partial class ProgressBarView : BorderImageView
    {
        private ProgressBar _target;

        public ProgressBarView(ProgressBar target): base(target)
        {
            _target = target;
            _lastKnownOrientation = target.Orientation;
            _lastKnownRange = target.Range;
            _lastKnownValue = target.Value;
            _lastKnownLoadingPercentStyle = target.LoadingPercentStyle;
            _lastKnownShowPercentText = target.ShowPercentText;
        }

        public new ProgressBar Target => _target;

        #region Property Orientation

        private Orientation _lastKnownOrientation;

        public static readonly DirectProperty<ProgressBarView, Orientation> OrientationProperty =
            UrhoProperty.RegisterDirect<ProgressBarView, Orientation>(
                nameof(Orientation),
                o => o.Orientation,
                (o, v) => o.Orientation = v);

        public Orientation Orientation
        {
            get => _target.Orientation;

            set
            {
                SetAndRaise(OrientationProperty, _lastKnownOrientation, value, _ =>
                {
                    _lastKnownOrientation = value;
                    _target.Orientation = value;
                });
            }
        }

        #endregion Property Orientation
        #region Property Range

        private float _lastKnownRange;

        public static readonly DirectProperty<ProgressBarView, float> RangeProperty =
            UrhoProperty.RegisterDirect<ProgressBarView, float>(
                nameof(Range),
                o => o.Range,
                (o, v) => o.Range = v);

        public float Range
        {
            get => _target.Range;

            set
            {
                SetAndRaise(RangeProperty, _lastKnownRange, value, _ =>
                {
                    _lastKnownRange = value;
                    _target.Range = value;
                });
            }
        }

        #endregion Property Range
        #region Property Value

        private float _lastKnownValue;

        public static readonly DirectProperty<ProgressBarView, float> ValueProperty =
            UrhoProperty.RegisterDirect<ProgressBarView, float>(
                nameof(Value),
                o => o.Value,
                (o, v) => o.Value = v);

        public float Value
        {
            get => _target.Value;

            set
            {
                SetAndRaise(ValueProperty, _lastKnownValue, value, _ =>
                {
                    _lastKnownValue = value;
                    _target.Value = value;
                });
            }
        }

        #endregion Property Value
        #region Property LoadingPercentStyle

        private string _lastKnownLoadingPercentStyle;

        public static readonly DirectProperty<ProgressBarView, string> LoadingPercentStyleProperty =
            UrhoProperty.RegisterDirect<ProgressBarView, string>(
                nameof(LoadingPercentStyle),
                o => o.LoadingPercentStyle,
                (o, v) => o.LoadingPercentStyle = v);

        public string LoadingPercentStyle
        {
            get => _target.LoadingPercentStyle;

            set
            {
                SetAndRaise(LoadingPercentStyleProperty, _lastKnownLoadingPercentStyle, value, _ =>
                {
                    _lastKnownLoadingPercentStyle = value;
                    _target.LoadingPercentStyle = value;
                });
            }
        }

        #endregion Property LoadingPercentStyle
        #region Property ShowPercentText

        private bool _lastKnownShowPercentText;

        public static readonly DirectProperty<ProgressBarView, bool> ShowPercentTextProperty =
            UrhoProperty.RegisterDirect<ProgressBarView, bool>(
                nameof(ShowPercentText),
                o => o.ShowPercentText,
                (o, v) => o.ShowPercentText = v);

        public bool ShowPercentText
        {
            get => _target.ShowPercentText;

            set
            {
                SetAndRaise(ShowPercentTextProperty, _lastKnownShowPercentText, value, _ =>
                {
                    _lastKnownShowPercentText = value;
                    _target.ShowPercentText = value;
                });
            }
        }

        #endregion Property ShowPercentText
    }
    public partial class ScrollBarView : BorderImageView
    {
        private ScrollBar _target;

        public ScrollBarView(ScrollBar target): base(target)
        {
            _target = target;
            _lastKnownOrientation = target.Orientation;
            _lastKnownRange = target.Range;
            _lastKnownValue = target.Value;
            _lastKnownScrollStep = target.ScrollStep;
            _lastKnownStepFactor = target.StepFactor;
        }

        public new ScrollBar Target => _target;

        #region Property Orientation

        private Orientation _lastKnownOrientation;

        public static readonly DirectProperty<ScrollBarView, Orientation> OrientationProperty =
            UrhoProperty.RegisterDirect<ScrollBarView, Orientation>(
                nameof(Orientation),
                o => o.Orientation,
                (o, v) => o.Orientation = v);

        public Orientation Orientation
        {
            get => _target.Orientation;

            set
            {
                SetAndRaise(OrientationProperty, _lastKnownOrientation, value, _ =>
                {
                    _lastKnownOrientation = value;
                    _target.Orientation = value;
                });
            }
        }

        #endregion Property Orientation
        #region Property Range

        private float _lastKnownRange;

        public static readonly DirectProperty<ScrollBarView, float> RangeProperty =
            UrhoProperty.RegisterDirect<ScrollBarView, float>(
                nameof(Range),
                o => o.Range,
                (o, v) => o.Range = v);

        public float Range
        {
            get => _target.Range;

            set
            {
                SetAndRaise(RangeProperty, _lastKnownRange, value, _ =>
                {
                    _lastKnownRange = value;
                    _target.Range = value;
                });
            }
        }

        #endregion Property Range
        #region Property Value

        private float _lastKnownValue;

        public static readonly DirectProperty<ScrollBarView, float> ValueProperty =
            UrhoProperty.RegisterDirect<ScrollBarView, float>(
                nameof(Value),
                o => o.Value,
                (o, v) => o.Value = v);

        public float Value
        {
            get => _target.Value;

            set
            {
                SetAndRaise(ValueProperty, _lastKnownValue, value, _ =>
                {
                    _lastKnownValue = value;
                    _target.Value = value;
                });
            }
        }

        #endregion Property Value
        #region Property ScrollStep

        private float _lastKnownScrollStep;

        public static readonly DirectProperty<ScrollBarView, float> ScrollStepProperty =
            UrhoProperty.RegisterDirect<ScrollBarView, float>(
                nameof(ScrollStep),
                o => o.ScrollStep,
                (o, v) => o.ScrollStep = v);

        public float ScrollStep
        {
            get => _target.ScrollStep;

            set
            {
                SetAndRaise(ScrollStepProperty, _lastKnownScrollStep, value, _ =>
                {
                    _lastKnownScrollStep = value;
                    _target.ScrollStep = value;
                });
            }
        }

        #endregion Property ScrollStep
        #region Property StepFactor

        private float _lastKnownStepFactor;

        public static readonly DirectProperty<ScrollBarView, float> StepFactorProperty =
            UrhoProperty.RegisterDirect<ScrollBarView, float>(
                nameof(StepFactor),
                o => o.StepFactor,
                (o, v) => o.StepFactor = v);

        public float StepFactor
        {
            get => _target.StepFactor;

            set
            {
                SetAndRaise(StepFactorProperty, _lastKnownStepFactor, value, _ =>
                {
                    _lastKnownStepFactor = value;
                    _target.StepFactor = value;
                });
            }
        }

        #endregion Property StepFactor
    }
    public partial class SliderView : BorderImageView
    {
        private Slider _target;

        public SliderView(Slider target): base(target)
        {
            _target = target;
            _lastKnownOrientation = target.Orientation;
            _lastKnownRange = target.Range;
            _lastKnownValue = target.Value;
            _lastKnownRepeatRate = target.RepeatRate;
        }

        public new Slider Target => _target;

        #region Property Orientation

        private Orientation _lastKnownOrientation;

        public static readonly DirectProperty<SliderView, Orientation> OrientationProperty =
            UrhoProperty.RegisterDirect<SliderView, Orientation>(
                nameof(Orientation),
                o => o.Orientation,
                (o, v) => o.Orientation = v);

        public Orientation Orientation
        {
            get => _target.Orientation;

            set
            {
                SetAndRaise(OrientationProperty, _lastKnownOrientation, value, _ =>
                {
                    _lastKnownOrientation = value;
                    _target.Orientation = value;
                });
            }
        }

        #endregion Property Orientation
        #region Property Range

        private float _lastKnownRange;

        public static readonly DirectProperty<SliderView, float> RangeProperty =
            UrhoProperty.RegisterDirect<SliderView, float>(
                nameof(Range),
                o => o.Range,
                (o, v) => o.Range = v);

        public float Range
        {
            get => _target.Range;

            set
            {
                SetAndRaise(RangeProperty, _lastKnownRange, value, _ =>
                {
                    _lastKnownRange = value;
                    _target.Range = value;
                });
            }
        }

        #endregion Property Range
        #region Property Value

        private float _lastKnownValue;

        public static readonly DirectProperty<SliderView, float> ValueProperty =
            UrhoProperty.RegisterDirect<SliderView, float>(
                nameof(Value),
                o => o.Value,
                (o, v) => o.Value = v);

        public float Value
        {
            get => _target.Value;

            set
            {
                SetAndRaise(ValueProperty, _lastKnownValue, value, _ =>
                {
                    _lastKnownValue = value;
                    _target.Value = value;
                });
            }
        }

        #endregion Property Value
        #region Property RepeatRate

        private float _lastKnownRepeatRate;

        public static readonly DirectProperty<SliderView, float> RepeatRateProperty =
            UrhoProperty.RegisterDirect<SliderView, float>(
                nameof(RepeatRate),
                o => o.RepeatRate,
                (o, v) => o.RepeatRate = v);

        public float RepeatRate
        {
            get => _target.RepeatRate;

            set
            {
                SetAndRaise(RepeatRateProperty, _lastKnownRepeatRate, value, _ =>
                {
                    _lastKnownRepeatRate = value;
                    _target.RepeatRate = value;
                });
            }
        }

        #endregion Property RepeatRate
    }
    public partial class SpriteView : UIElementView
    {
        private Sprite _target;

        public SpriteView(Sprite target): base(target)
        {
            _target = target;
            _lastKnownPosition = target.Position;
            _lastKnownHotSpot = target.HotSpot;
            _lastKnownScale = target.Scale;
            _lastKnownRotation = target.Rotation;
            _lastKnownImageRect = target.ImageRect;
            _lastKnownBlendMode = target.BlendMode;
        }

        public new Sprite Target => _target;

        #region Property Position

        private Vector2 _lastKnownPosition;

        public static readonly DirectProperty<SpriteView, Vector2> PositionProperty =
            UrhoProperty.RegisterDirect<SpriteView, Vector2>(
                nameof(Position),
                o => o.Position,
                (o, v) => o.Position = v);

        public Vector2 Position
        {
            get => _target.Position;

            set
            {
                SetAndRaise(PositionProperty, _lastKnownPosition, value, _ =>
                {
                    _lastKnownPosition = value;
                    _target.Position = value;
                });
            }
        }

        #endregion Property Position
        #region Property HotSpot

        private IntVector2 _lastKnownHotSpot;

        public static readonly DirectProperty<SpriteView, IntVector2> HotSpotProperty =
            UrhoProperty.RegisterDirect<SpriteView, IntVector2>(
                nameof(HotSpot),
                o => o.HotSpot,
                (o, v) => o.HotSpot = v);

        public IntVector2 HotSpot
        {
            get => _target.HotSpot;

            set
            {
                SetAndRaise(HotSpotProperty, _lastKnownHotSpot, value, _ =>
                {
                    _lastKnownHotSpot = value;
                    _target.HotSpot = value;
                });
            }
        }

        #endregion Property HotSpot
        #region Property Scale

        private Vector2 _lastKnownScale;

        public static readonly DirectProperty<SpriteView, Vector2> ScaleProperty =
            UrhoProperty.RegisterDirect<SpriteView, Vector2>(
                nameof(Scale),
                o => o.Scale,
                (o, v) => o.Scale = v);

        public Vector2 Scale
        {
            get => _target.Scale;

            set
            {
                SetAndRaise(ScaleProperty, _lastKnownScale, value, _ =>
                {
                    _lastKnownScale = value;
                    _target.Scale = value;
                });
            }
        }

        #endregion Property Scale
        #region Property Rotation

        private float _lastKnownRotation;

        public static readonly DirectProperty<SpriteView, float> RotationProperty =
            UrhoProperty.RegisterDirect<SpriteView, float>(
                nameof(Rotation),
                o => o.Rotation,
                (o, v) => o.Rotation = v);

        public float Rotation
        {
            get => _target.Rotation;

            set
            {
                SetAndRaise(RotationProperty, _lastKnownRotation, value, _ =>
                {
                    _lastKnownRotation = value;
                    _target.Rotation = value;
                });
            }
        }

        #endregion Property Rotation
        #region Property ImageRect

        private IntRect _lastKnownImageRect;

        public static readonly DirectProperty<SpriteView, IntRect> ImageRectProperty =
            UrhoProperty.RegisterDirect<SpriteView, IntRect>(
                nameof(ImageRect),
                o => o.ImageRect,
                (o, v) => o.ImageRect = v);

        public IntRect ImageRect
        {
            get => _target.ImageRect;

            set
            {
                SetAndRaise(ImageRectProperty, _lastKnownImageRect, value, _ =>
                {
                    _lastKnownImageRect = value;
                    _target.ImageRect = value;
                });
            }
        }

        #endregion Property ImageRect
        #region Property BlendMode

        private BlendMode _lastKnownBlendMode;

        public static readonly DirectProperty<SpriteView, BlendMode> BlendModeProperty =
            UrhoProperty.RegisterDirect<SpriteView, BlendMode>(
                nameof(BlendMode),
                o => o.BlendMode,
                (o, v) => o.BlendMode = v);

        public BlendMode BlendMode
        {
            get => _target.BlendMode;

            set
            {
                SetAndRaise(BlendModeProperty, _lastKnownBlendMode, value, _ =>
                {
                    _lastKnownBlendMode = value;
                    _target.BlendMode = value;
                });
            }
        }

        #endregion Property BlendMode
    }
    public partial class TextView : UISelectableView
    {
        private Text _target;

        public TextView(Text target): base(target)
        {
            _target = target;
            _lastKnownFontSize = target.FontSize;
            _lastKnownTextAlignment = target.TextAlignment;
            _lastKnownRowSpacing = target.RowSpacing;
            _lastKnownWordwrap = target.Wordwrap;
            _lastKnownAutoLocalizable = target.AutoLocalizable;
            _lastKnownEffectShadowOffset = target.EffectShadowOffset;
            _lastKnownEffectStrokeThickness = target.EffectStrokeThickness;
            _lastKnownEffectRoundStroke = target.EffectRoundStroke;
            _lastKnownEffectColor = target.EffectColor;
            _lastKnownEffectDepthBias = target.EffectDepthBias;
            _lastKnownTextAttr = target.TextAttr;
        }

        public new Text Target => _target;

        #region Property FontSize

        private float _lastKnownFontSize;

        public static readonly DirectProperty<TextView, float> FontSizeProperty =
            UrhoProperty.RegisterDirect<TextView, float>(
                nameof(FontSize),
                o => o.FontSize,
                (o, v) => o.FontSize = v);

        public float FontSize
        {
            get => _target.FontSize;

            set
            {
                SetAndRaise(FontSizeProperty, _lastKnownFontSize, value, _ =>
                {
                    _lastKnownFontSize = value;
                    _target.FontSize = value;
                });
            }
        }

        #endregion Property FontSize
        #region Property TextAlignment

        private HorizontalAlignment _lastKnownTextAlignment;

        public static readonly DirectProperty<TextView, HorizontalAlignment> TextAlignmentProperty =
            UrhoProperty.RegisterDirect<TextView, HorizontalAlignment>(
                nameof(TextAlignment),
                o => o.TextAlignment,
                (o, v) => o.TextAlignment = v);

        public HorizontalAlignment TextAlignment
        {
            get => _target.TextAlignment;

            set
            {
                SetAndRaise(TextAlignmentProperty, _lastKnownTextAlignment, value, _ =>
                {
                    _lastKnownTextAlignment = value;
                    _target.TextAlignment = value;
                });
            }
        }

        #endregion Property TextAlignment
        #region Property RowSpacing

        private float _lastKnownRowSpacing;

        public static readonly DirectProperty<TextView, float> RowSpacingProperty =
            UrhoProperty.RegisterDirect<TextView, float>(
                nameof(RowSpacing),
                o => o.RowSpacing,
                (o, v) => o.RowSpacing = v);

        public float RowSpacing
        {
            get => _target.RowSpacing;

            set
            {
                SetAndRaise(RowSpacingProperty, _lastKnownRowSpacing, value, _ =>
                {
                    _lastKnownRowSpacing = value;
                    _target.RowSpacing = value;
                });
            }
        }

        #endregion Property RowSpacing
        #region Property Wordwrap

        private bool _lastKnownWordwrap;

        public static readonly DirectProperty<TextView, bool> WordwrapProperty =
            UrhoProperty.RegisterDirect<TextView, bool>(
                nameof(Wordwrap),
                o => o.Wordwrap,
                (o, v) => o.Wordwrap = v);

        public bool Wordwrap
        {
            get => _target.Wordwrap;

            set
            {
                SetAndRaise(WordwrapProperty, _lastKnownWordwrap, value, _ =>
                {
                    _lastKnownWordwrap = value;
                    _target.Wordwrap = value;
                });
            }
        }

        #endregion Property Wordwrap
        #region Property AutoLocalizable

        private bool _lastKnownAutoLocalizable;

        public static readonly DirectProperty<TextView, bool> AutoLocalizableProperty =
            UrhoProperty.RegisterDirect<TextView, bool>(
                nameof(AutoLocalizable),
                o => o.AutoLocalizable,
                (o, v) => o.AutoLocalizable = v);

        public bool AutoLocalizable
        {
            get => _target.AutoLocalizable;

            set
            {
                SetAndRaise(AutoLocalizableProperty, _lastKnownAutoLocalizable, value, _ =>
                {
                    _lastKnownAutoLocalizable = value;
                    _target.AutoLocalizable = value;
                });
            }
        }

        #endregion Property AutoLocalizable
        #region Property EffectShadowOffset

        private IntVector2 _lastKnownEffectShadowOffset;

        public static readonly DirectProperty<TextView, IntVector2> EffectShadowOffsetProperty =
            UrhoProperty.RegisterDirect<TextView, IntVector2>(
                nameof(EffectShadowOffset),
                o => o.EffectShadowOffset,
                (o, v) => o.EffectShadowOffset = v);

        public IntVector2 EffectShadowOffset
        {
            get => _target.EffectShadowOffset;

            set
            {
                SetAndRaise(EffectShadowOffsetProperty, _lastKnownEffectShadowOffset, value, _ =>
                {
                    _lastKnownEffectShadowOffset = value;
                    _target.EffectShadowOffset = value;
                });
            }
        }

        #endregion Property EffectShadowOffset
        #region Property EffectStrokeThickness

        private int _lastKnownEffectStrokeThickness;

        public static readonly DirectProperty<TextView, int> EffectStrokeThicknessProperty =
            UrhoProperty.RegisterDirect<TextView, int>(
                nameof(EffectStrokeThickness),
                o => o.EffectStrokeThickness,
                (o, v) => o.EffectStrokeThickness = v);

        public int EffectStrokeThickness
        {
            get => _target.EffectStrokeThickness;

            set
            {
                SetAndRaise(EffectStrokeThicknessProperty, _lastKnownEffectStrokeThickness, value, _ =>
                {
                    _lastKnownEffectStrokeThickness = value;
                    _target.EffectStrokeThickness = value;
                });
            }
        }

        #endregion Property EffectStrokeThickness
        #region Property EffectRoundStroke

        private bool _lastKnownEffectRoundStroke;

        public static readonly DirectProperty<TextView, bool> EffectRoundStrokeProperty =
            UrhoProperty.RegisterDirect<TextView, bool>(
                nameof(EffectRoundStroke),
                o => o.EffectRoundStroke,
                (o, v) => o.EffectRoundStroke = v);

        public bool EffectRoundStroke
        {
            get => _target.EffectRoundStroke;

            set
            {
                SetAndRaise(EffectRoundStrokeProperty, _lastKnownEffectRoundStroke, value, _ =>
                {
                    _lastKnownEffectRoundStroke = value;
                    _target.EffectRoundStroke = value;
                });
            }
        }

        #endregion Property EffectRoundStroke
        #region Property EffectColor

        private Color _lastKnownEffectColor;

        public static readonly DirectProperty<TextView, Color> EffectColorProperty =
            UrhoProperty.RegisterDirect<TextView, Color>(
                nameof(EffectColor),
                o => o.EffectColor,
                (o, v) => o.EffectColor = v);

        public Color EffectColor
        {
            get => _target.EffectColor;

            set
            {
                SetAndRaise(EffectColorProperty, _lastKnownEffectColor, value, _ =>
                {
                    _lastKnownEffectColor = value;
                    _target.EffectColor = value;
                });
            }
        }

        #endregion Property EffectColor
        #region Property EffectDepthBias

        private float _lastKnownEffectDepthBias;

        public static readonly DirectProperty<TextView, float> EffectDepthBiasProperty =
            UrhoProperty.RegisterDirect<TextView, float>(
                nameof(EffectDepthBias),
                o => o.EffectDepthBias,
                (o, v) => o.EffectDepthBias = v);

        public float EffectDepthBias
        {
            get => _target.EffectDepthBias;

            set
            {
                SetAndRaise(EffectDepthBiasProperty, _lastKnownEffectDepthBias, value, _ =>
                {
                    _lastKnownEffectDepthBias = value;
                    _target.EffectDepthBias = value;
                });
            }
        }

        #endregion Property EffectDepthBias
        #region Property TextAttr

        private string _lastKnownTextAttr;

        public static readonly DirectProperty<TextView, string> TextAttrProperty =
            UrhoProperty.RegisterDirect<TextView, string>(
                nameof(TextAttr),
                o => o.TextAttr,
                (o, v) => o.TextAttr = v);

        public string TextAttr
        {
            get => _target.TextAttr;

            set
            {
                SetAndRaise(TextAttrProperty, _lastKnownTextAttr, value, _ =>
                {
                    _lastKnownTextAttr = value;
                    _target.TextAttr = value;
                });
            }
        }

        #endregion Property TextAttr
    }
    public partial class UISelectableView : UIElementView
    {
        private UISelectable _target;

        public UISelectableView(UISelectable target): base(target)
        {
            _target = target;
            _lastKnownSelectionColor = target.SelectionColor;
            _lastKnownHoverColor = target.HoverColor;
        }

        public new UISelectable Target => _target;

        #region Property SelectionColor

        private Color _lastKnownSelectionColor;

        public static readonly DirectProperty<UISelectableView, Color> SelectionColorProperty =
            UrhoProperty.RegisterDirect<UISelectableView, Color>(
                nameof(SelectionColor),
                o => o.SelectionColor,
                (o, v) => o.SelectionColor = v);

        public Color SelectionColor
        {
            get => _target.SelectionColor;

            set
            {
                SetAndRaise(SelectionColorProperty, _lastKnownSelectionColor, value, _ =>
                {
                    _lastKnownSelectionColor = value;
                    _target.SelectionColor = value;
                });
            }
        }

        #endregion Property SelectionColor
        #region Property HoverColor

        private Color _lastKnownHoverColor;

        public static readonly DirectProperty<UISelectableView, Color> HoverColorProperty =
            UrhoProperty.RegisterDirect<UISelectableView, Color>(
                nameof(HoverColor),
                o => o.HoverColor,
                (o, v) => o.HoverColor = v);

        public Color HoverColor
        {
            get => _target.HoverColor;

            set
            {
                SetAndRaise(HoverColorProperty, _lastKnownHoverColor, value, _ =>
                {
                    _lastKnownHoverColor = value;
                    _target.HoverColor = value;
                });
            }
        }

        #endregion Property HoverColor
    }
    public partial class ToolTipView : UIElementView
    {
        private ToolTip _target;

        public ToolTipView(ToolTip target): base(target)
        {
            _target = target;
            _lastKnownDelay = target.Delay;
        }

        public new ToolTip Target => _target;

        #region Property Delay

        private float _lastKnownDelay;

        public static readonly DirectProperty<ToolTipView, float> DelayProperty =
            UrhoProperty.RegisterDirect<ToolTipView, float>(
                nameof(Delay),
                o => o.Delay,
                (o, v) => o.Delay = v);

        public float Delay
        {
            get => _target.Delay;

            set
            {
                SetAndRaise(DelayProperty, _lastKnownDelay, value, _ =>
                {
                    _lastKnownDelay = value;
                    _target.Delay = value;
                });
            }
        }

        #endregion Property Delay
    }
    public partial class View3DView : WindowView
    {
        private View3D _target;

        public View3DView(View3D target): base(target)
        {
            _target = target;
            _lastKnownFormat = target.Format;
            _lastKnownAutoUpdate = target.AutoUpdate;
        }

        public new View3D Target => _target;

        #region Property Format

        private uint _lastKnownFormat;

        public static readonly DirectProperty<View3DView, uint> FormatProperty =
            UrhoProperty.RegisterDirect<View3DView, uint>(
                nameof(Format),
                o => o.Format,
                (o, v) => o.Format = v);

        public uint Format
        {
            get => _target.Format;

            set
            {
                SetAndRaise(FormatProperty, _lastKnownFormat, value, _ =>
                {
                    _lastKnownFormat = value;
                    _target.Format = value;
                });
            }
        }

        #endregion Property Format
        #region Property AutoUpdate

        private bool _lastKnownAutoUpdate;

        public static readonly DirectProperty<View3DView, bool> AutoUpdateProperty =
            UrhoProperty.RegisterDirect<View3DView, bool>(
                nameof(AutoUpdate),
                o => o.AutoUpdate,
                (o, v) => o.AutoUpdate = v);

        public bool AutoUpdate
        {
            get => _target.AutoUpdate;

            set
            {
                SetAndRaise(AutoUpdateProperty, _lastKnownAutoUpdate, value, _ =>
                {
                    _lastKnownAutoUpdate = value;
                    _target.AutoUpdate = value;
                });
            }
        }

        #endregion Property AutoUpdate
    }
    public partial class WindowView : BorderImageView
    {
        private Window _target;

        public WindowView(Window target): base(target)
        {
            _target = target;
            _lastKnownIsMovable = target.IsMovable;
            _lastKnownIsResizable = target.IsResizable;
            _lastKnownFixedWidthResizing = target.FixedWidthResizing;
            _lastKnownFixedHeightResizing = target.FixedHeightResizing;
            _lastKnownResizeBorder = target.ResizeBorder;
            _lastKnownIsModal = target.IsModal;
            _lastKnownModalShadeColor = target.ModalShadeColor;
            _lastKnownModalFrameColor = target.ModalFrameColor;
            _lastKnownModalFrameSize = target.ModalFrameSize;
            _lastKnownModalAutoDismiss = target.ModalAutoDismiss;
        }

        public new Window Target => _target;

        #region Property IsMovable

        private bool _lastKnownIsMovable;

        public static readonly DirectProperty<WindowView, bool> IsMovableProperty =
            UrhoProperty.RegisterDirect<WindowView, bool>(
                nameof(IsMovable),
                o => o.IsMovable,
                (o, v) => o.IsMovable = v);

        public bool IsMovable
        {
            get => _target.IsMovable;

            set
            {
                SetAndRaise(IsMovableProperty, _lastKnownIsMovable, value, _ =>
                {
                    _lastKnownIsMovable = value;
                    _target.IsMovable = value;
                });
            }
        }

        #endregion Property IsMovable
        #region Property IsResizable

        private bool _lastKnownIsResizable;

        public static readonly DirectProperty<WindowView, bool> IsResizableProperty =
            UrhoProperty.RegisterDirect<WindowView, bool>(
                nameof(IsResizable),
                o => o.IsResizable,
                (o, v) => o.IsResizable = v);

        public bool IsResizable
        {
            get => _target.IsResizable;

            set
            {
                SetAndRaise(IsResizableProperty, _lastKnownIsResizable, value, _ =>
                {
                    _lastKnownIsResizable = value;
                    _target.IsResizable = value;
                });
            }
        }

        #endregion Property IsResizable
        #region Property FixedWidthResizing

        private bool _lastKnownFixedWidthResizing;

        public static readonly DirectProperty<WindowView, bool> FixedWidthResizingProperty =
            UrhoProperty.RegisterDirect<WindowView, bool>(
                nameof(FixedWidthResizing),
                o => o.FixedWidthResizing,
                (o, v) => o.FixedWidthResizing = v);

        public bool FixedWidthResizing
        {
            get => _target.FixedWidthResizing;

            set
            {
                SetAndRaise(FixedWidthResizingProperty, _lastKnownFixedWidthResizing, value, _ =>
                {
                    _lastKnownFixedWidthResizing = value;
                    _target.FixedWidthResizing = value;
                });
            }
        }

        #endregion Property FixedWidthResizing
        #region Property FixedHeightResizing

        private bool _lastKnownFixedHeightResizing;

        public static readonly DirectProperty<WindowView, bool> FixedHeightResizingProperty =
            UrhoProperty.RegisterDirect<WindowView, bool>(
                nameof(FixedHeightResizing),
                o => o.FixedHeightResizing,
                (o, v) => o.FixedHeightResizing = v);

        public bool FixedHeightResizing
        {
            get => _target.FixedHeightResizing;

            set
            {
                SetAndRaise(FixedHeightResizingProperty, _lastKnownFixedHeightResizing, value, _ =>
                {
                    _lastKnownFixedHeightResizing = value;
                    _target.FixedHeightResizing = value;
                });
            }
        }

        #endregion Property FixedHeightResizing
        #region Property ResizeBorder

        private IntRect _lastKnownResizeBorder;

        public static readonly DirectProperty<WindowView, IntRect> ResizeBorderProperty =
            UrhoProperty.RegisterDirect<WindowView, IntRect>(
                nameof(ResizeBorder),
                o => o.ResizeBorder,
                (o, v) => o.ResizeBorder = v);

        public IntRect ResizeBorder
        {
            get => _target.ResizeBorder;

            set
            {
                SetAndRaise(ResizeBorderProperty, _lastKnownResizeBorder, value, _ =>
                {
                    _lastKnownResizeBorder = value;
                    _target.ResizeBorder = value;
                });
            }
        }

        #endregion Property ResizeBorder
        #region Property IsModal

        private bool _lastKnownIsModal;

        public static readonly DirectProperty<WindowView, bool> IsModalProperty =
            UrhoProperty.RegisterDirect<WindowView, bool>(
                nameof(IsModal),
                o => o.IsModal,
                (o, v) => o.IsModal = v);

        public bool IsModal
        {
            get => _target.IsModal;

            set
            {
                SetAndRaise(IsModalProperty, _lastKnownIsModal, value, _ =>
                {
                    _lastKnownIsModal = value;
                    _target.IsModal = value;
                });
            }
        }

        #endregion Property IsModal
        #region Property ModalShadeColor

        private Color _lastKnownModalShadeColor;

        public static readonly DirectProperty<WindowView, Color> ModalShadeColorProperty =
            UrhoProperty.RegisterDirect<WindowView, Color>(
                nameof(ModalShadeColor),
                o => o.ModalShadeColor,
                (o, v) => o.ModalShadeColor = v);

        public Color ModalShadeColor
        {
            get => _target.ModalShadeColor;

            set
            {
                SetAndRaise(ModalShadeColorProperty, _lastKnownModalShadeColor, value, _ =>
                {
                    _lastKnownModalShadeColor = value;
                    _target.ModalShadeColor = value;
                });
            }
        }

        #endregion Property ModalShadeColor
        #region Property ModalFrameColor

        private Color _lastKnownModalFrameColor;

        public static readonly DirectProperty<WindowView, Color> ModalFrameColorProperty =
            UrhoProperty.RegisterDirect<WindowView, Color>(
                nameof(ModalFrameColor),
                o => o.ModalFrameColor,
                (o, v) => o.ModalFrameColor = v);

        public Color ModalFrameColor
        {
            get => _target.ModalFrameColor;

            set
            {
                SetAndRaise(ModalFrameColorProperty, _lastKnownModalFrameColor, value, _ =>
                {
                    _lastKnownModalFrameColor = value;
                    _target.ModalFrameColor = value;
                });
            }
        }

        #endregion Property ModalFrameColor
        #region Property ModalFrameSize

        private IntVector2 _lastKnownModalFrameSize;

        public static readonly DirectProperty<WindowView, IntVector2> ModalFrameSizeProperty =
            UrhoProperty.RegisterDirect<WindowView, IntVector2>(
                nameof(ModalFrameSize),
                o => o.ModalFrameSize,
                (o, v) => o.ModalFrameSize = v);

        public IntVector2 ModalFrameSize
        {
            get => _target.ModalFrameSize;

            set
            {
                SetAndRaise(ModalFrameSizeProperty, _lastKnownModalFrameSize, value, _ =>
                {
                    _lastKnownModalFrameSize = value;
                    _target.ModalFrameSize = value;
                });
            }
        }

        #endregion Property ModalFrameSize
        #region Property ModalAutoDismiss

        private bool _lastKnownModalAutoDismiss;

        public static readonly DirectProperty<WindowView, bool> ModalAutoDismissProperty =
            UrhoProperty.RegisterDirect<WindowView, bool>(
                nameof(ModalAutoDismiss),
                o => o.ModalAutoDismiss,
                (o, v) => o.ModalAutoDismiss = v);

        public bool ModalAutoDismiss
        {
            get => _target.ModalAutoDismiss;

            set
            {
                SetAndRaise(ModalAutoDismissProperty, _lastKnownModalAutoDismiss, value, _ =>
                {
                    _lastKnownModalAutoDismiss = value;
                    _target.ModalAutoDismiss = value;
                });
            }
        }

        #endregion Property ModalAutoDismiss
    }
}