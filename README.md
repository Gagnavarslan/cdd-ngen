# CoreData Desktop v5+ "code making and code breaking"
___

## What is CoreData Desktop
Desktop application to work with CoreData docs from native user environment - File Explorer.
It provides virtual file system on drive(Z) as projection of CoreData.
_to be added_

## Install
_to be added_

## User environment requirements
- Windows 7+ x86|x64. __?Server editions?__
- Microsoft .NET Framework 4.7.2

## Developer environment
- Windows 10
- Microsoft .NET Framework 4.7.2
- VS 2017 (+ extensions)
- Nuget(Paket)
-...

## [Changelog](./CHANGELOG.md#sub-section)

## [Libraries, Components, Guides and Fundamentals](./docs/Components.md#sub-section) 

## Coding|naming conventions
Standard conventions with some exceptions:
- 👍ILogView.LogView 'service' property(debug and logging) is next to openning class brackets
  - close to its default usage - [DebuggerDisplay], [DebuggerTypeProxy]
  - not messing with 'real' class members
- 👍👎"If-else" brackets
- 👍👎Interface and implementation could be in the same file

## TEMP_UNSORTED_TOPICS

## class FileContentCache. Extension to virtual file system.
- One writer, Many readers.
- __CachePolicyWriteThrough__ property - by default is false, meaning first writes data to the cache and then flushes the cache asynchronously.
- __TimeToLiveAfterClose__ property - by default 1min.
- __MaxCacheSize__ property - by default 500MB, max total memory which might be allocated for cache.

## rem*** Timeouts.
If some callback needs more time to complete the operation, it can call ResetTimeout method for this
particular call. To make your code fast, don't perform lengthy operations (especially network 
operations) from the callback. Create a worker thread, that will do the job. When you need to write 
the data, pass this data to the worker thread and return. The worker thread will send the data to 
the remote side. When the file is opened, cache some of it's data with help of the worker thread and 
return this data from reading callback.

## todo*** Troubleshooter tab|page
### FiddlerCore for authentication issues
 - Record|Play web browser authentication flow
 - SSO - reuse web browser session token
### Shell Folders
When: Sync issue. !NOT [USED|SHOWN]!
 Sync center. Shell:::{9C73F5E5-7AE7-4E32-A8E8-8D23B85255BF}
When: Connection issue.
 Network Connections. Shell:::{7007ACC7-3202-11D1-AAD2-00805FC1270E}
When: Default open app is not working.
 Programs that Windows uses by default. Shell:::{17cd9488-1228-4b2f-88ce-4298e93e0966}
 Default Apps. Shell:::{2559a1f7-21d7-11d4-bdaf-00c04f60b9f0}
When: Contact us.
 Email client. Shell:::{2559a1f5-21d7-11d4-bdaf-00c04f60b9f0} (or use 'mailto:xxx@azazo.is')
When: 'Quick access' recently|frequently settings, 'Navigation pane' settings and other Explorer options.
 File Explorer Options. Shell:::{6DFD7C5C-2451-11d3-A299-00C04F8EF6AF}
When: 'Frequently used folders' list is needed.
 Frequently used folders. Shell:::{3936E9E4-D92C-4EEE-A85A-BC16D5EA0819}
When: 'Recent folders' list is needed.
 Recent folders. Shell:::{22877a6d-37a1-461a-91b0-dbda5aaebc99}
When: 'Recycle bin'.
 Recycle bin. Shell:::{645FF040-5081-101B-9F08-00AA002F954E}

## KB*** [Description of the way that Excel saves files](https://support.microsoft.com/en-us/help/814068/description-of-the-way-that-excel-saves-files).
## KB*** [Excel file save error](https://support.microsoft.com/en-us/help/214073/you-receive-an-error-message-when-you-try-to-save-a-file-in-excel)
## KB*** [Resolving a File Sync Warning](https://support.efolder.net/hc/en-us/articles/115010634648-Anchor-Resolving-a-File-Sync-Warning)
## KB*** [WPF Auth via web. Application.GetCookie and Application.SetCookie methods](https://stackoverflow.com/a/24268188)
## KB*** [Pack URIs in WPF](https://docs.microsoft.com/en-us/dotnet/framework/wpf/app-development/pack-uris-in-wpf#programming-with-pack-uris)
## KB*** [Programmatic creation of PKCS#10 certification signing requests and X.509 public key certificates](https://blogs.msdn.microsoft.com/dotnet/2018/04/30/announcing-the-net-framework-4-7-2/)
## KB*** [WPF: i) ResourceDictionaries by Source ii) StaticResource references](https://blogs.msdn.microsoft.com/dotnet/2018/04/30/announcing-the-net-framework-4-7-2/)
## KB*** [WIX variables file example](https://github.com/loresoft/msbuildtasks/tree/master/Source/MSBuild.Community.Tasks.Setup)
