using System;
using System.Collections.Generic;
using System.Text;

namespace GVInsgihtWorkerService.CustomersImport
{
    public class CustomerApiImport
    {
        public int Id { get; set; }
       
        public DateTime? LastImport { get; set; }

        public string WhenType { get; set; }

    }
}
