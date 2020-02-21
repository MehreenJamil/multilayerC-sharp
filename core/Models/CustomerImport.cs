using System;
using System.Collections.Generic;
using System.Text;

namespace core.Models
{
    public class CustomerImport
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public String Name { get; set; }
        public DateTime LastImport { get; set; }
        public DateTime? NextImport { get; set; }
        public int ExternalImportId { get; set; }
        public Boolean Removed { get; set; }

        public Customer Customer { get; set; }
    }
}
