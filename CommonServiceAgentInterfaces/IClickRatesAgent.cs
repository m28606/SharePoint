using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CommonDataModel;

namespace TPCIP.CommonServiceAgentInterfaces
{
    [ServiceContract]
    public interface IClickRatesAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "/addclick?button={buttonName}&userid={userId}&department={departmentName}&tool={toolName}")]
        ClickRateMessage TriggerClickEvent(string buttonName, string userId, string departmentName, string toolName);
    }
}
