namespace Platform.Messaging
{
    public abstract class Event : IEvent
    {
        public Event(string causationId, string aggregateId)
        {
            CausationId = causationId;
            AggregateId = aggregateId;
        }

        public string CausationId { get; }
        public string AggregateId { get; }

        public string Type => GetType().Name;
    }
}
