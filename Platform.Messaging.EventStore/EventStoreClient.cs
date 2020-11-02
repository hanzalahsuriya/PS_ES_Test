using EventStore.ClientAPI;
using EventStore.ClientAPI.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Platform.Messaging.EventStore.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Messaging.EventStore
{
    public class EventStoreClient : IEventPersistenceClient
    {
        private readonly IEventStoreConnection _connection;
        private readonly ILogger<EventStoreClient> _logger;

        public EventStoreClient(
            IEventStoreConnectionFactory factory,
            ILogger<EventStoreClient> logger)
        {
            _connection = factory.Create();
            _logger = logger;
        }

        public async Task<List<IEvent>> Load(string streamName, Dictionary<string, Type> subscribedEventTypes)
        {
            var streamedEvents = new List<IEvent>();

            var nextPageStart = 0L;
            do
            {
                var page = await _connection.ReadStreamEventsForwardAsync(
                    streamName, nextPageStart, 4096, false);

                if (page.Events.Length > 0)
                {
                    streamedEvents.AddRange(page.Events
                        .Select(x => Event(x.Event.Data, subscribedEventTypes[x.Event.EventType]))
                        .Cast<IEvent>());
                }

                nextPageStart = !page.IsEndOfStream ? page.NextEventNumber : -1;
            } while (nextPageStart != -1);

            return streamedEvents;
        }

        private object Event(byte[] data, Type type)
        {
            return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), type);
        }

        public async Task<EventWriteResult> Save(List<IEvent> events, string streamName, long expectedVersion)
        {
            var eventWriteResult = new EventWriteResult();
            var eventData = events
                .Select(MapEventToEventData)
                .ToArray();

            try
            {
                await _connection.AppendToStreamAsync(streamName, expectedVersion, eventData);

                eventWriteResult.WriteStatus = WriteStatus.Success;
            }
            catch (WrongExpectedVersionException ex)
            {
                _logger.LogWarning(ex, "WrongExpectedVersionException");

                eventWriteResult.WriteStatus = WriteStatus.WrongExpectedVersion;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception of type [{ex.GetType().Name}] - Message = {ex.Message}");

                eventWriteResult.WriteStatus = WriteStatus.Error;
            }

            return eventWriteResult;
        }

        private EventData MapEventToEventData(IEvent e)
        {
            return new EventData(
                Guid.NewGuid(),
                e.GetType().Name,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e)),
                null);
        }
    }
}
