using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using CoreData.Common.Extensions;
using static System.Environment;

namespace CoreData.Common.HostEnvironment
{
    [DebuggerDisplay("{" + nameof(IDebugView.Value) + "}")]
    public class EnvInfo : IDebugView
    {
        public string Value => $"{OS} {(Is64 ? "x64" : "x86")}";

        public string OS => OSVersion.ToString();

        public bool Is64 => Is64BitOperatingSystem;

        //public string ClrDirectory => RuntimeEnvironment.GetRuntimeDirectory();

        //public string Clr => Environment.ve

        //public string ClrMachineConfigPath => RuntimeEnvironment.SystemConfigurationFile;

        //[System.Obsolete("Use ClrVersion instead.")]
        //public System.Version Clr => Version;

        public string CommandLineArgs => GetCommandLineArgs().Prepend(CommandLine).Join(" ");

        public string Machine => MachineName;

        public string User => $"{UserName}@{UserDomainName}";

        public string Drives => GetLogicalDrives().Join(" | ");

        public string Vars => GetEnvironmentVariables().Cast<DictionaryEntry>().Select(e => $"[{e.Key}={e.Value}]").Join("\t");

        // Detecting Power Events http://www.blackwasp.co.uk/DetectPowerEvents.aspx
        // Detecting User Inactivity http://www.blackwasp.co.uk/InactivityDetection.aspx
    }
}
