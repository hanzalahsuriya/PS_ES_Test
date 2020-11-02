using Platform.Messaging;
using System.Collections.Generic;

namespace Platform.Domain
{
    public interface IAggregate
    {
        string Id { get; }
        long ExpectedVersion { get; }
        IReadOnlyCollection<IEvent> GetPendingChanges();
        IReadOnlyCollection<string> GetCausationIds();
        void ClearPendingChanges(bool committed);
        void Apply(IEvent @event);
        void Load(IEnumerable<IEvent> evts);
        bool HasProcessed { get; }
    }
}
