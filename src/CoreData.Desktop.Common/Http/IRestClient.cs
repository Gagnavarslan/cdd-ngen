using System.Threading.Tasks;
using CoreData.Desktop.Common.Models;
using Flurl;

namespace CoreData.Desktop.Common.Http
{
    /// <summary>CD http messages client</summary>
    // ???: mb move to FileSystem prj ???
    public interface IRestClient
    {
        /// <summary>CD base address</summary>
        Url Host { get; }

        Task<bool> Authenticate(); //Uri host, string user, string pwd

        Task<ListResult<NavigationNode>> GetChildrenAsync(string path);
    }
}
