using CoreData.Desktop.Server.Http.Handlers;
using System.Net.Http;

namespace CoreData.Desktop.Server.Http
{
    public static class MessageExtensions
    {
        public static AttachedProperties GetAttachedContext(this HttpRequestMessage request)
        {
            return (AttachedProperties)request.Properties[AttachedProperties.Key];
        }

        public static void SetAttachedContext(this HttpRequestMessage request, AttachedProperties context)
        {
            request.Properties[AttachedProperties.Key] = context;
        }
    }
    //public static class MessageExtensions
    //{
    //    public static HttpRequestProperties GetProperties(this HttpRequestMessage request)
    //    {
    //        return request != null
    //            && request.Properties.TryGetValue(HttpRequestProperties.RequestPropertyName, out var value)
    //            ? (HttpRequestProperties)value : null;
    //    }
    //}
}
