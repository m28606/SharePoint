using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Fakes = TPCIP.ChangeEquipmentPopUp.Fakes;
using BcWithFakeFallback = TPCIP.ChangeEquipmentPopUp.Fakes.BcWithFakeFallback;
using TPCIP.ChangeEquipmentPopUp;


namespace TPCIP.ChangeEquipmentPopUp
{
    class ServiceLocatorChangeEquipmentPopUp 
    {
         public ServiceLocator.ServiceLocator objServiceLocator;


         public ServiceLocatorChangeEquipmentPopUp()
            {
                var settings = new ServiceLocator.Settings();
                objServiceLocator = new ServiceLocator.ServiceLocator(settings.BusinessCoreUrl);
                objServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(IOssiAgent), "ossi");               
                objServiceLocator.Map(objServiceLocator._bcAgentHelper.CreateChannel<IOssiAgent>());
              
                string useFakes = HttpContext.Current.Request.QueryString["usefakes"];

                if (useFakes == "1" || useFakes == "true")
                {
                    objServiceLocator.Map<IOssiAgent>(new Fakes.OssiAgent());
                }
                else if (useFakes == "2")
                {
                    objServiceLocator.Map<IOssiAgent>(new BcWithFakeFallback.OssiAgent2(objServiceLocator._bcAgentHelper.CreateChannel<IOssiAgent>()));
                }
            }

        }

    }

