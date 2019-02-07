using System.Windows.Input;

namespace CoreData.Desktop.UI.Views
{
    public class VirtualDriveViewModel
    {
        public VirtualDriveViewModel(string key, FileSystem.Settings.CoreDataStorage coreDataStorage)
            //ICoreDataDriveService coreDataDriveService)
        {
            Key = key;
            CoreData = new ConnectionViewModel(coreDataStorage.CoreData);
            LocalStorage = coreDataStorage.LocalStorage;
            VirtualStorage = coreDataStorage.VirtualStorage;
            //Connect = new Command(_ => coreDataDriveService.Connect(LocalStorage, VirtualStorage, CoreData));
        }

        public string Key { get; set; }

        public ConnectionViewModel CoreData { get; } // ConnectionViewModel

        public FileSystem.Settings.VirtualStorage VirtualStorage { get; }

        public FileSystem.Settings.LocalStorage LocalStorage { get; }

        public ICommand Connect { get; }
    }
}
