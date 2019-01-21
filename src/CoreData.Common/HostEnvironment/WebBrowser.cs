using System.Diagnostics;
using ImTools;
using NLog;

namespace CoreData.Common.HostEnvironment
{
    public class WebBrowser : IWebBrowser
    {
        private readonly ILogger _logger;
        private readonly AppInfo _appInfo;
        private readonly IShell<ProcessStartInfo> _commandRunner;

        public WebBrowser(
            ILogger logger,
            AppInfo appInfo,
            IShell<ProcessStartInfo> commandRunner)
        {
            _logger = logger;
            _appInfo = appInfo;
            _commandRunner = commandRunner;
        }

        /// <summary>Opening an URL in the default browser 
        /// <see cref="http://byteflux.me/opening-an-url-in-the-default-browser-on-windows-8/"/></summary>
        public void OpenInDefaultBrowser(string url)
        {
            _logger.Info($"Openning [{url}] with default browser..");

            var command = _appInfo.IsRunningElevated()
                ? _commandRunner.Build("explorer.exe").Do(setAsElevated)
                : _commandRunner.Build(url);
            _commandRunner.Run(command);

            ShellCommand<ProcessStartInfo> setAsElevated(ShellCommand<ProcessStartInfo> cmd)
            {
                cmd.StartInfo.Arguments = url;
                cmd.StartInfo.UseShellExecute = true;
                cmd.StartInfo.Verb = "runas";
                return cmd;
            }
        }
    }
}
