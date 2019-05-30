using System;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [Serializable]
    [DataContract]
    public class FasoCreateRequest
    {
        [DataMember(Name = "note")]
        public string note { get; set; }

        [DataMember(Name = "youseeInstallationId")]
        public string youseeInstallationId { get; set; }

    }
}
