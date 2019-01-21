using CoreData.Common.HostEnvironment;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreData.Common.Threading
{
    /// <summary>Queue of tasks to be started and executed serially.</summary> 
    [DebuggerDisplay("{" + nameof(IDebugInfo.PrintValue) + "}")]
    public class SerialTaskQueue : IDebugInfo
    {
        public string PrintValue => _lockData.LogView;

        private readonly ILogger _logger;
        private readonly SyncBlock _lockData;

        public SerialTaskQueue(ILogger logger)
        {
            _logger = logger;
            _lockData = new SyncBlock(_logger);
        }

        /// <summary>Enqueues non-started Func(Of task) to be processed serially and in order.</summary> 
        public void Enqueue(Func<Task> factory) => _lockData.Enqueue(factory);
        public Task Enqueue(Task task)
        {
            _lockData.Enqueue(task);
            return task; 
        }

        /// <summary>Provides artificial task that represents the completion of all previously queued tasks.</summary> 
        public Task Completed() => Enqueue(new Task(() => { }));
        // todo: remove task creation+manage from SyncBlock.TaskCompletionSource|event|token
        
        /// <summary>Sync lock and its guarded resources.</summary>
        struct SyncBlock
        {
            private readonly ILogger _logger;
            private readonly Queue<object> _tasks; // lock resource
            private Task _runningTask; // lock resource
            
            public SyncBlock(ILogger logger)
            {
                _logger = logger;
                _tasks = new Queue<object>();
                _runningTask = null;
                LogView = "Queue initialized";
            }

            public string LogView { get; private set; }

            /// <summary>Enqueues not yet started task to be processed serially and in order.</summary> 
            public void Enqueue(object taskOrFunction)
            {
                //taskOrFunction.ThrowIfNull();
                //if (taskOrFunction == null) throw new ArgumentNullException(nameof(taskOrFunction));
                lock (_tasks)
                {
                    if (_runningTask == null)
                    {
                        LogView = "Starting new task.";
                        StartNext(taskOrFunction);
                    }
                    else // queue task to be started later 
                    {
                        LogView = "Task queued to be started later.";
                        _tasks.Enqueue(taskOrFunction);
                    }
                }
            }

            /// <summary>Starts the provided task (or function that returns a task).</summary> 
            private void StartNext(object item)
            {
                var task = item as Task ?? ((Func<Task>)item)();

                if (task.Status == TaskStatus.Created)
                {
                    task.Start();
                }
                _runningTask = task;
                task.ContinueWith(StartNextIfAny);
            }

            /// <summary>Called when a Task completes to potentially start the next in the queue.</summary> 
            private void StartNextIfAny(Task completed)
            {
                LogView = "Task has completed. Next task start attempt.";
                _logger.Info(LogView);

                lock (_tasks)
                {
                    _runningTask = null;
                    if (_tasks.Any())
                    {
                        StartNext(_tasks.Dequeue());
                    }
                    else
                    {
                        _logger.Info("No next task to process, queue is empty.");
                    }
                }
            }
        }
    }
}
