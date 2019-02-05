using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace CoreData.Common.HostEnvironment
{
    [DebuggerDisplay("{" + nameof(IDebugView.Now) + "}")]
    public class AppInfo : IDebugView
    {
        public string Now => $"{Product} {Version}";

        public AppInfo(Assembly main)
        {
            Path = new Uri(main.CodeBase).LocalPath;
            Location = System.IO.Path.GetDirectoryName(Path);
            Location2 = main.Location;
            var info = FileVersionInfo.GetVersionInfo(Path);
            //Product = main.GetCustomAttribute<AssemblyProductAttribute>().Product;
            //Title = main.GetCustomAttribute<AssemblyTitleAttribute>().Title;
            //Company = main.GetCustomAttribute<AssemblyCompanyAttribute>().Company;
            //InfoVersion = main.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            //Version = main.GetName().Version.ToString();
            //Description = main.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
            //ImageRuntimeVersion = main.ImageRuntimeVersion;
        }

        public bool Is64Bit => Environment.Is64BitProcess;

        public string Path { get; }
        public string Location { get; }
        public string Location2 { get; }
        public string Company { get; }
        public string Product { get; }
        public string InfoVersion { get; }
        public string Version { get; }
        public string Title { get; }
        public string Description { get; }
        public string ImageRuntimeVersion { get; }

        /// <summary>Checks if app's process run is elevated. 
        /// <see cref="https://stackoverflow.com/a/31856353"/></summary>
        public bool IsRunningElevated()
        {
            var id = WindowsIdentity.GetCurrent();
            return id.Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid)
                && id.Owner != id.User;
        }
    }

    /// <summary>App runtime properties.</summary>
    [DebuggerDisplay("{" + nameof(IDebugView.Now) + "}")]
    public class AppRuntime : IDebugView
    {
        public string Now => $"{ClrVersion}({Clr}) : {ClrDirectory}";

        public string ClrDirectory => RuntimeEnvironment.GetRuntimeDirectory();

        public string ClrVersion => RuntimeEnvironment.GetSystemVersion();

        public string ClrMachineConfigPath => RuntimeEnvironment.SystemConfigurationFile;

        [System.Obsolete("Use ClrVersion instead.")]
        public System.Version Clr => Environment.Version;
    }
}
