using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CustomerOpenCases.DataModel;

namespace TPCIP.CustomerOpenCases
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
