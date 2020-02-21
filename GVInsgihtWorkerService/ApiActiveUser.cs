using System;
using System.Collections.Generic;
using System.Text;

namespace GVInsgihtWorkerService
{
   public class ApiActiveUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public String IP { get; set; }

        public DateTime Date { get; set; }
    }
}
