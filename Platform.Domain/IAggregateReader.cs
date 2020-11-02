using System.Threading.Tasks;

namespace Platform.Domain
{
    public interface IAggregateReader<TAggregate> where TAggregate : class, IAggregate, new()
    {
        Task<TAggregate> Load(string aggregateId);
    }
}
