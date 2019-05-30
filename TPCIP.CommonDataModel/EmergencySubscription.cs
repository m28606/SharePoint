using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [DataContract]
    public class EmergencySubscription
    {
        [DataMember(Name = "accountNo")]
        public long parentAccountNo { get; set; }

        [DataMember(Name = "lid")]
        public string rootLid { get; set; }

        [DataMember(Name = "customerName")]
        public string customerName { get; set; }

        [DataMember(Name = "postalNumber")]
        public string zipCity { get; set; }

        [DataMember(Name = "address")]
        public string fullAddress { get; set; }

        [DataMember(Name = "madId")]
        public string madId { get; set; }

    }

}

