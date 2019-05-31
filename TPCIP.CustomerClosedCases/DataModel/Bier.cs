using System;
using System.Runtime.Serialization;

namespace TPCIP.CustomerClosedCases.DataModel
{
    [DataContract]
    public class BierParameter
    {
        [DataMember]
        public string search_type { get; set; }

        [DataMember]
        public string kundenummer { get; set; }

        [DataMember]
        public string anlaeg { get; set; }

        [DataMember]
        public string anlaegsnummer { get; set; }
    }

    [Serializable]
    [DataContract]
    public class BierDocument
    {
        public DateTime modtaget { get; set; }

        [DataMember(Name = "modtaget")]
        string receivedString
        {
            get
            {
                return modtaget.ToString();
            }
            set
            {
                modtaget = DateTime.Parse(value);
            }
        }

        [DataMember]
        public string aktivitet { get; set; }

        [DataMember]
        public string bierstatus { get; set; }

        public DateTime tid_oprettet { get; set; }

        [DataMember(Name = "tid_oprettet")]
        string createdString
        {
            get
            {
                return tid_oprettet.ToString();
            }
            set
            {
                tid_oprettet = DateTime.Parse(value);
            }
        }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string status { get; set; }
    }
}
