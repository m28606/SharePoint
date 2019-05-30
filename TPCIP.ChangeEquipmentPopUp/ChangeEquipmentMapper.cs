using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TPCIP.CommonDataModel;
using TPCIP.ChangeEquipmentPopUp.DataModel;
using TPCIP.ChangeEquipmentPopUp.Domain;
using TPCIP.ToolBox.Constants;

namespace TPCIP.ChangeEquipmentPopUp
{
    class ChangeEquipmentMapper
    {
        public static AccessNetDetails MapAccessNetDetails(EquipmentDetails equipmentDetails)
        {
            var accessNetdetails = new AccessNetDetails();
            SetDefaultValue(accessNetdetails);

            if (equipmentDetails != null)
            {
                if (equipmentDetails.accessnetList != null && equipmentDetails.accessnetList.Count > 0)
                {
                    accessNetdetails.ModemMac = equipmentDetails.accessnetList.Any(m => m.type == AccessNetType.CableModem.ToString()) ?
                        GetValidMacAddress(equipmentDetails.accessnetList.Where(m => m.type == AccessNetType.CableModem.ToString()).FirstOrDefault().macAddress) : string.Empty;

                    accessNetdetails.MtaMac = equipmentDetails.accessnetList.Any(m => m.type == AccessNetType.Mta.ToString()) ?
                        GetValidMacAddress(equipmentDetails.accessnetList.Where(m => m.type == AccessNetType.Mta.ToString()).FirstOrDefault().mtaMac) : string.Empty;
                    accessNetdetails.StdCpeMac = equipmentDetails.accessnetList.Any(m => m.type == AccessNetType.StdCpe.ToString()) ? equipmentDetails.accessnetList.Where(m => m.type == AccessNetType.StdCpe.ToString()).FirstOrDefault().cpeMac : string.Empty;
                    accessNetdetails.AddCpeMac = equipmentDetails.accessnetList.Any(m => m.type == AccessNetType.AddCpe.ToString()) ? equipmentDetails.accessnetList.Where(m => m.type == AccessNetType.AddCpe.ToString()).FirstOrDefault().cpeMac : string.Empty;
                    accessNetdetails.IsAddCpeExist = equipmentDetails.accessnetList.Any(m => m.type == AccessNetType.AddCpe.ToString());
                }
                if (equipmentDetails.cpeList != null && equipmentDetails.cpeList.Count > 0)
                {
                    var cpeListDetails = equipmentDetails.cpeList.Select(m => m.macAddress.Replace(":", ""));
                    accessNetdetails.StdCpeList = cpeListDetails.Select(x => new DefinePriority() { Value = x, Selected = x.Equals(accessNetdetails.StdCpeMac) }).ToList();
                    if (accessNetdetails.StdCpeList.All(m => m.Selected == false))
                    {
                        accessNetdetails.StdCpeList.Add(new DefinePriority() { Value = "Vælg", Selected = true });
                    }
                    accessNetdetails.StdCpeList = accessNetdetails.StdCpeList.OrderByDescending(m => m.Selected).ToList();

                    accessNetdetails.AddCpeList = cpeListDetails.Select(x => new DefinePriority() { Value = x, Selected = x.Equals(accessNetdetails.AddCpeMac) }).ToList();
                    if (accessNetdetails.AddCpeList.All(m => m.Selected == false))
                    {
                        accessNetdetails.AddCpeList.Add(new DefinePriority() { Value = "Vælg", Selected = true });
                    }
                    accessNetdetails.AddCpeList = accessNetdetails.AddCpeList.OrderByDescending(m => m.Selected).ToList();

                }
            }
            return accessNetdetails;
        }

