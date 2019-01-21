using Newtonsoft.Json;
using NLog;
using Shielded;
using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace CoreData.Common.Settings
{
    public class IsolatedSettingsStorage : ISettingsStorage
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private readonly IsolatedStorageFile _storage;
        private readonly ShieldedDict<string, object> _settings;
        private bool _disposed;
        //private static SpinLock _spinLock = new SpinLock(SettingsService.Default.AppSwitchSettings.Read<bool>()); // SpinLockThrowsOnReenter

        public IsolatedSettingsStorage(IsolatedStorageScope scope)
        {
            _storage = IsolatedStorageFile.GetStore(scope, null);
            _settings = new ShieldedDict<string, object>(StringComparer.CurrentCulture);
            _disposed = false;
        }

        public bool Exists(string key) => _storage.FileExists(key);

        public void Remove(string key) => _storage.DeleteFile(key);

        public void Write<T>(string key, T value)
        {
            try
            {
                using (var file = _storage.OpenFile(key, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    var s = JsonConvert.SerializeObject(value);
                    using (var writer = new StreamWriter(file))
                    {
                        writer.Write(s);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Failed to save #settings");
            }
        }

        public T Read<T>(string key, T @default)
        {
            try
            {
                using (var file = _storage.OpenFile(key, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(file))
                    {
                        var s = reader.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(s);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Failed to load #settings");
                return @default;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _storage.Dispose();
            _disposed = true;
        }
    }
}
