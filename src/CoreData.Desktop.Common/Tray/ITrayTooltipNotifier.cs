using System;
using System.Windows.Input;

namespace CoreData.Desktop.Common.Tray
{
    /// <summary>App tray tooltip notification service</summary>
    public interface ITrayTooltipNotifier
    {
        void Info(Balloon balloon);

        void Warn(Balloon balloon);

        void Error(Balloon balloon);
    }

    public struct Balloon
    {
        public Balloon(string title, string text, Exception ex = null, ICommand click = null)
        {
            Title = title;
            Text = text;
            Error = ex;
            Hit = click;
        }

        public string Title { get; }
        public string Text { get; }
        public Exception Error { get; }
        public ICommand Hit { get; } // ???: consider to use CommandTarget https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/commanding-overview#command-target
    }
}
