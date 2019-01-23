using System;

namespace CoreData.Desktop.Server.Settings
{
    public abstract class AuthConnection// : IOptions<CoreData> //: ISettings<Connection>
    {
        protected AuthConnection(Uri host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            //RememberIt = true;
        }

        public Uri Host { get; }

        public bool RememberIt { get; set; }
        
        public TimeSpan AuthTimeout { get; set; }
    }

    public class BasicConnection : AuthConnection
    {
        public BasicConnection(Uri host, string user, string password) : base(host)
        {
            User = user;
            Password = password;
        }

        public string User { get; }
        public string Password { get; }
    }

    public class SsoConnection : AuthConnection
    {
        public SsoConnection(Uri host) : base(host) { }
    }
}
