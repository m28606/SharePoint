using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Fakes = TPCIP.PortalUserGuides.Fakes;
using BcWithFakeFallback = TPCIP.PortalUserGuides.Fakes.BcWithFakeFallback;
using TPCIP.PortalUserGuides;

namespace TPCIP.PortalUserGuides
{
    class ServiceLocatorPortalUserGuides
    {
        public ServiceLocator.ServiceLocator objServiceLocator;
        public ServiceLocatorPortalUserGuides()
        {
            var settings = new ServiceLocator.Settings();
            objServiceLocator = new ServiceLocator.ServiceLocator(settings.BusinessCoreUrl);
            objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(ICustomerAgent), "customer");

            objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<ICustomerAgent>());

            string useFakes = HttpContext.Current.Request.QueryString["usefakes"];
            if (useFakes == "1" || useFakes == "true")
            {
                objServiceLocator.Map<ICustomerAgent>(new Fakes.CustomerAgent());

            }
            else if (useFakes == "2")
            {
                objServiceLocator.Map<ICustomerAgent>(new BcWithFakeFallback.CustomerAgent2(objServiceLocator._bcAgentHelper.CreateChannel<ICustomerAgent>()));

            }

        }
    }
}
