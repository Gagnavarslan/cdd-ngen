using System.ComponentModel;

namespace CoreData.Desktop.FileSystem
{
    /// <summary>File and directory name collision resolution actions.
    /// <seealso cref="https://docs.microsoft.com/en-us/uwp/api/windows.storage.namecollisionoption"/>
    /// <seealso CreationCollisionOption cref="https://docs.microsoft.com/en-us/uwp/api/windows.storage.creationcollisionoption"/></summary>
    public enum NameCollisionOption
    {
        [Description("Automatically append a number to the specified name if the file or folder already exists")]
        GenerateUnique = 0,

        [Description("Replace the existing item if the file or folder already exists")]
        ReplaceExisting = 1,

        [Description("Raise an exception of type System.Exception if the file or folder already exists")]
        FailIfExists = 2
    }
}
