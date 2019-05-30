using System.Runtime.Serialization;
namespace TPCIP.CommonDataModel
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
    }
}
