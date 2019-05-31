using System;
using System.Collections.Generic;
using System.Linq;
using TPCIP.CommonDataModel;
using TPCIP.CommonDataModel.Householddatamodel;
using TPCIP.CommonDomain;

namespace TPCIP.CommonServiceFacade
{
    public static class CustomerOverviewMapper
    {
        public static CustomerDetails MapProductParameters(CustomerOverviewProducts productParameters, List<TPCIP.CommonServiceFacade.HouseholdFacade.AddonProductDetailsFlag> addonProdSpList)
        {
            var ProductParameters = new CustomerDetails();
            var addonProdCodeList = new List<string>();
            addonProdCodeList = addonProdSpList != null ? addonProdSpList.Select(m => m.prodCode).ToList() : null;
            if (productParameters != null)
            {
                ProductParameters.ErrorMessage = string.IsNullOrEmpty(productParameters.message) ? string.Empty : productParameters.message;
                ProductParameters.CustomerOverviewDetails = productParameters.HouseholdDetails != null ?
                productParameters.HouseholdDetails.Select(c =>
                {
                    var customerOverview = new CustomerOverviewDetails()
                    {
                        Category = string.IsNullOrEmpty(c.groupName) ? string.Empty : c.groupName.ToUpper(),
                        ProductName = string.IsNullOrEmpty(c.product) ? string.Empty : c.product,
                        CustomerId = string.IsNullOrEmpty(c.subscriptionId) ? string.Empty : c.subscriptionId,
                        AccountNo = string.IsNullOrEmpty(c.accountNo) ? string.Empty : c.accountNo,
                        IsFutureProduct = c.isFutureProduct,
                        AddOnProdList = c.addOnProductList != null ? c.addOnProductList.Where(m => m.addOnProducts != null).SelectMany(n => n.addOnProducts).Where(x => x.addOnProduct != null).Select(p => p.addOnProduct).Select(t => new AdditionalProductList()
                        {
                            prodCode = addonProdCodeList != null ? (addonProdCodeList.Contains(t.productCode) ? t.productCode : "") : "",
                            prodName = t.productName,
                            IsAttentionMarker = addonProdSpList != null ? (addonProdSpList.Count(m => m.prodCode == t.productCode) > 0 ? addonProdSpList.First(m => m.prodCode == t.productCode).AttentionMrakerFlag.ToString() : "") : "",
                            AttentionMarkerName = "Tyverispærring",
                        }).OrderBy(z => z.IsAttentionMarker).ToList() : new List<AdditionalProductList>(),
                    };
                 
                    MapCategoryDetails(ref customerOverview, c);
                    return customerOverview;
                }).OrderBy(o => o.Priority).ToList() : new List<CustomerOverviewDetails>();

                if (productParameters.HouseholdDetails != null && (string.IsNullOrEmpty(productParameters.message) && productParameters.HouseholdDetails.Count <= 0))
                {
                    ProductParameters.ErrorMessage = "Indlæsning af kundens produkter fejlede - tjek eOrdre/CU";
                }
            }
            else
            {
                ProductParameters.ErrorMessage = "Indlæsning af kundens produkter fejlede - tjek eOrdre/CU";
            }


            return ProductParameters;
        }

