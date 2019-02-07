using CoreData.Desktop.Server.Http.Auth;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticator _authenticator;

        public AuthenticationHandler(IAuthenticator authenticator)
        {
            _authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!_authenticator.Token.Valid)
            {
                var authenticated = await _authenticator.Authenticate(cancellationToken).ConfigureAwait(false);
                if (authenticated)
                {
                    await _authenticator.ReassignToken(cancellationToken).ConfigureAwait(false);
                }
            }

            _authenticator.ApplyAuthentication(request);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
