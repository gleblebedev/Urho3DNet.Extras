using System;
using System.Runtime.CompilerServices;

namespace Urho3DNet.MVVM.Utilities
{
    /// <summary>
    /// A task-like operation that is guaranteed to finish continuations synchronously,
    /// can be used for parametrized one-shot events
    /// </summary>
    public struct SynchronousCompletionAsyncResult<T> : INotifyCompletion
    {
        private readonly SynchronousCompletionAsyncResultSource<T> _source;
        private readonly T _result;
        private readonly bool _isValid;
        internal SynchronousCompletionAsyncResult(SynchronousCompletionAsyncResultSource<T> source)
        {
            _source = source;
            _result = default;
            _isValid = true;
        }

        public SynchronousCompletionAsyncResult(T result)
        {
            _result = result;
            _source = null;
            _isValid = true;
        }

        static void ThrowNotInitialized() =>
            throw new InvalidOperationException("This SynchronousCompletionAsyncResult was not initialized");

        public bool IsCompleted
        {
            get
            {
                if (!_isValid)
                    ThrowNotInitialized();
                return _source == null || _source.IsCompleted;
            }
        }

        public T GetResult()
        {
            if (!_isValid)
                ThrowNotInitialized();
            return _source == null ? _result : _source.Result;
        }


        public void OnCompleted(Action continuation)
        {
            if (!_isValid)
                ThrowNotInitialized();
            if (_source == null)
                continuation();
            else
                _source.OnCompleted(continuation);
        }

        public SynchronousCompletionAsyncResult<T> GetAwaiter() => this;
    }
}