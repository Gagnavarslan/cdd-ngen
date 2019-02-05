using CoreData.Common.HostEnvironment;
using CoreData.Common.ModelNotifyChanged;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace CoreData.Desktop.FileSystem.Settings
{
    [Serializable]
    [DebuggerDisplay("{" + nameof(IDebugView.Now) + "}")]
    public class LocalStorage : ViewModel
    {
        [Browsable(false)]
        public override string Now => Home;

        [DisplayName("Local storage location. E.g. root folder of physical drive, path to LiteDB, etc.")]
        public string Home { get; set; }
    }
}
