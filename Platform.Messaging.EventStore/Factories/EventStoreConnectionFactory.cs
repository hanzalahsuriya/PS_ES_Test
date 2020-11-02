using System;
using EventStore.ClientAPI;
using Platform.Messaging.EventStore.Configuration;

namespace Platform.Messaging.EventStore.Factories
{
    public class EventStoreConnectionFactory : IEventStoreConnectionFactory
    {
        private readonly IEventStoreConfiguration _configuration;

        public EventStoreConnectionFactory(IEventStoreConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEventStoreConnection Create()
        {
            var connectionSettings = ConnectionSettings.Create().KeepReconnecting();
            var eventStoreClient = EventStoreConnection.Create(connectionSettings, new Uri(_configuration.Endpoint.ToString()));
            eventStoreClient.ConnectAsync().GetAwaiter().GetResult();

            return eventStoreClient;
        }
    }
}