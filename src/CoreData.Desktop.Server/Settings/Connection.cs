using System;
using System.Text;

namespace CoreData.Desktop.Server.Settings
{
    // public class 
    public abstract class Connection //: IOptions<Connection> //: ISettings<Connection>
    {
        internal static readonly Encoding Encoding = Encoding.UTF8;

        protected Connection(Uri server)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
            AuthTimeout = TimeSpan.FromSeconds(10);
            RememberIt = true;
        }

        public Uri Server { get; }

        public bool RememberIt { get; set; }
        
        public TimeSpan AuthTimeout { get; set; }
    }

    public class BasicConnection : Connection
    {
        public BasicConnection(Uri server, string user, string password) : base(server)
        {
            User = user;
            Password = password;
        }

        public string User { get; }
        public string Password { get; }
    }

    public class SsoConnection : Connection
    {
        public SsoConnection(Uri server) : base(server) { }
    }
}
