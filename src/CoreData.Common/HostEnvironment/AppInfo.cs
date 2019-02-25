using CoreData.Common.Extensions;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace CoreData.Common.HostEnvironment
{
    [DebuggerDisplay("{" + nameof(TraceExtensions.GetTraceValue) + "}")]
    public partial class AppInfo : ITraceView
    {
        public string Value => Title;// ? Title : $"{Title}({Version2})";
    }
    public sealed partial class AppInfo
    {
        public AppInfo(Assembly main)
        {
            Path = new Uri(main.CodeBase).LocalPath;
            var info = FileVersionInfo.GetVersionInfo(Path);
            Product = info.ProductName;
            Version = $"{info.ProductMajorPart}.{info.ProductMinorPart}.{info.ProductBuildPart}.{info.ProductPrivatePart}"; // "5.0.0.0"
            Version2 = info.ProductVersion; // "5.0-rc1"
            Company = info.CompanyName;
            Clr = main.ImageRuntimeVersion;
            Title = $"{Product} v{Version}";
            //Product = main.GetCustomAttribute<AssemblyProductAttribute>().Product;
            //Company = main.GetCustomAttribute<AssemblyCompanyAttribute>().Company;
            //InfoVersion = main.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            //Version = main.GetName().Version.ToString();
        }

        public string Path { get; }
        public string Location => System.IO.Path.GetDirectoryName(Path);
        public string Company { get; }
        public string Product { get; }
        public string Version { get; }
        public string Version2 { get; }
        public string Title { get; }
        public string Clr { get; }
        public bool Is64 => Environment.Is64BitProcess;

        /// <summary>Checks if app's process run is elevated. 
        /// <see cref="https://stackoverflow.com/a/31856353"/></summary>
        public bool IsRunningElevated()
        {
            var id = WindowsIdentity.GetCurrent();
            return id.Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid)
                && id.Owner != id.User;
        }
    }

    ///// <summary>App runtime properties. Cross-platform alt.</summary>
    //[DebuggerDisplay("{" + nameof(Value) + "}")]
    //public class AppRuntime : IDebugView
    //{
    //    public string Value => $"{ClrVersion} {ClrDirectory}";

    //    public static readonly AppRuntime Current = new AppRuntime();
    //    AppRuntime() { }

    //    public string ClrDirectory { get; } = RuntimeEnvironment.GetRuntimeDirectory();

    //    public string ClrVersion { get; } = RuntimeEnvironment.GetSystemVersion();

    //    public string ClrMachineConfigPath { get; } = RuntimeEnvironment.SystemConfigurationFile;

    //    //[System.Obsolete("Use ClrVersion instead.")]
    //    //public System.Version Clr { get; } = Environment.Version;
    //}
}
