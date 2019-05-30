using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPCIP.CommonDomain
{
    public class CustomerOverviewDetails
    {
        public string Category { get; set; }

        public string ProductName { get; set; }

        public string CustomerId { get; set; }

        public string AccountNo { get; set; }

        public string CategoryIcon { get; set; }

        public int Priority { get; set; }

        public Boolean IsFutureProduct { get; set; }

        public int CountForAM { get; set; }

        public List<AdditionalProductList> AddOnProdList { get; set; }

        public List<AttentionMarker> ProductAttentionMarker { get; set; }
    }

    public class AdditionalProductList
    {
        public String prodCode { get; set; }

        public String prodName { get; set; }

        public String IsAttentionMarker { get; set; }

        public String AttentionMarkerName { get; set; }

    }

    public class CustomerDetails
    {
        public List<CustomerOverviewDetails> CustomerOverviewDetails { get; set; }
        public List<CustomerOverviewDetails> FutureCustomerOverviewDetails { get; set; }
        public List<string> MobileNumbersList { get; set; }
        public string ErrorMessage { get; set; }
        public Boolean IsYouseeMoreCustomer { get; set; }
        public Boolean IsVisible { get; set; }
        public List<CategoryBenefits> CategoryBenefits { get; set; }
        public int CountForFutureProduct { get; set; }
        public int CountForExistingProduct { get; set; }
        public Boolean YSMoreOptIn { get; set; }

    }

    public class CategoryBenefits
    {
        public string CategoryName { get; set; }
        public Boolean HasYouseeBenefit { get; set; }
    }
}
