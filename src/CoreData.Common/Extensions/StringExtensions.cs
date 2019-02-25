using System.Collections.Generic;

namespace CoreData.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>Returns this value if its not null or empty, otherwise value2.</summary>
        public static string Or(this string ctx, string value2) =>
            string.IsNullOrEmpty(ctx) ? value2 : ctx;

        public static bool IsNullOrEmpty(this string ctx) =>
            string.IsNullOrEmpty(ctx);

        //public static string Join<T>(this IEnumerable<T> ctx, string separ, Func<T, string> convert) =>
        //    Join(ctx.Select(convert), separ);
        public static string Join(this IEnumerable<string> ctx, string separator) =>
            ctx == null ? null : string.Join(separator, ctx);

        public static string ToCharString(this char ctx, int times = 1) =>
            new string(ctx, times);

        public static string AsString(this object obj, string nullValue = "<null>") =>
            obj == null ? nullValue : obj.ToString();
    }
}
