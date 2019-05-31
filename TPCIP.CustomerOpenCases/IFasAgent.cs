using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;

namespace TPCIP.CustomerOpenCases
{
    [ServiceContract]
    public interface IFasAgent
    {
        //from new North bound interface.
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fas/requeststatus?callback={callback}&requesttype={type}&daysback={daysBack}&queryby={queryBy}&queryvalue={val}&performlinecheck={performlinecheck}&checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a",
        BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        Stream RequestStatusOnOpenClosedFault(string val, string queryBy, int daysBack, string type, string callback, bool performlinecheck);
    }
}
