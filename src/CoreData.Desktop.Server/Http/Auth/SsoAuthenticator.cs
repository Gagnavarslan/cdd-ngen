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
        private readonly Url _tokenEndpoint;

        public SsoAuthenticator(HttpClient client, Url server)
            : base(client, server)
        {
            _tokenEndpoint = server.AppendPathSegment("api/v2/token/");
        }

        public override string AuthScheme => "token";

        public override Url LoginEndpoint => _server.AppendPathSegment("saml2/login/");

        protected override Task<bool> Login(NetworkCredential credentials, CancellationToken cancellationToken)
        {
            return Task.FromException<bool>(new NotImplementedException());
        }

        public override Task ReassignToken(CancellationToken cancellationToken)
        {
            return Task.FromException(new NotImplementedException());
        }
    }
}
