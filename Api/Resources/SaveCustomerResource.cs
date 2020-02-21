using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Resources
{
    public class SaveCustomerResource
    {
        public String Name { get; set; }

        public String HostUrl { get; set; }
        public String ApiUserName { get; set; }
        public String ApiPassword { get; set; }
        public Boolean Removed { get; set; }
        public String image { get; set; }
        public String CustomerColor { get; set; }
    }
}
