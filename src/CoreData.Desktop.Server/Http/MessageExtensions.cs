using NLog;
using System;
using System.Net.Http;

namespace CoreData.Desktop.Server.Http
{
    public static class MessageExtensions
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        //public static Action<HttpRequestMessage> Default =>
        //    request =>
        //    {
        //        if (!request.Properties.ContainsKey(SetupContext.PropertyKey))
        //        {
        //            var id = Interlocked.Increment(ref MessageId); //.ToString("X8");
        //            var context = new SetupContext(id);
        //            var context = new Context($"({request.Method}) {request.RequestUri}", initData);
        //            request.Properties[SetupContext] = context;
        //        }
        //    };

        public static void With(this HttpRequestMessage request, Action<HttpRequestMessage> setup)
        {
            if (request != null && setup != null)
            {
                setup(request);
            }

            var error = new ArgumentNullException(nameof(MessageExtensions));
            Logger.Error(error);
            throw error;
        }

        public static HttpRequestProperties GetProperties(this HttpRequestMessage request)
        {
            return request != null
                && request.Properties.TryGetValue(HttpRequestProperties.RequestPropertyName, out var value)
                ? (HttpRequestProperties)value : null;
        }
    }
}
