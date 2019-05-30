using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TPCIP.ChangeEquipmentPopUp.DataModel;
using TPCIP.CommonDataModel;

namespace TPCIP.ChangeEquipmentPopUp.Fakes.BcWithFakeFallback
{
    public class OssiAgent2 : OssiAgent
    {
        private readonly IOssiAgent _bcChannel;

        public OssiAgent2(IOssiAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override EquipmentDetails getAccessNetDetails(string lid)
        {
            try
            {
                return _bcChannel.getAccessNetDetails(lid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getAccessNetDetails(lid);
        }

        public override EquipmentDetails getAvailableAccessNetDetails(string mac)
        {
            try
            {
                return _bcChannel.getAvailableAccessNetDetails(mac);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getAvailableAccessNetDetails(mac);
        }

        public override ChangeEquipmentStatus setEquipmentDetails(string lid, ChangeEquipmentDetails changeEquipmentDetails)
        {
            try
            {
                return _bcChannel.setEquipmentDetails(lid, changeEquipmentDetails);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.setEquipmentDetails(lid, changeEquipmentDetails);
        }

        public override ChangeEquipmentStatus removeEquipment(string lid, string cmMac, string cpeMac, string addCpeMac)
        {
            try
            {
                return _bcChannel.removeEquipment(lid, cmMac, cpeMac, addCpeMac);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.removeEquipment(lid, cmMac, cpeMac, addCpeMac);
        }


        public override List<RegisteredUserStatus> getRegisteredUserAccessNetDetails(string cmmac)
        {
            try
            {
                return _bcChannel.getRegisteredUserAccessNetDetails(cmmac);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getRegisteredUserAccessNetDetails(cmmac);
        }
    }
}
