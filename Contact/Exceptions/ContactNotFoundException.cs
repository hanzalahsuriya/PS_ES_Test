namespace Contact.Exceptions
{
    public class ContactNotFoundException : ContactDomainException
    {
        public ContactNotFoundException() : base("contact not found")
        {
        }
    }
}
