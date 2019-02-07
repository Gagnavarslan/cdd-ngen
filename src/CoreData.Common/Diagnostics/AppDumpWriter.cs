using System;
using System.Diagnostics;
using System.IO;
using DumpWriter;

namespace CoreData.Common.HostEnvironment
{
    /// <summary>Application memory dump writer.
    /// <seealso Materials and hands-on labs for a production diagnostics workshop cref="https://github.com/goldshtn/production-diagnostics-day"/>
    /// <seealso Attaching to a live process cref="https://github.com/Microsoft/clrmd/blob/master/Documentation/GettingStarted.md#attaching-to-a-live-process"/>
    /// <seealso MiniDumper features cref="https://lowleveldesign.org/2015/12/21/new-features-coming-to-minidumper/"/>
    /// <seealso NET Crash Dump and Live Process Inspection cref="https://blogs.msdn.microsoft.com/dotnet/2013/05/01/net-crash-dump-and-live-process-inspection/"/>
    /// <seealso Microsoft.Diagnostics.Runtime cref="https://github.com/microsoft/clrmd"/>
    /// <seealso cref="https://support.microsoft.com/en-us/help/2895198/debug-diagnostics-tool-v2-0-is-now-available"/></summary>
    public class AppDumpWriter
    {
        [DebuggerDisplay("{" + nameof(IDebugView.Value) + "}")]
        public class DumpInfo : IDebugView
        {
            string IDebugView.Value => Name;

            public static readonly DumpInfo Default = new DumpInfo(System.IO.Directory.GetCurrentDirectory());

            public DumpInfo(string dir) : this(dir, Process.GetCurrentProcess()) { }
            public DumpInfo(string dir, Process process) : this(dir, process.Id, $"{process.Id}-{process.ProcessName}") { }
            public DumpInfo(string dir, int processId, string name)
            {
                Location = dir;
                ProcessId = processId;
                Name = name;
            }

            public string Location { get; }
            public int ProcessId { get; }
            public string Name { get; }
        }

        public static string GetNewDumpPath(DumpInfo dump)
        {
            string result;
            do { result = Path.Combine(dump.Location, $"{dump.Name}_{DateTime.Now:yyMMdd_HHmmss}.dmp"); }
            while (File.Exists(result));

            return result;
        }

        public static readonly AppDumpWriter Null = new AppDumpWriter(null, (_, __) => { });
        private readonly TextWriter _logger;
        private readonly Action<int, string> _dump;

        public AppDumpWriter(TextWriter logger) : this(logger, default) { }
        private AppDumpWriter(TextWriter logger, Action<int, string> dump)
        {
            _logger = logger ?? TextWriter.Null;
            _dump = dump ?? DumpInternal;
        }

        public string LastDumpPath { get; private set; }

        public void Dump() => Dump(DumpInfo.Default);
        public void Dump(DumpInfo info) => Dump(info.ProcessId, GetNewDumpPath(info));
        public void Dump(int pid, string path)
        {
            _logger.WriteLine($"Dumping memory of {pid} to {path} ..");
            _dump(pid, path);
            _logger.WriteLine($"Done: {pid} to {path}");
        }
        private void DumpInternal(int pid, string path)
        {
            var writer = new DumpWriter.DumpWriter(_logger);
            writer.Dump(pid, DumpType.MinimalWithFullCLRHeap, IntPtr.Zero, path); // https://github.com/goldshtn/minidumper/blob/master/MiniDumper/Debugger.cs
            LastDumpPath = path;
        }
    }
}
