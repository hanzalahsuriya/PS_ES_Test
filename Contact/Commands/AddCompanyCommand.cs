using System;
using MediatR;

namespace Contact.Commands
{
    public class AddCompanyCommand : IRequest
    {
        public string ContactId { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
