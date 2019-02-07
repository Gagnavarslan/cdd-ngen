using CoreData.Desktop.Server.Settings;
using Flurl;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http.Auth
{
    /// <summary>
    /// <seealso cref="https://stackoverflow.com/a/2851021"/>Using UI Web View delegating.</summary>
    public class SsoAuthenticator : Authenticator
    {
        //private readonly NetworkCredential _credentials;
        private SsoConnection _connection;
        private readonly Url _loginEndpoint;
        private readonly Url _tokenEndpoint;

        public SsoAuthenticator(HttpClient client, SsoConnection connection) : base(client)
        {
            _connection = connection;
            _loginEndpoint = _connection.Server.AbsoluteUri.AppendPathSegment("saml2/login/");
            _tokenEndpoint = _connection.Server.AbsoluteUri.AppendPathSegment("api/v2/token/");
        }

        public override string AuthScheme => "token";

        protected override Task<bool> Login(CancellationToken cancellationToken)
        {
            return Task.FromException<bool>(new NotImplementedException());
        }

        public override void ApplyAuthentication(HttpRequestMessage request)
        {
            base.ApplyAuthentication(request);

            // todo: implement
        }

        public override Task ReassignToken(CancellationToken cancellationToken)
        {
            return Task.FromException(new NotImplementedException());
        }
    }
}
