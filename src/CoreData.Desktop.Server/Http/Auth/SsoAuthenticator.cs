using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http.Auth
{
    public class SsoAuthenticator : Authenticator
    {
        private readonly NetworkCredential _credentials;
        private readonly Uri _tokenUri;

        public SsoAuthenticator(CoreDataConnection connection, NetworkCredential credentials)
            : base(connection, AuthenticationSchemes.IntegratedWindowsAuthentication, "saml2/login/")
        {
            _credentials = credentials;
            _tokenUri = new Uri(connection.Host, "api/v2/token/");
        }

        public override Task<bool> Authenticate(CancellationToken cancellationToken)
        {
            return Task.FromException<bool>(new NotImplementedException());
        }

        public override Task SetToken(CancellationToken cancellationToken)
        {
            return Task.FromException(new NotImplementedException());
        }
    }
}
