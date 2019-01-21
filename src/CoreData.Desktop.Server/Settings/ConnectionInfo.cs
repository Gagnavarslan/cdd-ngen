using System;

namespace CoreData.Desktop.Server.Settings
{
    public class ConnectionInfo  //: ISettings<Connection>
    {
        public static readonly ConnectionInfo Test =
            new ConnectionInfo(new Uri("https://test02-dev.coredata.is"));//, new CoreDataConnection)
        //{
        //    Credential = new NetworkCredential("autoit", "test123!")
        //};

        public ConnectionInfo(Uri host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
        }

        public Uri Host { get; }

        //public CoreDataConnection
        //public string AuthSchema { get; } // AuthenticationSchemes

        //public NetworkCredential Credential { get; set; }
    }
}
