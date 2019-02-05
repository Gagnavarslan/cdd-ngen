using CoreData.Common.HostEnvironment;
using NLog;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CoreData.Desktop.UI.AppScope
{
    ///// <summary>Unhandled app exceptions Monitor</summary>
    //public interface IAppHealthMonitor : IDisposable
    //{
    //    Application App { get; }

    //    IFailureHandler FailureHandler { get; }
    //}

    // todo: INotifyDataErrorInfo
    // !!!: Core HealthCheck https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-2.2
    public class AppHealthMonitor : IDisposable //: IAppHealthMonitor
    {
        private readonly ILogger _logger;
        private readonly Application _app;
        private readonly AppDumpWriter _crashDumper;

        public AppHealthMonitor(
            ILogger logger,
            Application app,
            AppDumpWriter crashDumper)
        {
            _logger = logger;
            _app = app; // null for stub
            _crashDumper = crashDumper ?? AppDumpWriter.Null;

            AttachCrashHandling();
        }

        public void Dispose() => DetachCrashHandling();

        private void AttachCrashHandling()
        {
            _logger.Info($"ATTACHING crash monitor TO [{_app}]..");

            WeakEventManager<AppDomain, UnhandledExceptionEventArgs>.AddHandler(
                AppDomain.CurrentDomain, nameof(AppDomain.CurrentDomain.UnhandledException), DomainUnhandled);
            WeakEventManager<Application, DispatcherUnhandledExceptionEventArgs>.AddHandler(
                _app, nameof(Application.DispatcherUnhandledException), AppUnobserved);
            TaskScheduler.UnobservedTaskException += TaskUnobserved;
        }

        private void DetachCrashHandling()
        {
            _logger.Info($"DETACHING crash monitor: [{_app}]..");

            TaskScheduler.UnobservedTaskException -= TaskUnobserved;
            WeakEventManager<Application, DispatcherUnhandledExceptionEventArgs>.RemoveHandler(
                _app, nameof(Application.DispatcherUnhandledException), AppUnobserved);
            WeakEventManager<AppDomain, UnhandledExceptionEventArgs>.RemoveHandler(
                AppDomain.CurrentDomain, nameof(AppDomain.CurrentDomain.UnhandledException), DomainUnhandled);
        }

        #region unobserved event handlers
        void OnUnhandled(Exception failure, string origin)
        {
            _logger.Fatal(failure, $"Unhandled exception origin: {origin}");
            _crashDumper.Dump();
        }
        void DomainUnhandled(object _, UnhandledExceptionEventArgs e) => OnUnhandled(e.ExceptionObject as Exception, "Domain");
        void AppUnobserved(object _, DispatcherUnhandledExceptionEventArgs e) => OnUnhandled(e.Exception, "Dispatcher");
        void TaskUnobserved(object _, UnobservedTaskExceptionEventArgs e) => OnUnhandled(e.Exception, "Scheduler");
        #endregion
    }
}
