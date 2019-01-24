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
        public LocalStorage LocalStorage { get; } // local data already downloaded from CD(local DB)
        public AuthConnection CoreData { get; } // connection to CD API
    }
}
