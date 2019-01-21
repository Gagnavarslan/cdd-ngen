using System.Net.Http;

namespace CoreData.Common.Extensions
{
    // todo: READ IT and take into account https://stackoverflow.com/a/20787020
    public static class HttpExtensions
    {
        // Enabling Cross-Origin Requests In ASP.NET Core https://www.c-sharpcorner.com/article/enabling-cross-origin-requests-in-asp-net-core/

        #region Is.. based on StatusCodes internals
        #endregion

    //    public static HttpClient With(this HttpClient client, HttpClientHandler clientHandler)
    //    {
    //        client.
    ////services.AddHttpClient("configured-inner-handler")
    ////.ConfigurePrimaryHttpMessageHandler(() =>
    ////{
    ////    return new HttpClientHandler()
    ////    {
    ////        AllowAutoRedirect = false,
    ////        UseDefaultCredentials = true
    ////    };
    ////});
    //        return client;
    //    }

    //services.AddHttpClient("clientwithhandlers")
    //// This handler is on the outside and called first during the 
    //// request, last during the response.
    //.AddHttpMessageHandler<SecureRequestHandler>()
    //// This handler is on the inside, closest to the request being 
    //// sent.
    //.AddHttpMessageHandler<RequestDataHandler>();

        #region Handlers: 1. Predefined lists 
        // Default HttpClientHandlers (loger + error + cache)
        // 
        #endregion
    }
}
