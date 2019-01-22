using Flurl;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http.Auth
{
    public class BasicAuthenticator : Authenticator
    {
        //private readonly NetworkCredential _credentials; // ClientFormsAuthenticationCredentials
        //SecurityCredentialsManager fd;
        //BasicSecurityProfileVersion d;
        //SecurityAlgorithmSuite sd;
        public BasicAuthenticator(ICoreDataClientFactory clientFactory, Url server)
            : base(clientFactory, server) { }

        public override string SchemeType => "basic";

        public override Url AuthEndpoint => _server.AppendPathSegment("api/auth");

        public override Task<bool> Authenticate(NetworkCredential credentials, CancellationToken cancellationToken)
        {
            var creds = new[]
            {
                new KeyValuePair<string, string>("username", credentials.UserName),
                new KeyValuePair<string, string>("password", credentials.Password)
            };
            var content = new FormUrlEncodedContent(creds);


            return Task.FromResult<bool>(true);
        }

        public override Task RefreshToken(CancellationToken cancellationToken)
        {
            var credentials = $"{_credentials.UserName}:{_credentials.Password}";
            var token = Convert.ToBase64String(Utf8.GetBytes(credentials));
            Token.Set(token, null);
            return Task.CompletedTask;
        }
    }
}
