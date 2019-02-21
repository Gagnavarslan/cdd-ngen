using CoreData.Desktop.FileSystem.LocalFileSystem;
using DokanNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using FileAccess = DokanNet.FileAccess;

namespace CoreData.Desktop.FileSystem.VirtualStorage
{
    /// <summary>Dokan file system implementor.</summary>
    /// <see cref="https://dokan-dev.github.io/dokan-dotnet-doc/html/interface_dokan_net_1_1_i_dokan_operations.html"/>
    /// <remarks impl examples https://csharp.hotexamples.com/examples/Dokan/DokanFileInfo/-/php-dokanfileinfo-class-examples.html />
    public class VirtualFileSystem : IVirtualFileSystem, IDokanOperations
    {
        //todo: see https://github.com/cryptomator/cryptofs#vault-initialization
        //private readonly VirtualDrive _virtualDrive;
        //private ILocalStorage LocalStorage => _virtualDrive.LocalStorage;
        private readonly Settings.VirtualVolume _settings;

        public VirtualFileSystem(Settings.VirtualVolume settings, ILocalStorage localStorage) //VirtualDrive virtualDrive)
        {
            _settings = settings;
            LocalStorage = localStorage;
            //_virtualDrive = virtualDrive;
        }

        public ILocalStorage LocalStorage { get; }
        //public VirtualDrive VirtualDrive { get; }

        private const FileAccess DataAccess = FileAccess.ReadData | FileAccess.WriteData | FileAccess.AppendData |
                                                      FileAccess.Execute |
                                                      FileAccess.GenericExecute | FileAccess.GenericWrite |
                                                      FileAccess.GenericRead;

        private const FileAccess DataWriteAccess = FileAccess.WriteData | FileAccess.AppendData |
                                                   FileAccess.Delete |
                                                   FileAccess.GenericWrite;

        public NtStatus GetVolumeInformation(out string volumeLabel, out FileSystemFeatures features,
            out string fileSystemName, out uint maximumComponentLength, DokanFileInfo info)
        { // seealso https://docs.microsoft.com/en-us/windows/desktop/FileIO/about-volume-management
            volumeLabel = _settings.Label; // "CoreData";
            fileSystemName = _settings.Format; // "NTFS";
            maximumComponentLength = _settings.MaxPathLength; // 256;
            features = _settings.Features
                | (LocalStorage.SecurityService.IsSupported ? FileSystemFeatures.PersistentAcls : FileSystemFeatures.None);

            return DokanResult.Success;
        }

