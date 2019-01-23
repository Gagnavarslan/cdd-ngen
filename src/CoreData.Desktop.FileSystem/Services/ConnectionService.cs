using CoreData.Desktop.Common.Http;
using CoreData.Desktop.Common.Tray;
using CoreData.Desktop.FileSystem.LocalFileSystem;
using CoreData.Desktop.FileSystem.Settings;
using CoreData.Desktop.FileSystem.VirtualStorage;
using DokanNet;
using NLog;
using System;
using System.Threading.Tasks;

namespace CoreData.Desktop.FileSystem.Services
{
    public interface IConnectionService
    {
        /// <summary>Opens specified connections session.</summary>
        Task<VirtualDrive> Connect(StoragesUnit unit);

        /// <summary>Restores last succeed connection session if any.</summary>
        Task<VirtualDrive> RestoreLast();
    }

    public class ConnectionService : IConnectionService
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private readonly ITrayTooltipNotifier _tooltipNotifier;
        private readonly Func<Settings.LocalStorage, ILocalStorage> _localStorageFactory;
        private readonly Func<Server.Settings.CoreDataConnection, IRestClient> _coreDataConnectionFactory;
        private readonly Func<Settings.VirtualStorage, IRestClient, ILocalStorage, VirtualDrive> _virtualDriveFactory;

        public ConnectionService(
            ITrayTooltipNotifier tooltipNotifier,
            Func<Settings.LocalStorage, ILocalStorage> localStorageFactory,
            Func<Settings.VirtualStorage, IRestClient, ILocalStorage, VirtualDrive> virtualDriveFactory,
            Func<Server.Settings.CoreDataConnection, IRestClient> coreDataConnectionFactory)
        {
            _tooltipNotifier = tooltipNotifier;
            _localStorageFactory = localStorageFactory;
            _virtualDriveFactory = virtualDriveFactory;
            _coreDataConnectionFactory = coreDataConnectionFactory;
        }

        public async Task<VirtualDrive> Connect(StoragesUnit unit)
        {
            var localStorage = _localStorageFactory(unit.LocalStorage);
            if (localStorage.Exists)
            {
                _tooltipNotifier.Warn("Local storage", "Local storage doesn't exist");
                return null;
            }

            var coreDataClient = _coreDataConnectionFactory(unit.CoreData);
            var authenticated = await coreDataClient.Authenticate();

            var drive = _virtualDriveFactory(unit.VirtualStorage, coreDataClient, localStorage);
            // todo: save drive as last successful connections session, to be able to restore within this or next app session.

            return drive;
        }

        public Task<VirtualDrive> RestoreLast()
        {
            // todo: load last connected session from settings
            var @virtual = new Settings.VirtualStorage()
            {
                MountOptions = DokanOptions.DebugMode | DokanOptions.AltStream | DokanOptions.CurrentSession,
                Drive = 'Z',
                DriveMustBeReused = false,
                Format = "NTFS",
                Label = "CoreData",
                MaxPathLength = 256,
                Features = FileSystemFeatures.CaseSensitiveSearch | FileSystemFeatures.CasePreservedNames
                | FileSystemFeatures.UnicodeOnDisk | FileSystemFeatures.PersistentAcls
            };

            var local = new Settings.LocalStorage { Home = Environment.CurrentDirectory };

            var coreData = new Server.Settings.BasicConnection(
                new Uri("https://test01-dev.coredata.is"), "autoit", "test123!");

            var lastSession = new StoragesUnit(@virtual, local, coreData);

            return Connect(lastSession);
        }
    }
}
