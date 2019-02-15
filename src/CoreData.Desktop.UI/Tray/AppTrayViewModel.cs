using CoreData.Common.HostEnvironment;
using CoreData.Common.Settings;
using CoreData.Desktop.Common.Windows;
using CoreData.Desktop.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CoreData.Desktop.UI.Tray
{
    public class AppTrayViewModel
    {
        public const string DefaultIcon = "/Icons/tray.ico";
        private readonly ISettingsService _settings;

        public AppTrayViewModel(AppInfo appInfo) //, ISettingsService settings
        {
            //_settings = settings;
            Title = appInfo.Title;

            IconSource = DefaultIcon;

            //var storedUserSessions = _settings.AppUserSettings.Read(
            //    "user_sessions", new List<VirtualDriveViewModel>());
            var storedUserSessions = new List<VirtualDriveViewModel>();
            CoreDataStorages = new ObservableCollection<VirtualDriveViewModel>(storedUserSessions);

            ShowDashboard = new Command(_ =>
                {
                    Application.Current.MainWindow = new DashboardView();
                    Application.Current.MainWindow.Show();
                }); //, _ => Application.Current.MainWindow == null);

            ShowSettings = new Command(_ =>
            {
                Application.Current.MainWindow = new DashboardView();
                Application.Current.MainWindow.Show();
            });

            Exit = new Command(_ => Application.Current.Shutdown());
        }

        public string Title { get; set; }
        public string IconSource { get; set; }

        public ObservableCollection<VirtualDriveViewModel> CoreDataStorages { get; }
        public VirtualDriveViewModel SelectedCoreData { get; set; }

        public ICommand ShowDashboard { get; }
        public ICommand ShowSettings { get; }
        public ICommand Exit { get; }
    }
}
