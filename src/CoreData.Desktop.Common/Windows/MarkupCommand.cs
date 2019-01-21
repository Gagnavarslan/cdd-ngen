using System;
using System.Windows.Input;
using System.Workflow.ComponentModel.Serialization;

namespace CoreData.Desktop.Common.Windows
{
    /// <summary>Xaml markup command.
    /// <seealso cref="http://www.hardcodet.net/2009/04/wpf-command-markup-extension"/>
    /// <seealso cref="http://blogs.profitbase.com/tsenn/?p=73" Custom Markup Extension with bindable properties/></summary>
    public abstract class CommandExtension<T> : MarkupExtension, ICommand
        where T : class, ICommand, new()
    {
        private static T _command;
        public override object ProvideValue(IServiceProvider serviceProvider) =>
            _command ?? (_command = new T());

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public abstract void Execute(object parameter);

        public virtual bool CanExecute(object parameter) => true;
    }
}
