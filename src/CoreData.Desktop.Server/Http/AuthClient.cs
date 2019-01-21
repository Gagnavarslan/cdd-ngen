using CoreData.Common.Extensions;
using CoreData.Desktop.Server.Settings;
using Flurl;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.ClientServices.Providers;

namespace CoreData.Desktop.Server.Http
{
    public class Token
    {
        internal Token() { }

        public bool Active => !Value.IsNullOrEmpty() && (ExpirationUtc == null || ExpirationUtc >= DateTime.UtcNow);

        public string Value { get; private set; }

        public DateTime? ExpirationUtc { get; private set; }

        public void Set(string value, DateTime? expirationUtc)
        {
            Value = value;
            ExpirationUtc = expirationUtc;
        }

        public void Clear() => Set(null, null);
    }

    public abstract class Authenticator
    { // ???: reuse ServiceAuthenticationManager?
        protected static readonly Encoding Utf8 = Encoding.UTF8;

        protected readonly ConnectionInfo _connection;
        
        //protected readonly ConnectionToken _authToken;
        protected readonly Uri _authAddress;

        protected Authenticator(ConnectionInfo connection, AuthenticationSchemes schema, Url authEndpoint)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _connection.Host.Scheme != Uri.UriSchemeHttps

                Schema = schema;
            _authAddress = new Uri(connection.Host, authEndpoint);
            Token = new Token();
        }

        //private bool _authenticated;
        //public bool Authenticated { get; private set; }
        // HttpClientCredentialType

        public AuthenticationSchemes Schema { get; }

        public Token Token { get; }

        public abstract Task<bool> Authenticate(CancellationToken cancellationToken);

        public abstract Task SetToken(CancellationToken cancellationToken);

        public void ApplyToken(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(Schema.ToString(), Token.Value);
            //AuthenticationDescription
            //Authentication
        }

        //protected abstract Uri AuthResource { get; }

        //protected abstract Task<bool> Authenticate(Uri authResource, CancellationToken cancellationToken);
    }

    public class BasicAuthenticator : Authenticator
    {
        private readonly NetworkCredential _credentials; // ClientFormsAuthenticationCredentials
        SecurityCredentialsManager fd;
        BasicSecurityProfileVersion d;
        SecurityAlgorithmSuite sd;
        public BasicAuthenticator(ConnectionInfo connection, NetworkCredential credentials)
            : base(connection, AuthenticationSchemes.Basic, "api/auth/")
        {
            _credentials = credentials;
            //AuthResource = new Uri(connection.Host, "api/auth/");
        }

        //protected override Uri AuthResource { get; }

        public override Task<bool> Authenticate(CancellationToken cancellationToken)
        {
        }

        public override Task SetToken(CancellationToken cancellationToken)
        {
            var credentials = $"{_credentials.UserName}:{_credentials.Password}";
            var token = Convert.ToBase64String(Utf8.GetBytes(credentials));
            Token.Set(token, null);
            return Task.CompletedTask;
        }
    }

    public class SsoAuthenticator : Authenticator
    {
        public SsoAuthenticator(Uri authenticationEndpoint, Uri tokenEndpoint, WindowsClientCredential )
        {
            AuthenticationEndpoint = authenticationEndpoint;
            TokenEndpoint = tokenEndpoint;
        }
    }

    public class AuthenticationHandler : DelegatingHandler
    {
        //private readonly Settings.ConnectionInfo _connection;
        //private AuthenticationHeaderValue _authenticationToken;
        private readonly Authenticator _authenticator;
        //private readonly ConnectionToken _authentication;

        public AuthenticationHandler(Authenticator authenticator)
        {
            _authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
            //_authentication = new ConnectionToken();
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!_authenticator.Token.Active)
            {
                _authenticator.Token.Clear();
                var authenticated = await _authenticator.Authenticate(cancellationToken).ConfigureAwait(false);
                if (authenticated)
                {
                    await _authenticator.SetToken(cancellationToken).ConfigureAwait(false);
                }
            }

            _authenticator.ApplyToken(request);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
