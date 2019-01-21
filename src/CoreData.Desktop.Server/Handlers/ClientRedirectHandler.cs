using CoreData.Desktop.Common.Http;
using CoreData.Desktop.Server.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Handlers
{
    public class ClientRedirectHandler : DelegatingHandler
    {
        private readonly CoreDataConnection _coreData;

        public ClientRedirectHandler(CoreDataConnection coreData) : base()
            //: base(new HttpClientHandler() { AllowAutoRedirect = false })
        {
            _coreData = coreData;
            Policies = RedirectPolicies.Default();
        }
        
        public RedirectPolicies Policies { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            var uri = response.Headers.Location;
            if (!response.IsRedirectStatusCode() || uri == null)
            {
                return response;
            }

            //var request = response.RequestMessage;
            request.RequestUri = new Uri(request.RequestUri, uri);
            // Status code for a resource that has moved to a new URI and should be retrieved using GET.
            if (response.StatusCode == HttpStatusCode.SeeOther)
            {
                request.Method = HttpMethod.Get;
            }
            // Clear Authorization and If-* headers.
            request.Headers.Remove("Authorization");
            request.Headers.IfMatch.Clear();
            request.Headers.IfNoneMatch.Clear();
            request.Headers.IfModifiedSince = null;
            request.Headers.IfUnmodifiedSince = null;
            request.Headers.Remove("If-Range");

            return await base.SendAsync(request, cancellationToken);
            //if (response.StatusCode == HttpStatusCode.RedirectKeepVerb
            //    || response.StatusCode == HttpStatusCode.MovedPermanently)
            //{
            //    var location = response.Headers.Location;
            //    if(location == null)
            //    {
            //        return response;
            //    }

            //    if (Policies.EnforceSameHost
            //        && !UriComponents.Host.Equals(request.RequestUri, location))
            //    {
            //        return response;
            //    }

            //    request.RequestUri = location;
            //    return await base.SendAsync(request, cancellationToken);

            //}
            //return response;
        }
    }

    public class RedirectPolicies
    {
        private const int DefaultMaxRedirects = 5; // https://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html

        public static RedirectPolicies Default() =>
            new RedirectPolicies { EnforceSameHost = true, MaxRedirects = DefaultMaxRedirects };

        public bool EnforceSameHost { get; set; }
        public int MaxRedirects { get; set; }
    }
}
