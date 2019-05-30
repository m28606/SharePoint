using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [DataContract]
    public class ClickRateMessage
    {
        [DataMember]
        public string message { get; set; }
    }
}
