using CoreData.Common.HostEnvironment;
using System;
using System.Diagnostics;

namespace CoreData.Common.Messaging.Peer
{
    /// <summary>Participant registration info</summary>
    [DebuggerDisplay("{" + nameof(IDebugInfo.PrintValue) + "}")]
    [Obsolete("SessionIdentity should be used instead")]
    public struct ParticipantIdentity : IDebugInfo
    {
        public string PrintValue { get; }
        
        public ParticipantIdentity(Process participantProcess)
        {
            // Process.GetCurrentProcess();
            User = participantProcess.StartInfo.EnvironmentVariables["USERNAME"];
            Process = participantProcess.Id;
            Session = participantProcess.SessionId;
            PrintValue = $"{User}@{Process}@{Session}";
        }

        public string User { get; }
        public int Process { get; }
        public int Session { get; }
    }
}
