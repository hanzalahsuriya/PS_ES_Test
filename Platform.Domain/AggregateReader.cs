using Platform.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Domain
{
    public abstract class AggregateReader<TAggregate> : IAggregateReader<TAggregate> where TAggregate : class, IAggregate, new()
    {
        protected readonly IEventPersistenceClient _eventPersistenceClient;
        protected readonly IStreamNameProvider _streamNameProvider;

        protected abstract string Type { get; }

        protected abstract Dictionary<string, Type> SubscribedEventTypes { get; }

        public AggregateReader(IEventPersistenceClient eventPersistenceClient, IStreamNameProvider streamNameProvider)
        {
            _eventPersistenceClient = eventPersistenceClient;
            _streamNameProvider = streamNameProvider;
        }

        public async Task<TAggregate> Load(string aggregateId)
        {
            var aggregate = new TAggregate();

            var streamName = _streamNameProvider.GetStreamName(Type, aggregateId);

            var events = await _eventPersistenceClient.Load(streamName, SubscribedEventTypes);

            if (events.Any())
            {
                aggregate.Load(events);
            }

            return aggregate;
        }
    }
}
