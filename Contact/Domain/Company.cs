using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Domain
{
    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }
}
