using CoreData.Common.HostEnvironment;
using ImTools;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using System;

namespace CoreData.Common.Diagnostics
{
    public class Bookmarks : MemoryCache
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static MemoryCacheOptions Options(Opt<TimeSpan> frequency = default, long? sizeLimit = null) =>
            new MemoryCacheOptions
            {
                ExpirationScanFrequency = frequency.HasValue ? frequency.Value : TimeSpan.FromMinutes(5),
                SizeLimit = sizeLimit
            };
        public static MemoryCacheEntryOptions _entryOptions;

        public Bookmarks(MemoryCacheOptions options) : base(options) { }

        // ???: is it ok to have possible interference or add another SafeXXX method?
        public ICacheEntry Create(PostEvictionDelegate evicted = null) =>
            CreateEntry(AppWatch.ElapsedTicks).RegisterPostEvictionCallback(evicted ?? LogEvicted);

        public static void LogEvicted(object key, object value, EvictionReason reason, object state)
        {
            if (reason != EvictionReason.Removed && reason != EvictionReason.Replaced)
            {
                Logger.Warn($"[{key}:{value}] was removed: {reason}. {state}");
            }
        }

        //public static long AddMark(TimeSpan? timeout = null)
        //{
        //    var id = Interlocked.Increment(ref CacheId);
        //    Bookmarks.Set(id, Elapsed, DefaultCacheEntryOptions);
        //    return id;
        //}

        //public static TimeSpan Duration(this Diagnostics.Bookmarks, long bookmark)
        //{
        //    if (Bookmarks.TryGetValue<TimeSpan>(bookmark, out var from))
        //    {
        //        return Duration(from);
        //    }
        //    Logger.Warn($"Timer bookmark wasn't found: {bookmark}");
        //    return Timeout.InfiniteTimeSpan;
        //}
    }
}
