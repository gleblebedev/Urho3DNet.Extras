using System;
using Urho3DNet.MVVM.Layout;

namespace Urho3DNet.MVVM
{
    public partial class UIElementView
    {
        #region Property HorizontalAlignment

        private Layout.HorizontalAlignment _lastKnownHorizontalAlignment;

        public override Layout.HorizontalAlignment HorizontalAlignment
        {
            get
            {
                switch (_target.HorizontalAlignment)
                {
                    case Urho3DNet.HorizontalAlignment.HaLeft:
                        return Layout.HorizontalAlignment.Left;
                    case Urho3DNet.HorizontalAlignment.HaCenter:
                        return Layout.HorizontalAlignment.Center;
                    case Urho3DNet.HorizontalAlignment.HaRight:
                        return Layout.HorizontalAlignment.Right;
                    case Urho3DNet.HorizontalAlignment.HaCustom:
                        return Layout.HorizontalAlignment.Stretch;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            set
            {
                SetAndRaise(HorizontalAlignmentProperty, _lastKnownHorizontalAlignment, value, _ =>
                {
                    _lastKnownHorizontalAlignment = value;
                    switch (value)
                    {
                        case Layout.HorizontalAlignment.Stretch:
                            _target.HorizontalAlignment = Urho3DNet.HorizontalAlignment.HaCustom;
                            break;
                        case Layout.HorizontalAlignment.Left:
                            _target.HorizontalAlignment = Urho3DNet.HorizontalAlignment.HaLeft;
                            break;
                        case Layout.HorizontalAlignment.Center:
                            _target.HorizontalAlignment = Urho3DNet.HorizontalAlignment.HaCenter;
                            break;
                        case Layout.HorizontalAlignment.Right:
                            _target.HorizontalAlignment = Urho3DNet.HorizontalAlignment.HaRight;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(value), value, null);
                    }
                });
            }
        }

        #endregion Property HorizontalAlignment
        #region Property VerticalAlignment

        private Layout.VerticalAlignment _lastKnownVerticalAlignment;

        public override Layout.VerticalAlignment VerticalAlignment
        {
            get
            {
                switch (_target.VerticalAlignment)
                {
                    case Urho3DNet.VerticalAlignment.VaTop:
                        return Layout.VerticalAlignment.Top;
                    case Urho3DNet.VerticalAlignment.VaCenter:
                        return Layout.VerticalAlignment.Center;
                    case Urho3DNet.VerticalAlignment.VaBottom:
                        return Layout.VerticalAlignment.Bottom;
                    case Urho3DNet.VerticalAlignment.VaCustom:
                        return Layout.VerticalAlignment.Stretch;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            set
            {
                SetAndRaise(VerticalAlignmentProperty, _lastKnownVerticalAlignment, value, _ =>
                {
                    _lastKnownVerticalAlignment = value;
                    switch (value)
                    {
                        case Layout.VerticalAlignment.Stretch:
                            _target.VerticalAlignment = Urho3DNet.VerticalAlignment.VaCustom;
                            break;
                        case Layout.VerticalAlignment.Top:
                            _target.VerticalAlignment = Urho3DNet.VerticalAlignment.VaTop;
                            break;
                        case Layout.VerticalAlignment.Center:
                            _target.VerticalAlignment = Urho3DNet.VerticalAlignment.VaCenter;
                            break;
                        case Layout.VerticalAlignment.Bottom:
                            _target.VerticalAlignment = Urho3DNet.VerticalAlignment.VaBottom;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(value), value, null);
                    }
                });
            }
        }

        #endregion Property VerticalAlignment

        partial void PartialInit(UIElement target)
        {
            _lastKnownHorizontalAlignment = HorizontalAlignment;
            _lastKnownVerticalAlignment = VerticalAlignment;
        }
    }
}