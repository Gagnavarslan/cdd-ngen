using NLog;
using System;
using System.Diagnostics;

namespace CoreData.Common.HostEnvironment
{
    [Obsolete("Use perf counters?")]
    public static class AppWatch
    {
        private static readonly ILogger Logger;
        private static readonly BooleanSwitch UseEnvironmentSwitch;

        private static readonly Stopwatch Watch;

        public static readonly DateTime Created;

        static AppWatch()
        {
            Logger = LogManager.GetCurrentClassLogger();
            UseEnvironmentSwitch = new BooleanSwitch("UseEnvironmentWatch",
                "AppWatch uses Environment.TickCount instead of own Stopwatch");

            Watch = Stopwatch.StartNew();
            Created = DateTime.UtcNow;
            Logger.Info($"App watch was created and started at(UTC): {Created}");
        }

        //public static long TimerTicks => Watch.ElapsedTicks;

        public static TimeSpan Elapsed => Watch.Elapsed;

        public static TimeSpan Duration(TimeSpan from) => Elapsed.Subtract(from);
    }
}
