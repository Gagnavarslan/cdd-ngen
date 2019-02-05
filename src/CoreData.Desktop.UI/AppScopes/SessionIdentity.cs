using CoreData.Common.HostEnvironment;
using System.Diagnostics;

namespace CoreData.Desktop.UI.AppScope
{
    //todo: remove ParticipantIdentity
    /// <summary>Participant registration info</summary>
    [DebuggerDisplay("{" + nameof(IDebugView.Now) + "}")]
    public partial class SessionIdentity : IDebugView
    {
        public string Now => Id;

        public static readonly SessionIdentity Current =
            new SessionIdentity(System.Diagnostics.Process.GetCurrentProcess());

        public SessionIdentity(Process sessionProcess)
        {
            Name = sessionProcess.ProcessName;

            User = sessionProcess.StartInfo.EnvironmentVariables["USERNAME"];
            Process = sessionProcess.Id;
            Session = sessionProcess.SessionId;

            Id = $"{User}@{Process}@{Session}";
        }

        public string Name { get; }
        public string User { get; }
        public int Process { get; }
        public int Session { get; }
        public string Id { get; }
    }
}
