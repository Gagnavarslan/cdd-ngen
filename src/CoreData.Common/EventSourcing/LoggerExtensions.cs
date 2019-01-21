using CoreData.Common.Logging;
using Newtonsoft.Json;
using NLog;

namespace CoreData.Common.EventSourcing
{
    // todo: remove it and implement event sourcing functionality to record user's(offline) changes and propogate them to CoreData once online using aggregated events.
    public static class LoggerExtensions
    {
        private static readonly ILogger Logger = LogManager.GetLogger("EventSourcing");

        public static void Event(string @event) =>
            Logger.Info($"{Spec.Event} {@event}");

        public static void Event(this ILogger logger, object @event) =>
            Logger.Info(() => $"{Spec.Event} {JsonConvert.SerializeObject(@event)}");
    }
}
