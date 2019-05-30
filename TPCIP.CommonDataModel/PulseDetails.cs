using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [DataContract]
    public class PulseDetails
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string loennr { get; set; }
        [DataMember]
        public string a_number { get; set; }
        [DataMember]
        public string kontorfork { get; set; }
        [DataMember]
        public string account_name { get; set; }
    }

}
