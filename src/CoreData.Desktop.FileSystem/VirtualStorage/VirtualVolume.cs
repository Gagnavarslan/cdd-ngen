using CoreData.Common.HostEnvironment;
using CoreData.Desktop.Common.Http;
using CoreData.Desktop.FileSystem.LocalFileSystem;
using DokanNet;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

namespace CoreData.Desktop.FileSystem.VirtualStorage
{
    //public interface IServerStorage
    //{

    //}
    //public class ItemFactory<T> : IItemFactory<T>
    //{
    //}
    //public interface IItemFactory<T>
    //{

    //}

    [DebuggerDisplay("{" + nameof(IDebugView.Value) + "}")]
    public class VirtualVolume : IDisposable, IDebugView
    {
        string IDebugView.Value => _settings.Value;

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly Settings.VirtualVolume _settings;
        private readonly IRestClient _restClient;
        private readonly ILocalStorage _localStorage;

        public VirtualVolume(
            Settings.VirtualVolume settings,
            IRestClient restClient,
            ILocalStorage localStorage)
        {
            _settings = settings;
            _restClient = restClient;
            _localStorage = localStorage;
        }

        public bool IsDriverInstalled => Logger.Swallow(() => Dokan.DriverVersion >= 400);

        public IEnumerable<char> GetAvailableDriveLetters()
        {
            var occupiedLetters = new BitVector32(NativeMethods.GetLogicalDrives());
            for (var i = 0; i < 32; i++)
            {
                if (occupiedLetters[i]) yield return (char)('A' + i);
            }
            //while (nonExistingDrives != 0)
            //{
            //    if((1 & nonExistingDrives) == 1)
            //        yield return
            //}
            //AllDriveLetters.Where(letter => ((1 >> letter) & existingDrives)).Select(s => s[0])).ToList();
        }
        //public VirtualDrive(Settings.VirtualDrive settings, IClient client) { }

        //public IClient Client { get; }

        //public ILocalStorage LocalStorage { get; }

        /// <summary>Mounts new virtual file system.</summary>
        /// <seealso cref="https://dokan-dev.github.io/dokany-doc/html/struct_d_o_k_a_n___o_p_t_i_o_n_s.html#a3a1a0f2bd42381d5eb6f53d82d473959" />
        public IVirtualFileSystem Mount()
        {
            var fileSystem = new VirtualFileSystem(_settings, _localStorage);
            try
            {
                fileSystem.Mount(
                    _settings.Drive + ":\\",
                    _settings.MountOptions, // todo: remove DebugMode for release; try out RemovableDrive | MountManager
                    _settings.Threads,
                    _settings.Version,
                    _settings.Timeout,
                    _settings.UncName,
                    _settings.AllocationUnitSize,
                    _settings.SectorSize,
                    _settings.Logger);

                return fileSystem;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new InvalidOperationException($"Failed to mount virtual drive at [{_settings.Drive}]", ex);
            }
        }

        private void Unmount() => Dokan.Unmount(_settings.Drive);

        public void Dispose() => Unmount();
    }
}
