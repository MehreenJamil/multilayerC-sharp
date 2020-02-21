using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Resources
{
    public class CustomerModuleResource
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public int CustomerId { get; set; }
        public int ModuleId { get; set; }
        public CustomerResource Customer { get; set; }
        public ModuleResource Module { get; set; }
    }
}
