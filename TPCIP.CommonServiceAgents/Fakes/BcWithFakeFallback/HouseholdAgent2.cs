using System;
using System.Collections.Generic;
using System.Diagnostics;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonDataModel.Householddatamodel;

namespace TPCIP.CommonServiceAgents.Fakes.BcWithFakeFallback
{
    public class HouseholdAgent2 : HouseholdAgent
    {
        private readonly IHouseholdAgent _bcChannel;

        public HouseholdAgent2(IHouseholdAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }      
   
        public override CustomerOverviewProducts GetCustomerOverviewDetails(string lid, long accountNo)
        {
            try
            {
                return _bcChannel.GetCustomerOverviewDetails(lid, accountNo);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetCustomerOverviewDetails(lid, accountNo);
        }

        public override List<ProductAttentionMarker> GetProductAttentionMarker(string lid)
        {
            try
            {
                return _bcChannel.GetProductAttentionMarker(lid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetProductAttentionMarker(lid);
        }

        public override SimpleResult<bool> GetSplitBillingDetails(string lid)
        {
            try
            {
                return _bcChannel.GetSplitBillingDetails(lid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetSplitBillingDetails(lid);
        }
    }
}
