using System;
using System.Runtime.Serialization;
using CoreData.Common.Extensions;
using Newtonsoft.Json;

namespace CoreData.Desktop.Common.Models
{
    [Flags]
    public enum Access
    {
        Read = 0,
        Write = 0x1,
        Remove = 0x2
    }

    /// <summary>Http response --[serialization]-> Nav node --[mutation+ext(cache, log, metrics, etc.)]-> FS node.</summary>
    public static class NodeTransformations
    {
        /// <summary>Common for nav nodes json (de)serialization settings value.</summary>
        public static readonly JsonSerializerSettings HttpSerializerSettings =
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        //public const byte ReadAccess = 0x0;
        public const byte WriteAccess = 0x1;
        public const byte RemoveAccess = 0x2;

        public static bool HasWriteAccess(NavigationNode node)
        {
            //var occupied = new BitVector32(NativeMethods.GetLogicalDrives());
            //for (var i = 0; i < 32; i++)
            //{
            //    if (occupied[i]) yield return (char)('A' + i);
            //}
        }
    }

    /// <summary>CoreData json-object representing node.</summary>
    [JsonObject]
    public class NavigationNode
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        [JsonProperty(PropertyName = "snapshotId")]
        public string SnapshotId { get; private set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "parentId")]
        public string ParentId { get; private set; }

        [JsonProperty(PropertyName = "vnode_parent_id")]
        private string VirtualParentId { get; set; }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            ParentId = VirtualParentId.IsNullOrEmpty() ? ParentId : VirtualParentId;
        }
    }
}
