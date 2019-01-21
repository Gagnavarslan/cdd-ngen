using System.Net;
using System.Net.Http;

namespace CoreData.Desktop.Common.Http
{
    public static class HttpExtenstions
    {
        /// <summary>Returns <c>true</c> if the response contains one of the redirect status codes.</summary>
        public static bool IsRedirectStatusCode(this HttpResponseMessage message)
        {
            switch (message.StatusCode)
            {
                case HttpStatusCode.Moved:
                case HttpStatusCode.Redirect:
                case HttpStatusCode.RedirectMethod:
                case HttpStatusCode.TemporaryRedirect:
                    return true;
                default:
                    return false;
            }
        }
    }
}
