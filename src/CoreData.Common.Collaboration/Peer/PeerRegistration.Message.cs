using System;

namespace CoreData.Common.Messaging.Peer
{
    [Serializable]
    public struct PeerRegistration
    {
        public PeerRegistration(ParticipantIdentity participant)
        {
            Participant = participant;
            PeerId = null;
        }

        public ParticipantIdentity Participant { get; }

        public string PeerId { get; set; }
    }
}
