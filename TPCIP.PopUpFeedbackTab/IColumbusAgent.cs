using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using TPCIP.PopUpFeedbackTab.DataModel;

namespace TPCIP.PopUpFeedbackTab
{
    [ServiceContract]
    public interface IColumbusAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "customer/diagnose?source={sources}&lid={lid}")]
        ColumbusOrderDroolsResult GetColumbusOpenOrders(string sources, string lid);
    }
}
