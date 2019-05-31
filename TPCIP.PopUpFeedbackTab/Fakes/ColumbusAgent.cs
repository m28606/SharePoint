using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.PopUpFeedbackTab.DataModel;
using TPCIP.PopUpFeedbackTab.Domain;

namespace TPCIP.PopUpFeedbackTab.Fakes
{
    public class ColumbusAgent : IColumbusAgent
    {
        public virtual ColumbusOrderDroolsResult GetColumbusOpenOrders(string sources, string lid)
        {
            if (lid == "EM120268")
            {
                throw new Exception();
            }
            else
            {
                var columbusOrder = new ColumbusOrder() { creationDate = 1473372000000, orderNumber = 100880470, orderType = "Oprettelse", orderStatus = "Bestilt" };
                return new ColumbusOrderDroolsResult { columbusOpenOrderList = new List<ColumbusOrder>() { columbusOrder } };
            }
        }
    }
}
