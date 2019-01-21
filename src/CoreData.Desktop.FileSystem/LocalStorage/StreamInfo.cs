using System;
using System.IO;

namespace CoreData.Desktop.FileSystem.BackingDataStore
{
    // todo: is it applicable? https://docs.microsoft.com/en-us/dotnet/api/system.io.packaging.streaminfo?view=netframework-4.7.2
    public class StreamInfo
    {
        /// <summary>Gets the name of the stream</summary>
        public string Name { get; }

        public Stream GetStream(FileMode mode, FileAccess access) //mode default = FileMode.OpenOrCreate
        {
            throw new NotImplementedException();
        }
    }
}
