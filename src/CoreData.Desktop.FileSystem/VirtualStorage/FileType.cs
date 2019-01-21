namespace CoreData.Desktop.FileSystem.VirtualStorage
{
    /// <summary>
    /// <seealso cref="https://code.visualstudio.com/api/references/vscode-api#FileType"/>
    /// </summary>
    public enum FileType
    { // todo: split into [File, Dir] + bool SymLink
        File,
        Directory,
        FileSymLink,
        DirSymLink
    }
}
