using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using TPCIP.ServiceLocatorInterfaces;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonDataModel;
using TPCIP.ToolBox.Portal;
using TPCIP.ToolBox;
using TPCIP.CommonDomain;

namespace TPCIP.CommonServiceFacade
{
    public class SubscriptionFacade
    {
        public const string KPM_NUMBER_PARAMCODE = "PA_INTERNET_NO";
        public const string MOBILE_NUMBER_LID_PARAMCODE = "PA_LID";
        public const string MOBILE_NUMBER_MSISDN_PARAMCODE = "MSISDN";

        public const string EMAILSTATUS_MAX_EMAIL_SIZE = "1.2GB";
        public const int EMAILSTATUS_MAX_EMAILS_IN_INBOX = 1000;

        private readonly ISubscriptionAgent _subscriptionAgent;

        public SubscriptionFacade(IServiceLocator serviceLocator)
        {
            //customerFacade = new CustomerFacade(serviceLocator);
            _subscriptionAgent = serviceLocator.GetService<ISubscriptionAgent>();

        }

        /// <summary>
        /// This is onload details of the associated user in context
        /// </summary>
        /// <param name="term">NA</param>
        /// <param name="searchType">NA</param>
        /// <param name="customerId">Not used</param>
        /// <returns>List of DkpCustomer</returns>
        // Changes made for EFT of 15448 for defect 15583
        // It call userprofile service with LID's in repsonse{key:value} where value is List<UserProfile>
        /// Send the Reset password to the user mobile (New Service from NET Company for ToolDkpCustomerAdmin)
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="mobileNo">Mobile Number</param>
        /// <returns></returns>

        public List<Subscription> searchSubscriptions(string subscriptionId)
        {
            string portal = PortalToolBox.GetPortalId().ToString();
            List<Subscription> subscriptionsData = null;

            try
            {
                if (subscriptionId.ToUpper() == "YC000000")
                {
                    subscriptionsData = CacheManager.Get<List<Subscription>>(CacheManager.CacheCategory.Subscription, subscriptionId + '_' + portal, () => createSubscriptionObject(subscriptionId), 20);
                }
                else
                {
                    subscriptionsData = _subscriptionAgent.searchSubscriptions(subscriptionId);
                }
            }
            catch (Exception ex)
            {
                if (subscriptionId.ToUpper().Substring(0, 2) == "YB" && portal == PortalMode.TSC.ToString())
                {
                    subscriptionsData = CacheManager.Get<List<Subscription>>(CacheManager.CacheCategory.Subscription, subscriptionId + '_' + portal, () => createSubscriptionObject(subscriptionId), 20);
                }

                else
                {
                    throw new FriendlyException("Ingen kunde fundet");
                }
            }

            foreach (var subscription in subscriptionsData)
            {
                if (subscription.rootLid == null)
                {
                    subscription.rootLid = subscription.subscriptionId;
                }
                //if (subscriptionId.ToUpper().Substring(0, 2) == "YB" && portal == PortalMode.TSC.ToString())
                //{
                //    subscription.rootLid = subscriptionId;
                //}
                if (subscription.parentAccountNo != null)
                {
                    // Pass the parentSegment as "PRIV" for MBilling users
                    if (subscription.parentAccountNo.ToString().Length == 6 || subscription.parentAccountNo.ToString().Length == 7)
                        subscription.parentSegment = "PRIV";
                }
            }
            //subscriptions.ToList().ForEach(n => n.parentSegment = (n.parentAccountNo.ToString().Length == 6 || n.parentAccountNo.ToString().Length == 6) ? "PRIV" : n.parentSegment);
            return subscriptionsData;
        }

        public static string GetMobilePhoneNr(Subscription subscription)
        {
            //Search for MSISDN, or PA_LID in product parameters. if not found search in related products parameters
            var result = GetProductParameter(subscription, MOBILE_NUMBER_MSISDN_PARAMCODE, searchInRelatedProducts: false)
                         ?? GetProductParameter(subscription, MOBILE_NUMBER_LID_PARAMCODE, searchInRelatedProducts: false)
                         ?? GetProductParameter(subscription, MOBILE_NUMBER_MSISDN_PARAMCODE, searchInRelatedProducts: true)
                         ?? GetProductParameter(subscription, MOBILE_NUMBER_LID_PARAMCODE, searchInRelatedProducts: true);
            return result;
        }
        private static string GetProductParameter(Subscription subscription, string paramCode, bool searchInRelatedProducts)
        {
            if (subscription.products != null)
            {
                var productParameters = subscription.products
                    .SelectMany(p => p.productParameters ?? new List<ProductParameter>());

                if (searchInRelatedProducts)
                {
                    var relatedProductParametes = subscription.products
                        .SelectMany(product => product.productRelations ?? new List<ProductRelations>())
                        .SelectMany(relation => relation.addOnProducts ?? new List<AddOnProduct>())
                        .SelectMany(a => a.addOnProduct.productParameters ?? new List<ProductParameter>());

                    productParameters = productParameters.Concat(relatedProductParametes);
                }

                var kpmNumber = productParameters
                    .Where(p => p.paramCode == paramCode)
                    .Select(p => p.paramValue)
                    .FirstOrDefault();

                return kpmNumber;
            }

            return null;
        }


        private List<Subscription> createSubscriptionObject(string lid)
        {
            var subs = new List<Subscription>
            {
                            new Subscription 
                        {
                            subscriptionId=lid,
                            rootLid=lid,
                            permissions=null,
                            parentAccountNo=0,
                            user = new Party() { firstName = "", lastName = "", customerNo = "", street = "", streetnumber = "", floor = "", floorside = "", city = "", zipCode = ""},   
                            parentSegment = "PRIV"
                        }
            };
            return subs;
        }
    }
}
