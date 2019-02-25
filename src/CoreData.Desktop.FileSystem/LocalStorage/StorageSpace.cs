using System.IO;

namespace CoreData.Desktop.FileSystem.LocalStorage
{
    public class StorageSpace
    {
        private readonly DriveInfo _root;

        public StorageSpace(string storageHomePath)
        {
            _root = new DriveInfo(Path.GetPathRoot(storageHomePath));
        }

        public long Available => _root.AvailableFreeSpace;
        public long Total => _root.TotalSize;
        public long TotalFree => _root.TotalFreeSpace;
    }
}
