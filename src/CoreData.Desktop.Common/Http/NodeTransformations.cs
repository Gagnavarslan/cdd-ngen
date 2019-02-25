using Newtonsoft.Json;

namespace CoreData.Desktop.Common.Models
{
    /// <summary>Http response --[serialization]-> Nav node --[mutation+ext(cache, log, metrics, etc.)]-> FS node.</summary>
    public static class NodeTransformations
    {
        /// <summary>Common for nav nodes json (de)serialization settings value.</summary>
        public static readonly JsonSerializerSettings HttpSerializerSettings =
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                //DateFormatString = "2019-02-18T15:21:05.177000"
            };

        ////public const byte ReadAccess = 0x0;
        //public const byte WriteAccess = 0x1;
        //public const byte RemoveAccess = 0x2;

        //public static bool HasWriteAccess(NavigationNode node)
        //{
        //    //var occupied = new BitVector32(NativeMethods.GetLogicalDrives());
        //    //for (var i = 0; i < 32; i++)
        //    //{
        //    //    if (occupied[i]) yield return (char)('A' + i);
        //    //}
        //}
    }
}
