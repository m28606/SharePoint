using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [DataContract]
    public class SimpleResult<T>
    {
        [DataMember(IsRequired = true)]
        public T value { get; set; }
    }
} 