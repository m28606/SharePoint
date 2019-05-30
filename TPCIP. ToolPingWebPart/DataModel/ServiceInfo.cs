using System.Runtime.Serialization;

namespace TPCIP.ToolPingWebPart.DataModel
{
    [DataContract]
    public class ServiceInfo
    {
        [DataMember]
        public string serviceClosed { get; set; }
        [DataMember]
        public string serviceSpeedDown { get; set; }
        [DataMember]
        public string serviceSpeedUp { get; set; }
        [DataMember]
        public string serviceType { get; set; }
        [DataMember]
        public string sik { get; set; }
        [DataMember]
        public string tpNo { get; set; }
        [DataMember]
        public string urlMrtg { get; set; }
        [DataMember]
        public string urlMtman { get; set; }
        [DataMember]
        public string urlTestman { get; set; }
        [DataMember]
        public string urlTvman { get; set; }
        [DataMember]
        public string urlWebman { get; set; }
    }
}
