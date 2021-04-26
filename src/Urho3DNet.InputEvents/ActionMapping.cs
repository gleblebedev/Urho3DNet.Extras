using System.Collections;
using System.Collections.Generic;

namespace Urho3DNet.InputEvents
{
    public class AxisActionMapping<T> : IInputListener, IDictionary<UniAxis, T>
    {
        private readonly IDictionary<T, IAxisAction> _actions;

        private readonly Dictionary<UniAxis, T> _mapping = new Dictionary<UniAxis, T>();

        public AxisActionMapping(IDictionary<T, IAxisAction> actions)
        {
            _actions = actions;
        }

        public int Count => _mapping.Count;

        public ICollection<UniAxis> Keys => _mapping.Keys;

        public ICollection<T> Values => _mapping.Values;

        bool ICollection<KeyValuePair<UniAxis, T>>.IsReadOnly => false;

        public T this[UniAxis key]
        {
            get => _mapping[key];
            set => _mapping[key] = value;
        }

        public void Clear()
        {
            _mapping.Clear();
        }

        public void Add(UniAxis key, T value)
        {
            _mapping.Add(key, value);
        }

        public bool ContainsKey(UniAxis key)
        {
            return _mapping.ContainsKey(key);
        }

        public bool Remove(UniAxis key)
        {
            return _mapping.Remove(key);
        }

        public bool TryGetValue(UniAxis key, out T value)
        {
            return _mapping.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<UniAxis, T>> GetEnumerator()
        {
            return _mapping.GetEnumerator();
        }

        void ICollection<KeyValuePair<UniAxis, T>>.Add(KeyValuePair<UniAxis, T> item)
        {
            ((ICollection<KeyValuePair<UniAxis, T>>) _mapping).Add(item);
        }

        bool ICollection<KeyValuePair<UniAxis, T>>.Contains(KeyValuePair<UniAxis, T> item)
        {
            return ((ICollection<KeyValuePair<UniAxis, T>>) _mapping).Contains(item);
        }

        void ICollection<KeyValuePair<UniAxis, T>>.CopyTo(KeyValuePair<UniAxis, T>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<UniAxis, T>>) _mapping).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<UniAxis, T>>.Remove(KeyValuePair<UniAxis, T> item)
        {
            return ((ICollection<KeyValuePair<UniAxis, T>>) _mapping).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _mapping).GetEnumerator();
        }

        void IInputListener.OnTouchscreenTouchBegin(object sender, TouchEventArgs args)
        {
        }

        void IInputListener.OnTouchscreenTouchEnd(object sender, TouchEventArgs args)
        {
        }

        void IInputListener.OnTouchscreenTouchMoved(object sender, TouchEventArgs args)
        {
        }

        void IInputListener.OnTouchscreenTouchCanceled(object sender, TouchEventArgs args)
        {
        }

        void IInputListener.OnMousePointerMoved(object sender, PointerEventArgs args)
        {
        }

        void IInputListener.OnMouseButtonUp(object sender, KeyEventArgs args)
        {
        }

        void IInputListener.OnMouseButtonDown(object sender, KeyEventArgs args)
        {
        }

        void IInputListener.OnMouseButtonCanceled(object sender, KeyEventArgs args)
        {
        }

        void IInputListener.OnGamepadAxisMoved(object sender, AxisEventArgs args)
        {
            if (_mapping.TryGetValue(args.Axis, out var key))
                if (_actions.TryGetValue(key, out var action))
                    action?.Update(args.DeviceId, args.Value);
        }

        void IInputListener.OnGamepadButtonUp(object sender, KeyEventArgs args)
        {
        }

        void IInputListener.OnGamepadButtonDown(object sender, KeyEventArgs args)
        {
        }

        void IInputListener.OnGamepadButtonCanceled(object sender, KeyEventArgs args)
        {
        }

        void IInputListener.OnGamepadDeviceConnected(object sender, DeviceEventArgs args)
        {
        }

        void IInputListener.OnGamepadDeviceDisconnected(object sender, DeviceEventArgs args)
        {
        }

        void IInputListener.OnKeyboardButtonUp(object sender, KeyEventArgs args)
        {
        }

        void IInputListener.OnKeyboardButtonDown(object sender, KeyEventArgs args)
        {
        }

        void IInputListener.OnKeyboardButtonCanceled(object sender, KeyEventArgs args)
        {
        }

        void IInputListener.ListenerSubscribed(IInputSource container)
        {
        }

        void IInputListener.ListenerUnsubscribed(IInputSource container)
        {
        }
    }
}