using System;

namespace Urho3DNet.InputEvents
{
    public class AxisToKeyTranslator
    {
        private readonly UniKey _neg;
        private readonly UniKey _pos;
        private readonly Action<object, KeyEventArgs> _keyPressed;
        private readonly Action<object, KeyEventArgs> _keyReleased;
        private readonly Action<object, KeyEventArgs> _keyCanceled;
        private int _prevSign;
        private bool _preparing;

        public AxisToKeyTranslator(UniKey neg, UniKey pos, IInputListener listener)
            :this(neg,pos, listener.OnKeyboardButtonDown, listener.OnKeyboardButtonUp, listener.OnKeyboardButtonCanceled)
        {

        }
        public AxisToKeyTranslator(UniKey neg, UniKey pos, 
            Action<object, KeyEventArgs> keyPressed,
            Action<object, KeyEventArgs> keyReleased,
            Action<object, KeyEventArgs> keyCanceled)
        {
            _neg = neg;
            _pos = pos;
            _keyPressed = keyPressed;
            _keyReleased = keyReleased;
            _keyCanceled = keyCanceled;
            _preparing = true;
        }

        public void Reset()
        {
            _preparing = true;
            if (_prevSign != 0)
            {
                var eventArgsPool = EventArgsPool<KeyEventArgs>.Default;
                var eventArgs = eventArgsPool.Borrow();
                switch (_prevSign)
                {
                    case -1:
                        eventArgs.Init(_neg);
                        _keyCanceled(this, eventArgs);
                        break;
                    case 1:
                        eventArgs.Init(_pos);
                        _keyCanceled(this, eventArgs);
                        break;
                }

                eventArgsPool.Return(eventArgs);
            }

            _prevSign = 0;
        }
        
        public void OnAxisMoved(object sender, AxisEventArgs args)
        {
            float threshold = (_prevSign == 0) ? 0.6f : 0.4f;
            int newSign = (args.Value < -threshold)?-1:((args.Value > threshold) ? 1: 0);
            if (newSign == 0)
                _preparing = false;
            if (_prevSign != newSign && !_preparing)
            {
                var eventArgsPool = EventArgsPool<KeyEventArgs>.Default;
                var eventArgs = eventArgsPool.Borrow();
                switch (_prevSign)
                {
                    case -1:
                        eventArgs.Init(_neg);
                        _keyReleased(this, eventArgs);
                        break;
                    case 1:
                        eventArgs.Init(_pos);
                        _keyReleased(this, eventArgs);
                        break;
                }
                _prevSign = newSign;
                switch (newSign)
                {
                    case -1:
                        eventArgs.Init(_neg);
                        _keyPressed(this, eventArgs);
                        break;
                    case 1:
                        eventArgs.Init(_pos);
                        _keyPressed(this, eventArgs);
                        break;
                }
                eventArgsPool.Return(eventArgs);
            }
        }
    }
}