using System;

namespace Contact.Exceptions
{
    public class ContactDomainException : Exception
    {
        public ContactDomainException(string message) : base(message)
        {

        }
    }
}
