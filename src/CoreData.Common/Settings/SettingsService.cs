using System;
using System.IO.IsolatedStorage;

namespace CoreData.Common.Settings
{
    public class SettingsService : ISettingsService
    {
        //public static readonly ISettingsService Default = new SettingsService();

        public SettingsService()
        {
            SecureSettings = null;

            RoamingSettings = new IsolatedSettingsStorage(IsolatedStorageScope.User | IsolatedStorageScope.Roaming);
            AppUserSettings = new IsolatedSettingsStorage(IsolatedStorageScope.Application | IsolatedStorageScope.User);
            AppSettings = new IsolatedSettingsStorage(IsolatedStorageScope.Machine | IsolatedStorageScope.Application);
            FileSettings = new IsolatedSettingsStorage(IsolatedStorageScope.None);

            EnvVarsSettings = null;
            RegistrySettings = null;
            AppConfigSettings = new AppConfigSettingsStorage();
            AppSwitchSettings = new AppSwitchSettingsStorage();
            CliSettings = null;
        }

        public ISettingsStorage SecureSettings { get; }

        public ISettingsStorage RoamingSettings { get; }
        public ISettingsStorage AppUserSettings { get; }
        public ISettingsStorage AppSettings { get; }
        public ISettingsStorage FileSettings { get; }

        public ISettingsStorage EnvVarsSettings { get; }
        public ISettingsStorage RegistrySettings { get; }
        public ISettingsStorage AppConfigSettings { get; }
        public ISettingsStorage AppSwitchSettings { get; }
        public ISettingsStorage CliSettings { get; }

        public void Dispose()
        {
            SecureSettings.Dispose();

            RoamingSettings.Dispose();
            AppUserSettings.Dispose();
            AppSettings.Dispose();
            FileSettings.Dispose();

            EnvVarsSettings.Dispose();
            RegistrySettings.Dispose();
            AppConfigSettings.Dispose();
            AppSwitchSettings.Dispose();
            CliSettings.Dispose();
        }
    }

    public interface ISettingsService : IDisposable
    {
        ISettingsStorage SecureSettings { get; }

        ISettingsStorage RoamingSettings { get; }
        ISettingsStorage AppUserSettings { get; }
        ISettingsStorage AppSettings { get; }
        ISettingsStorage FileSettings { get; }

        ISettingsStorage EnvVarsSettings { get; }
        ISettingsStorage RegistrySettings { get; }
        ISettingsStorage AppConfigSettings { get; }
        ISettingsStorage AppSwitchSettings { get; }
        ISettingsStorage CliSettings { get; }
    }
}
