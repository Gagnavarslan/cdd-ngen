using System;
using System.Text;

namespace CoreData.Desktop.FileSystem.VirtualStorage
{
    /// <summary>Virtual file system uniformed path.
    /// <seealso cref="https://docs.microsoft.com/en-us/windows/desktop/FileIO/naming-a-file"/></summary>
    public struct VPath //todo: IEquatable<VPath>, IComparable<VPath>
    {
        public const string ExtendedLengthPathPrefix = @"\\?\"; // to specify "\\?\C:\very long path"
        public const char DirSeparator = '\\';
        public static readonly VPath Empty = new VPath(string.Empty, true);
        public static readonly VPath Root = new VPath("\\", true);

        // Fastest and closest to NTFS file system path comparer https://stackoverflow.com/a/2749676
        public static readonly StringComparer DefaultComparer = StringComparer.OrdinalIgnoreCase;

        public static readonly Encoding DefaultEncoding = Encoding.UTF8; // new UTF8Encoding(false, false);

        public VPath(string path) : this(path, false)
        {
        }
        internal VPath(string path, bool safe)
        {
            if(safe)
            {
                Name = path;
            }
            else
            {
                //(string name, string error) = ValidateAndNormalize(path);
                //if(errorMessage != null)
                //    throw new ArgumentException(errorMessage, nameof(path));
                Name = path;
            }
        }

        public string Name { get; }

        private (string, string) ValidateAndNormalize(string path)
        {
            return (path, null);
        }
    }
}
