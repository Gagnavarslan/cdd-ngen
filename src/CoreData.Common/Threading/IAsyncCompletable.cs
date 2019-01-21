using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CoreData.Common.Threading
{
    /// <summary>Asynchronous completion operation mechanism.
    /// <seealso cref="https://blog.stephencleary.com/2013/03/async-oop-6-disposal.html"/>
    /// <seealso cref="https://github.com/StephenClearyArchive/AsyncEx.Coordination/blob/master/src/Nito.AsyncEx.Coordination/AwaitableDisposable.cs"/></summary>
    // ???: is it needed?
    public interface IAsyncCompletable<TResult> //: IAsyncCompletable
    {
        void Complete();

        Task<TResult> Completion { get; }
    }
    //public interface IAsyncCompletable
    //{
    //    void Complete();

    //    Task Completion { get; }
    //}

    /// <summary>An awaitable wrapper around a task whose result is disposable.</summary>
    // ???: is it needed?
    public struct AwaitableDisposable<T> where T : IDisposable
    {
        private readonly Task<T> _task;

        public AwaitableDisposable(Task<T> task)
        {
            _task = task ?? throw new ArgumentNullException(nameof(task));
        }

        /// <summary>
        /// Returns the underlying task.
        /// </summary>
        public Task<T> AsTask()
        {
            return _task;
        }

        /// <summary>
        /// Implicit conversion to the underlying task.
        /// </summary>
        /// <param name="source">The awaitable wrapper.</param>
        public static implicit operator Task<T>(AwaitableDisposable<T> source)
        {
            return source.AsTask();
        }

        /// <summary>
        /// Infrastructure. Returns the task awaiter for the underlying task.
        /// </summary>
        public TaskAwaiter<T> GetAwaiter()
        {
            return _task.GetAwaiter();
        }

        /// <summary>
        /// Infrastructure. Returns a configured task awaiter for the underlying task.
        /// </summary>
        /// <param name="continueOnCapturedContext">Whether to attempt to marshal the continuation back to the captured context.</param>
        public ConfiguredTaskAwaitable<T> ConfigureAwait(bool continueOnCapturedContext)
        {
            return _task.ConfigureAwait(continueOnCapturedContext);
        }
    }
}
