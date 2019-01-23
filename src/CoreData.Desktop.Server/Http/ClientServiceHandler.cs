using NLog;
using Polly;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http
{
    public class ClientServiceHandler : HttpClientHandler
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly Action<HttpRequestMessage> _customMessageSetup;

        /// <summary>Global request id, which might have inner child requests, e.g. on redirects</summary>
        internal long MessageId;

        public ClientServiceHandler(Action<HttpRequestMessage> customMessageSetup)
        {
            _customMessageSetup = customMessageSetup;
            MessageId = 0;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.With(DefaultSetup);
            request.With(_customMessageSetup);

            return base.SendAsync(request, cancellationToken);
        }

        private void DefaultSetup(HttpRequestMessage request)
        {
            var id = Interlocked.Increment(ref MessageId); //.ToString("X8");
            var context = new HttpRequestProperties(id);
            request.Properties[HttpRequestProperties.RequestPropertyName] = context;

            var polly = new Context($"({request.Method}) {request.RequestUri}");
            request.SetPolicyExecutionContext(polly);
        }
    }
}
