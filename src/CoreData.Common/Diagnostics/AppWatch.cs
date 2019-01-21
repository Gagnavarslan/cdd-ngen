using System;
using System.Diagnostics;

namespace CoreData.Common.HostEnvironment
{
    [Obsolete("Use perf counters?")]
    public static class AppWatch
    {
        public static readonly DateTime StartedOn;
        private static readonly Stopwatch Watch;

        static AppWatch()
        {
            Watch = Stopwatch.StartNew();
            StartedOn = DateTime.Now;
        }

        public static TimeSpan Elapsed => Watch.Elapsed;

        public static TimeSpan Duration(TimeSpan from) => Elapsed.Subtract(from);
    }
}
