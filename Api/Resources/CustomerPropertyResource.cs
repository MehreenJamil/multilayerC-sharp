using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Resources
{
    public class CustomerPropertyResource
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Address { get; set; }
        public String Latitude { get; set; }
        public String Longitude { get; set; }
        public Double Size { get; set; }
        public int Year { get; set; }
        public int CustomerId { get; set; }
        public CustomerResource Customer { get; set; }
        public int ExternalId { get; set; }
        public Boolean Enabled { get; set; }
    }
}
