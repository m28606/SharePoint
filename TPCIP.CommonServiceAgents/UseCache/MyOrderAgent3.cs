using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceFacade;

namespace TPCIP.CommonServiceAgents.UseCache
{
    public class MyOrderAgent3 : IMyOrderAgent
    {
        private readonly IMyOrderAgent _bcChannel;

        public MyOrderAgent3(IMyOrderAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }
        public List<MyOrderData> GetMyOrders(string userId)
        {
            var myActiveOrders = CacheManager.Get<List<MyOrderData>>(CacheManager.CacheCategory.Orders, userId, () => _bcChannel.GetMyOrders(userId), 5);
            return myActiveOrders;
        }
    }
}
