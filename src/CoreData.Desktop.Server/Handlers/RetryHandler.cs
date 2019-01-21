using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Handlers
{
    public class RetryHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    // base.SendAsync calls the inner handler
                    var response = await base.SendAsync(request, cancellationToken);

                    if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        // 503 Service Unavailable - wait a bit and try again later
                        await Task.Delay(5000, cancellationToken);
                        continue;
                    }

                    if (response.StatusCode == (HttpStatusCode)429)
                    {
                        // 429 Too many requests
                        // Wait a bit and try again later
                        await Task.Delay(1000, cancellationToken);
                        continue;
                    }

                    // Not something we can retry, return the response as is
                    return response;
                }
                catch (Exception ex) when (IsNetworkError(ex))
                {
                    // Network error
                    // Wait a bit and try again later
                    await Task.Delay(2000, cancellationToken);
                    continue;
                }
            }
        }

        private static bool IsNetworkError(Exception ex)
        {
            // Check if it's a network error
            if (ex is SocketException)
                return true;
            if (ex.InnerException != null)
                return IsNetworkError(ex.InnerException);
            return false;
        }
    }
}
