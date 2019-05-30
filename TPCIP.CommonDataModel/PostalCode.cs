using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [DataContract]
    public class PostalCode
    {
        [DataMember]
        public string postalCode { get; set; }

        [DataMember]
        public string postalDistrict { get; set; }
    }
}
