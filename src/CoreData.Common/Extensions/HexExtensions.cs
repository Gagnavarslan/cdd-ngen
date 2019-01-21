using System.Diagnostics.CodeAnalysis;
using static System.Globalization.CultureInfo;
using static System.Globalization.NumberStyles;

namespace CoreData.Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HexExtensions
    {
        //private static readonly ArraySegment<NumberStyles> HexNumberStyles =
        //    new ArraySegment<NumberStyles>(new[] { NumberStyles.HexNumber, NumberStyles.AllowHexSpecifier });
        public static bool IsHex(this string sender, bool allowSpaces = true) =>
            long.TryParse(sender, allowSpaces ? HexNumber : AllowHexSpecifier, InvariantCulture, out var _);
    }
}
