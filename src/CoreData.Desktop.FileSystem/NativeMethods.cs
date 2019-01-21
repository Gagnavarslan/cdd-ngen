using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace CoreData.Desktop.FileSystem
{
    static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetFileTime(SafeFileHandle hFile, ref long lpCreationTime, ref long lpLastAccessTime, ref long lpLastWriteTime);

        // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getlogicaldrives#return-value
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int GetLogicalDrives();
    }
}
