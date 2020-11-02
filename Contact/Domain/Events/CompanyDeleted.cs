using System;
using Platform.Messaging;

namespace Contact.Domain.Events
{
    public class CompanyDeleted : IEvent
    {
        public string CausationId { get; } = Guid.NewGuid().ToString();
        public string ContactId { get; set; }
        public string CompanyId { get; set; }
    }
}
