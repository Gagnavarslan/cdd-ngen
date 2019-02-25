using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CoreData.Common.Extensions;
using CoreData.Common.HostEnvironment;
using DokanNet;
using NLog;

namespace CoreData.Desktop.FileSystem.VirtualStorage
{
    [DebuggerDisplay("{" + nameof(ITraceView.Value) + "}")]
    public partial class VirtualFileSystem : ITraceView
    {
        string ITraceView.Value => _settings.Value;
    }

    public static class VirtualFileSystemTraceExtensions
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static string GetTraceValue(this DokanFileInfo info) => info.Serialize(true);

        public static NtStatus Log(this NtStatus result, string path, DokanFileInfo info,
            IEnumerable<string> parameters, LogLevel logLevel = null, [CallerMemberName]string origin = null)
        {
            var level = logLevel ?? (result == NtStatus.Success ? LogLevel.Debug : LogLevel.Warn);
            Logger.Log(level, getLogData());
            return result;

            string getLogData() =>
                new { R = $"{origin}: ({result}) {path}",
                    DK = info.GetTraceValue(), With = parameters.Join(", ") }
                .Serialize();
        }
    }
}
