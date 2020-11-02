using System.Threading;
using System.Threading.Tasks;
using Contact.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Api.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactAsync([FromBody] CreateContactCommand createContactCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(createContactCommand, cancellationToken);
            return Ok();
        }

        [HttpPost("{id}/company")]
        public async Task<IActionResult> AddCompany(string id, [FromBody] AddCompanyCommand createContactCommand, CancellationToken cancellationToken)
        {
            createContactCommand.ContactId = id;
            await _mediator.Send(createContactCommand, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}/company/{companyId}")]
        public async Task<IActionResult> DeleteCompany(string id, string companyId , CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCompanyCommand()
            {
                CompanyId = companyId,
                ContactId = id
            }, cancellationToken);

            return Ok();
        }
    }
}