        /// <summary>Creates file handler each time a request is made on a file system object
        /// <seealso cref="https://dokan-dev.github.io/dokany-doc/html/struct_d_o_k_a_n___o_p_e_r_a_t_i_o_n_s.html#a40c2f61e1287237f5fd5c2690e795183"/>
        /// </summary>
        public NtStatus CreateFile(string fileName, FileAccess access, FileShare share, FileMode mode,
            FileOptions options, FileAttributes attributes, DokanFileInfo info)
        {
            var result = DokanResult.Success;
            
            if (info.IsDirectory)
            {
                try
                {
                    switch(mode)
                    {
                        case FileMode.Open:
                            if(!LocalStorage.DirectoryExists(fileName))
                            {
                                try
                                {
                                    if(!LocalStorage.GetAttributes(fileName).HasFlag(FileAttributes.Directory))
                                    {
                                        return DokanResult.NotADirectory;
                                    }
                                }
                                catch(Exception)
                                {
                                    return DokanResult.FileNotFound;
                                }
                                return DokanResult.PathNotFound;
                            }

                            new DirectoryInfo(fileName).EnumerateFileSystemInfos().Any();
                            // you can't list the directory
                            break;

                        case FileMode.CreateNew:
                            if(LocalStorage.DirectoryExists(fileName))
                            {
                                return DokanResult.FileExists;
                            }

                            try
                            {
                                LocalStorage.GetAttributes(fileName).HasFlag(FileAttributes.Directory);
                                return DokanResult.AlreadyExists;
                            }
                            catch(IOException)
                            {
                            }

                            LocalStorage.CreateDirectory(fileName);
                            break;
                    }
                }
                catch(UnauthorizedAccessException)
                {
                    return DokanResult.AccessDenied;
                }
            }
            else
            {
                var pathExists = true;
                var pathIsDirectory = false;

                var readWriteAttributes = (access & DataAccess) == 0;
                var readAccess = (access & DataWriteAccess) == 0;

                try
                {
                    pathExists = LocalStorage.DirectoryExists(fileName) || LocalStorage.FileExists(fileName);
                    pathIsDirectory = LocalStorage.GetAttributes(fileName).HasFlag(FileAttributes.Directory);
                }
                catch(IOException)
                {
                }

                switch(mode)
                {
                    case FileMode.Open:

                        if(pathExists)
                        {
                            // check if driver only wants to read attributes, security info, or open directory
                            if(readWriteAttributes || pathIsDirectory)
                            {
                                if(pathIsDirectory && (access & FileAccess.Delete) == FileAccess.Delete
                                    && (access & FileAccess.Synchronize) != FileAccess.Synchronize)
                                    //It is a DeleteFile request on a directory
                                    return DokanResult.AccessDenied;

                                info.IsDirectory = pathIsDirectory;
                                info.Context = new object();
                                // must set it to someting if you return DokanError.Success

                                return DokanResult.Success;
                            }
                        }
                        else
                        {
                            return DokanResult.FileNotFound;
                        }
                        break;

                    case FileMode.CreateNew:
                        if(pathExists)
                            return DokanResult.FileExists;
                        break;

                    case FileMode.Truncate:
                        if(!pathExists)
                            return DokanResult.FileNotFound;
                        break;
                }

                try
                {
                    info.Context = LocalStorage.OpenFile(fileName, mode, // new FileStream(filePath, mode,
                        readAccess ? System.IO.FileAccess.Read : System.IO.FileAccess.ReadWrite, share, options);

                    if(pathExists && (mode == FileMode.OpenOrCreate || mode == FileMode.Create))
                        result = DokanResult.AlreadyExists;

                    if(mode == FileMode.CreateNew || mode == FileMode.Create) //Files are always created as Archive
                        attributes |= FileAttributes.Archive;
                    LocalStorage.SetAttributes(fileName, attributes);
                }
                catch(UnauthorizedAccessException) // don't have access rights
                {
                    return DokanResult.AccessDenied;
                }
                catch(DirectoryNotFoundException)
                {
                    return DokanResult.PathNotFound;
                }
                catch(Exception ex)
                {
                    var hr = (uint)Marshal.GetHRForException(ex);
                    switch(hr)
                    {
                        case 0x80070020: //Sharing violation
                            return DokanResult.SharingViolation;
                        default:
                            throw;
                    }
                }
            }
            return result;
        }

        /// <summary>Cleanup request before closing the file.</summary>
        /// <seealso cref="https://dokan-dev.github.io/dokany-doc/html/struct_d_o_k_a_n___o_p_e_r_a_t_i_o_n_s.html#a2780a352d1d83104843971bc551aa643"/>
        public void Cleanup(string fileName, DokanFileInfo info)
        {
            (info.Context as FileStream)?.Dispose();
            info.Context = null;

            if(info.DeleteOnClose)
            {
                if(info.IsDirectory)
                {
                    LocalStorage.DeleteDirectory(fileName, true);
                }
                else
                {
                    LocalStorage.DeleteFile(fileName);
                }
            }
        }

