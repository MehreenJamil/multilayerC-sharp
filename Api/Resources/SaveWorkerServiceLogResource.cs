﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Resources
{
    public class SaveWorkerServiceLogResource
    {
      
        public DateTime Start_time { get; set; }

        public DateTime? End_time { get; set; }
        public String Exception { get; set; }
        public DateTime? Execution_time { get; set; }
    }
}
