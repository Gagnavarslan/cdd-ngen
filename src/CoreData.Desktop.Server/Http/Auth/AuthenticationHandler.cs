using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http.Auth
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly Authenticator _authenticator;

        public AuthenticationHandler(Authenticator authenticator)
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
