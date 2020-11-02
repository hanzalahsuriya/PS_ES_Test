using System;
using Platform.Messaging;

namespace Contact.Domain.Events
{
    public class CompanyAdded : IEvent
    {
        public string CausationId => Guid.NewGuid().ToString();
        public string ContactId { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
