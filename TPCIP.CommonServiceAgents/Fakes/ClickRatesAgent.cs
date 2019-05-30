using System;
using System.Collections.Generic;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CommonServiceAgents.Fakes
{
    public class ClickRatesAgent : IClickRatesAgent
    {
        public virtual ClickRateMessage TriggerClickEvent(string buttonName, string userId, string departmentName, string toolName)
        {
            return new ClickRateMessage()
            {
                message = "Click",
            };
        }
    }
}
