using CoreData.Common.Extensions;
using NLog;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http.Auth
{
    public interface IAuthenticator
    {
        string AuthScheme { get; }

        AccessToken Token { get; }

        string RemoteUser { get; }

        Task<bool> Authenticate(CancellationToken cancellationToken);

        Task ReassignToken(CancellationToken cancellationToken);

        void ApplyAuthentication(HttpRequestMessage request);
    }

    public abstract class Authenticator : IAuthenticator
    {
        protected const string RemoteUserHeaderName = "X-Remote-User-Name";

        protected static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        protected readonly HttpClient _client;
        //protected readonly Url _server; // auth server = coredata uri
        
        protected Authenticator(HttpClient client)
        {
            _client = client;
            //_server = server ?? throw new ArgumentNullException(nameof(server));
            //if (!string.Equals(Uri.UriSchemeHttps, _authServer.Scheme, StringComparison.OrdinalIgnoreCase))
            //{
            //    throw new ArgumentException(nameof(_authServer.Scheme));
            //}
            Token = AccessToken.Empty();
        }

        public abstract string AuthScheme { get; }

        public AccessToken Token { get; }

        public string RemoteUser { get; private set; }

        public Task<bool> Authenticate(CancellationToken cancellationToken)
        {
            Token.Clear();
            RemoteUser = null;
            return Login(cancellationToken);
        }

        public abstract Task ReassignToken(CancellationToken cancellationToken);

        public virtual void ApplyAuthentication(HttpRequestMessage request) =>
            request.Headers.Authorization = new AuthenticationHeaderValue(AuthScheme, Token.Value);

        protected abstract Task<bool> Login(CancellationToken cancellationToken);

        protected void ReassignRemoteUser(HttpResponseMessage loginResponse)
        {
            Logger.Info($"#User before: {RemoteUser}");
            RemoteUser = loginResponse.Headers.TryGetValues(RemoteUserHeaderName, out var values)
                ? values?.Join(Environment.NewLine)
                : null;
            Logger.Info($"#User after: {RemoteUser}");
        }
    }
}
