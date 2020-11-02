using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Messaging
{
    public interface IEventPersistenceClient
    {
        Task<List<IEvent>> Load(string streamName, Dictionary<string, Type> subscribedEventTypes);

        Task<EventWriteResult> Save(List<IEvent> events, string streamName, long expectedVersion);
    }
}