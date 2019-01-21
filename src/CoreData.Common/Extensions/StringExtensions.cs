using System;
using System.Collections.Generic;

namespace CoreData.Common.Extensions
{
    public static class StringExtensions
    {
        private static readonly int CharSize = sizeof(char);

        public static byte[] ToRawBytes(this string ctx)
        {
            var bytes = new byte[ctx.Length * CharSize];
            Buffer.BlockCopy(ctx.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string ToRawString(this byte[] ctx)
        {
            var chars = new char[ctx.Length / CharSize];
            Buffer.BlockCopy(ctx, 0, chars, 0, ctx.Length);
            return new string(chars);
        }

        public static bool IsNullOrEmpty(this string ctx) =>
            string.IsNullOrEmpty(ctx);

        //public static string Join<T>(this IEnumerable<T> ctx, string separ, Func<T, string> convert) =>
        //    Join(ctx.Select(convert), separ);
        public static string Join(this IEnumerable<string> ctx, string separator) =>
            string.Join(separator, ctx);

        public static string ToCharString(this char ctx, int times = 1) =>
            new string(ctx, times);
    }
}
