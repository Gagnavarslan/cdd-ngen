using Flurl;
using NLog;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http.Auth
{
    public interface IAuthenticator
    {

    }

    public abstract class Authenticator : IAuthenticator
    {
        protected static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        protected static readonly Encoding Utf8 = Encoding.UTF8;

        protected readonly ICoreDataClientFactory _clientFactory;
        protected readonly Url _server; // auth server = coredata uri
        
        protected Authenticator(ICoreDataClientFactory clientFactory, Url server)
        {
            _clientFactory = clientFactory;
            _server = server ?? throw new ArgumentNullException(nameof(server));
            //if (!string.Equals(Uri.UriSchemeHttps, _authServer.Scheme, StringComparison.OrdinalIgnoreCase))
            //{
            //    throw new ArgumentException(nameof(_authServer.Scheme));
            //}
            Token = AccessToken.Empty();
        }

        public abstract string SchemeType { get; }

        public abstract Url AuthEndpoint { get; }

        public AccessToken Token { get; }

        public abstract Task<bool> Authenticate(NetworkCredential credentials, CancellationToken cancellationToken);

        public abstract Task RefreshToken(CancellationToken cancellationToken);

        public void ApplyAccess(HttpRequestMessage request) =>
            request.Headers.Authorization = new AuthenticationHeaderValue(SchemeType, Token.Value);
    }
}
