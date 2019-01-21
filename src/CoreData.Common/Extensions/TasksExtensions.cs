using System;
using System.Threading;
using System.Threading.Tasks;
using CoreData.Common.Threading;

namespace CoreData.Common.Extensions
{
    public static class TasksExtensions
    {
        #region WaitHandle
        /// <summary>Wraps a <see cref="WaitHandle"/> with a <see cref="Task{Boolean}"/>.
        /// If the <see cref="WaitHandle"/> is signalled, the returned task is completed with a <c>true</c> result.
        /// If the observation times out, the returned task is completed with a <c>false</c> result.
        /// If the observation is cancelled, the returned task is cancelled.
        /// If the handle is already signalled, the timeout is zero, or the cancellation token is already cancelled, then this method acts synchronously.
        /// </summary>
        public static Task<bool> ToAsync(this WaitHandle ctx,
            TimeSpan? timeout = null, CancellationToken token = default)
        {
            // Handle synchronous cases.
            var alreadySignalled = ctx.WaitOne(0);
            if(alreadySignalled) return CompletedTasks.True;
            if(timeout == TimeSpan.Zero) return CompletedTasks.False;
            if(token.IsCancellationRequested) return CompletedTasks.Canceled;

            // Register all asynchronous cases.
            return ToAsyncInternal(ctx, timeout.GetValueOrDefault(Timeout.InfiniteTimeSpan), token);
        }

        private static async Task<bool> ToAsyncInternal(
            WaitHandle handle, TimeSpan timeout, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (new Scoped<RegisteredWaitHandle>(
                ThreadPool.RegisterWaitForSingleObject(
                    handle,
                    (state, timedOut) => ((TaskCompletionSource<bool>)state).TrySetResult(!timedOut),
                    tcs, timeout, executeOnlyOnce: true),
                reg => reg.Unregister(null)))
            {
                using (token.Register(o => ((TaskCompletionSource<bool>)o).TrySetCanceled(), tcs, false))
                    return await tcs.Task.ConfigureAwait(false);
            }
        }
        #endregion AsyncWaitHandle
    }
}
