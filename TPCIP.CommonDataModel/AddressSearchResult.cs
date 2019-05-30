using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [DataContract]
    public class AddressSearchResult
    {
        [DataMember]
        public List<MadAddress> addresses { get; set; }
    }

    [DataContract]
    public class MadAddress
    {
        public enum AddressType { SPECIFIC, ACCESS };

        [DataMember(Name = "addressType")]
        string addressTypeString
        {
            get { return addressType.ToString(); }
            set { addressType = (AddressType)Enum.Parse(typeof(AddressType), value); }
        }
        public AddressType addressType { get; set; }

        [DataMember]
        public string districtSubdivisionIdentifier { get; set; }

        [DataMember]
        public string exchangeArea { get; set; }

        [DataMember]
        public string floorId { get; set; }

        [DataMember]
        public string gksStreetNameForAddressing { get; set; }

        [DataMember]
        public string madId { get; set; }

        [DataMember]
        public string municipalityCode { get; set; }

        [DataMember]
        public string municipalityName { get; set; }

        [DataMember]
        public int objectOwner { get; set; }

        [DataMember]
        public string postArea { get; set; }
        
        [DataMember]
        public string postCode { get; set; }

        [DataMember]
        public string streetCode { get; set; }

        [DataMember]
        public string streetName { get; set; }

        [DataMember]
        public string streetNameForAddressing { get; set; }

        [DataMember]
        public string streetNumber { get; set; }

        [DataMember]
        public string suiteId { get; set; }

        [DataMember]
        public double x { get; set; }

        [DataMember]
        public double y { get; set; }
    }
}
