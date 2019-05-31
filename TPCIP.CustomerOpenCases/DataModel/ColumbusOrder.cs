using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.CustomerOpenCases.DataModel
{
    [DataContract]
    public class ColumbusOrder
    {
        [DataMember(Name = "orderNumber")]
        public long orderNumber { get; set; }

        [DataMember(Name = "orderType")]
        public string orderType { get; set; }

        [DataMember(Name = "orderStatus")]
        public string orderStatus { get; set; }

        [DataMember(Name = "creationDate")]
        public long creationDate { get; set; }
    }

    [DataContract]
    public class ColumbusOrderDroolsResult
    {

        [DataMember(Name = "errorClass")]
        public string errorClass { get; set; }

        [DataMember(Name = "errorClass_sub1")]
        public string errorClass_sub1 { get; set; }

        [DataMember(Name = "errorClass_sub2")]
        public string errorClass_sub2 { get; set; }

        [DataMember(Name = "errorClass_sub3")]
        public string errorClass_sub3 { get; set; }

        [DataMember(Name = "triggerAlgorithm")]
        public string triggerAlgorithm { get; set; }

        [DataMember(Name = "columbusOPenOrderList")]
        public List<ColumbusOrder> columbusOpenOrderList { get; set; }

        [DataMember(Name = "columbusCLosedOrderList")]
        public List<ColumbusOrder> columbusClosedOrderList { get; set; }
    }
}
