using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace CoreData.Desktop.Server.Http
{
    public class PolicyHandler : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> _policySelector;

        public PolicyHandler(IAsyncPolicy<HttpResponseMessage> policy) : this(_ => policy) { }

        public PolicyHandler(Func<HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> policySelector)
        {
            _policySelector = policySelector ?? throw new ArgumentNullException(nameof(policySelector));
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var policy = _policySelector(request);
            var context = request.GetPolicyContext();
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
