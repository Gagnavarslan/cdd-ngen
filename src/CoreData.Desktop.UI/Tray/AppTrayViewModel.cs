using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CoreData.Common.HostEnvironment;
using CoreData.Desktop.Common.Windows;
using CoreData.Desktop.UI.VVMs;

namespace CoreData.Desktop.UI.Tray
{
    public class ViewFactory
    {
        public TView GetOrCreate<TView, TViewModel>(Func<TView> viewFactory, TViewModel data)
            where TView : FrameworkElement, IView<TViewModel>
        {
            var view = viewFactory();
            //view.DataContext = data;
            view.LoadData(data);
            return view;
        }
    }
    // Quick and dirty (but nice!) ToolTips – revisited and interactive http://www.hardcodet.net/2013/11/quick-and-dirty-but-nice-tooltips-revisited-and-interactive
    // https://docs.microsoft.com/en-us/dotnet/framework/wpf/data/how-to-bind-to-a-method?view=netframework-4.7.2
    // https://docs.microsoft.com/en-us/dotnet/framework/wpf/data/data-templating-overview?view=netframework-4.7.2#styling-and-templating-an-itemscontrol
    // AutofacContentLoader - https://www.dotnetfalcon.com/modern-ui-for-wpf-autofac-based-contentloader/
    // DynamicObject - http://www.lostindetails.com/blog/post/Binding-your-View-to-your-ViewModel-in-Wpf

    public interface IView<TViewModel>
    {
        //TV
        TViewModel Data { get; }

        void LoadData(TViewModel data); // !!!: mb reuse of LoadComponent
    }
    
    public class AppTrayViewModel
    {
        public const string DefaultIcon = "/Icons/tray.ico";

        public AppTrayViewModel(AppInfo appInfo)
        {
            Title = appInfo.Title;
            IconSource = DefaultIcon;

            CoreDataStorages = new ObservableCollection<CoreDataStorageViewModel>()
            {
                CoreDataStorageViewModel.Test
            };

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

        public ObservableCollection<CoreDataStorageViewModel> CoreDataStorages { get; }
        public CoreDataStorageViewModel SelectedCoreData { get; set; }

        public ICommand ShowDashboard { get; }
        public ICommand ShowSettings { get; }
        public ICommand Exit { get; }
    }
}
