using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using CoreData.Common.Extensions;
using CoreData.Common.HostEnvironment;
using CoreData.Desktop.Server.Http;
using NLog;

namespace CoreData.Desktop.Server.Handlers
{
    public class LogMessageHandler : MessageProcessingHandler
    {
        public static class HttpMessageLog
        {
            public static void LogRequest(HttpRequestMessage request)
            {
                var id = request.GetId();

                var message = new StringBuilder();
                message.AppendLine($"#Initiated {id}: ({request.Method}) {request.RequestUri}");
                AddHeaders(message, request.Headers);
                AddHeaders(message, request.Content?.Headers);
                Log.Info(message.ToString());
            }

            public static void LogResponse(HttpResponseMessage response)
            {
                var id = response.RequestMessage.GetId();
                var spent = AppWatch.Duration(response.RequestMessage.GetActivatedOn());

                var message = new StringBuilder();
                message.AppendLine($"#Received {id} after {spent}");
                AddHeaders(message, response.Headers);
                AddHeaders(message, response.Content?.Headers);
                if (response.IsSuccessStatusCode)
                {
                    Log.Info(message.ToString());
                }
                else
                {
                    Log.Error(message.ToString());
                }
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

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        //public LogMessageHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }

        protected override HttpRequestMessage ProcessRequest(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpMessageLog.LogRequest(request);
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(
            HttpResponseMessage response, CancellationToken cancellationToken)
        {
            HttpMessageLog.LogResponse(response);
            return response;
        }
    }
}

