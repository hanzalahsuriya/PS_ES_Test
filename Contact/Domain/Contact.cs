using System;
using System.Collections.Generic;
using System.Linq;
using Contact.Domain.Events;
using Contact.Exceptions;
using Platform.Domain;
using Platform.Messaging;

namespace Contact.Domain
{
    public class Contact : Aggregate, IAggregate
    {
        public string Id => ContactId;
        public string ContactId { get; private set; }
        public string UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTimeOffset? DateOfBirth { get; private set; }
        public string NationalityIsoAlpha3 { get; private set; }
        public string CountryOfResidenceIsoAlpha3 { get; private set; }
        public string EmailAddress { get; private set; }
        public Address Address { get; private set; }


        private readonly List<Company> _companies = new List<Company>();
        public IReadOnlyCollection<Company> Companies => _companies;

        protected override void When(IEvent @event)
        {
            switch (@event)
            {
                case ContactCreated contactCreated: OnContactCreated(contactCreated); break;
                case AddressUpdated addressUpdated: OnAddressUpdated(addressUpdated); break;
                case CompanyAdded companyAdded: OnCompanyAdded(companyAdded); break;
                case CompanyDeleted companyDeleted: OnCompanyDeleted(companyDeleted); break;
            }
        }

        public void CreateContact(string contactId, string userId, string firstName, string lastName,
            DateTimeOffset? dateOfBirth, string nationality, string email, string country)
        {
            if (ExpectedVersion >= 0)
            {
                throw new ContactAlreadyExistsException();
            }

            Apply(new ContactCreated
            {
                ContactId = contactId,
                UserId = userId,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName,
                Nationality = nationality,
                DateOfBirth = dateOfBirth,
                Country = country
            });
        }

        public void SetAddress(string contactId, string houseNameNumber, string street, string locality, string city, string county, string postcode, string countryIsoAlpha3)
        {
            Apply(new AddressUpdated(contactId)
            {
                HouseNumber = houseNameNumber,
                Street = street,
                Locality = locality,
                City = city,
                Country = countryIsoAlpha3,
                County = county,
                Postcode = postcode
            });
        }

        public void AddCompany(string companyId, string companyName, DateTimeOffset companyCreatedDate)
        {
            if (ExpectedVersion == -1)
            {
                throw new ContactNotFoundException();
            }

            var isExistingCompany =  _companies.Any(x => x.Id == companyId);
            if (isExistingCompany)
            {
                throw new ContactDomainException("company already exists");
            }
            Apply(new CompanyAdded()
            {
                ContactId = ContactId,
                CompanyId = companyId,
                CompanyName = companyName,
                CreatedDate = companyCreatedDate
            });
        }

        public void DeleteCompany(string companyId)
        {
            if (ExpectedVersion == -1)
            {
                throw new ContactNotFoundException();
            }

            var isCompanyExists = _companies.Any(x => x.Id == companyId);
            if (!isCompanyExists)
            {
                throw new ContactDomainException("company not found or already deleted");
            }

            Apply(new CompanyDeleted()
            {
                ContactId = ContactId,
                CompanyId = companyId,
            });
        }


        // Event Handlers
        private void OnContactCreated(ContactCreated contactCreatedEvent)
        {
            ContactId = contactCreatedEvent.ContactId;
            UserId = contactCreatedEvent.UserId;
            EmailAddress = contactCreatedEvent.EmailAddress;
            FirstName = contactCreatedEvent.FirstName;
            LastName = contactCreatedEvent.LastName;
            DateOfBirth = contactCreatedEvent.DateOfBirth;
            NationalityIsoAlpha3 = contactCreatedEvent.Nationality;
            CountryOfResidenceIsoAlpha3 = contactCreatedEvent.Country;
        }

        private void OnAddressUpdated(AddressUpdated addressUpdated)
        {
            Address = new Address(addressUpdated.HouseNumber, addressUpdated.Street, addressUpdated.Locality,
                addressUpdated.City, addressUpdated.County, addressUpdated.Postcode, addressUpdated.Country);
        }

        private void OnCompanyAdded(CompanyAdded companyAdded)
        {
            _companies.Add(new Company()
            {
                Id = companyAdded.CompanyId,
                Name = companyAdded.CompanyName,
                CreatedDate = companyAdded.CreatedDate
            });
        }

        private void OnCompanyDeleted(CompanyDeleted companyDeleted)
        {
            _companies.Remove(_companies.Single(c => c.Id == companyDeleted.CompanyId));
        }
    }
}