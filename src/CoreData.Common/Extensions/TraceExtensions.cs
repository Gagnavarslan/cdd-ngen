using CoreData.Common.HostEnvironment;
using System.Diagnostics;

namespace CoreData.Common.Extensions
{
    public static class TraceExtensions
    {
        private static readonly BooleanSwitch ExtendedSwitch =
            new BooleanSwitch("ExtendedTraceValue", "ITraceView.Value contains extended info");

        /// <summary>Gets json string or implemented trace value, depending on diagnostics switch.</summary>
        public static string GetTraceValue<T>(this T ctx) where T : ITraceView =>
            ExtendedSwitch.Enabled ? ctx.Serialize(true) : ctx.Value;

    }
}
