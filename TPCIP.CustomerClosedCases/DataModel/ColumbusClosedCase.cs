using System.Runtime.Serialization;

namespace TPCIP.CustomerClosedCases.DataModel
{
    [DataContract]
    public class ColumbusClosedCase
    {
        [DataMember]
        public string orderNo { get; set; }

        [DataMember]
        public string newLid { get; set; }

        [DataMember]
        public string oldLid { get; set; }

        [DataMember]
        public string kusagkd { get; set; }

        [DataMember]
        public string customerCaseText { get; set; }

        [DataMember]
        public string transcode { get; set; }

        [DataMember]
        public string orderText { get; set; }

        [DataMember]
        public string orderDate { get; set; }

        [DataMember]
        public string performedDate { get; set; }

        [DataMember]
        public string fakDate { get; set; }

        [DataMember]
        public string cancellationDate { get; set; }
    }
}
