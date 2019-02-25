using CoreData.Desktop.Server.Http;
using NLog;
using System.Net.Http;
using System.Threading;

namespace CoreData.Desktop.Server.Handlers
{
    public class LogMessageHandler : MessageProcessingHandler
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        protected override HttpRequestMessage ProcessRequest(
            HttpRequestMessage request, CancellationToken _)
        {
            Logger.Log(request);
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(
            HttpResponseMessage response, CancellationToken _)
        {
            Logger.Log(response);
            return response;
        }
    }
}

