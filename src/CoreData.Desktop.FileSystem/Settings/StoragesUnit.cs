using CoreData.Desktop.Server.Settings;

namespace CoreData.Desktop.FileSystem.Settings
{
    /// <summary>Unit of related storages.</summary>
    public class StoragesUnit
    {
        public StoragesUnit(VirtualStorage virtualStorage, LocalStorage localStorage, AuthConnection coreData)
        {
            VirtualStorage = virtualStorage;
            LocalStorage = localStorage;
            CoreData = coreData;
        }

        public VirtualStorage VirtualStorage { get; } // virtual disk Z: 
        public AuthConnection CoreData { get; } // connection to CD API
        public LocalStorage LocalStorage { get; } // local data already downloaded from CD(local DB)
    }
}
