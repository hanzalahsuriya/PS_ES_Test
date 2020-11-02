using Platform.Messaging;

namespace Contact.Domain.Events
{
    public class AddressUpdated : IEvent
    {
        public AddressUpdated(string causationId)
        {
            CausationId = causationId;
        }

        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string CausationId { get; }
    }
}
