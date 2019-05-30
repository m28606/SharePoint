using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPCIP.ToolPingWebPart.Domain
{
    public class ServiceInfo
    {
        public List<ServiceInfoDetails> SikDetails { get; set; }
        public string Port { get; set; }
        public string Dslam { get; set; }
        public string DslamCountry { get; set; }
    }

    public class ServiceInfoDetails
    {
        public string serviceClosed { get; set; }
        public string serviceSpeedDown { get; set; }
        public string serviceSpeedUp { get; set; }
        public string serviceType { get; set; }
        public string sik { get; set; }
        public string tpNo { get; set; }
        public string urlMrtg { get; set; }
        public string urlMtman { get; set; }
        public string urlTestman { get; set; }
        public string urlTvman { get; set; }
        public string urlWebman { get; set; }
    }
}
