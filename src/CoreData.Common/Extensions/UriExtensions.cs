using System;

namespace CoreData.Common.Extensions
{
    //[ExcludeFromCodeCoverage]
    public static class UriExtensions
    {
        public static bool Equals(this UriComponents @this, Uri a, Uri b,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return a != null && b != null
                && Uri.Compare(a, b, @this, UriFormat.SafeUnescaped, comparison) == 0;
            //return string.Equals(sender.IdnHost, other.IdnHost);
            // see also Uri.IsBaseOf
        }
    }
}