        /// <summary>Closes file and cleaning context.</summary>
        /// <seealso cref="https://dokan-dev.github.io/dokany-doc/html/struct_d_o_k_a_n___o_p_e_r_a_t_i_o_n_s.html#a8cec45a9afcc0fc737fb765eb97c639d"/>
        public void CloseFile(string fileName, DokanFileInfo info)
        {
            (info.Context as FileStream)?.Dispose();
            info.Context = null;
        }

        /// <summary>Reads previously opened in ZwCreateFile file.</summary>
        /// <seealso cref="https://dokan-dev.github.io/dokany-doc/html/struct_d_o_k_a_n___o_p_e_r_a_t_i_o_n_s.html#a92457133b49740e54142183f90efd723"/>
        public NtStatus ReadFile(string fileName, byte[] buffer, out int bytesRead, long offset, DokanFileInfo info)
        {
            if(info.Context == null) // memory mapped read
            {
                using(var stream = LocalStorage.OpenFile(fileName, FileMode.Open, System.IO.FileAccess.Read,
                    FileShare.None, FileOptions.None))
                {
                    stream.Position = offset;
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                }
            }
            else // normal read
            {
                var stream = info.Context as FileStream;
                lock(stream) //Protect from overlapped read
                {
                    stream.Position = offset;
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                }
            }
            return DokanResult.Success;
        }

        /// <summary>Writes previously opened in ZwCreateFile file.</summary>
        /// <seealso cref="https://stackoverflow.com/a/9348474"/>
        /// <seealso cref="https://dokan-dev.github.io/dokany-doc/html/struct_d_o_k_a_n___o_p_e_r_a_t_i_o_n_s.html#ab1aa9dab25dcb38903e0790c36b87cce"/>
        public NtStatus WriteFile(string fileName, byte[] buffer, out int bytesWritten, long offset, DokanFileInfo info)
        {
            if(info.Context == null)
            {
                using(var stream = LocalStorage.OpenFile(fileName, FileMode.Open, System.IO.FileAccess.Write,
                    FileShare.None, FileOptions.None))
                {
                    stream.Position = offset;
                    stream.Write(buffer, 0, buffer.Length);
                    bytesWritten = buffer.Length;
                }
            }
            else
            {
                var stream = info.Context as FileStream;
                lock(stream) //Protect from overlapped write
                {
                    stream.Position = offset;
                    stream.Write(buffer, 0, buffer.Length);
                }
                bytesWritten = buffer.Length;
            }
            return DokanResult.Success;
        }

        public NtStatus FlushFileBuffers(string fileName, DokanFileInfo info)
        { // see also https://web.archive.org/web/20121003033759/http://www.eldos.com:80/documentation/cbfs/ref_evt_flushfile.html
            try
            {
                ((FileStream)(info.Context)).Flush();
                return DokanResult.Success;
            }
            catch(IOException)
            {
                return DokanResult.DiskFull;
            }
        }

        public NtStatus GetFileInformation(string fileName, out FileInformation fileInfo, DokanFileInfo info)
        {
            // may be called with info.Context == null, but usually it isn't
            var filePath = LocalStorage.GetPath(fileName);
            FileSystemInfo finfo = new FileInfo(filePath);
            if(!finfo.Exists)
                finfo = new DirectoryInfo(filePath);

            fileInfo = new FileInformation
            {
                FileName = fileName,
                Attributes = finfo.Attributes,
                CreationTime = finfo.CreationTime,
                LastAccessTime = finfo.LastAccessTime,
                LastWriteTime = finfo.LastWriteTime,
                Length = (finfo as FileInfo)?.Length ?? 0,
            };

            return DokanResult.Success;
        }

        public NtStatus FindFiles(string fileName, out IList<FileInformation> files, DokanFileInfo info)
        {
            // This function is not called because FindFilesWithPattern is implemented
            // Return DokanResult.NotImplemented in FindFilesWithPattern to make FindFiles called
            files = FindFilesHelper(fileName, "*");
            return DokanResult.Success;
        }

