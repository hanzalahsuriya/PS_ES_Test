using EventStore.ClientAPI;

namespace Platform.Messaging.EventStore.Factories
{
    public interface IEventStoreConnectionFactory
    {
        IEventStoreConnection Create();
    }
}