using System.Collections;
using System.Collections.Generic;

namespace Urho3DNet.InputEvents
{
    public class KeyActionCollection<T> : IDictionary<T, IKeyAction>
    {
        private readonly Dictionary<T, IKeyAction> _mapping = new Dictionary<T, IKeyAction>();

        public ICollection<T> Keys => _mapping.Keys;

        public ICollection<IKeyAction> Values => _mapping.Values;

        int ICollection<KeyValuePair<T, IKeyAction>>.Count => _mapping.Count;

        bool ICollection<KeyValuePair<T, IKeyAction>>.IsReadOnly => false;

        public IKeyAction this[T key]
        {
            get => _mapping[key];
            set => _mapping[key] = value;
        }

        public void Add(T key, IKeyAction value)
        {
            _mapping.Add(key, value);
        }

        public bool ContainsKey(T key)
        {
            return _mapping.ContainsKey(key);
        }

        public bool Remove(T key)
        {
            return _mapping.Remove(key);
        }

        public bool TryGetValue(T key, out IKeyAction value)
        {
            return _mapping.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<T, IKeyAction>> GetEnumerator()
        {
            return _mapping.GetEnumerator();
        }

        void ICollection<KeyValuePair<T, IKeyAction>>.Add(KeyValuePair<T, IKeyAction> item)
        {
            ((ICollection<KeyValuePair<T, IKeyAction>>) _mapping).Add(item);
        }

        void ICollection<KeyValuePair<T, IKeyAction>>.Clear()
        {
            _mapping.Clear();
        }

        bool ICollection<KeyValuePair<T, IKeyAction>>.Contains(KeyValuePair<T, IKeyAction> item)
        {
            return ((ICollection<KeyValuePair<T, IKeyAction>>) _mapping).Contains(item);
        }

        void ICollection<KeyValuePair<T, IKeyAction>>.CopyTo(KeyValuePair<T, IKeyAction>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<T, IKeyAction>>) _mapping).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<T, IKeyAction>>.Remove(KeyValuePair<T, IKeyAction> item)
        {
            return ((ICollection<KeyValuePair<T, IKeyAction>>) _mapping).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _mapping).GetEnumerator();
        }
    }
}