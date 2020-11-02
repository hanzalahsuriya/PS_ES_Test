using System.Collections.Generic;
using Platform.Domain;

namespace Contact.Domain
{
    public class Address : ValueObject
    {
        public Address()
        {

        }

        public Address(string houseNameNumber, string street, string locality, string city, string county, string postcode, string countryIsoAlpha3)
        {
            HouseNameNumber = houseNameNumber;
            Street = street;
            Locality = locality;
            City = city;
            County = county;
            Postcode = postcode;
            CountryIsoAlpha3 = countryIsoAlpha3;
        }

        public string HouseNameNumber { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string CountryIsoAlpha3 { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return HouseNameNumber;
            yield return Street;
            yield return Locality;
            yield return City;
            yield return County;
            yield return Postcode;
            yield return CountryIsoAlpha3;

        }
    }
}
