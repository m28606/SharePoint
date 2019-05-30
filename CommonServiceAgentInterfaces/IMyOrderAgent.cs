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
    public interface IMyOrderAgent
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "fsm/person/{personId}/orders/ACTIVE/summary")]
        List<MyOrderData> GetMyOrders(string personId);
    }
}

