using System.Runtime.Serialization;

namespace TPCIP.ToolPingWebPart.DataModel
{
    [DataContract]
    public class DslLineTestResult
    {
        [DataMember]
        public string equipment { get; set; }
        [DataMember]
        public string ipAddress { get; set; }

        [DataMember]
        public string macAddress { get; set; }

        [DataMember]
        public string ip { get; set; }
    }
}
