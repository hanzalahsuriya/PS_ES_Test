using System;

namespace Platform.Messaging.EventStore.Configuration
{
    public interface IEventStoreConfiguration
    {
        string Username { get; }

        string Password { get; }

        Uri Endpoint { get; }
    }
}