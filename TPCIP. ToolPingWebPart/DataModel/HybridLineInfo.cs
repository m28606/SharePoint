using System.Runtime.Serialization;
using System.Collections.Generic;

namespace TPCIP.ToolPingWebPart.DataModel
{
    //Start - Hybrid domain models
    public class WanUUmtsInterfaceConfig
    {
        [DataMember]
        public string simStatus { get; set; }
        [DataMember]
        public string serviceStatus { get; set; }
        [DataMember]
        public string systemMode { get; set; }
        [DataMember]
        public string ltebandValue { get; set; }
        [DataMember]
        public string signalQuality { get; set; }
        [DataMember]
        public string downLoadData { get; set; }
        [DataMember]
        public string upLoadData { get; set; }
        [DataMember]
        public string antennaSet { get; set; }
    }

    public class WanAppConnection
    {
        [DataMember]
        public string connectionStatus { get; set; }
        [DataMember]
        public string externalIPAddress { get; set; }
        [DataMember]
        public string ipV4Enable { get; set; }
        [DataMember]
        public string ipV6Enable { get; set; }
        [DataMember]
        public string ipV6ConnectionStatus { get; set; }
        [DataMember]
        public string ipV6Address { get; set; }
    }

    public class BondingStatus
    {
        [DataMember]
        public string bondingStatus { get; set; }
        [DataMember]
        public string bondingMode { get; set; }
        [DataMember]
        public string bondingServerName { get; set; }
        [DataMember]
        public string bondingServerIPAddress { get; set; }
        [DataMember]
        public string username { get; set; }
    }

    public class SimCardInfo
    {
        [DataMember]
        public string iccid { get; set; }
        [DataMember]
        public string imei { get; set; }
        [DataMember]
        public string imsi { get; set; }
    }

    public class NetworkStatus
    {
        [DataMember]
        public WanUUmtsInterfaceConfig wanUUmtsInterfaceConfig { get; set; }
        [DataMember]
        public WanAppConnection wanAppConnection { get; set; }
        [DataMember]
        public BondingStatus bondingStatus { get; set; }
        [DataMember]
        public SimCardInfo simCardInfo { get; set; }
    }
    [DataContract]
    public class HybridLineInfo
    {
        [DataMember]
        public List<NetworkStatus> networkStatus { get; set; }

    }
    //Ends - Hybrid domain models
}
