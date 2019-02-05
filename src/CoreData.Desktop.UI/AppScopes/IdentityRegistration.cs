using System;
using System.Diagnostics;
using System.Threading;
using NLog;

namespace CoreData.Desktop.UI.AppScope
{
    public partial class IdentityRegistration : IDisposable
    {
        private readonly ILogger _logger;
        //private readonly Lazy<ITrayTooltipNotifier> _trayNotifier;
        private readonly EventWaitHandle _eventHandle;

        //public SessionIdentityRegistration(Lazy<ITrayTooltipNotifier> trayNotifier)
        public IdentityRegistration(ILogger logger)
        {
            _logger = logger;
            var identity = new SessionIdentity(Process.GetCurrentProcess());
            _eventHandle = new EventWaitHandle(false, EventResetMode.AutoReset, identity.Id, out var created);
            //_trayNotifier = trayNotifier;
        }

        //public bool AlreadyInUse()
        //{
        //    var process = Process.GetCurrentProcess();

        //    var existingProcess = Process.GetProcessesByName(process.ProcessName).FirstOrDefault(p => IsAnotherProcessOfUser(process, p));
        //    if (existingProcess != null)
        //    {
        //        _logger.Error("Already running another instance of CoreData Desktop!");
        //        NotifyExistingInstance(existingProcess);
        //        return true;
        //    }
        //}

        //private static void NotifyAboutExistingInstance(Process existingProcess)
        //{
        //    string globalEventName = GetProcessEventName(existingProcess);
        //    EventWaitHandle alreadyExistingAppSingleton;

        //    if (!EventWaitHandle.TryOpenExisting(globalEventName, out alreadyExistingAppSingleton))
        //    {
        //        Log.Error("Can't open existing process event handle");
        //        return;
        //    }

        //    alreadyExistingAppSingleton.Set();
        //}

        public void Dispose() => _eventHandle.Dispose();
    }
}
