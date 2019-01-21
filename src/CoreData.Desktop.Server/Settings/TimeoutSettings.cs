using System;
using System.Threading;

namespace CoreData.Desktop.Server.Settings
{
    public class TimeoutSettings
    {
        public TimeSpan AuthTimeout { get; set; } = Timeout.InfiniteTimeSpan;
    }
}
