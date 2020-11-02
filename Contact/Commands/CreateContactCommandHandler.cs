using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Platform.Domain;

namespace Contact.Commands
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand>
    {
        private readonly IAggregateWriter<Domain.Contact> _aggregateRepository;

        public CreateContactCommandHandler(IAggregateWriter<Domain.Contact> aggregateWriter)
        {
            _aggregateRepository = aggregateWriter;
        }

        public async Task<Unit> Handle(CreateContactCommand command, CancellationToken cancellationToken)
        {
            var contact = await _aggregateRepository.Load(command.ContactId);

            contact.CreateContact(command.ContactId, command.UserId, command.FirstName, command.LastName, command.DateOfBirth, command.Nationality, command.EmailAddress, command.Country);

            contact.SetAddress(command.ContactId, command.HouseNumber, command.Street, command.Locality,
                command.City, command.County, command.Postcode, command.Country);

            await _aggregateRepository.Save(contact);

            return Unit.Value;
        }
    }
}
