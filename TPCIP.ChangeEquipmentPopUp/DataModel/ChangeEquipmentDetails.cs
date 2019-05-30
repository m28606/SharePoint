using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.ChangeEquipmentPopUp.DataModel
{
    [DataContract]
    public class EquipmentDetails
    {
        [DataMember(Name = "cpeList")]
        public List<CpeList> cpeList { get; set; }

        [DataMember(Name = "accessnetList")]
        public List<AccessnetList> accessnetList { get; set; }
    }

    [DataContract]
    public class CpeList
    {
        [DataMember(Name = "ownerid")]
        public string ownerid { get; set; }

        [DataMember(Name = "macAddress")]
        public string macAddress { get; set; }
        
        [DataMember(Name = "giAddress")]
        public string giAddress { get; set; }
        
        [DataMember(Name = "deviceType")]
        public string deviceType { get; set; }
        
        [DataMember(Name = "wifiCapable")]
        public bool wifiCapable { get; set; }
        
        [DataMember(Name = "docsis3Capable")]
        public bool docsis3Capable { get; set; }
    }

    [DataContract]
    public class AccessnetList
    {
        [DataMember(Name = "docsis3Capable")]
        public bool docsis3Capable { get; set; }

        [DataMember(Name = "wifiCapable")]
        public bool wifiCapable { get; set; }

        [DataMember(Name = "ownerid")]
        public string ownerid { get; set; }

        [DataMember(Name = "svcProviderNm")]
        public string svcProviderNm { get; set; }

        [DataMember(Name = "cmTechnology")]
        public string cmTechnology { get; set; }

        [DataMember(Name = "maxNumCpe")]
        public string maxNumCpe { get; set; }

        [DataMember(Name = "equiqmentType")]
        public string equiqmentType { get; set; }

        [DataMember(Name = "manufacturer")]
        public string manufacturer { get; set; }

        [DataMember(Name = "macAddress")]
        public string macAddress { get; set; }

        [DataMember(Name = "cmts")]
        public string cmts { get; set; }

        [DataMember(Name = "giAddress")]
        public string giAddress { get; set; }

        [DataMember(Name = "model")]
        public string model { get; set; }

        [DataMember(Name = "serialNo")]
        public string serialNo { get; set; }

        [DataMember(Name = "classOfService")]
        public string classOfService { get; set; }

        [DataMember(Name = "type")]
        public string type { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "cpeMac")]
        public string cpeMac { get; set; }

        [DataMember(Name = "cmMacAddress")]
        public string cmMacAddress { get; set; }

        [DataMember(Name = "modemId")]
        public string modemId { get; set; }

        [DataMember(Name = "mtaMac")]
        public string mtaMac { get; set; }
    }

    [DataContract]
    public class ChangeEquipmentDetails
    {
        [DataMember(Name = "cmMac")]
        public string cmMac { get; set; }

        [DataMember(Name = "cpeMac")]
        public List<string> cpeMac { get; set; }
    }

    [DataContract]
    public class ChangeEquipmentStatus
    {
        [DataMember(Name = "orderId")]
        public int orderId { get; set; }

        [DataMember(Name = "request")]
        public string request { get; set; }

        [DataMember(Name = "response")]
        public string response { get; set; }

        [DataMember(Name = "state")]
        public string state { get; set; }
       
    }

    [DataContract]
    public class RegisteredUserStatus
    {
        [DataMember(Name = "lid")]
        public string lid { get; set; }

    }

}
