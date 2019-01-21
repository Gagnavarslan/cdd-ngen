using Microsoft.Extensions.Caching.Memory;
using NLog;
using System;
using System.Diagnostics;
using System.Threading;

namespace CoreData.Common.HostEnvironment
{
    [Obsolete("Use perf counters?")]
    public static class AppWatch
    {
        private static readonly ILogger Logger;

        public static readonly DateTime StartedOnUtc;
        private static readonly Stopwatch Watch;
        private static readonly MemoryCache Bookmarks;
        private static readonly MemoryCacheEntryOptions DefaultCacheEntryOptions;
        private static long CacheId;

        static AppWatch()
        {
            Logger = LogManager.GetCurrentClassLogger();

            Watch = Stopwatch.StartNew();
            StartedOnUtc = DateTime.UtcNow;

            var cacheOptions = new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromSeconds(10),
                SizeLimit = 2048
            };
            Bookmarks = new MemoryCache(cacheOptions);

            DefaultCacheEntryOptions = new MemoryCacheEntryOptions
            {
                Size = 1,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            }
            .RegisterPostEvictionCallback(OnBookmarkEvicted);

            Logger.Info($"App watch started at(UTC): {StartedOnUtc}");
        }

        public static long AddMark(TimeSpan? timeout = null)
        {
            var id = Interlocked.Increment(ref CacheId);
            Bookmarks.Set(id, Elapsed, DefaultCacheEntryOptions);
            return id;
        }

        public static TimeSpan Duration(long bookmark)
        {
            if (Bookmarks.TryGetValue<TimeSpan>(bookmark, out var from))
            {
                return Duration(from);
            }
            Logger.Warn($"Timer bookmark wasn't found: {bookmark}");
            return Timeout.InfiniteTimeSpan;
        }

        public static TimeSpan Elapsed => Watch.Elapsed;

        public static TimeSpan Duration(TimeSpan from) => Elapsed.Subtract(from);

        private static void OnBookmarkEvicted(object key, object value, EvictionReason reason, object state)
        {
            if (reason != EvictionReason.Removed && reason != EvictionReason.Replaced)
            {
                Logger.Warn($"[{key} | {value}] was removed: {reason}. {state}");
            }
        }
    }
}
