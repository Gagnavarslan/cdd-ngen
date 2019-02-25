using Newtonsoft.Json;

namespace CoreData.Desktop.Common.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MetaInfo
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
        [JsonProperty("previous")]
        public string Previous { get; set; }
    }
}
