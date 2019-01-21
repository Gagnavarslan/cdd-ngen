﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http.Auth
{
    public class AuthenticationHandler : DelegatingHandler
    {
        //private readonly Settings.ConnectionInfo _connection;
        //private AuthenticationHeaderValue _authenticationToken;
        private readonly Authenticator _authenticator;
        //private readonly ConnectionToken _authentication;

        public AuthenticationHandler(Authenticator authenticator)
        {
            _authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!_authenticator.Access.Valid)
            {
                _authenticator.Access.Clear();
                var authenticated = await _authenticator.Authenticate(cancellationToken).ConfigureAwait(false);
                if (authenticated)
                {
                    await _authenticator.SetToken(cancellationToken).ConfigureAwait(false);
                }
            }

            _authenticator.ApplyAccessToken(request);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}