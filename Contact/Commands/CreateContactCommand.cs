using System;
using MediatR;

namespace Contact.Commands
{
    public class CreateContactCommand : IRequest
    {
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string EmailAddress { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }
}
