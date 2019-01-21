using Microsoft.AspNet.SignalR.Client;

namespace CoreData.Common.Messaging.Local
{
    public interface ILocalConnection
    {
        /// <summary>Communication channel for local env cdd instances</summary>
        Connection Connection { get; }
        //SubscriptionType
    }

    public class LocalConnection : ILocalConnection
    {
        public LocalConnection(Connection connection) // http://localhost:8118
        {
        }

        public Connection Connection { get; }
    }
}
