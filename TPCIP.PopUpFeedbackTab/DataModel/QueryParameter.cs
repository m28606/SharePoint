using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TPCIP.PopUpFeedbackTab.DataModel
{
    [Serializable]
    [DataContract]
    public class QueryParameter
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string value { get; set; }
    }
}
