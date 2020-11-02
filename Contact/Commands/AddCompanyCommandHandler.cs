using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Platform.Domain;

namespace Contact.Commands
{
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand>
    {
        private readonly IAggregateWriter<Domain.Contact> _aggregateRepository;

        public AddCompanyCommandHandler(IAggregateWriter<Domain.Contact> aggregateWriter)
        {
            _aggregateRepository = aggregateWriter;
        }

        public async Task<Unit> Handle(AddCompanyCommand command, CancellationToken cancellationToken)
        {
            var contact = await _aggregateRepository.Load(command.ContactId);

            contact.AddCompany(command.CompanyId, command.CompanyName, command.CreatedDate);

            await _aggregateRepository.Save(contact);

            return Unit.Value;
        }
    }
}
