using CoreData.Common.HostEnvironment;
using System.IO;

namespace CoreData.Desktop.FileSystem.LocalFileSystem
{
    public interface ILocalStorageInfo : IDebugView
    {
    }

    public interface ILocalStorage : IDebugView
    {
        string Home { get; }

        bool Exists { get; }

        string GetPath(string path);

        void CreateDirectory(string path);

        bool DirectoryExists(string path);

        FileAttributes GetAttributes(string path);
        void SetAttributes(string path, FileAttributes attributes);

        bool FileExists(string path);

        Stream OpenFile(string path, FileMode mode, FileAccess access, FileShare share, FileOptions options);

        void DeleteFile(string path);
        void DeleteDirectory(string path, bool recursive);

        void FileMove(string oldpath, string newpath);
        void DirectoryMove(string oldpath, string newpath);
    }
}
