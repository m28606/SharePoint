using System.Collections.Generic;
using System.Linq;
using TPCIP.ServiceLocatorInterfaces;
using TPCIP.CommonDataModel;
using TPCIP.ToolBox.Constants;

namespace TPCIP.CustomerOpenCases
{
    public class BillingFacade
    {
       // private readonly ISubscriptionAgent _subscriptionAgent;
        private readonly ISettings _settings;

        public BillingFacade(IServiceLocator serviceLocator)
        {
           // _subscriptionAgent = serviceLocator.GetService<ISubscriptionAgent>();
            _settings = serviceLocator.Settings;
        }


        public List<string> ExtractUniqueSubscriptionIds(List<Subscription> subscription)
        {
            List<string> lidPhoneNumbers = null;
            foreach (var sub in subscription)
            {
                if (sub.products != null)
                {
                    var productParameters = sub.products
                        .Select(p => new { p.productParameters, p.productCode });


                    var relatedProductParametes = sub.products
                            .SelectMany(product => product.productRelations ?? new List<ProductRelations>())
                            .SelectMany(relation => relation.addOnProducts ?? new List<AddOnProduct>())
                            .Select(a => new { a.addOnProduct.productParameters, a.addOnProduct.productCode });

                    productParameters = productParameters.Concat(relatedProductParametes);

                    //To make fake work
                    productParameters = productParameters.Where(m => m.productParameters != null);

                    //Get paramValue having paramCode as MSISDN
                    var msisdnParam = productParameters.SelectMany(p => p.productParameters.Where(m => m.paramCode == UserConstants.MOBILE_NUMBER_MSISDN_PARAMCODE).
                        Select(m => m.paramValue));

                    //Get paramValue having paramCode as PA_LID, also check if it needs to be set selected
                    var paLidParam = productParameters.SelectMany(p => p.productParameters.
                        Where(m => m.paramCode == UserConstants.MOBILE_NUMBER_PA_LID_PARAMCODE).
                       Select(m => m.paramValue));

                    //select only Unique values
                    if (lidPhoneNumbers == null)
                        lidPhoneNumbers = new List<string>();

                    if (msisdnParam != null && paLidParam != null)
                        lidPhoneNumbers.AddRange(msisdnParam.Concat(paLidParam).Distinct().ToList());
                    else if (msisdnParam != null)
                        lidPhoneNumbers.AddRange(paLidParam.Distinct().ToList());
                    else if (paLidParam != null)
                        lidPhoneNumbers.AddRange(msisdnParam.Distinct().ToList());

                }
            }
            if (lidPhoneNumbers != null)
                return lidPhoneNumbers.Distinct().ToList();

            return lidPhoneNumbers;
        }

    }
}
