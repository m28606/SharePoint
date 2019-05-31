using System;
using System.Runtime.Serialization;

namespace TPCIP.CustomerOpenCases.DataModel
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
