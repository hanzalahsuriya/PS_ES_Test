using Platform.Messaging;
using System.Threading.Tasks;

namespace Platform.Domain
{
    public interface IAggregateWriter<TAggregate> : IAggregateReader<TAggregate> where TAggregate : class, IAggregate, new()
    {
        Task<EventWriteResult> Save(IAggregate aggregate);
    }
}