        //todo: implement GetFileAttributes(..)
        public NtStatus SetFileAttributes(string fileName, FileAttributes attributes, DokanFileInfo info)
        {
            try
            {
                // MS-FSCC 2.6 File Attributes : There is no file attribute with the value 0x00000000
                // because a value of 0x00000000 in the FileAttributes field means that the file attributes for this file MUST NOT be changed when setting basic information for the file
                if(attributes != 0)
                    LocalStorage.SetAttributes(fileName, attributes);
                return DokanResult.Success;
            }
            catch(UnauthorizedAccessException)
            {
                return DokanResult.AccessDenied;
            }
            catch(FileNotFoundException)
            {
                return DokanResult.FileNotFound;
            }
            catch(DirectoryNotFoundException)
            {
                return DokanResult.PathNotFound;
            }
        }

        public NtStatus SetFileTime(string fileName, DateTime? creationTime, DateTime? lastAccessTime,
            DateTime? lastWriteTime, DokanFileInfo info)
        {
            try
            {
                if(info.Context is FileStream stream)
                {
                    var ct = creationTime?.ToFileTime() ?? 0;
                    var lat = lastAccessTime?.ToFileTime() ?? 0;
                    var lwt = lastWriteTime?.ToFileTime() ?? 0;
                    if(NativeMethods.SetFileTime(stream.SafeFileHandle, ref ct, ref lat, ref lwt))
                        return DokanResult.Success;
                    throw Marshal.GetExceptionForHR(Marshal.GetLastWin32Error());
                }

                var filePath = LocalStorage.GetPath(fileName);

                if(creationTime.HasValue)
                    File.SetCreationTime(filePath, creationTime.Value);

                if(lastAccessTime.HasValue)
                    File.SetLastAccessTime(filePath, lastAccessTime.Value);

                if(lastWriteTime.HasValue)
                    File.SetLastWriteTime(filePath, lastWriteTime.Value);

                return DokanResult.Success;
            }
            catch(UnauthorizedAccessException)
            {
                return DokanResult.AccessDenied;
            }
            catch(FileNotFoundException)
            {
                return DokanResult.FileNotFound;
            }
        }

        public NtStatus DeleteFile(string fileName, DokanFileInfo info)
        {
            if(LocalStorage.DirectoryExists(fileName))
                return DokanResult.AccessDenied;

            if(!LocalStorage.FileExists(fileName))
                return DokanResult.FileNotFound;

            if(LocalStorage.GetAttributes(fileName).HasFlag(FileAttributes.Directory))
                return DokanResult.AccessDenied;

            return DokanResult.Success;
            // we just check here if we could delete the file - the true deletion is in Cleanup
        }

        public NtStatus DeleteDirectory(string fileName, DokanFileInfo info)
        {
            var path = LocalStorage.GetPath(fileName);
            return Directory.EnumerateFileSystemEntries(path).Any()
                    ? DokanResult.DirectoryNotEmpty
                    : DokanResult.Success;
            // if dir is not empty it can't be deleted
        }

        public NtStatus MoveFile(string oldName, string newName, bool replace, DokanFileInfo info)
        {
            var oldpath = LocalStorage.GetPath(oldName);
            var newpath = LocalStorage.GetPath(newName);

            (info.Context as FileStream)?.Dispose();
            info.Context = null;

            var exist = info.IsDirectory
                ? LocalStorage.DirectoryExists(newpath)
                : LocalStorage.FileExists(newpath);

            try
            {
                if(!exist)
                {
                    info.Context = null;
                    if(info.IsDirectory)
                        LocalStorage.DirectoryMove(oldpath, newpath);
                    else
                        LocalStorage.FileMove(oldpath, newpath);
                    return DokanResult.Success;
                }
                else if(replace)
                {
                    info.Context = null;

                    if(info.IsDirectory) //Cannot replace directory destination - See MOVEFILE_REPLACE_EXISTING
                        return DokanResult.AccessDenied;

                    LocalStorage.DeleteFile(newpath);
                    LocalStorage.FileMove(oldpath, newpath);
                    return DokanResult.Success;
                }
            }
            catch(UnauthorizedAccessException)
            {
                return DokanResult.AccessDenied;
            }
            return DokanResult.FileExists;
        }

