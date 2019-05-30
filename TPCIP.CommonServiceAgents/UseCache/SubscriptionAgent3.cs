using System;
using System.Collections.Generic;
using System.Threading;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonServiceFacade;
using TPCIP.ToolBox.Portal;


namespace TPCIP.CommonServiceAgents.UseCache
{
    public class SubscriptionAgent3 : ISubscriptionAgent
    {
        private readonly ISubscriptionAgent _bcChannel;

        public SubscriptionAgent3(ISubscriptionAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public List<Subscription> searchSubscriptions(string subscriptionId)
        {
            var portalId = GetCurrentPortalId();
            List<Subscription> subscription = CacheManager.Get<List<Subscription>>(CacheManager.CacheCategory.Subscription, subscriptionId + '_' + portalId, () => _bcChannel.searchSubscriptions(subscriptionId), 20);
            return subscription;
        }

        private string GetCurrentPortalId()
        {
            var portalId = PortalToolBox.GetPortalId().ToString();
            return portalId;
        }

        public List<Subscription> searchSubscriptionsByAccountNo(string accountNo)
        {
            return _bcChannel.searchSubscriptionsByAccountNo(accountNo);
        }

        public List<Subscription> searchSpecificSubscriptions(string subscriptionId, string accountNo)
        {
            return _bcChannel.searchSubscriptionsByAccountNo(accountNo);
        }

        public SimpleResult<string> getAccountType(string accountNo)
        {
            return _bcChannel.getAccountType(accountNo);
        }
    }
}
