using CoreData.Common.Extensions;
using CoreData.Common.HostEnvironment;
using CoreData.Desktop.Server.Http;
using NLog;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace CoreData.Desktop.Server.Handlers
{
    public class LogMessageHandler : MessageProcessingHandler
    {
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

