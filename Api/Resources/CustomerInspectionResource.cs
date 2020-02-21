using System;


namespace Api.Resources
{
    public class CustomerInspectionResource
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Count { get; set; }

        public DateTime CompletedDatetime { get; set; }
        public CustomerResource Customer { get; set; }
    }
}
