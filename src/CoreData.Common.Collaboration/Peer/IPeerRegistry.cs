namespace CoreData.Common.Messaging.Peer
{
    /// <summary>Peer's container, which registers|unregisters identities for correct message routing.</summary>
    public interface IPeerRegistry
    {
        /// <summary>Registers participant(cdd process) as messaging Peer and returns calculated PeerId.
        /// <para>If such identity already registered, then all registered Peers will be notified 
        /// and ParticipantRegistrationException will be thrown.</para></summary>
        string RegisterParticipant(ParticipantIdentity participant);
    }
}
