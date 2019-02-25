using CoreData.Common.HostEnvironment;
using System;
using System.Diagnostics;

namespace CoreData.Common.Messaging.Peer
{
    /// <summary>Participant registration info</summary>
    [DebuggerDisplay("{" + nameof(ITraceView.Value) + "}")]
    [Obsolete("SessionIdentity should be used instead")]
    public struct ParticipantIdentity : ITraceView
    {
        public string Value { get; }
        
        public ParticipantIdentity(Process participantProcess)
        {
            // Process.GetCurrentProcess();
            User = participantProcess.StartInfo.EnvironmentVariables["USERNAME"];
            Process = participantProcess.Id;
            Session = participantProcess.SessionId;
            Value = $"{User}@{Process}@{Session}";
        }

        public string User { get; }
        public int Process { get; }
        public int Session { get; }
    }
}
