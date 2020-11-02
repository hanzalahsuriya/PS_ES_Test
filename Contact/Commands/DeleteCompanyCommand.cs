using MediatR;

namespace Contact.Commands
{
    public class DeleteCompanyCommand : IRequest
    {
        public string ContactId { get; set; }
        public string CompanyId { get; set; }
    }
}
