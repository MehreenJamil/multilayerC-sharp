using System;
using System.Collections.Generic;
using System.Text;

namespace core.Models
{
    public class CustomerModule
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public int CustomerId { get; set; }
        public int ModuleId { get; set; }
        public Customer Customer { get; set; }
        public Module Module { get; set; }


    }
}
