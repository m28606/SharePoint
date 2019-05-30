using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [DataContract]
    public class AddressSearchRequest
    {
        public enum AddressType { SPECIFIC, ACCESS };

        [DataMember(Name = "addressType")]
        string addressTypeString
        {
            get { return addressType.ToString(); }
            set { addressType = (AddressType)Enum.Parse(typeof(AddressType), value); }
        }
        public AddressType addressType { get; set; }

        //[DataMember]
        //public AddressType addressType { get; set; }

        [DataMember]
        public int max { get; set; }

        [DataMember]
        public string userName { get; set; }

        [DataMember]
        public List<AddressSearchParam> addressSearchParams { get; set; }
    }

    [DataContract]
    public class AddressSearchParam
    {
        public enum SearchKeyType
        {
            postCode, postArea, municipalityCode, municipalityName, streetCode, streetName, streetNameForAddressing,
            streetNumber, districtSubdivisionIdentifier, suiteId, floorId, exch, gksStreetNameForAddressing, objectOwner
        };

        [DataMember(Name = "key")]
        string keyString
        {
            get { return key.ToString(); }
            set { key = (SearchKeyType)Enum.Parse(typeof(SearchKeyType), value); }
        }
        public SearchKeyType key { get; set; }

        [DataMember]
        public bool phonetic { get; set; }

        [DataMember]
        public string value { get; set; }
    }
}
