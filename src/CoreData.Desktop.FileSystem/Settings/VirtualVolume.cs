using CoreData.Common.HostEnvironment;
using CoreData.Common.ModelNotifyChanged;
using DokanNet;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace CoreData.Desktop.FileSystem.Settings
{
    [Serializable]
    [DefaultProperty(nameof(MountOptions))]
    [DebuggerDisplay("{" + nameof(IDebugView.Value) + "}", Name = "VirtualVolumeSettings")]
    // todo: PropertyGrid: here and for all settings - 'Custom Editors with Attributes'
    // https://xceed.com/wp-content/documentation/xceed-toolkit-plus-for-wpf/webframe.html#PropertyGrid%20class.html
    public class VirtualVolume : ViewModel
    {
        [Browsable(false)]
        public override string Value => $"{Drive}: {Label}({Format})";

        // todo: Revisit once FileExplorer become LongPath aware https://blogs.msdn.microsoft.com/jeremykuhne/2016/07/30/net-4-6-2-and-long-paths-on-windows-10/
        //public const int MaxPath = 256;

        //public VirtualStorage(DokanOptions options, char drive)
        //{
        //    Drive = drive;
        //    Format = "NTFS";
        //    Label = "CoreData";
        //    MountOptions = DokanOptions.DebugMode
        //        | (network ? DokanOptions.NetworkDrive : DokanOptions.FixedDrive);
        //}
        [Category("Mounting point")]
        [DisplayName("Drive mounting options")]
        //[Description("This property must not be set manually via settings editor.")]
        [DefaultValue(DokanOptions.DebugMode | DokanOptions.AltStream | DokanOptions.CurrentSession)]
        public DokanOptions MountOptions { get; set; } // https://dokan-dev.github.io/dokan-dotnet-doc/html/namespace_dokan_net.html#a8b96a20dbe630fffdb505ca7ff3c32a6

        [Category("Mounting point")]
        [DisplayName("Local drive letter, 'Z' by default")]
        //[Description("This property must not be set manually via settings editor.")]
        [DefaultValue('Z')]
        public char Drive { get; set; }
        [Description("Drive letter reusage policy. Forced to be the same or closest available drive letter.")]
        public bool DriveMustBeReused { get; set; }

        [Category("File System")]
        [DisplayName("Format name")]
        [DefaultValue("NTFS")]
        public string Format { get; set; }

        [Category("File System")]
        [DisplayName("Volume label")]
        [DefaultValue("CoreData")]
        public string Label { get; set; }

        [Category("File System")]
        [DisplayName("Maximum PATH length")]
        [DefaultValue(32_767)]
        public uint MaxPathLength { get; set; }

        [Category("File System")]
        [DisplayName("VFS features: https://dokan-dev.github.io/dokan-dotnet-doc/html/namespace_dokan_net.html#a0e59c383e7aa7666852adcfa27b03b30")]
        [DefaultValue(FileSystemFeatures.CaseSensitiveSearch | FileSystemFeatures.CasePreservedNames
            | FileSystemFeatures.UnicodeOnDisk)] // | FileSystemFeatures.PersistentAcls | SupportsRemoteStorage
        // todo: use Enums.NET to convert, e.g. FlagEnums.ParseFlags<FileSystemFeatures>("CaseSensitiveSearch | CasePreservedNames", delimiter: "|") https://github.com/TylerBrinkley/Enums.NET
        public FileSystemFeatures Features { get; set; }

        [Category("File System")]
        [DisplayName("Maximum degree of parallelism")]
        [DefaultValue(1)]
        public int Threads { get; set; } // todo: try out 'Environment.ProcessorCount / 2' with DokanV2

        public int Version { get; set; } = 120;

        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(20);

        public string UncName { get; set; }
        public int AllocationUnitSize { get; set; } = 512;
        public int SectorSize { get; set; } = 512;
        public DokanNet.Logging.ILogger Logger { get; set; }
    }
}
