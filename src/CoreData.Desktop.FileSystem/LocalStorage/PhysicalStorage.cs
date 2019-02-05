using CoreData.Common.Extensions;
using CoreData.Common.HostEnvironment;
using CoreData.Desktop.FileSystem.LocalFileSystem;
using DryIoc;
using System;
using System.Diagnostics;
using System.IO;

namespace CoreData.Desktop.FileSystem.LocalStorage
{
    /// <summary>Represents local physical disk that stores files and folders.
    /// <seealso cref="https://docs.microsoft.com/en-us/uwp/api/windows.storage.storageprovider"/></summary>
    [DebuggerDisplay("{" + nameof(IDebugView.Now) + "}")]
    public class PhysicalStorage : ILocalStorage
    {
        public string Now => Home;
        //private MemoryCache
        //private readonly IClient _coreData;

        public PhysicalStorage(string home)//, IClient coreData)
        {
            Home = !home.IsNullOrEmpty() ? home : throw new ArgumentNullException(nameof(home));
            //if (!Directory.Exists(Home))
            //{
            //    Directory.CreateDirectory(Home);
            //}
        }

        public string Home { get; }

        public bool Exists => Directory.Exists(Home);

        public string GetPath(string path)
        {
            return Path.Combine(Home, path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public FileAttributes GetAttributes(string path)
        {
            return File.GetAttributes(path);
        }

        public void SetAttributes(string path, FileAttributes attributes)
        {
            File.SetAttributes(path, attributes);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public Stream OpenFile(string path, FileMode mode, FileAccess access, FileShare share, FileOptions options)
        {
            return new FileStream(path, mode, access, share, 4096, options);
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        public void FileMove(string oldpath, string newpath)
        {
            File.Move(oldpath, newpath);
        }

        public void DirectoryMove(string oldpath, string newpath)
        {
            Directory.Move(oldpath, newpath);
        }
    }
}
