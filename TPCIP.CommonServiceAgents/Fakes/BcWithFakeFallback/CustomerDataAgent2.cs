using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonServiceAgents;


namespace TPCIP.CommonServiceAgents.Fakes.BcWithFakeFallback
{
  public  class CustomerDataAgent2:CustomerDataAgent
    {
        private readonly ICustomerDataAgent _bcChannel;

        public CustomerDataAgent2(ICustomerDataAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }
        public override List<EmergencySubscription> getEmergencyCustomerInformation(string subscriptionId, string accountNo, string madId)
        {
            try
            {
                return _bcChannel.getEmergencyCustomerInformation(subscriptionId, accountNo, madId);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getEmergencyCustomerInformation(subscriptionId, accountNo, madId);
        }
    }
}
