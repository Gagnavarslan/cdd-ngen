using System;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Services
{
    // !!!: Built-in middleware https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.1#built-in-middleware
    public interface IHttpService
    {
        HttpClient Client { get; }
    }
    public static class HttpExtensions
    {
        public static Task<string> GetAsync(this IHttpService service, string url)
        {
            Contract.Requires<ArgumentNullException>(service != null);
            return service.Client.GetStringAsync(url);
        }
    }

    public class MetadataService : IMetadataService
    {
        public MetadataService()
        {

        }
    }

    public interface IMetadataService
    {
    }
}
