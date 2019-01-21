# Description
Virtual file system must support configurable case-sensitivity.
NTFS can be configured with case-sensitive filenames. Problem is on an upper level - applications 
which don't or partially support that (e.g. names are case-preserved, but name search 
is case-insensitive):
- [While File Explorer would show both files, only one would be opened regardless of which one you clicked](https://blogs.msdn.microsoft.com/commandline/2018/02/28/per-directory-case-sensitivity-and-wsl/)
- [names reflect the casing..but name comparisons are case-insensitive](https://docs.microsoft.com/en-us/dotnet/standard/io/file-path-formats#case-and-the-windows-file-system)
- [Do not assume case sensitivity](https://docs.microsoft.com/en-us/windows/desktop/fileio/naming-a-file#naming-conventions)
- [Microsoft Windows Search queries are not case-sensitive](https://docs.microsoft.com/en-us/windows/desktop/search/-search-sql-casesensitivityinsearches)

# Solution
MS introduced(build17093) case handling mechanism - [Per-directory case sensitivity attribute](https://blogs.msdn.microsoft.com/commandline/2018/02/28/per-directory-case-sensitivity-and-wsl/).
However, the most of win apps are not case-sensitive("for compatibility reasons, there is a global 
registry key that overrides this behavior").
Introducing support of case-sensitivity at CDD level won't help if 3rd party app is "case-unaware". 
Also behavior of 3rd party apps is unclear for folders without 'case' attribute.
Nevertheless, it make sense to revisit v4+v5 code to ensure that we do "case-sensitive":
- CDD must not corrupt names case.
- CDD search functionality must take into account precedence of case-eq over non-case-eq. I.e. looking for 'Gxe.tXt' among [some.tXt, gxe.tXt, Gxe.tXt, gxe.TXt] would result in 'Gxe.tXt' (with optional [gxe.tXt, gxe.TXt], where single|uniqueness is not important, e.g. WinExplorer search bar).

# Links
["Spike on case sensitivity on FileSystem level"](https://azazo.tpondemand.com/entity/18643-spike-on-case-sensitivity-on-filesystem)
[fsutil.exe to enable|disable per-directory case sensitivity](https://blogs.msdn.microsoft.com/commandline/2018/02/28/per-directory-case-sensitivity-and-wsl/)
[.net 462 and long paths support](https://blogs.msdn.microsoft.com/jeremykuhne/2016/07/30/net-4-6-2-and-long-paths-on-windows-10/)
[Windows Explorer 'DontPrettyPath' reg key](https://www.techsupportalert.com/content/how-force-your-windows-file-and-folder-names-have-case-you-want.htm) _outdated?_
[Session Manager 'obcaseinsensitive'](https://superuser.com/a/842670)
