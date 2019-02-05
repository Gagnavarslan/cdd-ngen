using CoreData.Desktop.Server.Http;

namespace CoreData.Desktop.Server.Services
{
    public interface IServerConnectionServices
    {

    }

    public class ServerConnectionServices : IServerConnectionServices
    {
        private readonly ICoreDataClientFactory _clientFactory;

        public ServerConnectionServices(ICoreDataClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        //public bool Connect();
    }
}
