using Newtonsoft.Json;

namespace CoreData.Common.Extensions
{
    public static class SerializationExtensions
    {
        public static string Serialize(this object obj, bool pretty = false) =>
            JsonConvert.SerializeObject(obj, pretty ? Formatting.Indented : Formatting.None);

        public static T Deserialize<T>(this string json) =>
            JsonConvert.DeserializeObject<T>(json);
    }
}
