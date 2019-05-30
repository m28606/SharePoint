using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CommonDataModel;
using TPCIP.CommonDataModel.Householddatamodel;

namespace TPCIP.CommonServiceAgentInterfaces
{
    [ServiceContract]
    public interface IHouseholdAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "customer/lid/{lid}/customeroverview?accountNo={accountNo}")]
        CustomerOverviewProducts GetCustomerOverviewDetails(string lid, long accountNo);

        [OperationContract]
        [WebGet(UriTemplate = "attentionmarker/{lid}")]
        List<ProductAttentionMarker> GetProductAttentionMarker(string lid);

        [OperationContract]
        [WebGet(UriTemplate = "subscription/{subscriptionId}/splitbilling")]
        SimpleResult<bool> GetSplitBillingDetails(string subscriptionId);
      
    }
}
