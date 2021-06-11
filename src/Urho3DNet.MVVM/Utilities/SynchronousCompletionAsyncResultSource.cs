using System;
using System.Collections.Generic;

namespace Urho3DNet.MVVM.Utilities
{
    /// <summary>
    /// Source for incomplete SynchronousCompletionAsyncResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SynchronousCompletionAsyncResultSource<T>
    {
        private T _result;
        internal bool IsCompleted { get; private set; }
        public SynchronousCompletionAsyncResult<T> AsyncResult => new SynchronousCompletionAsyncResult<T>(this);

        internal T Result => IsCompleted ?
            _result :
            throw new InvalidOperationException("Asynchronous operation is not yet completed");

        private List<Action> _continuations;

        internal void OnCompleted(Action continuation)
        {
            if (_continuations == null)
                _continuations = new List<Action>();
            _continuations.Add(continuation);
        }

        public void SetResult(T result)
        {
            if (IsCompleted)
                throw new InvalidOperationException("Asynchronous operation is already completed");
            _result = result;
            IsCompleted = true;
            if (_continuations != null)
                foreach (var c in _continuations)
                    c();
            _continuations = null;
        }

        public void TrySetResult(T result)
        {
            if (IsCompleted)
                return;
            SetResult(result);
        }
    }
}