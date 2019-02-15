using System;
using System.Configuration;

namespace CoreData.Common.Settings
{
    public class AppConfigSettingsStorage : ISettingsStorage
    { // todo: aggr AppSettings(local impl) and AppSwitches(AppSwitchSettingsStorage) into one class
        public AppConfigSettingsStorage()
        {
            //ConfigurationManager.AppSettings["Environment"]
        }

        public bool Exists(string key) => ConfigurationManager.AppSettings.Get(key) != null;
        public void Remove(string key) => throw new NotImplementedException();
        public void Write<T>(string key, T value) => throw new NotImplementedException();
        public T Read<T>(string key, T @default) => throw new NotImplementedException();
        public void Dispose() => throw new NotImplementedException();
    }
    // SpinLockThrowsOnReenter
}
