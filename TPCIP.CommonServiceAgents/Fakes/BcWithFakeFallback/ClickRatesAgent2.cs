using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CommonServiceAgents.Fakes.BcWithFakeFallback
{
    public class ClickRatesAgent2 : ClickRatesAgent
    {
        private readonly IClickRatesAgent _bcChannel;

        public ClickRatesAgent2(IClickRatesAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override ClickRateMessage TriggerClickEvent(string buttonName, string userId, string departmentName, string toolName)
        {
            try
            {
                return _bcChannel.TriggerClickEvent(buttonName, userId, departmentName, toolName);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.TriggerClickEvent(buttonName, userId, departmentName, toolName);
        }
    }
}
