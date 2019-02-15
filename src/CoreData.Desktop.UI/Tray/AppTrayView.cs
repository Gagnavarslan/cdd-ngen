using Hardcodet.Wpf.TaskbarNotification;
using NLog;

namespace CoreData.Desktop.UI.Tray
{
    public class AppTrayView : IView<AppTrayViewModel>
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public AppTrayView(TaskbarIcon tray, AppTrayViewModel data)//, Func<IView<AppTrayData>> viewValue)
        {
            //_logger = logger;
            tray.DataContext = Data = data;
            //_trayNotifier = 
        }

        public AppTrayViewModel Data { get; }

        public void LoadContent() // loader for itself
        {
            //Data.Title = "CoreData Desktop";
            //Data.IconSource = AppTrayViewModel.DefaultIcon;
            //Data.Connections = new ConnectionsViewModel();
            //Data.ShowDashboard = Commands.ShowDashboard;
            //Data.ShowSettings = Commands.ShowSettings;
            //Data.Exit = Commands.Exit;
        }

        public void LoadData(AppTrayViewModel data) => LoadContent();
    }
}
