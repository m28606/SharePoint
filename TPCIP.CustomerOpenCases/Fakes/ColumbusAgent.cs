using System;
using System.Collections.Generic;
using TPCIP.CustomerOpenCases.DataModel;

namespace TPCIP.CustomerOpenCases.Fakes
{
    public class ColumbusAgent : IColumbusAgent
    {
        public virtual ColumbusOrderDroolsResult GetColumbusOpenOrders(string sources, string lid)
        {
            if (lid == "EM120268")
            {
                throw new Exception();
            }

            var columbusOrder = new ColumbusOrder() { creationDate = 1473372000000, orderNumber = 100880470, orderType = "Oprettelse", orderStatus = "Bestilt" };
            return new ColumbusOrderDroolsResult { columbusOpenOrderList = new List<ColumbusOrder>() { columbusOrder } };
        }
    }
}
