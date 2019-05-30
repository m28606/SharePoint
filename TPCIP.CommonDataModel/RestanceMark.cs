using System;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [Serializable]
    [DataContract]
    public class ProductAttentionMarker
    {
        [DataMember(Name = "productName")]
        public string productName { get; set; }

        [DataMember(Name = "subscriptionNo")]
        public string subscriptionNo { get; set; }

        [DataMember(Name = "theftLockEnabled")]
        public Boolean isTheftLocked { get; set; }

        [DataMember(Name = "CommitmentEndDate")]
        public string commitmentEndDate { get; set; }
    }
}
