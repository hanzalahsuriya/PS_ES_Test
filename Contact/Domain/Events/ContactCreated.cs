using System;
using Platform.Messaging;

namespace Contact.Domain.Events
{
    public class ContactCreated : IEvent
    {
        public string CausationId => Guid.NewGuid().ToString();
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
    }
}
