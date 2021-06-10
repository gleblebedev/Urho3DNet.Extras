using Urho3DNet.MVVM.Binding;

namespace Urho3DNet.MVVM
{
    public partial class SliderView : BorderImageView
    {
        private Slider _target;

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
    public partial class BorderImageView : UIElementView
    {
        private BorderImage _target;

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
    public partial class UIElementView : AnimatableView
    {
        private UIElement _target;

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
        #region Property HorizontalAlignment

        private HorizontalAlignment _lastKnownHorizontalAlignment;

        public static readonly DirectProperty<UIElementView, HorizontalAlignment> HorizontalAlignmentProperty =
            UrhoProperty.RegisterDirect<UIElementView, HorizontalAlignment>(
                nameof(HorizontalAlignment),
                o => o.HorizontalAlignment,
                (o, v) => o.HorizontalAlignment = v);

        public HorizontalAlignment HorizontalAlignment
        {
            get => _target.HorizontalAlignment;

            set
            {
                SetAndRaise(HorizontalAlignmentProperty, _lastKnownHorizontalAlignment, value, _ =>
                {
                    _lastKnownHorizontalAlignment = value;
                    _target.HorizontalAlignment = value;
                });
            }
        }

        #endregion Property HorizontalAlignment
        #region Property VerticalAlignment

        private VerticalAlignment _lastKnownVerticalAlignment;

        public static readonly DirectProperty<UIElementView, VerticalAlignment> VerticalAlignmentProperty =
            UrhoProperty.RegisterDirect<UIElementView, VerticalAlignment>(
                nameof(VerticalAlignment),
                o => o.VerticalAlignment,
                (o, v) => o.VerticalAlignment = v);

        public VerticalAlignment VerticalAlignment
        {
            get => _target.VerticalAlignment;

            set
            {
                SetAndRaise(VerticalAlignmentProperty, _lastKnownVerticalAlignment, value, _ =>
                {
                    _lastKnownVerticalAlignment = value;
                    _target.VerticalAlignment = value;
                });
            }
        }

        #endregion Property VerticalAlignment
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

        public bool IsVisible
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
}