using CoreData.Desktop.Server.Http;
using CoreData.Desktop.Server.Http.Auth;
using CoreData.Desktop.Server.Settings;
using DryIoc;
using NLog;
using System;
using System.Net.Http;

namespace CoreData.Desktop.Server.Services
{
    public interface IRestServiceFactory
    {
        IAuthenticator CreateAuthenticator<TAuthConnection>(TAuthConnection connection)
            where TAuthConnection: Connection;
    }

    public class RestServiceFactory
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly ICoreDataClientFactory _clientFactory;
        private readonly HttpClient _sharedClient;
        private HttpClient _coreDataClient;

        public RestServiceFactory(ICoreDataClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _sharedClient = _clientFactory.CreateHttpClient();
        }

        public IAuthenticator CreateAuthenticator<TAuthConnection>(TAuthConnection connection)
            where TAuthConnection : Connection
        {
            Throw.ThrowIfNull(connection);
            if (connection is BasicConnection basicConnection)
            {
                return new BasicAuthenticator(_sharedClient, basicConnection);
            }
            else if (connection is SsoConnection ssoConnection)
            {
                return new SsoAuthenticator(_sharedClient, ssoConnection);
            }
            else throw new NotSupportedException();
        }
    }
}
