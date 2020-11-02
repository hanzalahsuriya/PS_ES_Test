using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Platform.Domain;

namespace Contact.Commands
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly IAggregateWriter<Domain.Contact> _aggregateRepository;

        public DeleteCompanyCommandHandler(IAggregateWriter<Domain.Contact> aggregateWriter)
        {
            _aggregateRepository = aggregateWriter;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
        {
            var contact = await _aggregateRepository.Load(command.ContactId);

            contact.DeleteCompany(command.CompanyId);

            await _aggregateRepository.Save(contact);

            return Unit.Value;
        }
    }
}
