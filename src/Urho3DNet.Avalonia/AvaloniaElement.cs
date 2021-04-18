﻿using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Urho3DNet.AvaliniaAdapter;
using Urho3DNet.InputEvents;

namespace Urho3DNet
{
    public class AvaloniaElement : Sprite
    {
        private Avalonia.Controls.Window _window;
        private UrhoTopLevelImpl _windowImpl;

        public AvaloniaElement(Context context) : base(context)
        {
            SetEnabledRecursive(true);
            IsEnabled = true;
            IsVisible = true;
            FocusMode = FocusMode.FmFocusable;


            _coreEventsAdapter = new CoreEventsAdapter(this);
            _coreEventsAdapter.Update += HandleUpdate;
            // Temporal workaround while virtual method override is broken
            _eventAdapter = new UIEventsAdapter(this);
            _eventAdapter.ClickEnd += HandleClickEnd;
            _eventAdapter.Click += HandleClickBegin;
            _eventAdapter.DragMove += HandleDragMove;
            //_eventAdapter.HoverBegin += HandleHoverBegin;
            //_eventAdapter.HoverEnd += HandleHoverEnd;
        }

        private void HandleUpdate(object sender, CoreEventsAdapter.UpdateEventArgs e)
        {
            var size = this.Size;
            _windowImpl.Resize(new Avalonia.Size(size.X, size.Y));
        }

        private void HandleDragMove(object sender, UIEventsAdapter.DragMoveEventArgs e)
        {
            if (e.Element != this)
                return;
            var screenPos = this.ScreenPosition;
            var position = new IntVector2(e.X - screenPos.X, e.Y - screenPos.Y);
            SendRawEvent(RawPointerEventType.Move, position);
        }

        private void HandleClickEnd(object sender, UIEventsAdapter.ClickEndEventArgs e)
        {
            if (e.Element != this)
                return;
            var screenPos = this.ScreenPosition;
            var position = new IntVector2(e.X- screenPos.X, e.Y- screenPos.Y);
            switch ((MouseButton)e.Button)
            {
                case MouseButton.MousebLeft:
                    _inputModifiers.Set(RawInputModifiers.LeftMouseButton);
                    SendRawEvent(RawPointerEventType.LeftButtonUp, position);
                    break;
                case MouseButton.MousebMiddle:
                    _inputModifiers.Set(RawInputModifiers.MiddleMouseButton);
                    SendRawEvent(RawPointerEventType.MiddleButtonUp, position);
                    break;
                case MouseButton.MousebRight:
                    _inputModifiers.Set(RawInputModifiers.RightMouseButton);
                    SendRawEvent(RawPointerEventType.RightButtonUp, position);
                    break;
                case MouseButton.MousebX1:
                    _inputModifiers.Set(RawInputModifiers.XButton1MouseButton);
                    SendRawEvent(RawPointerEventType.XButton1Up, position);
                    break;
                case MouseButton.MousebX2:
                    _inputModifiers.Set(RawInputModifiers.XButton2MouseButton);
                    SendRawEvent(RawPointerEventType.XButton2Up, position);
                    break;
            }
        }

        private void HandleClickBegin(object sender, UIEventsAdapter.ClickEventArgs e)
        {
            if (e.Element != this)
                return;
            var screenPos = this.ScreenPosition;
            var position = new IntVector2(e.X - screenPos.X, e.Y - screenPos.Y);
            switch ((MouseButton)e.Button)
            {
                case MouseButton.MousebLeft:
                    _inputModifiers.Set(RawInputModifiers.LeftMouseButton);
                    SendRawEvent(RawPointerEventType.LeftButtonDown, position);
                    break;
                case MouseButton.MousebMiddle:
                    _inputModifiers.Set(RawInputModifiers.MiddleMouseButton);
                    SendRawEvent(RawPointerEventType.MiddleButtonDown, position);
                    break;
                case MouseButton.MousebRight:
                    _inputModifiers.Set(RawInputModifiers.RightMouseButton);
                    SendRawEvent(RawPointerEventType.RightButtonDown, position);
                    break;
                case MouseButton.MousebX1:
                    _inputModifiers.Set(RawInputModifiers.XButton1MouseButton);
                    SendRawEvent(RawPointerEventType.XButton1Down, position);
                    break;
                case MouseButton.MousebX2:
                    _inputModifiers.Set(RawInputModifiers.XButton2MouseButton);
                    SendRawEvent(RawPointerEventType.XButton2Down, position);
                    break;
            }
        }