        public NtStatus SetEndOfFile(string fileName, long length, DokanFileInfo info)
        {
            try
            {
                ((FileStream)(info.Context)).SetLength(length);
                return DokanResult.Success;
            }
            catch(IOException)
            {
                return DokanResult.DiskFull;
            }
        }

        public NtStatus SetAllocationSize(string fileName, long length, DokanFileInfo info)
        {
            try
            {
                ((FileStream)(info.Context)).SetLength(length);
                return DokanResult.Success;
            }
            catch(IOException)
            {
                return DokanResult.DiskFull;
            }
        }

        public NtStatus LockFile(string fileName, long offset, long length, DokanFileInfo info)
        {
            try
            {
                ((FileStream)(info.Context)).Lock(offset, length);
                return DokanResult.Success;
            }
            catch(IOException)
            {
                return DokanResult.AccessDenied;
            }
        }
        public NtStatus UnlockFile(string fileName, long offset, long length, DokanFileInfo info)
        {
            try
            {
                ((FileStream)(info.Context)).Unlock(offset, length);
                return DokanResult.Success;
            }
            catch(IOException)
            {
                return DokanResult.AccessDenied;
            }
        }

        public NtStatus GetDiskFreeSpace(out long freeBytesAvailable, out long totalNumberOfBytes, out long totalNumberOfFreeBytes, DokanFileInfo info)
        {
            LocalStorage.GetStorageInfo(out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes);
            return DokanResult.Success;
        }

        public NtStatus GetFileSecurity(string file, out FileSystemSecurity scr, AccessControlSections acc, DokanFileInfo info) =>
            LocalStorage.SecurityService.Get(file, info, acc, out scr);

        public NtStatus SetFileSecurity(string file, FileSystemSecurity scr, AccessControlSections acc, DokanFileInfo info) =>
            LocalStorage.SecurityService.Set(file, info, acc, scr);

        public NtStatus Mounted(DokanFileInfo info) => DokanResult.Success;

        public NtStatus Unmounted(DokanFileInfo info) => DokanResult.Success;

        public NtStatus FindStreams(string fileName, IntPtr enumContext, out string streamName, out long streamSize,
            DokanFileInfo info)
        {
            streamName = string.Empty;
            streamSize = 0;
            return DokanResult.NotImplemented;
        }

        public NtStatus FindStreams(string fileName, out IList<FileInformation> streams, DokanFileInfo info)
        {
            streams = new FileInformation[0];
            return DokanResult.NotImplemented;
        }

        #region /API/FindFiles/ functionality. https://dokan-dev.github.io/dokany-doc/html/

        public IList<FileInformation> FindFilesHelper(string fileName, string searchPattern)
        {
            var filePath = LocalStorage.GetPath(fileName);
            var files = new DirectoryInfo(filePath)
                .EnumerateFileSystemInfos()
                .Where(finfo => DokanHelper.DokanIsNameInExpression(searchPattern, finfo.Name, true))
                .Select(finfo => new FileInformation
                {
                    Attributes = finfo.Attributes,
                    CreationTime = finfo.CreationTime,
                    LastAccessTime = finfo.LastAccessTime,
                    LastWriteTime = finfo.LastWriteTime,
                    Length = (finfo as FileInfo)?.Length ?? 0,
                    FileName = finfo.Name
                }).ToArray();

            return files;
        }

        public NtStatus FindFilesWithPattern(string fileName, string searchPattern,
            out IList<FileInformation> files, DokanFileInfo info)
        {
            files = FindFilesHelper(fileName, searchPattern);

            return DokanResult.Success;
        }
        #endregion
    }
}
