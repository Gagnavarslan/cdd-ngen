using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoreData.Desktop.Common.Windows
{
    /// <summary><seealso cref="http://www.blackwasp.co.uk/UIThreadContinuations.aspx"/></summary>
    public class TaskCommand : ICommand
    {
        private readonly Func<object, CancellationToken, Task> _executeAsync;
        private readonly Func<object, bool> _canExecute;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _task;

        public TaskCommand(Func<object, CancellationToken, Task> executeAsync, Func<object, bool> canExecute = null)
        {
            _executeAsync = executeAsync;
            _canExecute = canExecute ?? (_ => true);
        }

        public bool SwallowExceptions { get; set; }

        /// <summary>Gets a value indicating whether this command is executing.</summary>
        public bool IsExecuting => _cancellationTokenSource != null;

        /// <summary>Gets the asynchronous result that can be awaited.</summary>
        public Task Task => _task ?? Task.CompletedTask;

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();

        public async void Execute(object parameter)
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();

            RaiseCanExecuteChanged();

            var tcs = new TaskCompletionSource<object>();
            _task = tcs.Task;
            var handledException = false;

            try
            {
                //Log.Debug("Executing task command");
                var executionTask = _executeAsync(parameter, _cancellationTokenSource.Token);
                await executionTask.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                //Log.Debug("Task was canceled");
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Task ended with exception");

                // Important: end the task, the exception thrown below will be earlier than the finally block
                releaseCancellationTokenSource();

                if (!SwallowExceptions)
                {
                    handledException = true;
                    tcs.TrySetException(ex);
                    throw;
                }
            }
            finally
            {
                releaseCancellationTokenSource();

                if (!handledException)
                {
                    tcs.TrySetResult(null);
                }
            }

            RaiseCanExecuteChanged();

            void releaseCancellationTokenSource()
            {
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }

        public bool CanExecute(object parameter) => !IsExecuting && _canExecute(parameter);
    }
}
