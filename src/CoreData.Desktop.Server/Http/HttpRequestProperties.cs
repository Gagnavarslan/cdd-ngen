using CoreData.Common.HostEnvironment;
using System;

namespace CoreData.Desktop.Server.Http
{
    public class HttpRequestProperties
    {
        internal const string RequestPropertyName = "_request.context";
        //const string Id = "_request.imm.id";
        //const string InitiatedOn = "_request.imm.initiatedOn";
        //const string RetriesDone = "_request.retries";
        //const string RedirectsDone = "_request.redirects";
        //const string Polly = "_request.context";

        public HttpRequestProperties(long id)
        {
            Id = id;
            Initiated = AppWatch.Elapsed;
            RetriesDone = 0;
            RedirectsDone = 0;
        }

        public long Id { get; }

        public TimeSpan Initiated { get; }

        public int RetriesDone { get; internal set; }

        public int RedirectsDone { get; internal set; }
    }
}
