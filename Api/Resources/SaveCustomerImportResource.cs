using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Resources
{
    public class SaveCustomerImportResource
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public String Name { get; set; }
        public DateTime LastImport { get; set; }
        public DateTime NextImport { get; set; }
        public int ExternalImportId { get; set; }
        public Boolean Removed { get; set; }

    }
}
