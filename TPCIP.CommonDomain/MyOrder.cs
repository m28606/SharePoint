using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.CommonDomain;

namespace TPCIP.CommonDomain
{
    public class MyOrderDomain
    {
        public string fsmTaskId { get; set; }
        public string Lid { get; set; }
        public string TaskStatus { get; set; }
        public string SchedulingArea { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string Zippost { get; set; }
        public string HfNumber { get; set; }
        public string InstallationNumber { get; set; }
        public string AnalegNumber { get; set; }
        public string RequestID { get; set; }
        public string OrderType { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public DateTime PlanStartDate { get; set; }
        public string PlanStartDateDisplay { get; set; }
        public DateTime PlanEndDate { get; set; }
        public string PlanEndDateDisplay { get; set; }
    }
}




