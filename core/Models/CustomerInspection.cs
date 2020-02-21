using System;
using System.Collections.Generic;
using System.Text;

namespace core.Models
{
   public class CustomerInspection
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Count { get; set; }

        public DateTime CompletedDatetime { get; set; }
        public Customer Customer { get; set; }

    }
}
