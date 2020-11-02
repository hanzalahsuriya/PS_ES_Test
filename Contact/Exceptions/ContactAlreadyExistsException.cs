namespace Contact.Exceptions
{
    public class ContactAlreadyExistsException : ContactDomainException
    {
        public ContactAlreadyExistsException() : base("contact already exists")
        {
        }
    }
}
