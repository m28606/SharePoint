using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace TPCIP.PopUpFeedbackTab
{
    [ServiceContract]
    public interface IFasAgent
    {
        //from new North bound interface.
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fas/requeststatus?callback={callback}&requesttype={type}&daysback={daysBack}&queryby={queryBy}&queryvalue={val}&performlinecheck={performlinecheck}&checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a",
        BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        Stream requestStatusOnOpenClosedFault(string val, string queryBy, int daysBack, string type, string callback, bool performlinecheck);

    }
}
