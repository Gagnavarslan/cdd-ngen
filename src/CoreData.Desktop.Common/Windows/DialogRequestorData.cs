using CoreData.Common.ModelNotifyChanged;

namespace CoreData.Desktop.Common.Windows
{
    /// <summary>Base class for viewmodels raising dialogs.</summary>
    public abstract class DialogRequestorData : ViewModel
    {
        public DialogRequestorData(IDialogService dialogs) => Dialogs = dialogs;

        public IDialogService Dialogs { get; }
    }
}
