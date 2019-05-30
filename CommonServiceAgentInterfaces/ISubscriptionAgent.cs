using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CommonDataModel;

namespace TPCIP.CommonServiceAgentInterfaces
{
    [ServiceContract]
    public partial interface ISubscriptionAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "?subscriptionId={subscriptionId}")]
        List<Subscription> searchSubscriptions(string subscriptionId);

        [OperationContract]
        [WebGet(UriTemplate = "?accountNo={accountNo}")]
        List<Subscription> searchSubscriptionsByAccountNo(string accountNo);

        [OperationContract]
        [WebGet(UriTemplate = "?subscriptionId={subscriptionId}&accountNo={accountNo}")]
        List<Subscription> searchSpecificSubscriptions(string subscriptionId, string accountNo);

        [OperationContract]
        [WebGet(UriTemplate = "accounttype/{accountNo}")]
        SimpleResult<string> getAccountType(string accountNo);
    
    }

}
