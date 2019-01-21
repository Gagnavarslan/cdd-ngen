using System;
using System.Windows.Input;

namespace CoreData.Desktop.Common.Windows
{
    /// <summary>Always enabled lightweight command.</summary>
    public class Command : ICommand
    {
        private readonly Action<object> _execute;

        public Command(Action<object> execute) =>
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));

        public void Execute(object parameter) => _execute(parameter);

        public virtual bool CanExecute(object parameter) => true;
        public virtual event EventHandler CanExecuteChanged;
    }

    /// <summary>Command with 'can exec' state impl part.</summary>
    public class StateCommand : Command
    {
        private readonly Func<object, bool> _canExecute;

        public StateCommand(Action<object> execute, Func<object, bool> canExecute) : base(execute) =>
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));

        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();

        public override bool CanExecute(object parameter) => _canExecute(parameter);

        public override event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
