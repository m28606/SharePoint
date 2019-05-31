using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CustomerOpenCases.DataModel;

namespace TPCIP.CustomerOpenCases
{
    [ServiceContract]
    public interface IEtrayAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "etray/{lid}")]
        ETrayDocument[] GetDocuments(string lid);
    }
}
