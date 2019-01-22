using CoreData.Desktop.Server.Http;
using System.Net.Http;
using System.Threading;

namespace CoreData.Desktop.Server.Handlers
{
    public class LogMessageHandler : MessageProcessingHandler
    {
        protected override HttpRequestMessage ProcessRequest(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpMessageLogger.Log(request);
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(
            HttpResponseMessage response, CancellationToken cancellationToken)
        {
            HttpMessageLogger.Log(response);
            return response;
        }
    }
}

