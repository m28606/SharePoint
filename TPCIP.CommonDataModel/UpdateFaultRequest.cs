using System;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [Serializable]
    [DataContract]
    public class UpdateFaultRequest
    {
        [DataMember(Name = "note")]
        public string note { get; set; }
    }
}
