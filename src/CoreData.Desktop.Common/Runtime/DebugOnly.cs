using System.Diagnostics;
using static System.Diagnostics.PresentationTraceSources;

namespace CoreData.Desktop.Common.Runtime
{
    /// <summary>DEBUG var compiler conditional methods helper.
    /// <para>Public|direct members invocation supposed to be DEBUG conditional only!!!</para></summary>
    [DebuggerNonUserCode]
    public static class DebugOnlySession
    {
        [Conditional("DEBUG")]
        public static void Attach()
        {
            AddBindingIssueListeners();
        }

        private static void AddBindingIssueListeners()
        {
            Refresh();
            // DataBindingSource.Listeners.Add(new NLogTraceListener().);
            DataBindingSource.Listeners.Add(new ConsoleTraceListener());
            DataBindingSource.Switch.Level = SourceLevels.Warning | SourceLevels.Error;
        }
    }
}
