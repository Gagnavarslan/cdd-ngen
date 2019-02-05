using System.Net.Http;

namespace CoreData.Desktop.Server.Http
{
    public class CoreDataClient : HttpClient
    {
        public ActivationMessageHandler MessageHandler { get; }

        public CoreDataClient(ActivationMessageHandler handler, bool disposeHandler)
            : base(handler, disposeHandler)
        {
            MessageHandler = handler;
        }
    }
}
