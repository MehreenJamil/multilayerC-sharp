using System;


namespace Api.Resources
{
    public class CustomerImportResource
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public String Name { get; set; }
        public DateTime LastImport { get; set; }
        public DateTime? NextImport { get; set; }
        public int ExternalImportId { get; set; }
        public Boolean Removed { get; set; }

        public CustomerResource Customer { get; set; }
    }
}
