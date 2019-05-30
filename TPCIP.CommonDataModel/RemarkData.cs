using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    public class RemarkData
    {
        [DataMember]
        public string remark { get; set; }
        [DataMember]
        public string note { get; set; }
    }
}
