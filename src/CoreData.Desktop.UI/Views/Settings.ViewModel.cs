using CoreData.Common.ModelNotifyChanged;
using System;

namespace CoreData.Desktop.UI.Views
{
    public class SettingsViewModel : ViewModel
    {
        //public OverviewData(): base()
        //{

        //}

        public string Title
        {
            get => Properties.Get("Settings");
            set => Properties.Set(value);
        }

        public string TelemetryHint => "It does not contain neither user nor password." +
            Environment.NewLine + " It may contain some private info like names of folders or files.";
    }
}
