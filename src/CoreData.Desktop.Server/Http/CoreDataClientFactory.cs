using CoreData.Common.HostEnvironment;
using CoreData.Desktop.Server.Handlers;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace CoreData.Desktop.Server.Http
{
    // todo: client to use for authenticator
    public interface ICoreDataClientFactory
    {
        /// <summary>Creates http client for general purposes: base for cd API specific; sentry dsn; version|updater.</summary>
        HttpClient CreateHttpClient();

        /// <summary>CD client specific to particular API module|services.</summary>
        HttpClient CreateCoreDataClient(Uri host);
    }

    /// <summary>CoreData HTTP client factory.</summary>
    /// <seealso cref="https://github.com/googleapis/google-api-dotnet-client/blob/master/Src/Support/Google.Apis.Core/Http/ConfigurableMessageHandler.cs"/>
    public class CoreDataClientFactory : ICoreDataClientFactory
    {
        const int MaxClientConnections = 10;

        static CoreDataClientFactory()
        {
            if (Environment.GetCommandLineArgs().Contains("-nossl"))
            {
                ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
            }

            // https://stackoverflow.com/q/16194054
            ServicePointManager.DefaultConnectionLimit = MaxClientConnections;
            //ServicePointManager.Expect100Continue = false;
        }

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly EnvInfo _envInfo;
        private readonly AppInfo _appInfo;
        private readonly string _product;
        private readonly HttpClientHandler _coreDataClientHandler;

        public CoreDataClientFactory(EnvInfo envInfo, AppInfo appInfo)
        {
            _envInfo = envInfo;
            _appInfo = appInfo;
            _product = $"{_appInfo.Now}; {_envInfo.Now}";
            _coreDataClientHandler = new HttpClientHandler { UseCookies = true };
        }

        public HttpClient CreateHttpClient()
        {
            var pipeline = new DelegatingHandler[]
            {
                new SetupMessageHandler(),
                new LogMessageHandler()
            };
            var client = HttpClientFactory.Create(pipeline);

            client.DefaultRequestHeaders.Add(HttpRequestHeader.UserAgent.ToString(), _product); // ProductHeaderValue
            return client;
        }

        public HttpClient CreateCoreDataClient(Uri host)
        {
            var pipeline = new DelegatingHandler[]
            {
                new SetupMessageHandler(),
                new LogMessageHandler()
            };
            _coreDataClientHandler.CookieContainer = new CookieContainer();
            var client = HttpClientFactory.Create(_coreDataClientHandler, pipeline);

            client.BaseAddress = host;
            client.DefaultRequestHeaders.Add(HttpRequestHeader.UserAgent.ToString(), _product); // ProductHeaderValue
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.MaxResponseContentBufferSize = 256_000;
            return client;
        }

        // ???: MS recommends DotNetOpenAuth.OAuth2.Client pkg
        // https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-oauth-20-authorization-server#create-oauth-20-clients

        //private void InitializeWebServerClient()
        //{
        //    var authorizationServerUri = new Uri(Paths.AuthorizationServerBaseAddress);
        //    var authorizationServer = new AuthorizationServerDescription
        //    {
        //        AuthorizationEndpoint = new Uri(authorizationServerUri, Paths.AuthorizePath),
        //        TokenEndpoint = new Uri(authorizationServerUri, Paths.TokenPath)
        //    };
        //    _webServerClient = new WebServerClient(authorizationServer, Clients.Client1.Id, Clients.Client1.Secret);
        //}

        ///// <summary>Creates Basic Auth client handler</summary>
        //public HttpClientHandler CreateBasicHandler(Uri server, string user, string password) =>
        //    new HttpClientHandler // UserNamePasswordClientCredential
        //    {
        //        UseDefaultCredentials = false,
        //        AllowAutoRedirect = false,
        //        //PreAuthenticate = true,
        //        //Credentials = new NetworkCredential(user, password), //todo: will it be recreated for each request?
        //        //CookieContainer = new CookieContainer(), //todo: will it be recreated for each request?
        //        //UseCookies = true,
        //        Credentials = new CredentialCache { { server, "Basic", new NetworkCredential(user, password) } },
        //    };

        ///// <summary>Creates SSO Auth client handler</summary>
        //public HttpClientHandler CreateSsoHandler() =>
        //    new HttpClientHandler // HttpDigestClientCredential WindowsClientCredential
        //    {
        //        UseDefaultCredentials = true,
        //        //AllowAutoRedirect = false,
        //        PreAuthenticate = true, //false,
        //        ClientCertificateOptions = ClientCertificateOption.Automatic,
        //        //Credentials = new NetworkCredential(user, password),
        //        UseCookies = true,
        //        CookieContainer = new CookieContainer()
        //    };

        //public IHttpClient CreateHttpClient(CreateHttpClientArgs args)
        //{
        //    // Create the handler.
        //    var handler = CreateHandler(args);
        //    var configurableHandler = new ConfigurableMessageHandler(handler)
        //    {
        //        ApplicationName = args.ApplicationName
        //    };

        //    // Create the client.
        //    var client = new ConfigurableHttpClient(configurableHandler);
        //    foreach (var initializer in args.Initializers)
        //    {
        //        initializer.Initialize(client);
        //    }

        //    return client;
        //}

        ///// <summary>Creates a HTTP message handler. Override this method to mock a message handler.</summary>
        //protected virtual HttpMessageHandler CreateHandler(CreateHttpClientArgs args)
        //{
        //    // We need to handle three situations in order to intercept uncompressed data where necessary
        //    // while using the built-in decompression where possible.
        //    // - No compression requested
        //    // - Compression requested but not supported by HttpClientHandler (easy; just GzipDeflateHandler on top of an interceptor on top of HttpClientHandler)
        //    // - Compression requested and HttpClientHandler (complex: create two different handlers and decide which to use on a per-request basis)

        //    var clientHandler = CreateSimpleClientHandler();

        //    if (!args.GZipEnabled)
        //    {
        //        // Simple: nothing will be decompressing content, so we can just intercept the original handler.
        //        return new StreamInterceptionHandler(clientHandler);
        //    }
        //    else if (!clientHandler.SupportsAutomaticDecompression)
        //    {
        //        // Simple: we have to create our own decompression handler anyway, so there's still just a single chain.
        //        var interceptionHandler = new StreamInterceptionHandler(clientHandler);
        //        return new GzipDeflateHandler(interceptionHandler);
        //    }
        //    else
        //    {
        //        // Complex: we want to use a simple handler with no interception but with built-in decompression
        //        // for requests that wouldn't perform interception anyway, and a longer chain for interception cases.
        //        clientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

        //        return new TwoWayDelegatingHandler(
        //            // Normal handler (with built-in decompression) when there's no interception.
        //            clientHandler,
        //            // Alternative handler for requests that might be intercepted, and need that interception to happen
        //            // before decompression. We need to delegate to a new client handler that *doesn't* 
        //            new GzipDeflateHandler(new StreamInterceptionHandler(CreateSimpleClientHandler())),
        //            request => StreamInterceptionHandler.GetInterceptorProvider(request) != null);
        //    }
        //}

        ///// <summary>
        ///// Creates a simple client handler with redirection and compression disabled.
        ///// </summary>
        //private HttpClientHandler CreateSimpleClientHandler()
        //{
        //    var handler = new HttpClientHandler();
        //    if (handler.SupportsRedirectConfiguration)
        //    {
        //        handler.AllowAutoRedirect = false;
        //    }
        //    if (handler.SupportsAutomaticDecompression)
        //    {
        //        handler.AutomaticDecompression = DecompressionMethods.None;
        //    }
        //    return handler;
        //}
    }
}
