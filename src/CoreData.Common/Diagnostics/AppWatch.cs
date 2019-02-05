using NLog;
using System;
using System.Diagnostics;

namespace CoreData.Common.HostEnvironment
{
    [Obsolete("Use perf counters?")]
    public static class AppWatch
    {
        private static readonly ILogger Logger;
        public static readonly DateTime StartedOnUtc;
        private static readonly Stopwatch Watch;

        static AppWatch()
        {
            Logger = LogManager.GetCurrentClassLogger();
            StartedOnUtc = DateTime.UtcNow;
            Watch = Stopwatch.StartNew();

            Logger.Info($"App watch started at(UTC): {StartedOnUtc}");
        }

        public static long ElapsedTicks => Watch.ElapsedTicks;
        public static TimeSpan Elapsed => Watch.Elapsed;

        public static TimeSpan Duration(TimeSpan from) => Elapsed.Subtract(from);
    }
}
