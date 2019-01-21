using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace CoreData.Common.Threading
{
    public class TasksBlock : IDisposable
    {   // based on spike https://blogs.msdn.microsoft.com/pfxteam/2011/10/02/dont-forget-to-complete-your-tasks/
        // !!!: How to: Implement a Producer-Consumer Dataflow Pattern https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-implement-a-producer-consumer-dataflow-pattern

        private readonly ILogger _logger;
        private readonly BlockingCollection<Action> _actions;
        private readonly CancellationToken _cancellationToken;

        public TasksBlock(
            ILogger logger,
            BlockingCollection<Action> actions,
            CancellationToken cancellationToken = default)
        {
            _logger = logger;
            _actions = actions ?? throw new ArgumentNullException(nameof(actions));
            _cancellationToken = cancellationToken;

            Task.Run(() =>
            {
                foreach (var action in _actions.GetConsumingEnumerable(_cancellationToken))
                {
                    _logger.Swallow(action);
                }
            });
        }

        public void Enqueue(Action action) => _actions.Add(action, _cancellationToken);

        public void Clear()
        {
            while (_actions.TryTake(out var dumped, Timeout.Infinite, _cancellationToken)) ;
        }

        public void Dispose() => _actions.Dispose();
    }
}
