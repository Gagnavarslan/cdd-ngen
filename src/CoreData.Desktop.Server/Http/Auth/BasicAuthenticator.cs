using CoreData.Common.Extensions;
using CoreData.Desktop.Server.Settings;
using Flurl;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http.Auth
{
    public class BasicAuthenticator : Authenticator
    {
        private readonly BasicConnection _connection; // ClientFormsAuthenticationCredentials
        private readonly Url _loginEndpoint;

        //SecurityCredentialsManager fd;
        //BasicSecurityProfileVersion d;
        //SecurityAlgorithmSuite sd;
        public BasicAuthenticator(HttpClient client, BasicConnection connection) : base(client)
        {
            _connection = connection;
            _loginEndpoint = _connection.Host.AbsoluteUri.AppendPathSegment("api/auth");
        }

        public override string AuthScheme => "Basic";

        public override Task ReassignToken(CancellationToken cancellationToken)
        {
            var credentials = $"{_connection.User}:{_connection.Password}";
            var token = Convert.ToBase64String(Utf8.GetBytes(credentials));
            Token.Set(token, null);
            return Task.CompletedTask;
        }

        protected override async Task<bool> Login(CancellationToken cancellationToken)
        {
            var creds = new[]
            {
                new KeyValuePair<string, string>("username", _connection.User),
                new KeyValuePair<string, string>("password", _connection.Password)
            };
            using (var content = new FormUrlEncodedContent(creds))
            {
                try
                {
                    //var responseTest = await _client.PostAsJsonAsync(LoginEndpoint, model);
                    using (var response = await _client.PostAsync(LoginEndpoint, content).ConfigureAwait(false))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var sessionToken = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            ReassignRemoteUser(response);
                            return !sessionToken.IsNullOrEmpty();
                        }
                    }
                }
                catch (Exception ex) // todo: convert to fallback policy
                {
                    Logger.Error(ex);
                }

                return false;
            }
        }
    }
}