        private static void MapCategoryDetails(ref CustomerOverviewDetails customerOverviewDetails, HouseholdDetails houseHoldDetails)
        {
            if (!string.IsNullOrEmpty(houseHoldDetails.groupName))
            {
                customerOverviewDetails.CategoryIcon = houseHoldDetails.groupName.ToUpper().Equals("PAKKER") ? "../../../_Layouts/Images/TPCIP.Web/icons/Packaged products.png" : houseHoldDetails.groupName.ToUpper().Equals("TV") ? "../../../_Layouts/Images/TPCIP.Web/icons/TV.png" : houseHoldDetails.groupName.ToUpper().Equals("MOBIL TALE") ? "../../../_Layouts/Images/TPCIP.Web/icons/Mobil_Tale.png" : houseHoldDetails.groupName.ToUpper().Equals("MBILLING") ? "../../../_Layouts/Images/TPCIP.Web/icons/Mobil_Tale.png" : houseHoldDetails.groupName.ToUpper().Equals("BREDBAND") ? "../../../_Layouts/Images/TPCIP.Web/icons/bredband.png" : houseHoldDetails.groupName.ToUpper().Equals("MOBIL BREDBAND") ? "../../../_Layouts/Images/TPCIP.Web/icons/Simcard.png" : houseHoldDetails.groupName.ToUpper().Equals("FASTNET") ? "../../../_Layouts/Images/TPCIP.Web/icons/Fastnet1.png" : houseHoldDetails.groupName.ToUpper().Equals("MOBIL DATA") ? "../../../_Layouts/Images/TPCIP.Web/icons/Simcard.png" : houseHoldDetails.groupName.ToUpper().Equals("MAIL") ? "../../../_Layouts/Images/TPCIP.Web/icons/E-mail Product.png" : string.Empty;
                customerOverviewDetails.Priority = houseHoldDetails.groupName.ToUpper().Equals("PAKKER") ? 1 : houseHoldDetails.groupName.ToUpper().Equals("TV") ? 2 : houseHoldDetails.groupName.ToUpper().Equals("BREDBAND") ? 3 : houseHoldDetails.groupName.ToUpper().Equals("FASTNET") ? 4 : houseHoldDetails.groupName.ToUpper().Equals("MOBIL TALE") ? 5 : houseHoldDetails.groupName.ToUpper().Equals("MOBIL BREDBAND") ? 6 : houseHoldDetails.groupName.ToUpper().Equals("MBILLING") ? 7 : houseHoldDetails.groupName.ToUpper().Equals("MOBIL DATA") ? 8 : 9;
            }
            else
            {
                customerOverviewDetails.CategoryIcon = string.Empty;
                customerOverviewDetails.Priority = 8;
            }
        }

        public static void MapYouseeMoreBenefit(ref CustomerDetails customerOverviewDetails, CustomerOverviewProducts productParameters)
        {

            customerOverviewDetails.IsYouseeMoreCustomer = productParameters.isYouseeMoreCustomer;
            customerOverviewDetails.YSMoreOptIn = productParameters.ysMoreOptIn;
            customerOverviewDetails.IsVisible = DateTime.Now >= new DateTime(2017, 9, 25);
            //customerOverviewDetails.IsVisible = true;
            customerOverviewDetails.CategoryBenefits = new List<CategoryBenefits>();
            customerOverviewDetails.CategoryBenefits.Add(new CategoryBenefits { CategoryName = "TV", HasYouseeBenefit = productParameters.isTVCategoryBenefitSubscribed });
            customerOverviewDetails.CategoryBenefits.Add(new CategoryBenefits { CategoryName = "BB", HasYouseeBenefit = productParameters.isBBCategoryBenefitSubscribed });
            customerOverviewDetails.CategoryBenefits.Add(new CategoryBenefits { CategoryName = "Mobil", HasYouseeBenefit = productParameters.isMobilCategoryBenefitSubscribed });

        }

        public static void MapProductAttentionMarker(ref CustomerDetails customerOverviewDetails, List<ProductAttentionMarker> productAttentionMarkers)
        {
            foreach (var productDetail in customerOverviewDetails.CustomerOverviewDetails)
            {
                var productAttentionMarker = productAttentionMarkers.FirstOrDefault(m => m.productName == productDetail.ProductName && m.subscriptionNo == productDetail.CustomerId);

                productDetail.ProductAttentionMarker = new List<AttentionMarker>
                {
                    new AttentionMarker()
                    {
                        AttentionMarkerName = "Tyverispærring",
                        IsAMExists = productAttentionMarker != null && productAttentionMarker.isTheftLocked,
                        Subtext = string.Empty,
                    }
                };

            }

        }

    }
}
