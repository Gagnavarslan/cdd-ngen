using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Common.Services
{
    public interface IBackgroundJob
    {
        /// <summary>Starts the job.</summary>
        Task StartAsync(CancellationToken cancellationToken);

        /// <summary>Stops the job.</summary>
        Task StopAsync(CancellationToken cancellationToken);
    }
}
