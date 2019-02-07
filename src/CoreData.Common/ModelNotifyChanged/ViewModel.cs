using CoreData.Common.HostEnvironment;
using System.ComponentModel;
using System.Diagnostics;

namespace CoreData.Common.ModelNotifyChanged
{
    // todo: IChangeTracking https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.ichangetracking?view=netframework-4.7.2
    [DebuggerTypeProxy(typeof(DebugView))]
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public abstract class ViewModel : INotifyPropertyChanged, IDebugView
    {
        public virtual string Value => new DebugView(this).Value;

        public event PropertyChangedEventHandler PropertyChanged;

        protected ViewModel() => Properties = Properties.Empty(Raise);

        protected virtual Properties Properties { get; }

        protected virtual void Raise(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        sealed class DebugView
        {
            private readonly ViewModel _target;
            public DebugView(ViewModel target) => _target = target;

            public string Value => _target.Properties.Value;
        }
    }
}
