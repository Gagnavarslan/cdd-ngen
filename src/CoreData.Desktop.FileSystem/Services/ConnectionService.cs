using CoreData.Desktop.Common.Http;
using CoreData.Desktop.Common.Tray;
using CoreData.Desktop.FileSystem.LocalFileSystem;
using CoreData.Desktop.FileSystem.VirtualStorage;
using NLog;
using System;
using System.Threading.Tasks;

namespace CoreData.Desktop.FileSystem.Services
{
    public interface IConnectionService
    {
        Task<VirtualDrive> Connect(
            Settings.LocalStorage localSettings,
            Settings.VirtualStorage virtualSettings,
            Server.Settings.ConnectionInfo coreDataSettings);
    }

    public class ConnectionService : IConnectionService
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private readonly ITrayTooltipNotifier _tooltipNotifier;
        private readonly Func<Settings.LocalStorage, ILocalStorage> _localStorageFactory;
        private readonly Func<Server.Settings.ConnectionInfo, IRestClient> _coreDataConnectionFactory;
        private readonly Func<Settings.VirtualStorage, IRestClient, ILocalStorage, VirtualDrive> _virtualDriveFactory;

        public ConnectionService(
            ITrayTooltipNotifier tooltipNotifier,
            Func<Settings.LocalStorage, ILocalStorage> localStorageFactory,
            Func<Settings.VirtualStorage, IRestClient, ILocalStorage, VirtualDrive> virtualDriveFactory,
            Func<Server.Settings.ConnectionInfo, IRestClient> coreDataConnectionFactory)
        {
            _tooltipNotifier = tooltipNotifier;
            _localStorageFactory = localStorageFactory;
            _virtualDriveFactory = virtualDriveFactory;
            _coreDataConnectionFactory = coreDataConnectionFactory;
        }

        public async Task<VirtualDrive> Connect(Settings.LocalStorage localSettings, Settings.VirtualStorage virtualSettings,
            Server.Settings.ConnectionInfo coreDataSettings)
        {
            var localStorage = _localStorageFactory(localSettings);
            if (localStorage.Exists)
            {
                _tooltipNotifier.Warn("Local storage", "Local storage doesn't exist");
                return null;
            }

            var coreDataClient = _coreDataConnectionFactory(coreDataSettings);
            var authenticated = await coreDataClient.Authenticate();

            return _virtualDriveFactory(virtualSettings, coreDataClient, localStorage);
        }
    }
}
