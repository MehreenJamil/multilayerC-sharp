using System;
using System.Collections.Generic;
using System.Text;

namespace GVInsgihtWorkerService
{
    class ApiCustomerImport
    {
        public int Id { get; set; }
        public String WhenValue { get; set; }
        public DateTime LastImport { get; set; }
        public String WhenType { get; set; }
        public Boolean Enabled { get; set; }
      
        
    }
}
