using CoreData.Common.HostEnvironment;
using System;

namespace CoreData.Desktop.Server.Http.Handlers
{
    // ???: use local container instead httpRequest.Properties to min network data
    public class AttachedProperties
    {
        internal const string Key = nameof(AttachedProperties);
        //const string Id = "_request.imm.id";
        //const string InitiatedOn = "_request.imm.initiatedOn";
        //const string RetriesDone = "_request.retries";
        //const string RedirectsDone = "_request.redirects";
        //const string Polly = "_request.context";

        public AttachedProperties(long id)
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
