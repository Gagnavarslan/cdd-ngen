namespace CoreData.Desktop.FileSystem.Settings
{
    /// <summary>Unit of related storages.</summary>
    public class CoreDataStorage
    {
        public CoreDataStorage(VirtualStorage virtualStorage,
            LocalStorage localStorage, Server.Settings.Connection coreData)
        {
            VirtualStorage = virtualStorage;
            LocalStorage = localStorage;
            CoreData = coreData;
        }

        public Settings.VirtualStorage VirtualStorage { get; } // virtual disk Z: 

        public Server.Settings.Connection CoreData { get; } // connection to CD API
        public Settings.LocalStorage LocalStorage { get; } // local data already downloaded from CD(local DB)
    }
}
