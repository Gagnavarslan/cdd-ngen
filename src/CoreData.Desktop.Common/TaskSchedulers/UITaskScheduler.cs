using System;
using System.Threading.Tasks;

namespace CoreData.Desktop.Common.TaskSchedulers
{
    public static class UITaskScheduler
    {
        // todo: set it with UI scheduler
        public static TaskScheduler UIScheduler { get; internal set; }

        // !!!: Specifying a synchronization context to run on UI thread https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskscheduler?redirectedfrom=MSDN&view=netframework-4.7.2#specifying-a-synchronization-context
        public static Task ContinueOnUI(this Task ctx, Action<TaskStatus> with) =>
            ctx.ContinueWith(t => with(t.Status), UIScheduler);

        public static Task ContinueOnUI<T>(this Task<T> ctx, Action<T> with) =>
            ctx.ContinueWith(t => with(t.Result), UIScheduler);

        public static Task<R> ContinueOnUI<T, R>(this Task<T> ctx, Func<T, R> with) =>
            ctx.ContinueWith(t => with(t.Result), UIScheduler);
    }
}
