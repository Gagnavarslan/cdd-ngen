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
        private readonly NetworkCredential _credentials; // ClientFormsAuthenticationCredentials
        //SecurityCredentialsManager fd;
        //BasicSecurityProfileVersion d;
        //SecurityAlgorithmSuite sd;
        public BasicAuthenticator(CoreDataConnection connection, NetworkCredential credentials)
            : base(connection, AuthenticationSchemes.Basic, "api/auth/")
        {
            _credentials = credentials;
        }

        public override Task<bool> Authenticate(CancellationToken cancellationToken)
        {
            var creds = new[]
            {
                new KeyValuePair<string, string>("username", _credentials.UserName),
                new KeyValuePair<string, string>("password", _credentials.Password)
            };
            var content = new FormUrlEncodedContent(creds);
            return Task.FromResult<bool>(true);
        }

        public override Task SetToken(CancellationToken cancellationToken)
        {
            var credentials = $"{_credentials.UserName}:{_credentials.Password}";
            var token = Convert.ToBase64String(Utf8.GetBytes(credentials));
            Access.Set(token, null);
            return Task.CompletedTask;
        }
    }
}
