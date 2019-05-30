using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using TPCIP.ServiceLocatorInterfaces;
using System.Threading;
using TPCIP.ChangeEquipmentPopUp.Domain;
using TPCIP.ChangeEquipmentPopUp.DataModel;
using TPCIP.ChangeEquipmentPopUp;
using TPCIP.ToolBox.Constants;

namespace TPCIP.ChangeEquipmentPopUp
{
    class ChangeEquipmentFacade
    {
        private readonly IOssiAgent _ossiAgent;

        public ChangeEquipmentFacade(IServiceLocator serviceLocator)
        {
            _ossiAgent = serviceLocator.GetService<IOssiAgent>();
        }

                public AccessNetDetails GetAccessNetDetails(string lid)
        {
            try
            {
                var result = _ossiAgent.getAccessNetDetails(lid);
                var accessNetDetails = ChangeEquipmentMapper.MapAccessNetDetails(result);
                return accessNetDetails;
            }
            catch
            {
                return new AccessNetDetails() { IsSuccess = false, ModemMac=UserConstants.NoData,MtaMac=UserConstants.NoData };
            }
        }

        public AccessNetDetails GetAvailableAccessNetDetails(string mac)
        {
            try
            {
                var result = _ossiAgent.getAvailableAccessNetDetails(mac);
                var accessNetDetails = ChangeEquipmentMapper.MapAvailableAccessNetDetails(result);
                return accessNetDetails;
            }
            catch
            {
                return new AccessNetDetails() { IsSuccess = false };
            }
        }

        public AccessNetDetails GetRegisteredAccessNetDetails(string cmmac)
        {
            try
            {
                var result = _ossiAgent.getRegisteredUserAccessNetDetails(cmmac);
                var accessNetDetails = ChangeEquipmentMapper.MapRegisteredUserChangeEquipmentDetails(result);
                if (accessNetDetails != null)
                {
                    accessNetDetails.OtherUser = true;
                }
                return accessNetDetails;
            }
            catch
            {
                return new AccessNetDetails() { IsError = true, Message = "Vi kan ikke hente data ind nu. Prøv igen senere." };
            }
        }

        public bool SetEquipmentDetails(string lid, ChangeEquipmentElements changeEquipmentElements)
        {
            try
            {
                var changeEquipmentDetails = ChangeEquipmentMapper.MapChangeEquipmentDetails(changeEquipmentElements);
                var result = _ossiAgent.setEquipmentDetails(lid, changeEquipmentDetails);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool RemoveEquipment(string lid, bool cmMac, bool cpeMac, bool addCpeMac)
        {
            var CmMac = cmMac.ToString().ToLower();
            var CpeMac = cpeMac.ToString().ToLower();
            var AddCpeMac = addCpeMac.ToString().ToLower();
            try
            {
                if (CmMac == "true")
                {
                    CpeMac = null;
                    AddCpeMac = null;
                }

                var removeEquipmentDetails = _ossiAgent.removeEquipment(lid, CmMac, CpeMac, AddCpeMac);
                return true;
            }
            catch
            {
                return false; 
            }
        }
    }
}
