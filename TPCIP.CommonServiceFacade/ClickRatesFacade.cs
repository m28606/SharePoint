using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonDataModel;
using TPCIP.CommonDomain;
using TPCIP.CommonServiceFacade.Mapping;
using TPCIP.ServiceLocatorInterfaces;
using TPCIP.ToolBox.User;

namespace TPCIP.CommonServiceFacade
{
    public class ClickRatesFacade
    {
        private readonly IClickRatesAgent _clickRatesAgent;

        public ClickRatesFacade(IServiceLocator serviceLocator)
        {
            _clickRatesAgent = serviceLocator.GetService<IClickRatesAgent>();
        }

        public void ButtonClickEventTrigerred(string buttonName, string toolName, string userId, string departmentName)
        {
            try
            {
                var message = _clickRatesAgent.TriggerClickEvent(buttonName, userId, departmentName, toolName);
            }
            catch { }
        }
    }
}
