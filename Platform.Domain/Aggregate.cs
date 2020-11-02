using System.Collections.Generic;
using System.Linq;
using Platform.Messaging;

namespace Platform.Domain
{
    public abstract class Aggregate
    {
        protected readonly ICollection<IEvent> PendingChanges = new List<IEvent>();
        protected readonly ICollection<string> CausationIds = new List<string>();

        public long ExpectedVersion { get; private set; } = -1;

        protected abstract void When(IEvent @event);

        public IReadOnlyCollection<IEvent> GetPendingChanges()
        {
            return PendingChanges.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> GetCausationIds()
        {
            return CausationIds.ToList().AsReadOnly();
        }

        public bool HasProcessed
        {
            get
            {
                foreach (var pendingChange in PendingChanges)
                {
                    if (CausationIds.Contains(pendingChange.CausationId))
                        return true;
                }

                return false;
            }
        }

        public void ClearPendingChanges(bool committed = false)
        {
            if (committed)
            {
                foreach (var causationId in PendingChanges.Select(x => x.CausationId).Distinct())
                {
                    CausationIds.Add(causationId);
                }
            }
            PendingChanges.Clear();
        }

        public void Apply(IEvent @event)
        {
            When(@event);
            PendingChanges.Add(@event);
        }

        public void Load(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                When(@event);

                if (!CausationIds.Contains(@event.CausationId))
                    CausationIds.Add(@event.CausationId);

                ExpectedVersion++;
            }
        }
    }
}
