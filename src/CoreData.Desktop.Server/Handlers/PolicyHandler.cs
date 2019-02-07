using NLog;
using Polly;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Handlers
{
    /// <summary>Message handler based on provided policy.
    /// <seealso cref="https://github.com/App-vNext/Polly/blob/5f29a682fd979c92c9f71b09557450ff5f191d61/README.md"/></summary>
    public class PolicyHandler : DelegatingHandler
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly Func<HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> _policySelector;
        // !!!: Polly.Extensions.Http.HttpPolicyExtensions.
        //public PolicyHandler(string name) : this(_ => Polly.Registry.PolicyRegistry.)
        public PolicyHandler(IAsyncPolicy<HttpResponseMessage> policy) : this(_ => policy) { }
        public PolicyHandler(Func<HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> policySelector)
        {
            _policySelector = policySelector ?? throw new ArgumentNullException(nameof(policySelector));
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var policy = _policySelector(request);
            var context = request.GetPolicyExecutionContext();
            return policy.ExecuteAsync((c, ct) => base.SendAsync(request, ct), context, cancellationToken);
        }
        //protected override Task<HttpResponseMessage> SendAsync(
        //    HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    var cleanUpContext = false;
        //    var context = request.GetPolicyContext();
        //    if (context == null)
        //    {
        //        context = new Context();
        //        request.SetPolicyContext(context);
        //        cleanUpContext = true;
        //    }

        //    try
        //    {
        //        var policy = _policy ?? _policySelector(request);
        //        return policy.ExecuteAsync((c, ct) => base.SendAsync(request, ct), context, cancellationToken);
        //    }
        //    finally
        //    {
        //        if (cleanUpContext)
        //        {
        //            request.SetPolicyContext(null);
        //        }
        //    }
        //}
    }
}
