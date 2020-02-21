using System;
using System.Collections.Generic;
using System.Text;

namespace GVInsgihtWorkerService
{
    class ApiCustomerInspections
    {
        public int PropertyNumber { get; set; }
        public String InspectionCategoryName { get; set; }
        public DateTime? InspectedDate { get; set; }
        public DateTime? NextFinishDate { get; set; }
        public DateTime? NextStartDate { get; set; }
        public String PropertyName { get; set; }
        public String BuildingName { get; set; }
        public String BuildingNumber { get; set; }
        public String ComponentName { get; set; }
        public String InspectionResultDescription { get; set; }
        public Boolean IsPaused { get; set; }
        public int InspectionResult { get; set; }
        public int FaultCount { get; set; }
        public int FaultFixedCount { get; set; }
        public int InspectionId { get; set; }
        public int InspectionItemCategoryId { get; set; }
        public String MinFaultPrio { get; set; }

    }
}
