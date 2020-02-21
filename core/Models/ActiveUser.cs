using System;
using System.Collections.Generic;
using System.Text;

namespace core.Models
{
    public class ActiveUser
    {
        public int Id { get; set; }
       
        public int Count { get; set; }

        public DateTime UserActiveDate { get; set; }
        public int CustomerId { get; set; }
        
    }
}
