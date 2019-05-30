using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TPCIP.CommonDataModel;

namespace TPCIP.CommonServiceAgentInterfaces
{
    [ServiceContract]
    public partial interface IPaymentAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "account/{accountNo}")]
        SubscribeAutoCard GetAutoCardSubscribtionDetails(string accountNo);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "account/{accountNo}/deleteAutomaticCardPayment")]
        SimpleResult<string> DeleteAutomaticCardPayment(string accountNo);
    }
}
