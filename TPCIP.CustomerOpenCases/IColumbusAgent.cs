using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CustomerOpenCases.DataModel;

namespace TPCIP.CustomerOpenCases
{
    [ServiceContract]
    public interface IColumbusAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "customer/diagnose?source={sources}&lid={lid}")]
        ColumbusOrderDroolsResult GetColumbusOpenOrders(string sources, string lid);
    }
}
