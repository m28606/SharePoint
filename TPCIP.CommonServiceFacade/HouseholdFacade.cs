using System;
using System.Collections.Generic;
using System.Linq;
using ISubscriptionAgent = TPCIP.CommonServiceAgentInterfaces.ISubscriptionAgent;
using TPCIP.ServiceLocatorInterfaces;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceFacade;
using Microsoft.SharePoint;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonDomain;
using TPCIP.CommonDataModel.Householddatamodel;

namespace TPCIP.CommonServiceFacade
{
    public class HouseholdFacade
    {
        private readonly IHouseholdAgent _householdAgent;
        private readonly ISubscriptionAgent _subscriptionAgent;
        SubscriptionFacade subscriptionFacade;

        public HouseholdFacade(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }
            _householdAgent = serviceLocator.GetService<IHouseholdAgent>();
            _subscriptionAgent = serviceLocator.GetService<ISubscriptionAgent>();
            subscriptionFacade = new SubscriptionFacade(serviceLocator);
        }


        public List<string> GetMobileNumbers(string customerId, string accountNumber)
        {
            List<string> mobileNumbers = new List<string>();
            var phoneNumbersSubscription = "";
            if (string.IsNullOrEmpty(accountNumber))
            {
                var subscription = subscriptionFacade.searchSubscriptions(customerId).FirstOrDefault(); ;
                accountNumber = Convert.ToString(subscription.parentAccountNo);
                phoneNumbersSubscription = subscription.rootLid;
            }

            CustomerOverviewProducts CustomerOverviewDetails = new CustomerOverviewProducts();
            CustomerOverviewDetails = CheckYouseeLid(customerId) ? GetTdcCustomerOverviewDetails(customerId, accountNumber) : GetYsCustomerOverviewDetails(customerId);
            mobileNumbers = CustomerOverviewDetails.HouseholdDetails.Where(x => ((x.subscriptionId != null || x.subscriptionId != "") && (x.subscriptionId.Length == 8 && x.subscriptionId.All(char.IsDigit)))).Select(c => c.subscriptionId).ToList();
            mobileNumbers.Add(phoneNumbersSubscription);
            mobileNumbers = mobileNumbers.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
            mobileNumbers.Distinct();
            return mobileNumbers;
        }

        #region CustomerOverview

        public CustomerDetails GetProductDetails(string customerId, string accountNumber)
        {
             var phoneNumbersSubscription="" ;
            if (string.IsNullOrEmpty(accountNumber))
            {
                var subscription = subscriptionFacade.searchSubscriptions(customerId).FirstOrDefault(); ;
                accountNumber = Convert.ToString(subscription.parentAccountNo);
                phoneNumbersSubscription = subscription.rootLid;
            }

            CustomerOverviewProducts CustomerOverviewDetails = new CustomerOverviewProducts();
            CustomerOverviewDetails = CheckYouseeLid(customerId) ? GetTdcCustomerOverviewDetails(customerId, accountNumber) : GetYsCustomerOverviewDetails(customerId);

            List<AddonProductDetailsFlag> AddonProdCodeList = new List<AddonProductDetailsFlag>();
            AddonProdCodeList = GetAddonProdTemplateFromSPList();

            var customerOverviewDetails = CustomerOverviewMapper.MapProductParameters(CustomerOverviewDetails, AddonProdCodeList);

            if (!string.IsNullOrEmpty(customerOverviewDetails.ErrorMessage) ||
                customerOverviewDetails.CustomerOverviewDetails.Count <= 0) return customerOverviewDetails;
            CustomerOverviewMapper.MapYouseeMoreBenefit(ref customerOverviewDetails, CustomerOverviewDetails);

            //GetAttentionMarkerDetails(ref customerOverviewDetails, CustomerOverviewDetails);

            //GetSplitBillingDetails(ref customerOverviewDetails);
            var customerDetails = customerOverviewDetails;

            customerOverviewDetails.MobileNumbersList = customerDetails.CustomerOverviewDetails.Where(m => m.CustomerId != "").Select(m => m.CustomerId).ToList();
            customerOverviewDetails.FutureCustomerOverviewDetails = customerDetails.CustomerOverviewDetails.Where(m => m.IsFutureProduct == true).ToList();
            customerOverviewDetails.CustomerOverviewDetails = customerDetails.CustomerOverviewDetails.Where(m => m.IsFutureProduct == false).ToList();
            customerOverviewDetails.CountForFutureProduct = customerOverviewDetails.FutureCustomerOverviewDetails.Count();
            customerOverviewDetails.CountForExistingProduct = customerOverviewDetails.CustomerOverviewDetails.Count();

            customerOverviewDetails.MobileNumbersList.Add(phoneNumbersSubscription);
            customerOverviewDetails.MobileNumbersList.Distinct();

            return customerOverviewDetails;
        }

        public class AddonProductDetailsFlag
        {
            public string prodCode;
            public string AttentionMrakerFlag;
        }

