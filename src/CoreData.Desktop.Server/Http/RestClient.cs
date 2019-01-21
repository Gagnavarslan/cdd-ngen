using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Threading.Tasks;
using CoreData.Desktop.Common.Http;
using CoreData.Desktop.Common.Models;
using CoreData.Desktop.Server.Handlers;
using CoreData.Desktop.Server.Http;
using CoreData.Desktop.Server.Settings;
using Flurl;

namespace CoreData.Desktop.Server
{
    // todo: READ IT and take into account https://stackoverflow.com/a/20787020
    // !!!: HttpClientExtensions - making formatted requests https://docs.microsoft.com/en-us/previous-versions/aspnet/hh944845(v%3dvs.118)
    public class RestClient : IRestClient
    {
        private static readonly TimeSpan AuthTimeout = TimeSpan.FromSeconds(10);

        private readonly DelegatingHandler _authHandler;
        //private readonly string _user;
        //private readonly string _pwd;
        //private readonly FlurlClient _client;
        
        public RestClient(ConnectionInfo coreData) : this(coreData.Host)
        {
            _authHandler = coreData.Credential == null
                ? new ClientRedirectHandler(coreData)
                : (DelegatingHandler)new BasicAuthHandler(coreData);
            // HttpClientFactory.CreatePipeline()
        }
        private RestClient(Uri host)//, string user, string pwd)
        {
            var sp = ServicePointManager.FindServicePoint(host);
            sp.Expect100Continue = false;
            Host = host;
            //_user = user;
            //_pwd = pwd;
            //_client = new FlurlClient(Host).WithBasicAuth(user, pwd);
        }

        public Url Host { get; }

        //public async Task<User> LogInAsync(string username, string password)
        //{ 
        //    if (username == null)
        //        throw new ArgumentNullException(nameof(username));

        //    if (password == null)
        //        throw new ArgumentNullException(nameof(password));

        //    if ((this.User != null) && String.Equals(this.User.Username, username))
        //        return this.User;

        //    await this._agent.AuthenticateAsync(username, password).ConfigureAwait(false);
        //    return await this.GetUserAsync().ConfigureAwait(false);
        //}

        public async Task<bool> Authenticate()
        {
            //var url = Url.Combine(_client.BaseUrl, BasicAuthUrl);

            //var session0 = await url
            //    .WithBasicAuth(_user, _pwd)
            //    .WithTimeout(AuthTimeout)
            //    .GetJsonAsync<string>();

            //var request = _client.Request();
            //request.Url = BasicAuthUrl;

            //var dataString = $"username={Uri.EscapeDataString(_user)}&password={Uri.EscapeDataString(_pwd)}";//{[Authorization, Basic YXV0b2l0OnRlc3QxMjMh]
            //var data = new MemoryStream(Encoding.UTF8.GetBytes(dataString));
            //var session = await request.PostUrlEncodedAsync(data);

            //return session.IsSuccessStatusCode;
            return false;
        }

        public async Task<ListResult<Node>> GetChildrenAsync(string path)
        {
            //string uri = $"api/v2/nav/dir{path}".AppendPathSegment(path, true);
            //var request = _client.Request();
            //request.Url = uri;

            //return await request.GetJsonAsync<ListResult<Node>>();
            // todo: RequestNodes for uri with CancellationToken.None
            return null;
        }
    }
}
