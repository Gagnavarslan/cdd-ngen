using System;
using System.Windows.Input;

namespace CoreData.Desktop.Common.Tray
{
    /// <summary>App tray tooltip notification service</summary>
    public interface ITrayTooltipNotifier
    {
        void Info(string title, string message, ICommand hit = null, Exception ex = null);

        void Warn(string title, string message, ICommand hit = null, Exception ex = null);

        void Error(string title, string message, ICommand hit = null, Exception ex = null);
    }
}
