using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Common.Threading
{
    public static class CompletedTasks
    {
        public static readonly Task<bool> True = Task.FromResult(true);
        public static readonly Task<bool> False = Task.FromResult(false);

        public static readonly Task<bool> Canceled =
            Task.FromCanceled<bool>(new CancellationToken(true));
    }
}