        public Avalonia.Controls.Window Canvas
        {
            get
            {
                return _window;
            }
            set
            {
                if (_window != value)
                {
                    _window = value;
                    _windowImpl = _window.PlatformImpl as UrhoTopLevelImpl;
                    if (_windowImpl != null)
                    {
                        var size = _windowImpl.VisibleSize;
                        ImageRect = new IntRect(0, 0, size.X, size.Y);
                        Size = size;
                        Texture = _windowImpl.Texture;
                    }
                }
            }
        }

        public override void Update(float timeStep)
        {
            base.Update(timeStep);
        }

        //public override void OnHover(IntVector2 position, IntVector2 screenPosition, MouseButton buttons, Qualifier qualifiers, Cursor cursor)
        //{
        //    base.OnHover(position, screenPosition, buttons, qualifiers, cursor);
        //}

        //public override void OnDragMove(IntVector2 position, IntVector2 screenPosition, IntVector2 deltaPos, MouseButton buttons,
        //    Qualifier qualifiers, Cursor cursor)
        //{
        //    base.OnDragMove(position, screenPosition, deltaPos, buttons, qualifiers, cursor);
        //    SendRawEvent(RawPointerEventType.Move, position);
        //}

        //public override void OnClickBegin(IntVector2 position, IntVector2 screenPosition, MouseButton button, MouseButton buttons,
        //    Qualifier qualifiers, Cursor cursor)
        //{
        //    switch (button)
        //    {
        //        case MouseButton.MousebLeft:
        //            _inputModifiers.Set(RawInputModifiers.LeftMouseButton);
        //            SendRawEvent(RawPointerEventType.LeftButtonDown, position);
        //            break;
        //        case MouseButton.MousebMiddle:
        //            _inputModifiers.Set(RawInputModifiers.MiddleMouseButton);
        //            SendRawEvent(RawPointerEventType.MiddleButtonDown, position);
        //            break;
        //        case MouseButton.MousebRight:
        //            _inputModifiers.Set(RawInputModifiers.RightMouseButton);
        //            SendRawEvent(RawPointerEventType.RightButtonDown, position);
        //            break;
        //        case MouseButton.MousebX1:
        //            _inputModifiers.Set(RawInputModifiers.XButton1MouseButton);
        //            SendRawEvent(RawPointerEventType.XButton1Down, position);
        //            break;
        //        case MouseButton.MousebX2:
        //            _inputModifiers.Set(RawInputModifiers.XButton2MouseButton);
        //            SendRawEvent(RawPointerEventType.XButton2Down, position);
        //            break;
        //    }
        //}

        //public override void OnClickEnd(IntVector2 position, IntVector2 screenPosition, MouseButton button, MouseButton buttons,
        //    Qualifier qualifiers, Cursor cursor, UIElement beginElement)
        //{
        //    switch (button)
        //    {
        //        case MouseButton.MousebLeft:
        //            _inputModifiers.Drop(RawInputModifiers.LeftMouseButton);
        //            SendRawEvent(RawPointerEventType.LeftButtonUp, position);
        //            break;
        //        case MouseButton.MousebMiddle:
        //            _inputModifiers.Drop(RawInputModifiers.MiddleMouseButton);
        //            SendRawEvent(RawPointerEventType.MiddleButtonUp, position);
        //            break;
        //        case MouseButton.MousebRight:
        //            _inputModifiers.Drop(RawInputModifiers.RightMouseButton);
        //            SendRawEvent(RawPointerEventType.RightButtonUp, position);
        //            break;
        //        case MouseButton.MousebX1:
        //            _inputModifiers.Drop(RawInputModifiers.XButton1MouseButton);
        //            SendRawEvent(RawPointerEventType.XButton1Up, position);
        //            break;
        //        case MouseButton.MousebX2:
        //            _inputModifiers.Drop(RawInputModifiers.XButton2MouseButton);
        //            SendRawEvent(RawPointerEventType.XButton2Up, position);
        //            break;
        //    }
        //}

        private void SendRawEvent(RawPointerEventType type, IntVector2 position)
        {
            if (_windowImpl != null)
            {
                _windowImpl.Input(new RawPointerEventArgs(_windowImpl.MouseDevice, (ulong) DateTimeOffset.UtcNow.Ticks,
                    _windowImpl.InputRoot, type, new Point(position.X, position.Y),
                    _inputModifiers.Modifiers));
            }
        }

        private InputModifiersContainer _inputModifiers = new InputModifiersContainer();
        private UIEventsAdapter _eventAdapter;
        private CoreEventsAdapter _coreEventsAdapter;
    }
}