using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CommonDataModel;

namespace TPCIP.CommonServiceAgentInterfaces
{
    [ServiceContract]
    public interface ICustomerDataAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "customerdata?lid={lid}&accountno={accountno}&madid={madid}")]
        List<EmergencySubscription> getEmergencyCustomerInformation(string lid, string accountno, string madid);
    }
}
