using Flurl;
using System;
using System.Net;
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

        public SsoAuthenticator(ICoreDataClientFactory clientFactory, Url server)
            : base(clientFactory, server)
        {
            _tokenEndpoint = server.AppendPathSegment("api/v2/token/");
        }

        public override string SchemeType => "token";

        public override Url AuthEndpoint => _server.AppendPathSegment("saml2/login/");
        
        public override Task<bool> Authenticate(NetworkCredential credentials, CancellationToken cancellationToken)
        {
            return Task.FromException<bool>(new NotImplementedException());
        }

        public override Task RefreshToken(CancellationToken cancellationToken)
        {
            return Task.FromException(new NotImplementedException());
        }
    }
}
