using System;
using System.Collections.Generic;
using System.Diagnostics;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonServiceAgents;

namespace TPCIP.CommonServiceAgents.Fakes.BcWithFakeFallback
{
    public class SubscriptionAgent2 : SubscriptionAgent
    {
        private readonly ISubscriptionAgent _bcChannel;

        public SubscriptionAgent2(ISubscriptionAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override List<Subscription> searchSubscriptions(string subscriptionId)
        {
            try
            {
                return _bcChannel.searchSubscriptions(subscriptionId);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.searchSubscriptions(subscriptionId);
        }

        public override List<Subscription> searchSubscriptionsByAccountNo(string accountNo)
        {
            try
            {
                return _bcChannel.searchSubscriptionsByAccountNo(accountNo);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.searchSubscriptionsByAccountNo(accountNo);
        }
        public override List<Subscription> searchSpecificSubscriptions(string subscriptionId, string accountNo)
        {
            try
            {
                return _bcChannel.searchSpecificSubscriptions(subscriptionId, accountNo);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.searchSpecificSubscriptions(subscriptionId, accountNo);
        }

        public override SimpleResult<string> getAccountType(string accountNo)
        {
            try
            {
                return _bcChannel.getAccountType(accountNo);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getAccountType(accountNo);
        }

    }
}
