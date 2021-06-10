using System;
using System.Threading;
using System.Threading.Tasks;
using Urho3DNet.MVVM.Binding;

namespace Urho3DNet.MVVM.Threading
{
    /// <summary>
    /// Provides services for managing work items on a thread.
    /// </summary>
    /// <remarks>
    /// In Urho, there is usually only a single <see cref="Dispatcher"/> in the application -
    /// the one for the UI thread, retrieved via the <see cref="UIThread"/> property.
    /// </remarks>
    public class Dispatcher : IDispatcher
    {
        public static Dispatcher UIThread { get; } = new Dispatcher();

        public Dispatcher()
        {
        }

        /// <summary>
        /// Checks that the current thread is the UI thread.
        /// </summary>
        public bool CheckAccess() => true;//_platform?.CurrentThreadIsLoopThread ?? true;

        /// <summary>
        /// Checks that the current thread is the UI thread and throws if not.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The current thread is not the UI thread.
        /// </exception>
        public void VerifyAccess()
        {
            if (!CheckAccess())
                throw new InvalidOperationException("Call from invalid thread");
        }

        /// <summary>
        /// Runs continuations pushed on the loop.
        /// </summary>
        public void RunJobs()
        {
        }

        /// <inheritdoc/>
        public Task InvokeAsync(Action action)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            return Task.CompletedTask;
        }
        
        /// <inheritdoc/>
        public Task<TResult> InvokeAsync<TResult>(Func<TResult> function)
        {
            Contract.Requires<ArgumentNullException>(function != null);
            return Task.FromResult(default(TResult));
        }

        /// <inheritdoc/>
        public Task InvokeAsync(Func<Task> function)
        {
            Contract.Requires<ArgumentNullException>(function != null);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> function)
        {
            Contract.Requires<ArgumentNullException>(function != null);
            return Task.FromResult(default(TResult));
        }

        /// <inheritdoc/>
        public void Post(Action action)
        {
            Contract.Requires<ArgumentNullException>(action != null);
        }
    }
}