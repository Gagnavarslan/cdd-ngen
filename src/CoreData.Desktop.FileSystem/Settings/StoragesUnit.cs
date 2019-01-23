using CoreData.Desktop.Server.Settings;

namespace CoreData.Desktop.FileSystem.Settings
{
    /// <summary>Unit of related storages.</summary>
    public class StoragesUnit
    {
        public StoragesUnit(VirtualStorage virtualStorage, LocalStorage localStorage, CoreDataConnection coreData)
        {
            VirtualStorage = virtualStorage;
            LocalStorage = localStorage;
            CoreData = coreData;
        }

        public LocalStorage LocalStorage { get; }
        public VirtualStorage VirtualStorage { get; }
        public CoreDataConnection CoreData { get; }
    }
}
