using CoreData.Desktop.Common.Runtime;
using CoreData.Desktop.UI.Tray;
using NLog;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CoreData.Desktop.UI.AppScope
{
    /// <summary>App session scope - Startup and Disposal.</summary>
    public class AppSession : IDisposable
    {
        public static AppSession Create(ScopeRules with)
        {
            var logger = LogManager.GetLogger("App");
            if (!with.SkipSingletonValidation && !SingletonScopeRule.Verify())
            {
                logger.Error("Single app-user session validity check has failed");
                return null;
            }
            return new AppSession(with, logger);
        }

        private readonly ScopeRules _rules;
        private readonly ILogger _logger;
        //private TaskbarIcon _tray;
        private IoC.Container _container;

        AppSession(ScopeRules rules, ILogger logger)
        {
            _rules = rules;
            _logger = logger;
        }

        public void Initialize()
        {
            DebugOnlySession.Attach();
            //_tray = (TaskbarIcon)Application.Current.Resources["SysTray"];
            _container = new IoC.Container();
            var tray = _container.Resolve<AppTrayView>();
            tray.LoadContent();

            //ICoreDataServiceFactory
            //_container.BeginLifetimeScope()
            //Task.Run(async () => await WarnOthers().ConfigureAwait(false));
        }

        /// <summary>Completes App Session due to Logoff or Shutdown.</summary>
        /// <returns>Returns task that represents completion of App services.</returns>
        // ???: result is ignored and will be replaced with simple Task
        public Task<bool> Complete(ReasonSessionEnding reason)
        {
            _logger.Warn($"App completion requested due to (logoff|shutdown): [{reason}]");
            // Completion with TPLDataflow - https://stackoverflow.com/a/21491180
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _container.Dispose();
            //_tray.Dispose();
        }
    }
}
