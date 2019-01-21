using NLog;

namespace CoreData.Common.Messaging.Peer
{
    public class PeerRegistry : IPeerRegistry
    {
        private readonly ILogger _logger;

        public PeerRegistry(ILogger logger) => _logger = logger;

        public string RegisterParticipant(ParticipantIdentity participant)
        {
            var registration = new PeerRegistration(participant);
            // todo: tb implemented
            var ex = new RegistrationException($"Participant already registered: {participant}");
            _logger.Error(ex);
            throw ex;
        }
    }
}
