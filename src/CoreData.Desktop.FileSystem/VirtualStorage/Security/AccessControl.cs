using System;
using System.Security.AccessControl;
using System.Security.Principal;
using CoreData.Common.Extensions;
using DokanNet;
using DryIoc;
using ImTools;
using NLog;

namespace CoreData.Desktop.FileSystem.VirtualStorage.Security
{
    /// <summary>File system node security managing service.</summary>
    public interface IAccessControl
    {
        /// <summary>If ACL Feature supported with this service.</summary>
        bool IsSupported { get; }

        /// <summary>Get the security information about a file or directory.
        /// <seealso cref="https://github.com/dokan-dev/dokany/blob/master/dokan/dokan.h#L620"/>
        /// <seealso cref="https://github.com/viciousviper/DokanCloudFS/blob/daafb9565b9ca1d380e0f574e146fad731a49f04/DokanCloudFS/CloudOperations.cs#L365"/></summary>
        NtStatus Get(string path, DokanFileInfo info, AccessControlSections sections, out FileSystemSecurity security);

        /// <summary>Sets the security of a file or directory object.</summary>
        NtStatus Set(string path, DokanFileInfo info, AccessControlSections sections, FileSystemSecurity security);
    }

    public sealed class NoneAccessControl : IAccessControl
    {
        public bool IsSupported => false;

        public NtStatus Get(string path, DokanFileInfo info, AccessControlSections sections, out FileSystemSecurity security)
        {
            security = null;
            return DokanResult.NotImplemented;
        }

        public NtStatus Set(string path, DokanFileInfo info, AccessControlSections sections, FileSystemSecurity security)
        {
            return DokanResult.NotImplemented;
        }
    }

    public sealed class FullAccessControl : IAccessControl
    {
        private readonly FileSystemAccessRule _accessRule;

        public FullAccessControl()
        {
            var identity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            _accessRule = new FileSystemAccessRule(identity, FileSystemRights.FullControl, AccessControlType.Allow);
        }

        public bool IsSupported => true;

        /// <summary>Provides full access to everyone.
        /// <seealso cref="https://github.com/viciousviper/DokanCloudFS/blob/daafb9565b9ca1d380e0f574e146fad731a49f04/DokanCloudFS/CloudOperations.cs#L365"/></summary>
        public NtStatus Get(string path, DokanFileInfo info, AccessControlSections sections, out FileSystemSecurity security)
        {
            info.ThrowIfNull();
            path.ThrowIf(path.IsNullOrEmpty());

            //var filePath = LocalStorage.GetPath(fileName);
            try
            {
                security = info.IsDirectory
                    ? (FileSystemSecurity)new DirectorySecurity()
                    : new FileSecurity();

                security.AddAccessRule(_accessRule);

                return DokanResult.Success.Log(path, info, null);
            }
            catch (UnauthorizedAccessException ex)
            {
                security = null;
                return DokanResult.AccessDenied.Log(path, info, ex.ToString().One());
            }
        }

        /// <summary>Set does nothing as specified security info won't be used anyway - Get() allows 'everything'.
        /// <seealso cref="https://github.com/viciousviper/DokanCloudFS/blob/daafb9565b9ca1d380e0f574e146fad731a49f04/DokanCloudFS/CloudOperations.cs#L505"/></summary>
        public NtStatus Set(string path, DokanFileInfo info, AccessControlSections sections, FileSystemSecurity security)
        {
            return DokanResult.NotImplemented;
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

    public static class AccessControl //: IAccessControl
    {
        private static readonly ILogger Logger;

        public static readonly IAccessControl Undefined; // not implemented
        public static readonly IAccessControl AllAllowed; // full access on get
        //public static readonly IAccessControl Restricted;

        static AccessControl()
        {
            Logger = LogManager.GetCurrentClassLogger();

            Undefined = new NoneAccessControl(); // (false);
            AllAllowed = new FullAccessControl(); // (true);
            //Restricted = null; // new AccessControl(true, false);
        }

        ///// <summary>Access strategy depending on input flags.</summary>
        ///// <param name="supported">If feature should be supported at all.</param>
        ///// <param name="allAllowed">Defines security control strategy: full access or restricted access will be applied.</param>
        //public static IAccessControl WithConfig(bool supported, bool allAllowed = true) =>
        //    supported ? AllAllowed : None;

        //protected AccessControl(
        //    bool supported,
        //    Func<string, DokanFileInfo, AccessControlSections, (NtStatus, FileSystemSecurity)> get,
        //    Func<string, DokanFileInfo, AccessControlSections, FileSystemSecurity, NtStatus> set)
        //{
        //    IsSupported = supported;

        //}

        //public bool IsSupported { get; } // Feature in config

        ///// <summary></summary>
        //public NtStatus Get(string path, DokanFileInfo info, AccessControlSections sections, out FileSystemSecurity security)
        //{
        //    // https://github.com/viciousviper/DokanCloudFS/blob/daafb9565b9ca1d380e0f574e146fad731a49f04/DokanCloudFS/CloudOperations.cs#L365
        //    //if (info == null)
        //    //    throw new ArgumentNullException(nameof(info));

        //    //security = info.IsDirectory
        //    //    ? new DirectorySecurity() as FileSystemSecurity
        //    //    : new FileSecurity() as FileSystemSecurity;
        //    //security.AddAccessRule(new FileSystemAccessRule(new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));

        //    //return AsTrace(nameof(GetFileSecurity), fileName, info, DokanResult.Success, $"out {security}", $"{sections}".ToString(CultureInfo.CurrentCulture));

        //    throw new NotImplementedException();
        //    //var filePath = LocalStorage.GetPath(fileName);
        //    //try
        //    //{
        //    //    security = info.IsDirectory
        //    //        ? (FileSystemSecurity)Directory.GetAccessControl(filePath)
        //    //        : File.GetAccessControl(filePath);
        //    //    return DokanResult.Success;
        //    //}
        //    //catch(UnauthorizedAccessException)
        //    //{
        //    //    security = null;
        //    //    return DokanResult.AccessDenied;
        //    //}
        //}

        ///// <summary>Set does nothing as specified security info won't be used anyway - Get() allows 'everything'.
        ///// <seealso cref="https://github.com/viciousviper/DokanCloudFS/blob/daafb9565b9ca1d380e0f574e146fad731a49f04/DokanCloudFS/CloudOperations.cs#L505"/></summary>
        //public NtStatus Set(string path, DokanFileInfo info, AccessControlSections sections, FileSystemSecurity security)
        //{
        //    // return AsDebug(nameof(SetFileAttributes), fileName, info, DokanResult.NotImplemented, sections.ToString());

        //    throw new NotImplementedException();
        //    //var filePath = LocalStorage.GetPath(fileName);
        //    //try
        //    //{
        //    //    if(info.IsDirectory)
        //    //    {
        //    //        Directory.SetAccessControl(filePath, (DirectorySecurity)security);
        //    //    }
        //    //    else
        //    //    {
        //    //        File.SetAccessControl(filePath, (FileSecurity)security);
        //    //    }
        //    //    return DokanResult.Success;
        //    //}
        //    //catch(UnauthorizedAccessException)
        //    //{
        //    //    return DokanResult.AccessDenied;
        //    //}
        //}
    }
}
