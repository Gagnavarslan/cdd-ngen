using CoreData.Common.Extensions;
using Flurl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http.Auth
{
    public class BasicAuthenticator : Authenticator
    {
        private readonly NetworkCredential _credentials; // ClientFormsAuthenticationCredentials
        //SecurityCredentialsManager fd;
        //BasicSecurityProfileVersion d;
        //SecurityAlgorithmSuite sd;
        public BasicAuthenticator(HttpClient client, Url server, NetworkCredential credentials)
            : base(client, server)
        {
            _credentials = credentials;
        }

        public override string AuthScheme => "Basic";

        public override Task ReassignToken(CancellationToken cancellationToken)
        {
            var credentials = $"{_credentials.UserName}:{_credentials.Password}";
            var token = Convert.ToBase64String(Utf8.GetBytes(credentials));
            Token.Set(token, null);
            return Task.CompletedTask;
        }

        private Url LoginEndpoint => _server.AppendPathSegment("api/auth");

        protected override async Task<bool> Login(CancellationToken cancellationToken)
        {
            var creds = new[]
            {
                new KeyValuePair<string, string>("username", _credentials.UserName),
                new KeyValuePair<string, string>("password", _credentials.Password)
            };
            var content = new FormUrlEncodedContent(creds);

            try
            {
                using (var response = await _client.PostAsync(LoginEndpoint, content).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var sessionToken = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        response.Headers.TryGetValues("X-Remote-User-Name", out var values);
                        CoreDataUserName = values?.SingleOrDefault();

                        return !sessionToken.IsNullOrEmpty();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return false;
        }
    }
}
