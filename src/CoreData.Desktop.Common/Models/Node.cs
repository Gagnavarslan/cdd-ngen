using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CoreData.Desktop.Common.Models
{
    public class Node
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id")]
        public string Id { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "snapshot_id")]
        public string SnapshotId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "parent_id")]
        public string ParentId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "vnode_parent_id")]
        private string VnodeParentId { get; set; }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            ParentId = !string.IsNullOrEmpty(VnodeParentId) ? VnodeParentId : ParentId;
        }
    }
}
