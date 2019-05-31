using System;
using System.Runtime.Serialization;

namespace TPCIP.CustomerClosedCases.DataModel
{
    [Serializable]
    [DataContract]
    public class ETrayDocument
    {
        public DateTime created { get; set; }

        [DataMember(Name = "created")]
        string createdString
        {
            get
            {
                return created.ToString();
            }
            set
            {
                created = DateTime.Parse(value);
            }
        }

        [DataMember]
        public string docId { get; set; }

        [DataMember]
        public string linkUrl { get; set; }

        [DataMember]
        public string note { get; set; }
    }
}
