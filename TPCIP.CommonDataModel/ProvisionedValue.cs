using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{    
    [DataContract]
    public class ProvisionedValue
    {
        [DataMember]
        public bool active { get; set; }        
        [DataMember]
        public string forwardNumber { get; set; }
        [DataMember]
        public int timeoutValue { get; set; }
    }

    [DataContract]
    public class ProvisionedValueBts
    {
        [DataMember]
        public bool active { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string number { get; set; }
    }
}
