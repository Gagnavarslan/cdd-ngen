using CoreData.Common.HostEnvironment;
using CoreData.Common.ModelNotifyChanged;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace CoreData.Desktop.FileSystem.Settings
{
    [Serializable]
    [DebuggerDisplay("{" + nameof(IDebugInfo.PrintValue) + "}")]
    public class LocalStorage : ViewModel
    {
        [Browsable(false)]
        public override string PrintValue => Path;

        public static readonly LocalStorage Test = new LocalStorage { Path = Environment.CurrentDirectory };

        [DisplayName("Local storage root path. E.g. root folder for physical mirror or path to LiteDB.")]
        public string Path { get; set; }
    }
}
