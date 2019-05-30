using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using TPCIP.Web.ControlTemplates.TPCIP.Web.IncludedControls;
using TPCIP.CommonDomain;
using TPCIP.CommonTranslations;
using TPCIP.ChangeEquipmentPopUp.Domain;
using TPCIP.Web.AppCode;
using TPCIP.Web.AppCode.Mvc;

namespace TPCIP.ChangeEquipmentPopUp.ControlTemplates.TPCIP.ChangeEquipmentPopUp
{
    public partial class ChangeEquipmentPopUp : UserControl
    {

        public AccessNetDetails LoadEquipment { get; set; }
        [Action]
        public void Index(string customerId)
        {
            ServiceLocatorChangeEquipmentPopUp serviceLocator = new ServiceLocatorChangeEquipmentPopUp();
            var deviceFacade = new ChangeEquipmentFacade(serviceLocator.objServiceLocator);
            LoadEquipment = deviceFacade.GetAccessNetDetails(customerId);
        }

        [WebMethod]
        public AccessNetDetails LoadAvailableChangeEquipPopUpData(string newMac)
        {
            ServiceLocatorChangeEquipmentPopUp serviceLocator = new ServiceLocatorChangeEquipmentPopUp();
            var deviceFacade = new ChangeEquipmentFacade(serviceLocator.objServiceLocator);
            var loadAvailableChangeEquip = deviceFacade.GetAvailableAccessNetDetails(newMac.Replace(":", "").Trim());
            return loadAvailableChangeEquip;
        }


        [WebMethod]
        public bool UpdateChangeEquipPopUpData(string customerId, string regMac, string newMac, string cpeMac, string extCpeMac)
        {
            ServiceLocatorChangeEquipmentPopUp serviceLocator = new ServiceLocatorChangeEquipmentPopUp();
            var deviceFacade = new ChangeEquipmentFacade(serviceLocator.objServiceLocator);

            var changeEquipmentElements = new ChangeEquipmentElements();
            if (extCpeMac == "Vælg") { changeEquipmentElements.AddCpe = ""; }
            else { changeEquipmentElements.AddCpe = extCpeMac; }

            if (newMac == "") { changeEquipmentElements.CmMac = regMac; }
            else { changeEquipmentElements.CmMac = newMac; }

            if (cpeMac == "Vælg") { changeEquipmentElements.StdCpe = ""; }
            else { changeEquipmentElements.StdCpe = cpeMac; }

            var updateChangeEquip = deviceFacade.SetEquipmentDetails(customerId, changeEquipmentElements);
            return updateChangeEquip;
        }

        [WebMethod]
        public bool RemoveChangeEquipPopUpData(string customerId, bool removeRegMac, bool removeCpeMac, bool removeExtCpeMac)
        {
            ServiceLocatorChangeEquipmentPopUp serviceLocator = new ServiceLocatorChangeEquipmentPopUp();
            var deviceFacade = new ChangeEquipmentFacade(serviceLocator.objServiceLocator);
            var removeChangeEquip = deviceFacade.RemoveEquipment(customerId, removeRegMac, removeCpeMac, removeExtCpeMac);
            return removeChangeEquip;
        }
    }
}
