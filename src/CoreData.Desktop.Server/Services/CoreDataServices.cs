using CoreData.Desktop.Server.Http;

namespace CoreData.Desktop.Server.Services
{
    public interface ICoreDataServerServices
    {

    }

    public class CoreDataServerServices
    {
        private readonly ICoreDataClientFactory _clientFactory;

        public CoreDataServerServices(ICoreDataClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public bool Connect()
    }
}
