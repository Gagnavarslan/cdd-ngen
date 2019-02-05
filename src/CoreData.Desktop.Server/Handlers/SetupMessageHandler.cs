using CoreData.Desktop.Server.Http.Handlers;
using Polly;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Handlers
{
    public class SetupMessageHandler : DelegatingHandler
    {
        /// <summary>Global request id, which might have inner child requests, e.g. on redirects</summary>
        private long _messageId;

        public SetupMessageHandler(HttpMessageHandler inner) : base(inner)
        {

        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var id = Interlocked.Increment(ref _messageId); //.ToString("X8");
            var context = new AttachedProperties(id);
            request.Properties[AttachedProperties.Key] = context;

            var polly = new Context($"({request.Method}) {request.RequestUri}");
            request.SetPolicyExecutionContext(polly);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
