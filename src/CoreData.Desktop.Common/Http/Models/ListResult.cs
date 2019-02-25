using Newtonsoft.Json;

namespace CoreData.Desktop.Common.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ListResult<T>
    {
        [JsonProperty("meta")]
        public MetaInfo Info { get; set; }

        [JsonProperty("objects")]
        public T[] Objects { get; set; }

        public int TotalCount => Info.TotalCount;
    }
}
