using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [DataContract]
    public class ProductRelations
    {
        [DataMember]
        public List<AddOnProduct> addOnProducts { get; set; }
    }
}
