﻿using CoreData.Common.HostEnvironment;
using NLog;
using Polly;
using System;
using System.Net.Http;

namespace CoreData.Desktop.Server.Http
{
    public class RequestProperties
    {
        internal const string RequestPropertyName = "_request.context";
        //const string Id = "_request.imm.id";
        //const string InitiatedOn = "_request.imm.initiatedOn";
        //const string RetriesDone = "_request.retries";
        //const string RedirectsDone = "_request.redirects";
        //const string Polly = "_request.context";

        public RequestProperties(long id)
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

        public Context Polly { get; internal set; }
    }

    public static class MessageSetup
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        //public static Action<HttpRequestMessage> Default =>
        //    request =>
        //    {
        //        if (!request.Properties.ContainsKey(SetupContext.PropertyKey))
        //        {
        //            var id = Interlocked.Increment(ref MessageId); //.ToString("X8");
        //            var context = new SetupContext(id);
        //            var context = new Context($"({request.Method}) {request.RequestUri}", initData);
        //            request.Properties[SetupContext] = context;
        //        }
        //    };

        public static void With(this HttpRequestMessage request, Action<HttpRequestMessage> setup)
        {
            if (request != null && setup != null)
            {
                setup(request);
            }

            var error = new ArgumentNullException(nameof(MessageSetup));
            Logger.Error(error);
            throw error;
        }

        public static RequestProperties GetContext(this HttpRequestMessage request)
        {
            return request != null
                && request.Properties.TryGetValue(RequestProperties.RequestPropertyName, out var value)
                ? value : null;
        }
    }
}
