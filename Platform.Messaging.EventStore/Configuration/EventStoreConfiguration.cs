using System;

namespace Platform.Messaging.EventStore.Configuration
{
    public class EventStoreConfiguration : IEventStoreConfiguration
    {
        public string Username { get; set; }

        public string Password { get; set; }
        
        public Uri Endpoint { get; set; }
    }
}