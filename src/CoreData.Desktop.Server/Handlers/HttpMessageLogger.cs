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
        public static void Log(this ILogger logger, HttpRequestMessage request)
        {
            var context = request.GetAttachedContext();
            var message = new StringBuilder()
                .AppendLine($"#Sending {context.Id}: ({request.Method}) {request.RequestUri}")
                .AddHeaders(request.Headers)
                .AddHeaders(request.Content?.Headers);

            logger.Info(message.ToString());
        }

        public static void Log(this ILogger logger, HttpResponseMessage response)
        {
            var level = response.IsSuccessStatusCode ? LogLevel.Info : LogLevel.Warn;
            var context = response.RequestMessage.GetAttachedContext();
            var message = new StringBuilder()
                .AppendLine($"#Received {context.Id} after {AppWatch.Duration(context.Initiated)}")
                .AddHeaders(response.Headers)
                .AddHeaders(response.Content?.Headers);

            logger.Log(level, message.ToString());
        }

        static StringBuilder AddHeaders(this StringBuilder builder, HttpHeaders headers)
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
