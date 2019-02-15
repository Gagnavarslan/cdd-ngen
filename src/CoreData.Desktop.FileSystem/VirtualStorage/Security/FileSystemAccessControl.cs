using DokanNet;
using NLog;
using System;
using System.Security.AccessControl;

namespace CoreData.Desktop.FileSystem.VirtualStorage.Security
{
    /// <summary>File system node security managing service.</summary>
    public interface IFileSystemAccessControl
    {
        bool IsFeatureSupported { get; }

        /// <summary>Get the security information about a file or directory.
        /// <seealso cref="https://github.com/dokan-dev/dokany/blob/master/dokan/dokan.h#L620"/>
        /// <seealso cref="https://github.com/viciousviper/DokanCloudFS/blob/daafb9565b9ca1d380e0f574e146fad731a49f04/DokanCloudFS/CloudOperations.cs#L365"/></summary>
        NtStatus Get(string path, DokanFileInfo info, AccessControlSections sections, out FileSystemSecurity security);

        /// <summary>Sets the security of a file or directory object.</summary>
        NtStatus Set(string path, DokanFileInfo info, AccessControlSections sections, FileSystemSecurity security);
    }

    //public class AccessSecurity : IFileSystemAccessControl
    //{

    //    public bool IsFeatureSupported => false;

    //    public NtStatus Get(string path, DokanFileInfo info, AccessControlSections sections, out FileSystemSecurity security)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public NtStatus Set(string path, DokanFileInfo info, AccessControlSections sections, FileSystemSecurity security)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

        
    public class FileSystemAccessControl : IFileSystemAccessControl
    {
        private static readonly ILogger Logger;
        private static readonly IFileSystemAccessControl None;
        private static readonly IFileSystemAccessControl AllAllowed;
        private static readonly IFileSystemAccessControl UsingRestrictions;

        static FileSystemAccessControl()
        {
            Logger = LogManager.GetCurrentClassLogger();
            None = new FileSystemAccessControl(false);
            AllAllowed = new FileSystemAccessControl(true);
            UsingRestrictions = new FileSystemAccessControl(true, false);
        }

        /// <summary>Access strategy depending on input flags.</summary>
        /// <param name="supported">If feature should be supported at all.</param>
        /// <param name="allAllowed"></param>
        public static IFileSystemAccessControl WithConfig(bool supported, bool allAllowed = true) =>
            supported ? AllAllowed : None;

        private FileSystemAccessControl(
            bool supported,
            Func<string, DokanFileInfo, AccessControlSections, (NtStatus, FileSystemSecurity)> get,
            Func<string, DokanFileInfo, AccessControlSections, FileSystemSecurity, NtStatus> set)
        {
            IsSupported = supported;

        }

        public bool IsSupported { get; } // Feature in config

        /// <summary></summary>
        public NtStatus Get(string path, DokanFileInfo info, AccessControlSections sections, out FileSystemSecurity security)
        {
            // https://github.com/viciousviper/DokanCloudFS/blob/daafb9565b9ca1d380e0f574e146fad731a49f04/DokanCloudFS/CloudOperations.cs#L365
            //if (info == null)
            //    throw new ArgumentNullException(nameof(info));

            //security = info.IsDirectory
            //    ? new DirectorySecurity() as FileSystemSecurity
            //    : new FileSecurity() as FileSystemSecurity;
            //security.AddAccessRule(new FileSystemAccessRule(new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));

            //return AsTrace(nameof(GetFileSecurity), fileName, info, DokanResult.Success, $"out {security}", $"{sections}".ToString(CultureInfo.CurrentCulture));

            throw new NotImplementedException();
            //var filePath = LocalStorage.GetPath(fileName);
            //try
            //{
            //    security = info.IsDirectory
            //        ? (FileSystemSecurity)Directory.GetAccessControl(filePath)
            //        : File.GetAccessControl(filePath);
            //    return DokanResult.Success;
            //}
            //catch(UnauthorizedAccessException)
            //{
            //    security = null;
            //    return DokanResult.AccessDenied;
            //}
        }

        /// <summary>Set does nothing as specified security info won't be used anyway - Get() allows 'everything'.
        /// <seealso cref="https://github.com/viciousviper/DokanCloudFS/blob/daafb9565b9ca1d380e0f574e146fad731a49f04/DokanCloudFS/CloudOperations.cs#L505"/></summary>
        public NtStatus Set(string path, DokanFileInfo info, AccessControlSections sections, FileSystemSecurity security)
        {
            // return AsDebug(nameof(SetFileAttributes), fileName, info, DokanResult.NotImplemented, sections.ToString());

            throw new NotImplementedException();
            //var filePath = LocalStorage.GetPath(fileName);
            //try
            //{
            //    if(info.IsDirectory)
            //    {
            //        Directory.SetAccessControl(filePath, (DirectorySecurity)security);
            //    }
            //    else
            //    {
            //        File.SetAccessControl(filePath, (FileSecurity)security);
            //    }
            //    return DokanResult.Success;
            //}
            //catch(UnauthorizedAccessException)
            //{
            //    return DokanResult.AccessDenied;
            //}
        }
    }
}
