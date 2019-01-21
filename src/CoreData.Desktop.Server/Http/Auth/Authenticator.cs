using CoreData.Desktop.Server.Settings;
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

    // ???: reuse ServiceAuthenticationManager?
    public abstract class Authenticator : IAuthenticator
    {
        protected static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        protected static readonly Encoding Utf8 = Encoding.UTF8;

        protected readonly CoreDataConnection _connection;
        //protected readonly ConnectionToken _authToken;
        protected readonly Uri _authUri;
        
        protected Authenticator(CoreDataConnection connection, AuthenticationSchemes schema, Url authEndpoint)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (string.Equals(Uri.UriSchemeHttps, _connection.Host.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(nameof(_connection.Host.Scheme));
            }
            _authUri = new Uri(connection.Host, authEndpoint);
            Schema = schema;
            Access = AccessToken.Empty();
        }

        //private bool _authenticated;
        //public bool Authenticated { get; private set; }
        // HttpClientCredentialType

        public AuthenticationSchemes Schema { get; }

        public AccessToken Access { get; }

        public abstract Task<bool> Authenticate(CancellationToken cancellationToken);

        public abstract Task SetToken(CancellationToken cancellationToken);

        public void ApplyAccessToken(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(Schema.ToString(), Access.Value);
        }
    }
}
