using System;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace CoreData.Common.Extensions
{
    public static class PipeExtensions
    { // https://github.com/Rambalac/ACDDokanNet/
        public static Task WaitForConnection(this NamedPipeServerStream server)
        {
            var completition = new TaskCompletionSource<int>();
            server.BeginWaitForConnection(
                ar =>
                {
                    try
                    {
                        var pipeServer = (NamedPipeServerStream)ar.AsyncState;
                        pipeServer.EndWaitForConnection(ar);
                        completition.SetResult(0);
                    }
                    catch (Exception ex)
                    {
                        completition.SetException(ex);
                    }
                },
                server);
            return completition.Task;
        }
    }
}
