using System;
using System.Collections.Generic;
using TPCIP.ChangeEquipmentPopUp.DataModel;
using TPCIP.CommonDataModel;

namespace TPCIP.ChangeEquipmentPopUp.Fakes
{
    public class OssiAgent : IOssiAgent
    {

        public virtual EquipmentDetails getAccessNetDetails(string lid)
        {
            var changeEquipmentDetails = new EquipmentDetails();
            changeEquipmentDetails.accessnetList = new List<AccessnetList>();
            changeEquipmentDetails.cpeList = new List<CpeList>();
            var cableModemDetail = new AccessnetList() { type = "CableModem", macAddress = "4065a3fff2bc" };
            var mtaMac = new AccessnetList() { type = "Mta", mtaMac = "4065a3fff2bc" };
            var stdCpeDetails = new AccessnetList() { type = "StdCpe", cpeMac = "4065a3fff2ae" };
            var addCpeDetails = new AccessnetList() { type = "AddCpe", cpeMac = "" };
            changeEquipmentDetails.accessnetList.Add(stdCpeDetails);
            changeEquipmentDetails.accessnetList.Add(addCpeDetails);
            changeEquipmentDetails.accessnetList.Add(cableModemDetail);
            changeEquipmentDetails.accessnetList.Add(mtaMac);
            changeEquipmentDetails.cpeList = new List<CpeList>() { new CpeList() { macAddress = "40:65:a3:ff:f2:be" }, new CpeList() { macAddress = "50:65:a3:ff:f2:bf" }, new CpeList() { macAddress = "60:65:a3:ff:f2:bg" } };

            return changeEquipmentDetails;

        }

        public virtual EquipmentDetails getAvailableAccessNetDetails(string mac)
        {
            var changeEquipmentDetails = new EquipmentDetails();
            changeEquipmentDetails.accessnetList = new List<AccessnetList>();
            changeEquipmentDetails.cpeList = new List<CpeList>();
            var cableModemDetail = new AccessnetList() { type = "CableModem", macAddress = "4065a3fff2ab" };
            
            var mtaMac = new AccessnetList() { type = "Mta", mtaMac = "4065a3fff2ac" };
            changeEquipmentDetails.accessnetList.Add(cableModemDetail);
            
            changeEquipmentDetails.accessnetList.Add(mtaMac);
            changeEquipmentDetails.cpeList = new List<CpeList>() { new CpeList() { macAddress = "40:65:a3:ff:f2:ae" }, new CpeList() { macAddress = "50:65:a3:ff:f2:af" }, new CpeList() { macAddress = "60:65:a3:ff:f2:ag" } };

            return changeEquipmentDetails;

        }


        public virtual ChangeEquipmentStatus setEquipmentDetails(string lid, ChangeEquipmentDetails changeEquipmentDetails)
        {
            return new ChangeEquipmentStatus() { orderId = 3118531 };
        }

        public virtual ChangeEquipmentStatus removeEquipment(string lid, string cmMac, string cpeMac, string addCpeMac)
        {
            return new ChangeEquipmentStatus() { orderId = 3118531 };
        }

        public virtual List<RegisteredUserStatus> getRegisteredUserAccessNetDetails(string cmmac)
        {
            //return null;
            //or
            return new List<RegisteredUserStatus>()
            {
                new RegisteredUserStatus()
                {
                    lid = "EM123456",
                }
            };
        }
    }
}

