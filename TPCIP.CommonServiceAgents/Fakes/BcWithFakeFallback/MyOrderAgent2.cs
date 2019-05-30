using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CommonServiceAgents.Fakes.BcWithFakeFallback
{
    public class MyOrderAgent2 : MyOrderAgent
    {
       private readonly IMyOrderAgent _bcChannel;

         public MyOrderAgent2(IMyOrderAgent bcChannel)
           {
            _bcChannel = bcChannel; 
           }
         public override List<MyOrderData> GetMyOrders(string userId)
           {
            try
            {
                return _bcChannel.GetMyOrders(userId);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetMyOrders(userId);
        }
    }
}