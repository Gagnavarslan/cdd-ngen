using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace CoreData.Common.HostEnvironment
{
    [DebuggerDisplay("{" + nameof(IDebugView.Value) + "}")]
    public class AppInfo : IDebugView
    {
        public string Value => Title;

        public static readonly BooleanSwitch ExtendedTraceValue =
            new BooleanSwitch(nameof(ExtendedTraceValue), "Is IDebugView.Value extended");

        public AppInfo(Assembly main)
        {
            Path = new Uri(main.CodeBase).LocalPath;
            Location = System.IO.Path.GetDirectoryName(Path);
            var info = FileVersionInfo.GetVersionInfo(Path);
            Product = info.ProductName;
            Version = $"{info.ProductMajorPart}.{info.ProductMinorPart}.{info.ProductBuildPart}.{info.ProductPrivatePart}"; // "5.0.0.0"
            Version2 = info.ProductVersion; // "5.0-rc1"
            Company = info.CompanyName;
            ClrVersion = main.ImageRuntimeVersion;
            Title = $"{Product} v{Version}";
            //Product = main.GetCustomAttribute<AssemblyProductAttribute>().Product;
            //Company = main.GetCustomAttribute<AssemblyCompanyAttribute>().Company;
            //InfoVersion = main.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            //Version = main.GetName().Version.ToString();
        }

        public bool Is64Bit => Environment.Is64BitProcess;

        public string Path { get; }
        public string Location { get; }
        public string Company { get; }
        public string Product { get; }
        public string Version { get; }
        public string Version2 { get; }
        public string Title { get; }
        public string ClrVersion { get; }

        /// <summary>Checks if app's process run is elevated. 
        /// <see cref="https://stackoverflow.com/a/31856353"/></summary>
        public bool IsRunningElevated()
        {
            var id = WindowsIdentity.GetCurrent();
            return id.Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid)
                && id.Owner != id.User;
        }
    }

    /// <summary>App runtime properties. Cross-platform alt.</summary>
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public class AppRuntime : IDebugView
    {
        public string Value => $"{ClrVersion} {ClrDirectory}";

        public static readonly AppRuntime Current = new AppRuntime();
        AppRuntime() { }

        public string ClrDirectory { get; } = RuntimeEnvironment.GetRuntimeDirectory();

        public string ClrVersion { get; } = RuntimeEnvironment.GetSystemVersion();

        public string ClrMachineConfigPath { get; } = RuntimeEnvironment.SystemConfigurationFile;

        //[System.Obsolete("Use ClrVersion instead.")]
        //public System.Version Clr { get; } = Environment.Version;
    }
}
