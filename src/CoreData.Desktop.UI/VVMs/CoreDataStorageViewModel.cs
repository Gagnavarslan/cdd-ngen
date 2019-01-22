using CoreData.Desktop.Common.Windows;
using CoreData.Desktop.FileSystem.Services;
using CoreData.Desktop.FileSystem.Settings;
using System.Windows.Input;

namespace CoreData.Desktop.UI.VVMs
{
    public class CoreDataStorageViewModel
    {
        public static readonly CoreDataStorageViewModel Test = new CoreDataStorageViewModel(
            "default",
            Server.Settings.Connection.Test,
            FileSystem.Settings.VirtualStorage.Test,
            LocalStorage.Test,
            null);

        public CoreDataStorageViewModel(string key,
            Server.Settings.Connection coreData,
            FileSystem.Settings.VirtualStorage virtualStorage,
            FileSystem.Settings.LocalStorage localStorage,
            IConnectionService connectionService)
        {
            Key = key;
            CoreData = coreData;
            LocalStorage = localStorage;
            VirtualStorage = virtualStorage;
            Connect = new Command(_ => connectionService.Connect(LocalStorage, VirtualStorage, CoreData));
        }

        public string Key { get; set; }

        public Server.Settings.Connection CoreData { get; }
        public FileSystem.Settings.VirtualStorage VirtualStorage { get; }
        public FileSystem.Settings.LocalStorage LocalStorage { get; }

        public ICommand Connect { get; }
    }
}
