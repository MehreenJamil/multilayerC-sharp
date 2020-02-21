using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Resources
{
    public class SaveCustomerInspectionResource
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Count { get; set; }

        public DateTime CompletedDatetime { get; set; }

    }
}
