using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CustomerClosedCases.DataModel;

namespace TPCIP.CustomerClosedCases
{
    [ServiceContract]
    public interface ICustomerClosedCaseAgent
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/order/closedorders?lid={lid}")]
        List<ColumbusClosedCase> GetCustomerClosedCase(string lid);
    }
}
