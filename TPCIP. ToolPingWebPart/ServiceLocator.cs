using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Fakes = TPCIP.ToolPingWebPart.Fakes;
using BcWithFakeFallback = TPCIP.ToolPingWebPart.Fakes.BcWithFakeFallback;



namespace TPCIP.ToolPingWebPart
{
    class ServiceLocatorToolPingWebPart
    {
        public ServiceLocator.ServiceLocator objServiceLocator;
        public ServiceLocator.ServiceLocator ossiservicelocator;

        public ServiceLocatorToolPingWebPart()
            {
                var settings = new ServiceLocator.Settings();
                objServiceLocator = new ServiceLocator.ServiceLocator(settings.LinecheckServiceUrl);
                ossiservicelocator = new ServiceLocator.ServiceLocator(settings.BusinessCoreUrl);

                objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(ILineCheckAgent), "");
                ossiservicelocator._bcAgentHelper._serviceAreaMappings.Add(typeof(IOssiAgent), ""); 

                objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<ILineCheckAgent>());
                ossiservicelocator.Map(ossiservicelocator._bcAgentHelper.CreateChannel<IOssiAgent>());   

                string useFakes = HttpContext.Current.Request.QueryString["usefakes"];

                if (useFakes == "1" || useFakes == "true")
                {
                    objServiceLocator.Map<ILineCheckAgent>(new Fakes.LineCheckAgent());
                    ossiservicelocator.Map<IOssiAgent>(new Fakes.LineCheckAgent()); 
                }
                else if (useFakes == "2")
                {
                    objServiceLocator.Map<ILineCheckAgent>(new BcWithFakeFallback.LineCheckAgent2(objServiceLocator._bcAgentHelper.CreateChannel<ILineCheckAgent>()));
                    ossiservicelocator.Map<IOssiAgent>(new BcWithFakeFallback.OssiAgent2(ossiservicelocator._bcAgentHelper.CreateChannel<IOssiAgent>()));     
               }
            }
    }
}
