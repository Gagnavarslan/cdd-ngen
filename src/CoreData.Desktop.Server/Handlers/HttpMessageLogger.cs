using CoreData.Common.Extensions;
using CoreData.Common.HostEnvironment;
using NLog;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CoreData.Desktop.Server.Http
{
    public static class HttpMessageLogger
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static HttpRequestMessage Log(this HttpRequestMessage request)
        {
            var context = request.GetAttachedContext();
            var message = new StringBuilder();
            message.AppendLine($"#Initiated {context.Id}: ({request.Method}) {request.RequestUri}");
            AddHeaders(message, request.Headers);
            AddHeaders(message, request.Content?.Headers);
            Logger.Info(message.ToString());

            return request;
        }

        public static HttpResponseMessage Log(this HttpResponseMessage response)
        {
            var context = response.RequestMessage.GetAttachedContext();
            var message = new StringBuilder();
            message.AppendLine($"#Received {context.Id} after {AppWatch.Duration(context.Initiated)}");
            AddHeaders(message, response.Headers);
            AddHeaders(message, response.Content?.Headers);
            if (response.IsSuccessStatusCode)
            {
                Logger.Info(message.ToString());
            }
            else
            {
                Logger.Error(message.ToString());
            }

            return response;
        }

        static StringBuilder AddHeaders(StringBuilder builder, HttpHeaders headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    builder.AppendLine(header.Key).Append(" : ").Append(header.Value.Join(", "));
                }
            }
            return builder;
        }
    }
}