        private List<AddonProductDetailsFlag> GetAddonProdTemplateFromSPList()
        {

            List<AddonProductDetailsFlag> AddonProdTemplateList = new List<AddonProductDetailsFlag>();

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        SPList templateDetails = oWeb.Lists.TryGetList("CustomerOverviewAdminList");
                        if (templateDetails != null)
                        {
                            SPQuery query = new SPQuery();
                            query.Query = "";
                            SPListItemCollection items = templateDetails.GetItems(query);

                            if (items.Count > 0)
                            {
                                foreach (SPListItem item in items)
                                {
                                    var prodCode = item["Title"].ToString();
                                    var AMFlag = item["OBSRemark"].ToString();
                                    AddonProdTemplateList.Add(
                                        new AddonProductDetailsFlag()
                                        {
                                            prodCode = prodCode,
                                            AttentionMrakerFlag = AMFlag
                                        });
                                }
                            }
                        }
                    }
                }
            });

            return AddonProdTemplateList;
        }

        private void GetSplitBillingDetails(ref CustomerDetails customerOverviewDetails)
        {

            foreach (var productDetail in customerOverviewDetails.CustomerOverviewDetails)
            {
                SimpleResult<bool> isSplitBill = new SimpleResult<bool>();
                try
                {
                    isSplitBill = _householdAgent.GetSplitBillingDetails(productDetail.CustomerId);
                }
                catch
                { }
                productDetail.ProductAttentionMarker.Add(new AttentionMarker()
                {
                    IsAMExists = isSplitBill.value,
                    AttentionMarkerName = "Delt regning",
                    Subtext = string.Empty,

                });
                productDetail.CountForAM = productDetail.ProductAttentionMarker.Count(m => m.IsAMExists == true);
            }

        }

        private void GetAttentionMarkerDetails(ref CustomerDetails customerOverviewDetails, CustomerOverviewProducts CustomerOverviewDetails)
        {
            try
            {
                var productAttentionMarker = _householdAgent.GetProductAttentionMarker(CustomerOverviewDetails.rootLID);
                if (productAttentionMarker != null && productAttentionMarker.Count > 0)
                {
                    CustomerOverviewMapper.MapProductAttentionMarker(ref customerOverviewDetails, productAttentionMarker);
                }
                else
                {
                    SetDefaultAttentionMarker(ref customerOverviewDetails);
                }

            }
            catch (Exception)
            {
                SetDefaultAttentionMarker(ref customerOverviewDetails);
            }

        }

        private static void SetDefaultAttentionMarker(ref CustomerDetails customerOverviewDetails)
        {
            foreach (var productDetail in customerOverviewDetails.CustomerOverviewDetails)
            {
                productDetail.ProductAttentionMarker = new List<AttentionMarker>
                {
                    new AttentionMarker()
                {
                    AttentionMarkerName = "Tyverispærring",
                    IsAMExists = false,
                    Subtext = string.Empty,
            }
                };
            }
        }

        private CustomerOverviewProducts GetTdcCustomerOverviewDetails(string lid, string accountNumber)
        {
            CustomerOverviewProducts customerOverviewDetails = new CustomerOverviewProducts();
            customerOverviewDetails = GetCustomerOverviewDetails(lid, Convert.ToInt64(accountNumber));
            customerOverviewDetails.rootLID = lid;
            return customerOverviewDetails;
        }

        private CustomerOverviewProducts GetYsCustomerOverviewDetails(string lid)
        {
            CustomerOverviewProducts customerOverviewDetails = new CustomerOverviewProducts();
            try
            {
                var subscription = _subscriptionAgent.searchSubscriptionsByAccountNo(lid).FirstOrDefault();
                if (subscription == null)
                {

                    customerOverviewDetails.message = "Indlæsning af kundens produkter fejlede - tjek eOrdre/CU";
                }
                else
                {
                    customerOverviewDetails = GetCustomerOverviewDetails(subscription.rootLid, subscription.parentAccountNo);
                    customerOverviewDetails.rootLID = subscription.rootLid;
                }
            }
            catch
            {
                customerOverviewDetails.message = "Øv, en uventet fejl betyder indholdet ikke kan vises";
            }
            return customerOverviewDetails;
        }

        private CustomerOverviewProducts GetCustomerOverviewDetails(string lid, long accountNo)
        {
            CustomerOverviewProducts customerOverviewDetails = new CustomerOverviewProducts();
            try
            {

                customerOverviewDetails = CacheManager.Get<CustomerOverviewProducts>(CacheManager.CacheCategory.CustomerOverviewDetails, "#customeroverviewdetails"+'_'+lid, () => _householdAgent.GetCustomerOverviewDetails(lid, accountNo), 1440);

            }
            catch
            {
                customerOverviewDetails.message = "Øv, en uventet fejl betyder indholdet ikke kan vises";
            }
            return customerOverviewDetails;
        }

        private bool CheckYouseeLid(string lid)
        {
            if (lid.StartsWith("6") && lid.Length >= 9)
                return false;

            return true;
        }
        #endregion
    }
}
