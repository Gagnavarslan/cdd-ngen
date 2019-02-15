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

    public static class NavConfig
    {
        /// <summary>Common for nav nodes json (de)serialization settings value.</summary>
        public static readonly JsonSerializerSettings SerializerSettings =
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        //public const 
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
