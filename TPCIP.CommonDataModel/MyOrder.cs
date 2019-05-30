using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [DataContract]
    public class MyOrderData
    {
        [DataMember]
        public string fsmTaskId { get; set; }

        [DataMember]
        public string lid { get; set; }

        [DataMember]
        public string taskStatus { get; set; }

        [DataMember]
        public string schedulingArea { get; set;}   

        [DataMember]
        public string addressLine1 { get; set; }

        [DataMember]
        public string addressLine2 { get; set; }

        [DataMember]
        public string addressLine3 { get; set; }

        [DataMember]
        public string addressLine4 { get; set; }

        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string zippost { get; set; }

        [DataMember]
        public string hfNumber { get; set; }

        [DataMember]
        public string installationNumber { get; set; }

        [DataMember]
        public string analegNumber { get; set; }

        [DataMember]
        public string orderNo { get; set; }

        [DataMember]
        public string orderType { get; set; }

        [DataMember]
        public string product { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string start { get; set; }

        [DataMember]
        public string end { get; set; }

    }
}




