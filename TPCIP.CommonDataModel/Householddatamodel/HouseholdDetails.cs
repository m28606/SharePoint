using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel.Householddatamodel
{

    [Serializable]
    [DataContract]
    public class HouseholdDetails
    {
        [DataMember(Name = "categoryName")]
        public string groupName { get; set; }

        [DataMember(Name = "prodName")]
        public string product { get; set; }

        [DataMember(Name = "accNo")]
        public string accountNo { get; set; }

        [DataMember(Name = "price")]
        public string price { get; set; }

        [DataMember(Name = "upSale")]
        public bool? upSale { get; set; }

        [DataMember(Name = "subscriptionNo")]
        public string subscriptionId { get; set; }

        [DataMember(Name = "customerType")]
        public string customerType { get; set; }

        [DataMember(Name = "activeOrderPresent")]
        public Boolean isFutureProduct { get; set; }

         [DataMember]
        public String prodCode { get; set; }

        [DataMember]
        public List<ProductRelation> addOnProductList { get; set; } 
    }

    [DataContract]
    public class CustomerOverviewProducts
    {

        [DataMember(Name = "householdDetails")]
        public List<HouseholdDetails> HouseholdDetails { get; set; }

        [DataMember(Name = "message")]
        public string message { get; set; }

        [DataMember(Name = "youseeMoreCustomer")]
        public Boolean isYouseeMoreCustomer { get; set; }

        [DataMember(Name = "totalBenefitSlots")]
        public int totalBenefitSlots { get; set; }

        [DataMember(Name = "tvcategoryBenefitSubscribed")]
        public Boolean isTVCategoryBenefitSubscribed { get; set; }

        [DataMember(Name = "bbcategoryBenefitSubscribed")]
        public Boolean isBBCategoryBenefitSubscribed { get; set; }

        [DataMember(Name = "mobilCategoryBenefitSubscribed")]
        public Boolean isMobilCategoryBenefitSubscribed { get; set; }

        [DataMember(Name = "rootLID")]
        public string rootLID { get; set; }

        [DataMember(Name = "ysMoreOptIn")]
        public Boolean ysMoreOptIn { get; set; }

    }

    [DataContract]
    public class ProductRelation
    {
        [DataMember]
        public List<AddOnProduct> addOnProducts { get; set; }
    }

    [DataContract]
    public class AddOnProduct
    {
        [DataMember]
        public addOnProduct addOnProduct { get; set; }

    }
    
    [DataContract]
    public class addOnProduct
    {
        [DataMember]
        public String productCode { get; set; }

        [DataMember]
        public String productName { get; set; }

    }
    
}
