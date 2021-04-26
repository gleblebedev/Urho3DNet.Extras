using System;
using System.Collections;
using System.Collections.Generic;

namespace Urho3DNet.InputEvents
{
    public class KeyActionMapping<T> : IInputListener, IDictionary<UniKey, T>
    {
        private readonly IDictionary<T, IKeyAction> _actions;

        private readonly Dictionary<UniKey, T> _mapping = new Dictionary<UniKey, T>();

        public KeyActionMapping(IDictionary<T, IKeyAction> actions)
        {
            _actions = actions;
        }

        public KeyActionMapping(params KeyValuePair<T, IKeyAction>[] actions)
        {
            _actions = new KeyActionCollection<T>();
            foreach (var keyValuePair in actions) _actions.Add(keyValuePair);
        }

        public KeyActionMapping(params ValueTuple<T, IKeyAction>[] actions)
        {
            _actions = new KeyActionCollection<T>();
            foreach (var keyValuePair in actions) _actions.Add(keyValuePair.Item1, keyValuePair.Item2);
        }

        public KeyActionMapping(params Tuple<T, IKeyAction>[] actions)
        {
            _actions = new KeyActionCollection<T>();
            foreach (var keyValuePair in actions) _actions.Add(keyValuePair.Item1, keyValuePair.Item2);
        }

        int ICollection<KeyValuePair<UniKey, T>>.Count => _mapping.Count;

        bool ICollection<KeyValuePair<UniKey, T>>.IsReadOnly =>
            ((ICollection<KeyValuePair<UniKey, T>>) _mapping).IsReadOnly;

        ICollection<UniKey> IDictionary<UniKey, T>.Keys => _mapping.Keys;

        ICollection<T> IDictionary<UniKey, T>.Values => _mapping.Values;

        public T this[UniKey key]
        {
            get => _mapping[key];
            set => _mapping[key] = value;
        }

        public IKeyAction GetAction(UniKey key)
        {
            if (_mapping.TryGetValue(key, out var actionKey))
                if (_actions.TryGetValue(actionKey, out var action))
                    return action;
            return null;
        }

        public void Add(UniKey key, T value)
        {
            _mapping.Add(key, value);
        }

        public bool ContainsKey(UniKey key)
        {
            return _mapping.ContainsKey(key);
        }

        public bool Remove(UniKey key)
        {
            return _mapping.Remove(key);
        }

        public bool TryGetValue(UniKey key, out T value)
        {
            return _mapping.TryGetValue(key, out value);
        }

        public void OnGamepadButtonUp(object sender, KeyEventArgs args)
        {
            GetAction(args.Key)?.Stop(args.DeviceId);
        }

        public void OnGamepadButtonDown(object sender, KeyEventArgs args)
        {
            GetAction(args.Key)?.Start(args.DeviceId);
        }

        public void OnGamepadButtonCanceled(object sender, KeyEventArgs args)
        {
            GetAction(args.Key)?.Cancel(args.DeviceId);
        }

        public void OnKeyboardButtonUp(object sender, KeyEventArgs args)
        {
            GetAction(args.Key)?.Stop(args.DeviceId);
        }

        public void OnKeyboardButtonDown(object sender, KeyEventArgs args)
        {
            GetAction(args.Key)?.Start(args.DeviceId);
        }

        public void OnKeyboardButtonCanceled(object sender, KeyEventArgs args)
        {
            GetAction(args.Key)?.Cancel(args.DeviceId);
        }

        public void OnMouseButtonUp(object sender, KeyEventArgs args)
        {
            GetAction(args.Key)?.Stop(args.DeviceId);
        }

        public void OnMouseButtonDown(object sender, KeyEventArgs args)
        {
            GetAction(args.Key)?.Start(args.DeviceId);
        }

        public void OnMouseButtonCanceled(object sender, KeyEventArgs args)
        {
            GetAction(args.Key)?.Cancel(args.DeviceId);
        }

        void ICollection<KeyValuePair<UniKey, T>>.Add(KeyValuePair<UniKey, T> item)
        {
            ((ICollection<KeyValuePair<UniKey, T>>) _mapping).Add(item);
        }

        void ICollection<KeyValuePair<UniKey, T>>.Clear()
        {
            _mapping.Clear();
        }

        bool ICollection<KeyValuePair<UniKey, T>>.Contains(KeyValuePair<UniKey, T> item)
        {
            return ((ICollection<KeyValuePair<UniKey, T>>) _mapping).Contains(item);
        }

        void ICollection<KeyValuePair<UniKey, T>>.CopyTo(KeyValuePair<UniKey, T>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<UniKey, T>>) _mapping).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<UniKey, T>>.Remove(KeyValuePair<UniKey, T> item)
        {
            return ((ICollection<KeyValuePair<UniKey, T>>) _mapping).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _mapping).GetEnumerator();
        }

        IEnumerator<KeyValuePair<UniKey, T>> IEnumerable<KeyValuePair<UniKey, T>>.GetEnumerator()
        {
            return _mapping.GetEnumerator();
        }

        void IInputListener.OnGamepadAxisMoved(object sender, AxisEventArgs args)
        {
        }

        void IInputListener.OnGamepadDeviceConnected(object sender, DeviceEventArgs args)
        {
        }

        void IInputListener.OnGamepadDeviceDisconnected(object sender, DeviceEventArgs args)
        {
        }

        void IInputListener.OnMousePointerMoved(object sender, PointerEventArgs args)
        {
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

        void IInputListener.ListenerSubscribed(IInputSource container)
        {
        }

        void IInputListener.ListenerUnsubscribed(IInputSource container)
        {
        }
    }
}