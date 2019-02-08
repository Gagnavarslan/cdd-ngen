using System.Diagnostics;

namespace CoreData.Common.HostEnvironment
{
    public interface IDebugView
    {
        [MonitoringDescription("Debugger|trace|log display value")]
        string Value { get; }
    }
}
