using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CommonDataModel;

namespace TPCIP.CommonServiceAgentInterfaces
{
    [ServiceContract]
    public interface IPulseAgent
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/pulsdetails/user/{mid}")]
        List<PulseDetails> GetDepartmentDetails(string mid);
    }
}
