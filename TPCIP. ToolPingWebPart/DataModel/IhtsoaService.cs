using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.ToolPingWebPart.DataModel
{
    [DataContract]
    public class IhtsoaService
    {
        [DataMember]
        public string accessType { get; set; }
        [DataMember]
        public string bestEffort { get; set; }
        [DataMember]
        public string dslam { get; set; }

        [DataMember]
        public string dslamCountry { get; set; }
        [DataMember]
        public string dslamCapacityIssue { get; set; }
        [DataMember]
        public string lid { get; set; }

        [DataMember]
        public string lineSpeedDown { get; set; }
        [DataMember]
        public string lineSpeedUp { get; set; }
        [DataMember]
        public string port { get; set; }
        [DataMember]
        public List<ServiceInfo> serviceInfo { get; set; }
        [DataMember]
        public List<ServiceInfo> services { get; set; }        [DataMember]
        public string urlAdman { get; set; }
        [DataMember]
        public string urlDslmon { get; set; }
    }
}
