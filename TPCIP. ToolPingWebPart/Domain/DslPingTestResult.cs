using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPCIP.ToolPingWebPart.Domain
{
    public class DslPingTestResult
    {
        public PingTestResult PingTestResponse { get; set; }
    }

    public class PingTestResult
    {
        public IpPingTestResult IpPingTestResponse { get; set; }
        public string Statuscode { get; set; }
        public string Statusmessage { get; set; }
    }

    public class IpPingTestResult
    {
        public Ipv4PingResultDomain Ipv4PingResult { get; set; }
        public Ipv4PingResultDomain Ipv6PingResult { get; set; }
        public string RawOutput { get; set; }
    }

    public class Ipv4PingResultDomain
    {
        public string OptionalResult { get; set; }
        public string Summary { get; set; }
        public string Version { get; set; }
        public List<IpResultListDomain> IpResultList { get; set; }
    }

    public class IpResultListDomain
    {
        public string Dscp { get; set; }
        public string DstIp { get; set; }
        public string LossPct { get; set; }
        public string MaxDelayMs { get; set; }
        public string MinDelayMs { get; set; }
        public string AvgDelayMs { get; set; }
        public string IpType { get; set; }
    }

    public class HybridIpPingResultInfo
    {
        public string IPType { get; set; }
        public string ConnectionStatus { get; set; }
    }
}
