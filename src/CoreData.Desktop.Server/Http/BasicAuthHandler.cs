using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http
{
    public class BasicAuthHandler : DelegatingHandler
    {
        public const string BasicAuthUrl = "api/auth/";

        private readonly Settings.ConnectionInfo _connection;

        public BasicAuthHandler(Settings.ConnectionInfo connection)
        {
            _connection = connection;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var credentials = $"{_connection.Credential.UserName}:{_connection.Credential.Password}";
            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            request.Headers.Authorization =
                new AuthenticationHeaderValue(AuthenticationSchemes.Basic.ToString(), authHeader);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
