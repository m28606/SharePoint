using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TPCIP.ToolPingWebPart.DataModel
{
    [DataContract]
    public class DslPingTest
    {
        [DataMember]
        public PingTestResponse pingTestResponse { get; set; }
    }

    [DataContract]
    public class PingTestResponse
    {
        [DataMember]
        public IpPingTestResponse ipPingTestResponse { get; set; }
        [DataMember]
        public string statuscode { get; set; }
        [DataMember]
        public string statusmessage { get; set; }

    }

    [DataContract]
    public class IpPingTestResponse
    {
        [DataMember]
        public Ipv4PingResult ipv4PingResult { get; set; }
        [DataMember]
        public Ipv4PingResult ipv6PingResult { get; set; }
        [DataMember]
        public string rawOutput { get; set; }
    }

    [DataContract]
    public class Ipv4PingResult
    {
        [DataMember]
        public string optionalResult { get; set; }
        [DataMember]
        public string summary { get; set; }
        [DataMember]
        public string version { get; set; }
        [DataMember]
        public List<IpResultList> ipResult { get; set; }
    }

    [DataContract]
    public class IpResultList
    {
        [DataMember]
        public string dscp { get; set; }
        [DataMember]
        public string dstIp { get; set; }
        [DataMember]
        public string lossPct { get; set; }
        [DataMember]
        public string maxDelayMs { get; set; }
        [DataMember]
        public string minDelayMs { get; set; }
        [DataMember]
        public string avgDelayMs { get; set; }
        [DataMember]
        public string ipType { get; set; }

    }

}
