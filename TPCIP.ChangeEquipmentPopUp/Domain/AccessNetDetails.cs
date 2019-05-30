using System.Collections.Generic;

namespace TPCIP.ChangeEquipmentPopUp.Domain
{
    public class AccessNetDetails
    {
        public string ModemMac { get; set; }
        public string MtaMac { get; set; }
        public List<DefinePriority> ListCpeMac { get; set; }
        public string StdCpeMac { get; set; }
        public string AddCpeMac { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsAddCpeExist { get; set; }
        public List<DefinePriority> StdCpeList { get; set; }
        public List<DefinePriority> AddCpeList { get; set; }
        public string Message { get; set; }
        public string Lid { get; set; }
        public bool IsError { get; set; }
        public bool OtherUser { get; set; }
    }

    public class DefinePriority
    {
        public string Value { get; set; }
        public bool Selected { get; set; }
    }

    public enum AccessNetType
    {
        CableModem,
        StdCpe,
        Mta,
        AddCpe
    }

    public class ChangeEquipmentElements
    {
        public string CmMac { get; set; }
        public string StdCpe { get; set; }
        public string AddCpe { get; set; }
    }
}
