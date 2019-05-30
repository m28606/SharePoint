using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.ChangeEquipmentPopUp.DataModel;
using TPCIP.CommonDataModel;

namespace TPCIP.ChangeEquipmentPopUp
{
    [ServiceContract]
    public interface IOssiAgent
    {

        [OperationContract]
        [WebGet(UriTemplate = "accessnet?lid={lid}")]
        EquipmentDetails getAccessNetDetails(string lid);

        [OperationContract]
        [WebGet(UriTemplate = "accessnet?mac={mac}")]
        EquipmentDetails getAvailableAccessNetDetails(string mac);

        [OperationContract]
        [WebGet(UriTemplate = "subscribers?cmmac={cmmac}")]
        List<RegisteredUserStatus> getRegisteredUserAccessNetDetails(string cmmac);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "lid/{lid}/macactivation")]
        ChangeEquipmentStatus setEquipmentDetails(string lid, ChangeEquipmentDetails changeEquipmentDetails);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "lid/{lid}/mac?cmMac={cmMac}&cpeMac={cpeMac}&addCpeMac={addCpeMac}")]
        ChangeEquipmentStatus removeEquipment(string lid, string cmMac, string cpeMac, string addCpeMac);
    }
}
