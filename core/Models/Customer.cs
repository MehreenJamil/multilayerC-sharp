using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace core.Models
{
   
    public class Customer
    {
        public Customer()
        {
            CustomerImports = new Collection<CustomerImport>();
        }

        public int Id { get; set; }
        public String Name { get; set; }

        public String HostUrl { get; set; }
        public String ApiUserName { get; set; }
        public String ApiPassword { get; set; }
        public Boolean Removed { get; set; }
        public String image { get; set; }
        public String CustomerColor { get; set; }
        

        public ICollection<CustomerImport> CustomerImports { get; set; }
        public ICollection<CustomerInspection>? CustomerInspections { get; set; }
        public ICollection<CustomerModule>? CustomerModules { get; set; }
        public ICollection<CustomerProperty>? CustomerProperties { get; set; }

    }
}
