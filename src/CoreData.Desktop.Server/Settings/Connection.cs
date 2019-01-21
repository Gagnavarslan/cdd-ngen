using Microsoft.Extensions.Options;
using System;

namespace CoreData.Desktop.Server.Settings
{
    public class Connection : IOptions<Connection> //: ISettings<Connection>
    {
        public Connection() { } // for IOptions only!

        public Connection(Uri host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
        }

        public Uri Host { get; }

        public Connection Value { get; }

        //public CoreDataConnection
        //public string AuthSchema { get; } // AuthenticationSchemes

        //public NetworkCredential Credential { get; set; }
    }
}
