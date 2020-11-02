using System;
using System.Collections.Generic;
using System.Linq;
using Contact.Api.Controllers;
using Platform.Domain;
using Platform.Messaging;

namespace Contact.Domain
{
    public class ContactWriter : AggregateWriter<Contact>
    {
        public ContactWriter(IEventPersistenceClient eventPersistenceClient, IStreamNameProvider streamNameProvider)
            : base(eventPersistenceClient, streamNameProvider)
        {
        }

        protected override string Type => typeof(Contact).Name;

        protected override Dictionary<string, Type> SubscribedEventTypes
        {
            get
            {
                // use reflection
                var domainEvents = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                    .Where((x) => typeof(IEvent).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

                List<KeyValuePair<string, Type>> mappings = domainEvents.Select(CreateEventNameToTypeMapping).ToList();

                return mappings.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                //return new List<KeyValuePair<string, Type>>
                //{
                //    CreateEventNameToTypeMapping(typeof(ContactCreated)),
                //    CreateEventNameToTypeMapping(typeof(AddressUpdated)),
                //    CreateEventNameToTypeMapping(typeof(CompanyAdded)),
                //    CreateEventNameToTypeMapping(typeof(CompanyDeleted)),
                //}.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        private KeyValuePair<string, Type> CreateEventNameToTypeMapping(Type type)
        {
            return new KeyValuePair<string, Type>(type.Name, type);
        }
    }
}
