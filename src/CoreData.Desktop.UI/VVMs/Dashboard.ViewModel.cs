using CoreData.Common.ModelNotifyChanged;

namespace CoreData.Desktop.UI.VVMs
{
    // !!!: IViewManager loader - sln from Catel http://docs.catelproject.com/vnext/tips-tricks/mvvm/finding-view-of-view-model/ 
    //public interface IOverview : IView<Data> { }
    //IsBusy
    public class DashboardViewModel : ViewModel
    {
        public string Title
        {
            get => Properties.Get("Dashboard overview");
            set => Properties.Set(value);
        }
    }
}
