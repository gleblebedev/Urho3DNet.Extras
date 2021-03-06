using System;
using System.Threading.Tasks;

namespace Urho3DNet.MVVM.Threading
{
    /// <summary>
    /// Dispatches jobs to a thread.
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        /// Determines whether the calling thread is the thread associated with this <see cref="IDispatcher"/>.
        /// </summary>
        /// <returns>True if he calling thread is the thread associated with the dispatched, otherwise false.</returns>
        bool CheckAccess();

        /// <summary>
        /// Throws an exception if the calling thread is not the thread associated with this <see cref="IDispatcher"/>.
        /// </summary>
        void VerifyAccess();

        /// <summary>
        /// Invokes a method on the dispatcher thread.
        /// </summary>
        /// <param name="action">The method.</param>
        /// <returns>A task that can be used to track the method's execution.</returns>
        void Post(Action action);

        /// <summary>
        /// Posts an action that will be invoked on the dispatcher thread.
        /// </summary>
        /// <param name="action">The method.</param>
        Task InvokeAsync(Action action);

        /// <summary>
        /// Posts a function that will be invoked on the dispatcher thread.
        /// </summary>
        /// <param name="function">The method.</param>
        Task<TResult> InvokeAsync<TResult>(Func<TResult> function);

        /// <summary>
        /// Queues the specified work to run on the dispatcher thread and returns a proxy for the
        /// task returned by <paramref name="function"/>.
        /// </summary>
        /// <param name="function">The work to execute asynchronously.</param>
        /// <returns>A task that represents a proxy for the task returned by <paramref name="function"/>.</returns>
        Task InvokeAsync(Func<Task> function);

        /// <summary>
        /// Queues the specified work to run on the dispatcher thread and returns a proxy for the
        /// task returned by <paramref name="function"/>.
        /// </summary>
        /// <param name="function">The work to execute asynchronously.</param>
        /// <returns>A task that represents a proxy for the task returned by <paramref name="function"/>.</returns>
        Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> function);
    }
}