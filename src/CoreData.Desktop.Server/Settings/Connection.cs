using System;
using System.Net;

namespace CoreData.Desktop.Server.Settings
{
    public abstract class CoreDataConnection// : IOptions<CoreData> //: ISettings<Connection>
    {
        //public CoreData() { } // for IOptions only!

        protected CoreDataConnection(Uri host, bool rememberIt =  true)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            RememberIt = rememberIt;
        }

        public Uri Host { get; }

        public bool RememberIt { get; set; }
        // public Connection Value { get; }

        //public CoreDataConnection
        //public string AuthSchema { get; } // AuthenticationSchemes

        //public NetworkCredential Credential { get; set; }
    }

    public class BasicConnection : CoreDataConnection
    {
        public BasicConnection(Uri host, string user, string password) : base(host)
        {
            User = user;
            Password = password;
        }

        public string User { get; }
        public string Password { get; }
    }

    public class SsoConnection : CoreDataConnection
    {
        public SsoConnection(Uri host) : base(host) { }
    }
}
