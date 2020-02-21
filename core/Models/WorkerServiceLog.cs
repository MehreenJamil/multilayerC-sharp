using System;
using System.Collections.Generic;
using System.Text;

namespace core.Models
{
    public class WorkerServiceLog
    {
        public WorkerServiceLog()
        {
           
        }

        public int Id { get; set; }
        public DateTime Start_time { get; set; }

        public DateTime? End_time { get; set; }
        public String Exception { get; set; }
        public DateTime? Execution_time { get; set; }
        



    }
}
