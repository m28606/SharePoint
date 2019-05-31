using System.Web;
using BcWithFakeFallback = TPCIP.CustomerClosedCases.Fakes.BcWithFakeFallback;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CustomerClosedCases
{
    class ServiceLocatorCustomerClosedCases
    {      
        public ServiceLocator.ServiceLocator objServiceLocator;

        public ServiceLocatorCustomerClosedCases() 
        {
            var settings = new ServiceLocator.Settings();

            objServiceLocator = new ServiceLocator.ServiceLocator();             
         
            objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(IBierAgent), "bier");
            objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(IEtrayAgent), "");
            objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(IFasAgent), "");
            objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(ICustomerClosedCaseAgent), "");

            objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<IBierAgent>(settings.BusinessCoreUrl));
            objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<IEtrayAgent>(settings.BusinessCoreUrl));
            objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<IFasAgent>(settings.FasServiceUrl));
            objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<ICustomerClosedCaseAgent>(settings.BusinessCoreUrl));


            bool useCache = settings.CacheSubData;
            if (useCache)
            {
                objServiceLocator.Map<ISubscriptionAgent>(new CommonServiceAgents.UseCache.SubscriptionAgent3(objServiceLocator._bcAgentHelper.CreateChannel<ISubscriptionAgent>()));

            }
            else
            {
                objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<ISubscriptionAgent>());
            }

            string useFakes = HttpContext.Current.Request.QueryString["usefakes"];

            if (useFakes == "1" || useFakes == "true")
            {
                objServiceLocator.Map<IBierAgent>(new Fakes.BierAgent());
                objServiceLocator.Map<IEtrayAgent>(new Fakes.EtrayAgent());
                objServiceLocator.Map<IFasAgent>(new Fakes.FasAgent());
                objServiceLocator.Map<ICustomerClosedCaseAgent>(new Fakes.CustomerClosedCaseAgent());
            }
            else if (useFakes == "2")
            {
                objServiceLocator.Map<IBierAgent>(new BcWithFakeFallback.BierAgent2(objServiceLocator._bcAgentHelper.CreateChannel<IBierAgent>()));
                objServiceLocator.Map<IEtrayAgent>(new BcWithFakeFallback.EtrayAgent2(objServiceLocator._bcAgentHelper.CreateChannel<IEtrayAgent>()));
                objServiceLocator.Map<IFasAgent>(new BcWithFakeFallback.FasAgent2(objServiceLocator._bcAgentHelper.CreateChannel<IFasAgent>()));
                objServiceLocator.Map<ICustomerClosedCaseAgent>(new BcWithFakeFallback.CustomerClosedCaseAgent2(objServiceLocator._bcAgentHelper.CreateChannel<ICustomerClosedCaseAgent>()));
            }
        }
    }
}