        public static AccessNetDetails MapAvailableAccessNetDetails(EquipmentDetails equipmentDetails)
        {
            var accessNetdetails = new AccessNetDetails();
            SetAvailableDefaultValue(accessNetdetails);

            if (equipmentDetails != null)
            {
                if (equipmentDetails.accessnetList != null && equipmentDetails.accessnetList.Count > 0)
                {
                    accessNetdetails.MtaMac = equipmentDetails.accessnetList.Any(m => m.type == AccessNetType.Mta.ToString()) ?
                        GetValidMacAddress(equipmentDetails.accessnetList.Where(m => m.type == AccessNetType.Mta.ToString()).FirstOrDefault().mtaMac) : string.Empty;
                }
                if (equipmentDetails.cpeList != null && equipmentDetails.cpeList.Count > 0)
                {
                    var cpeListDetails = equipmentDetails.cpeList.Select(m => m.macAddress.Replace(":", ""));
                    accessNetdetails.ListCpeMac = cpeListDetails.Select(x => new DefinePriority() { Value = x, Selected = false }).ToList();
                    accessNetdetails.ListCpeMac.Add(new DefinePriority() { Value = "Vælg", Selected = true });
                    accessNetdetails.ListCpeMac = accessNetdetails.ListCpeMac.OrderByDescending(m => m.Selected).ToList();

                }
                accessNetdetails.IsAddCpeExist = accessNetdetails.ListCpeMac.Count > 1;
            }
            return accessNetdetails;
        }

        public static AccessNetDetails MapRegisteredUserChangeEquipmentDetails(List<RegisteredUserStatus> changeEquipmentElements)
        {
            var obj = new AccessNetDetails();
            if (changeEquipmentElements == null) 
            {
                obj.IsError = true;
                obj.Message = "Vi kan ikke hente data ind nu. Prøv igen senere.";               
            }
            if(changeEquipmentElements.Count == 0)
            {
                return null;
            }
            if (changeEquipmentElements != null)
            {
                obj.Lid = string.IsNullOrEmpty(changeEquipmentElements.FirstOrDefault().lid) ? "" : changeEquipmentElements.FirstOrDefault().lid;
                obj.IsError = false;
                obj.Message = "Registered on Other Subscriber";
            }
            return obj;
        }

        private static void SetDefaultValue(AccessNetDetails accessNetdetails)
        {
            accessNetdetails.IsSuccess = true;
            accessNetdetails.StdCpeList = new List<DefinePriority>();
            accessNetdetails.AddCpeList = new List<DefinePriority>();
            accessNetdetails.IsAddCpeExist = false;
            accessNetdetails.ModemMac = string.Empty;
            accessNetdetails.MtaMac = string.Empty;
            accessNetdetails.AddCpeMac = string.Empty;
            accessNetdetails.StdCpeMac = string.Empty;

        }

        private static void SetAvailableDefaultValue(AccessNetDetails accessNetdetails)
        {
            accessNetdetails.IsSuccess = true;
            accessNetdetails.ListCpeMac = new List<DefinePriority>();
            accessNetdetails.IsAddCpeExist = false;
            accessNetdetails.MtaMac = string.Empty;

        }

        private static string GetValidMacAddress(string macAddress)
        {
            string macDetails = UserConstants.NoData;
            if (!string.IsNullOrEmpty(macAddress))
            {
                macDetails = macAddress.Replace(":", "").Trim();
                macDetails = !macDetails.StartsWith(UserConstants.VirtualMacAddress) ? macDetails : UserConstants.NoData;
            }
            return macDetails;
        }

        public static ChangeEquipmentDetails MapChangeEquipmentDetails(ChangeEquipmentElements changeEquipmentElements)
        {
            var changeEquipmentDetails = new ChangeEquipmentDetails();
            changeEquipmentDetails.cmMac = changeEquipmentElements.CmMac.Trim();
            //Sequence of addition of cpemac is important, first one is StdCpe & next one is AddCpe
            changeEquipmentDetails.cpeMac = new List<string>();
            changeEquipmentDetails.cpeMac.Add(changeEquipmentElements.StdCpe.Trim());
            changeEquipmentDetails.cpeMac.Add(changeEquipmentElements.AddCpe.Trim());
            return changeEquipmentDetails;
        }
    }
}
