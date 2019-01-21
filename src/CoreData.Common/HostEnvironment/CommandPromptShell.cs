using System.Diagnostics;
using System.Threading;
using NLog;

namespace CoreData.Common.HostEnvironment
{
    /// <summary>Command shell executor.
    /// <para>[Alt]: https://github.com/manojlds/cmd </para></summary>
    public class CommandPromptShell : IShell<ProcessStartInfo>
    {
        private readonly ILogger _logger;
        public CommandPromptShell(ILogger logger) => _logger = logger;

        /// <summary>Builds initialized with defaults command for a new start</summary>
        public ShellCommand<ProcessStartInfo> Build(string command)
        {
            var startInfo = new ProcessStartInfo(command)
            {
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            return new ShellCommand<ProcessStartInfo>(command, startInfo);
        }
        //public Command<T> Build<T>(string command, Func<string, Func<T>> initializer) =>
        //    new Command<T>(command, initializer(command));

        public bool Run(ShellCommand<ProcessStartInfo> command, int timeout = Timeout.Infinite)
        {
            using (var process = new Process { StartInfo = command.StartInfo })
            {
                process.EnableRaisingEvents = true;
                // todo: to be uncommented
                //if (outputReceived != null)
                //{
                //    process.StartInfo.RedirectStandardOutput = true;
                //    process.OutputDataReceived += (_, e) => outputReceived(e.Data);
                //}
                //if (outputReceived != null)
                //{
                //    process.StartInfo.RedirectStandardError = true;
                //    process.ErrorDataReceived += (_, e) => errorReceived(e.Data);
                //}
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                return process.WaitForExit(timeout) && process.ExitCode == 0;
            }
        }
    }
}