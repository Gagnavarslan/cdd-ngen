using System.Diagnostics;

namespace CoreData.Common.HostEnvironment
{
    public interface ITraceView
    {
        [MonitoringDescription("Debugger|trace|log display value")]
        string Value { get; }
    }
}
