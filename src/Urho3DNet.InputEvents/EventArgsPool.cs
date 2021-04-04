using System;
using System.Collections.Concurrent;

namespace Urho3DNet.InputEvents
{
    public class EventArgsPool<T> where T: EventArgs, new()
    {
        public static readonly EventArgsPool<T> Default = new EventArgsPool<T>();
        
        public ConcurrentQueue<T> _pool = new ConcurrentQueue<T>();

        public T Borrow()
        {
            if (_pool.TryDequeue(out T obj))
            {
                return obj;
            }

            return new T();
        }

        public void Return(T instance)
        {
            _pool.Enqueue(instance);
        }
    }
}