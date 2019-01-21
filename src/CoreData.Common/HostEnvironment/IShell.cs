using System.Threading;

namespace CoreData.Common.HostEnvironment
{
    /// <summary>Command builder and runner - cmd, PS, browser, etc.</summary>
    public interface IShell<T>
    {
        ShellCommand<T> Build(string command);

        bool Run(ShellCommand<T> command, int timeout = Timeout.Infinite);
    }
}
