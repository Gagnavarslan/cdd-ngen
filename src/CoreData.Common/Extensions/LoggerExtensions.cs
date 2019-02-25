using System;

namespace CoreData.Common.Extensions
{
    //[ExcludeFromCodeCoverage]
    public static class LoggerExtensions
    {
        //public static string Tag(this string value) => Tags.Tag(value);

        public static readonly TimeSpan RestActionThreshold = TimeSpan.FromSeconds(10);
        public static readonly TimeSpan LocalActionThreshold = TimeSpan.FromSeconds(2);
        public const string PerfCounterCategory = "CDD.ElapsedTimeCategory";
        //private static readonly ILogger Logger;

        static LoggerExtensions()
        {
            //Logger = LogManager.GetCurrentClassLogger();

            // There is a latency to enable the counters, they should be created prior to executing the app.
            // Execute this a 2nd time to use the category.
            //if (PerformanceCounterCategory.Exists(PerfCounterCategory))
            //{
            //}
        }

        [Obsolete("Is it?")]
        public static string ToTypedString<T>(this T sender)
        { //todo: reuse 
            //Contract.Requires<ArgumentNullException>()
            var typeCode = Type.GetTypeCode(sender?.GetType());
            return $"[{sender}:{typeCode}]";
        }

        //log.InfoFormat("Operating System: {0}, Architecture : x{1}, Process: x{2} ",
        //        Environment.OSVersion,
        //        Environment.Is64BitOperatingSystem? 64 : 32,
        //        Environment.Is64BitProcess? 64 : 32);
        //    log.Info($".NET: {Environment.Version}");
        //    log.Info($"Application: {appName}");
        //    log.Info($"Logged-in user: {Environment.UserName}");
        //    log.Info($"Application running-as user: {WindowsIdentity.GetCurrent()?.Name}");

        //public static (bool exceeds, string data) Log(this TimeSpan duration,
        //    TimeSpan threshold = Timeout.InfiniteTimeSpan, ILogger logger = null, [CallerMemberName]string caller = null) =>
        //    duration.TotalMinutes != 0
        //    ? ($"({duration:m:%s\\.ff})", true)
        //    : ($"({duration:%s\\.fff})", false);
        //public static (string data, bool warn) Log(this TimeSpan duration) =>
        //    duration.TotalMinutes != 0
        //    ? ($"({duration:m:%s\\.ff})", tr
        //    ue)
        //    : ($"({duration:%s\\.fff})", false);
    }
}
