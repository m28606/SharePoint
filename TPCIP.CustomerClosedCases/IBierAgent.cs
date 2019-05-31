using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CustomerClosedCases.DataModel;

namespace TPCIP.CustomerClosedCases
{
    [ServiceContract]
    public interface IBierAgent
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/activity/search")]
        BierDocument[] GetBierPo(BierParameter param);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/fejl/search")]
        BierDocument[] GetBierTt(BierParameter param);
    }
}
