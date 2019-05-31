using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Fakes = TPCIP.CustomerOpenCases.Fakes;
using BcWithFakeFallback = TPCIP.CustomerOpenCases.Fakes.BcWithFakeFallback;
using TPCIP.CustomerOpenCases;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CustomerOpenCases
{
    class ServiceLocatorCustomerOpenCases
    {
        public ServiceLocator.ServiceLocator objServiceLocator;

        public ServiceLocatorCustomerOpenCases()
        {
            var settings = new ServiceLocator.Settings();

            objServiceLocator = new ServiceLocator.ServiceLocator();             
         
            objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(IBierAgent), "bier");
            objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(IColumbusAgent), "");
            objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(IEtrayAgent), "");
            objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(IFasAgent), "");

            objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<IBierAgent>(settings.BusinessCoreUrl));
            objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<IColumbusAgent>(settings.RestanceServiceUrl));
            objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<IEtrayAgent>(settings.BusinessCoreUrl));
            objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<IFasAgent>(settings.FasServiceUrl));


            bool useCache = settings.CacheSubData;
            if (useCache)
            {
                objServiceLocator.Map<ISubscriptionAgent>(new TPCIP.CommonServiceAgents.UseCache.SubscriptionAgent3(objServiceLocator._bcAgentHelper.CreateChannel<TPCIP.CommonServiceAgentInterfaces.ISubscriptionAgent>()));

            }
            else
            {
                objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<ISubscriptionAgent>());
            }

            string useFakes = HttpContext.Current.Request.QueryString["usefakes"];

            if (useFakes == "1" || useFakes == "true")
            {
                objServiceLocator.Map<IBierAgent>(new Fakes.BierAgent());
                objServiceLocator.Map<IColumbusAgent>(new Fakes.ColumbusAgent());
                objServiceLocator.Map<IEtrayAgent>(new Fakes.EtrayAgent());
                objServiceLocator.Map<IFasAgent>(new Fakes.FasAgent());
            }
            else if (useFakes == "2")
            {
                objServiceLocator.Map<IBierAgent>(new BcWithFakeFallback.BierAgent2(objServiceLocator._bcAgentHelper.CreateChannel<IBierAgent>()));
                objServiceLocator.Map<IColumbusAgent>(new BcWithFakeFallback.ColumbusAgent2(objServiceLocator._bcAgentHelper.CreateChannel<IColumbusAgent>()));
                objServiceLocator.Map<IEtrayAgent>(new BcWithFakeFallback.EtrayAgent2(objServiceLocator._bcAgentHelper.CreateChannel<IEtrayAgent>()));
                objServiceLocator.Map<IFasAgent>(new BcWithFakeFallback.FasAgent2(objServiceLocator._bcAgentHelper.CreateChannel<IFasAgent>()));
            }
        }
    }
}
