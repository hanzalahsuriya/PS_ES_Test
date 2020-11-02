using Platform.Messaging;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Domain
{
    public abstract class AggregateWriter<TAggregate> : AggregateReader<TAggregate>, IAggregateWriter<TAggregate> where TAggregate : class, IAggregate, new()
    {
        public AggregateWriter(
            IEventPersistenceClient eventPersistenceClient,
            IStreamNameProvider streamNameProvider) :
            base(eventPersistenceClient, streamNameProvider)
        {

        }

        public async Task<EventWriteResult> Save(IAggregate aggregate)
        {
            var streamName = _streamNameProvider.GetStreamName(Type, aggregate.Id);
            EventWriteResult result;
            bool committed = false;
            if (!aggregate.HasProcessed)
            {
                result = await _eventPersistenceClient.Save(aggregate.GetPendingChanges().ToList(), streamName, aggregate.ExpectedVersion);
                committed = true;
            }
            else
            {
                result = new EventWriteResult() { WriteStatus = WriteStatus.AlreadyProcessed };
            }

            aggregate.ClearPendingChanges(committed);

            return result;
        }
    }
}
