using System;
using System.IO;
using System.Threading.Tasks;
using FileAccess = DokanNet.FileAccess;

namespace CoreData.Desktop.FileSystem.VirtualStorage
{
    internal class VirtualStorageOperations
    {
        private class StreamContext : IDisposable
        {
            //public CloudFileNode File { get; }

            public FileAccess Access { get; }

            public Stream Stream { get; set; }

            public Task Task { get; set; }

            public bool IsLocked { get; set; }

            public bool CanWriteDelayed => Access.HasFlag(FileAccess.WriteData) && (Stream?.CanRead ?? false) && Task == null;

            //public StreamContext(CloudFileNode file, FileAccess access)
            //{
            //    File = file;
            //    Access = access;
            //}

            public void Dispose()
            {
                Stream?.Dispose();
            }

            //public override string ToString() => DebuggerDisplay;

            //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Debugger Display")]
            //[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            //private string DebuggerDisplay => $"{nameof(StreamContext)} {File.Name} [{Access}] [{nameof(Stream.Length)}={((Stream?.CanSeek ?? false) ? Stream.Length : 0)}] [{nameof(Task.Status)}={Task?.Status}] {nameof(IsLocked)}={IsLocked}".ToString(CultureInfo.CurrentCulture);
        }
    }
}
