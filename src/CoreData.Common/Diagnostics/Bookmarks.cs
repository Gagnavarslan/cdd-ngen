﻿//using CoreData.Common.HostEnvironment;
//using ImTools;
//using Microsoft.Extensions.Caching.Memory;
//using NLog;
//using System;

//namespace CoreData.Common.Diagnostics
//{
//    public class Bookmarks : MemoryCache
//    {
//        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
//        private static readonly TimeSpan DefaultExpFreq = TimeSpan.FromMinutes(5);

//        public static MemoryCacheOptions Options(Opt<TimeSpan> freq = default, long? maxSize = null) =>
//            new MemoryCacheOptions
//            {
//                ExpirationScanFrequency = freq.OrDefault(DefaultExpFreq),
//                SizeLimit = maxSize
//            };

//        //public static MemoryCacheEntryOptions _entryOptions;

//        public Bookmarks(MemoryCacheOptions options) : base(options) { }

//        // ???: is it ok to have possible key interference or add another SafeXXX method?
//        public ICacheEntry Create(PostEvictionDelegate evicted = null) =>
//            CreateEntry(AppWatch.TimerTicks).RegisterPostEvictionCallback(evicted ?? LogEvicted);

//        public static void LogEvicted(object key, object value, EvictionReason reason, object state)
//        {
//            if (reason != EvictionReason.Removed && reason != EvictionReason.Replaced)
//            {
//                Logger.Warn($"[{key}:{value}] was removed: {reason}. {state}");
//            }
//        }

//        //public static long AddMark(TimeSpan? timeout = null)
//        //{
//        //    var id = Interlocked.Increment(ref CacheId);
//        //    Bookmarks.Set(id, Elapsed, DefaultCacheEntryOptions);
//        //    return id;
//        //}

//        //public static TimeSpan Duration(this Diagnostics.Bookmarks, long bookmark)
//        //{
//        //    if (Bookmarks.TryGetValue<TimeSpan>(bookmark, out var from))
//        //    {
//        //        return Duration(from);
//        //    }
//        //    Logger.Warn($"Timer bookmark wasn't found: {bookmark}");
//        //    return Timeout.InfiniteTimeSpan;
//        //}
//    }
//}
