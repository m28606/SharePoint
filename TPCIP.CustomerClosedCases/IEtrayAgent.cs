using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CustomerClosedCases.DataModel;

namespace TPCIP.CustomerClosedCases
{
    [ServiceContract]
    public interface IEtrayAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "etray/{lid}/archived")]
        ETrayDocument[] GetETrayClosedCases(string lid);

    }
}
