# Components, Libraries, Guides, Ideas

## Settings
[CommandLineUtils](https://github.com/natemcmaster/CommandLineUtils)
[Jot - a .NET library for state persistence](https://github.com/anakic/jot)
### Per Installation
- Config files located at file system.
- [AppContext switches](https://docs.microsoft.com/en-us/dotnet/api/system.appcontext?view=netframework-4.7.2)
### Per User
- Config files persisted to isolated storage at file system.
- Protected data, e.g. CD connection string. [How to: Use Data Protection.](https://docs.microsoft.com/en-us/dotnet/standard/security/how-to-use-data-protection#example)
### Per Session
- In-memory settings. CLI:
    - 👍[CommandLineUtils - atm provides the most useful set features](https://github.com/natemcmaster/CommandLineUtils)
    - 👍👎[CommandLineParser - easy to start with; used for ConfigEditorV4, not flexible enough](https://github.com/commandlineparser/commandline/blob/master/demo/ReadText.Demo/Options.cs)
    - [Guidelines: Commands, Arguments and Options](https://msdn.microsoft.com/en-us/magazine/mt763239.aspx?f=255&MSPPError=-2147217396)
- In-memory settings: Provided by CD, e.g. FEATURE_FLAGS.
- In-memory settings: Process vars, etc.
- ~~In-memory data: MemoryCache for opened file stream until its closed.~~

## Web Communication [md](.%2FWeb%20Communication.md#sub-section)

## Collaboration [md](.%2FCollaboration.md#sub-section)

## Virtual Drive 'Z'
- 👍[Dokany - user mode file system](https://dokan-dev.github.io/dokany-doc/html/)
- 👎[CBFS](https://web.archive.org/web/20120603145458/http://eldos.com:80/documentation/cbfs/ref_cl_cbfs_mtd_getoriginatorprocessid.html)
### File Systems
- [__LiteDB - NoSQL Document Store in a single data file__](https://github.com/mbdavid/LiteDB)
- [__NDatabase - Object Database: a real native and transparent persistence layer for .NET__](https://archive.codeplex.com/?p=ndatabase)
- [NEventStore - persistence agnostic Event Store for .NET](https://github.com/NEventStore/NEventStore)
- [VFS for Git](https://github.com/Microsoft/VFSForGit)
- [Foundatio.Storage](https://github.com/FoundatioFx/Foundatio#file-storage)
- [VirtualPathProvider](https://docs.microsoft.com/en-us/dotnet/api/system.web.hosting.virtualpathprovider?view=netframework-4.7.2)
- [UnmanagedMemoryStream - consider to use that instead of MemoryStream](https://o7planning.org/en/10535/csharp-streams-tutorial-binary-streams-in-csharp)
- [WIMCore is a library for the reading and writing of Windows Image files (WIM)](https://github.com/hounsell/DecryptESD)
- [Windows Projected File System (ProjFS)](https://docs.microsoft.com/en-us/windows/desktop/projfs/projected-file-system)
- [__NTFS Streams - .NET library for working with alternate data streams__](https://github.com/RichardD2/NTFS-Streams)
### Case sensitivity [md](.%2FVFS.%20Case%20sensitivity.md#sub-section) 
### Cryptography [md](.%2FCryptography.md#sub-section) 
### Utils
- [FileHelpers](https://www.filehelpers.net/examples/)
- ['Files difference' as part of FileHelpers](https://github.com/MarcosMeli/FileHelpers/blob/master/FileHelpers/Engines/FileDiffEngine.cs)

## Resilience and transient-fault-handling (Polly) [md](.%2FFault%20handling.md)

## Middleware: Commands, Queues, Pipelines, TaskQueues
- [Proto Actor - Ultra fast distributed actors for Go, C# and Java/Kotlin](https://github.com/AsynkronIT/protoactor-dotnet)
- [Scott Hanselman: Exploring Brighter](https://www.hanselman.com/blog/ExploringCQRSWithinTheBrighterNETOpenSourceProject.aspx)
- [Brighter&Darker - Query, Command Processor & Dispatcher implementation with support for task queues](https://www.goparamore.io/)
- [Brighter project example: TaskList with a UI, back end Http API, a Mailer service, and core library](https://brightercommand.github.io/Brighter/TasksExample.html)
- [Brighter docs](https://paramore.readthedocs.io/en/latest/index.html)
- [MediatR - In-process messaging with no dependencies](https://github.com/jbogard/MediatR)
- [MediatR+DryIoC](https://github.com/jbogard/MediatR/blob/master/samples/MediatR.Examples.DryIoc/Program.cs)
- [Mediator.Net](https://github.com/mayuanyang/Mediator.Net)
- [Quartz.net Job Scheduler](https://www.quartz-scheduler.net/features.html)
- [System.IO.Pipelines](https://blogs.msdn.microsoft.com/dotnet/2018/07/09/system-io-pipelines-high-performance-io-in-net/)
- [Pipelines with TPL DataFlow and Rx.NET](https://jack-vanlightly.com/blog/2018/4/18/processing-pipelines-series-tpl-dataflow)
- [Quartz.NET - Job Scheduler](https://github.com/quartznet/quartznet)
- [Wexflow - task workflow engine](https://github.com/aelassas/Wexflow)
- [A lightweight F#/C# library for efficient functional-style pipelines on streams of data](http://nessos.github.io/Streams/)
- [An easy way to perform background job processing in your .NET and .NET Core applications](https://github.com/HangfireIO/Hangfire)
### Win Service
- 👍[Topshelf](http://topshelf-project.com/)

## Win Shell extensibility, Tools
- [System.Windows.Shell Namespace](https://docs.microsoft.com/en-us/dotnet/api/system.windows.shell?view=netframework-4.7.2)
- [Integrate a Cloud Storage Provider](https://docs.microsoft.com/en-us/windows/desktop/shell/integrate-cloud-storage)
- [NirSoft. ShellExView v1.97 - Shell Extensions Manager](http://www.nirsoft.net/utils/shexview.html)
- [Creating Registration-Free COM Objects](https://docs.microsoft.com/en-us/windows/desktop/sbscs/creating-registration-free-com-objects)
- [Windows Property System](https://docs.microsoft.com/en-us/windows/desktop/properties/windows-properties-system)
- [Codeproject: Windows Property System](https://www.codeproject.com/Articles/1156123/%2fArticles%2f1156123%2fThe-Windows-Property-System)
- [Acceptable Managed out-of-process Shell extensions](https://docs.microsoft.com/en-us/windows/desktop/shell/shell-and-managed-code#acceptable-uses-of-managed-code-and-other-runtimes)
- [Icons and Icon Overlays](https://docs.microsoft.com/en-us/windows/desktop/shell/icons-and-icon-overlays-bumper)
- [Shell Samples](https://docs.microsoft.com/en-us/windows/desktop/shell/samples-entry#shell-samples)
- [How to Customize Folders with Desktop.ini](https://docs.microsoft.com/en-us/windows/desktop/shell/how-to-customize-folders-with-desktop-ini)
- [Windows Sync Overview](https://docs.microsoft.com/en-us/previous-versions/windows/desktop/winsync/windows-sync-overview)
- [Windows Shell SDK Documentation](https://docs.microsoft.com/en-us/windows/desktop/shell/shell-entry#windows-shell-sdk-documentation)
- [How to Add or Remove Duplicate Drives in Navigation Pane of File Explorer in Windows 10](https://www.tenforums.com/tutorials/4675-add-remove-duplicate-drives-navigation-pane-windows-10-a.html)
- [How to Hide Specific Drives in Windows](https://www.tenforums.com/tutorials/79149-hide-specific-drives-windows.html)

## WPF
- [Generics in XAML](https://docs.microsoft.com/en-us/dotnet/framework/xaml-services/generics-in-xaml)
- [x:FactoryMethod - method other than a constructor for XAML processor to initialize an object](https://docs.microsoft.com/en-us/dotnet/framework/xaml-services/x-factorymethod-directive)
- [Calculated Properties](https://github.com/StephenCleary/CalculatedProperties)
- [Async MVVM helpers](https://github.com/StephenCleary/Mvvm.Async)
- [NotifyTaskCompletion](https://github.com/StephenCleary/AsyncEx/wiki/NotifyTaskCompletion)
- [WPF Animated GIF](https://github.com/XamlAnimatedGif/WpfAnimatedGif)
- [Enterwell Client WPF - Notifications](https://github.com/Enterwell/Wpf.Notifications#enterwell-client-wpf---notifications)
- [WPFLocalizationExtension](https://github.com/XAMLMarkupExtensions/WPFLocalizationExtension#localizationextension)
- [WPFToolKit](https://github.com/xceedsoftware/wpftoolkit/wiki/Wiki)
- [CrossCutterN - lightweight runtime AOP](https://github.com/keeper013/CrossCutterN); [link #2](https://www.codeproject.com/Tips/1187601/CrossCutterN-A-Light-Weight-AOP-Tool-for-NET)

## CI&CD
- [Build automation tool written in PowerShell](https://github.com/psake/psake)
- [FAKE](https://fake.build/fake-dotnetcore.html), [+link](https://fake.build/legacy-gettingstarted.html), [+link](https://fake.build/legacy-index.html), [+link](https://fake.build/help-docs.html#Tutorials), [+link](https://fake.build/dotnet-nuget.html)
- [Squirrel](https://github.com/Squirrel/Squirrel.Windows/blob/master/docs/readme.md)

## Logging
- [__.NET Logging Best Practices__](https://stackify.com/csharp-logging-best-practices/)
- [How to Configure NLog](https://docs.stackify.com/docs/errors-and-logs-configure-nlog)
- [Stackify API for .Net - Logs, Errors, Metrics - log4net, NLog](https://github.com/stackify/stackify-api-dotnet#stackify-api-for-net)
- [Logary - logs and metrics are one!](https://github.com/logary/logary/)
- [__CrossCutterN - lightweight runtime AOP__](https://github.com/keeper013/CrossCutterN), [__+link__](https://www.codeproject.com/Tips/1187601/CrossCutterN-A-Light-Weight-AOP-Tool-for-NET)
### Log management similar [solution](https://stackify.com/retrace-log-management/).
Use [NLog logging best practices](https://stackify.com/nlog-guide-dotnet-logging/) and [log4net guide](https://stackify.com/log4net-guide-dotnet-logging/) from [stackify](https://stackify.com/csharp-logging-best-practices/) as basement:
- [ ] App logs shipping(streaming) to us without user intervention for possible analyze and proactive alerting.
- [ ] To be used [Log tagging](https://stackify.com/get-smarter-log-management-with-log-tags/).
- [ ] To be used semantic(anonymous converted to json) objects instead of string messages.
- [ ] Use the Diagnostic Contexts to log additional info like correlation id.
- [ ] Error regression policy. Resolved, but reappearing bug.
- [ ] Error rate policy. E.g used to alert exceeding threshold or avg value or specific error.
## Tests
- [.NET Fake JSON Server](https://github.com/ttu/dotnet-fake-json-server)
- [FlaUI - UI automation library](https://github.com/Roemer/FlaUI)
- [Configure unit tests by using a .runsettings file](https://docs.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file#example-runsettings-file)
- [FluentValidation](https://github.com/JeremySkinner/FluentValidation)
- [Simple.Testing - Specification Framework](https://github.com/gregoryyoung/Simple.Testing)
- [Event Sourcing testing with Specifications](https://abdullin.com/post/event-sourcing-specifications/)

## Diagnostics|Metrics|Telemetry
- [Minidumper - tool that can capture dump files of .NET processes](https://github.com/goldshtn/minidumper/blob/master/README.md)
    https://lowleveldesign.org/2015/12/21/new-features-coming-to-minidumper/
    https://github.com/Microsoft/clrmd/blob/master/Documentation/GettingStarted.md#attaching-to-a-live-process
- [clrmd](https://github.com/Microsoft/clrmd)
- [Foundatio.Metrics](https://github.com/FoundatioFx/Foundatio#metrics)
- [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)
- [Metrics.NET - a .NET Port, with lots of additional functionality, of the awesome Java metrics library by Coda Hale](https://github.com/etishor/Metrics.NET)
- [AsyncDiagnostics](https://github.com/StephenCleary/AsyncDiagnostics)
- [__.NET DiagnosticSource Guides__](https://github.com/dotnet/corefx/tree/master/src/System.Diagnostics.DiagnosticSource/src)
- [__APM tips__](http://www.apmtips.com/blog/2018/09/26/filtering-bad-sampling-good/)
## Utils|Helpers
- [__Enums.NET is a high-performance type-safe .NET enum utility library__](https://github.com/TylerBrinkley/Enums.NET)
- [CompareNETObjects](https://github.com/GregFinzer/Compare-Net-Objects)
- [Chain of responsibility + Specification](https://www.codeproject.com/Articles/743783/Reusable-Chain-of-responsibility-in-Csharp)
- [__Shielded - strict and mostly lock-free Software Transactional Memory (STM) for .NET__](https://github.com/jbakic/Shielded)

## .NET(built-in) UnderUsed|Revealed
- [Stream. File-, Crypto-, Memory, etc.](https://o7planning.org/en/10535/csharp-streams-tutorial-binary-streams-in-csharp)
- [Array.ConstrainedCopy - Copies a range with guarantee that either all copied or undone](https://docs.microsoft.com/en-us/dotnet/api/system.array.constrainedcopy?view=netframework-4.7.2)
- [BufferedStream - buffering layer to read and write operations on another stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.bufferedstream?view=netframework-4.7.2)
- [Samples for Parallel Programming](https://code.msdn.microsoft.com/Samples-for-Parallel-b4b76364/sourcecode?fileId=44488&pathId=462437453)
- [Best Practices for Assembly Loading](https://docs.microsoft.com/en-us/dotnet/framework/deployment/best-practices-for-assembly-loading)
- [Valid consumption patterns for ValueTasks](https://blogs.msdn.microsoft.com/dotnet/2018/11/07/understanding-the-whys-whats-and-whens-of-valuetask#user-content-valid-consumption-patterns-for-valuetasks)
- 👍[HOWTO: Windows-task-snippets](https://github.com/Microsoft/Windows-task-snippets/tree/master/tasks)
- [async-await FAQ](https://blogs.msdn.microsoft.com/pfxteam/2012/04/12/asyncawait-faq/)
- 👍[Potential pitfalls to avoid when passing around async lambdas](https://blogs.msdn.microsoft.com/pfxteam/2012/02/08/potential-pitfalls-to-avoid-when-passing-around-async-lambdas/)
- 👍[Potential Pitfalls in Data and Task Parallelism](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/potential-pitfalls-in-data-and-task-parallelism?view=netframework-4.7.2)
- [TPL Dataflow Library](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/dataflow-task-parallel-library)
- [Samples for Parallel Programming](https://code.msdn.microsoft.com/Samples-for-Parallel-b4b76364/sourcecode?fileId=44488&pathId=105398781)
- 👍[Code Examples](https://csharp.hotexamples.com/examples/System.Threading/ManualResetEvent/WaitOneAsync/php-manualresetevent-waitoneasync-method-examples.html)

## Other references
[CDD LocalDB upload Failure Strategy. Initial discussion doc version](https://docs.google.com/document/d/1ptqS3FMS_EF6UipeDIZKE1vsNV8MBSjvh_qd5ow7vrg/edit#)
