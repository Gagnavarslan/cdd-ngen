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
    public interface ICoreDataDriveService
    {
        /// <summary>Opens specified connections session.</summary>
        Task<VirtualDrive> Connect(CoreDataStorage unit);

        /// <summary>Restores last succeed connection session if any.</summary>
        Task<VirtualDrive> Restore();
    }

    public class CoreDataDriveService : ICoreDataDriveService
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private readonly ITrayTooltipNotifier _trayNotifier;
        private readonly Func<Settings.LocalStorage, ILocalStorage> _localStorageFactory;
        private readonly Func<Server.Settings.Connection, IRestClient> _coreDataConnectionFactory;
        private readonly Func<Settings.VirtualStorage, IRestClient, ILocalStorage, VirtualDrive> _virtualDriveFactory;
        private readonly Balloon _connectError;

        public CoreDataDriveService(
            ITrayTooltipNotifier trayNotifier,
            Func<Settings.LocalStorage, ILocalStorage> localStorageFactory,
            Func<Settings.VirtualStorage, IRestClient, ILocalStorage, VirtualDrive> virtualDriveFactory,
            Func<Server.Settings.Connection, IRestClient> coreDataConnectionFactory)
        {
            _trayNotifier = trayNotifier;
            _localStorageFactory = localStorageFactory;
            _virtualDriveFactory = virtualDriveFactory;
            _coreDataConnectionFactory = coreDataConnectionFactory;

            _connectError = new Balloon("CoreData storage",
                "Local storage doesn't exist and CoreData connection has failed");
        }

        public async Task<VirtualDrive> Connect(CoreDataStorage session)
        {
            var localStorage = _localStorageFactory(session.LocalStorage);
            var coreDataClient = _coreDataConnectionFactory(session.CoreData);
            var authenticated = await coreDataClient.Authenticate();

            if (!localStorage.Exists && !authenticated)
            {
                _trayNotifier.Warn(_connectError);
                return null;
            }

            var drive = _virtualDriveFactory(session.VirtualStorage, coreDataClient, localStorage);
            // todo: save drive as last successful connections session, to be able to restore within this or next app session.

            return drive;
        }

        public Task<VirtualDrive> Restore()
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
            // todo: ^^^

            var lastSession = new CoreDataStorage(@virtual, local, coreData);

            return Connect(lastSession);
        }
    }
}
