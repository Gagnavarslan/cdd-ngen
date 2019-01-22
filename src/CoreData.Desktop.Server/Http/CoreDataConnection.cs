using CoreData.Common.HostEnvironment;
using System;
using System.Diagnostics;
using System.Net;

namespace CoreData.Desktop.Server.Http
{
    [DebuggerDisplay("{" + nameof(IDebugInfo.PrintValue) + "}")]
    public sealed partial class CoreDataConnection : IDebugInfo
    {
        public string PrintValue => $"{Host} (Context:{Cookies.Count}|{Headers.Count})";

        public CoreDataConnection(Uri host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Cookies = new CookieContainer();
            Headers = new WebHeaderCollection();
        }

        public Uri Host { get; }

        public CookieContainer Cookies { get; }

        public WebHeaderCollection Headers { get; }
    }

    public sealed partial class CoreDataConnection
    {
        public static readonly CoreDataConnection Default =
            new CoreDataConnection(new Uri("https://test01-dev.coredata.is"));
    }
}
