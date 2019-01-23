using Flurl;
using NLog;
using System;
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

        protected readonly HttpClient _client;
        protected readonly Url _server; // auth server = coredata uri
        
        protected Authenticator(HttpClient client, Url server)
        {
            _client = client;
            _server = server ?? throw new ArgumentNullException(nameof(server));
            //if (!string.Equals(Uri.UriSchemeHttps, _authServer.Scheme, StringComparison.OrdinalIgnoreCase))
            //{
            //    throw new ArgumentException(nameof(_authServer.Scheme));
            //}
            Token = AccessToken.Empty();
        }

        public abstract string AuthScheme { get; }

        public AccessToken Token { get; }

        public string CoreDataUserName { get; protected set; }

        public Task<bool> Authenticate(CancellationToken cancellationToken)
        {
            Token.Clear();
            CoreDataUserName = null;
            return Login(cancellationToken);
        }

        public abstract Task ReassignToken(CancellationToken cancellationToken);

        public virtual void ApplyAuthentication(HttpRequestMessage request) =>
            request.Headers.Authorization = new AuthenticationHeaderValue(AuthScheme, Token.Value);

        protected abstract Task<bool> Login(CancellationToken cancellationToken);
    }
}
