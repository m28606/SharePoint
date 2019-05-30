using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CommonServiceAgents.Fakes
{
    public class MyOrderAgent : IMyOrderAgent
    {
        public virtual List<MyOrderData> GetMyOrders(string userId)
        {
            return new List<MyOrderData>
            {
                new MyOrderData{ fsmTaskId="500", taskStatus = "CANCELED", lid = "EM115311", schedulingArea = "FAS", addressLine1 = "Skyttehusvejen 6 7100", addressLine2 = "Dæmningen 58 - 60", addressLine3 = "SIXABCVEJ 5 27 TV", addressLine4 = "Solvænget 1", city = "AALBORG", zippost = "9000", hfNumber = "0201002001", installationNumber = "0000329795", analegNumber = "0000876007", orderNo ="20171018-007233", orderType = "FMFASO", product = "product1", description="FLYTNING", start="2019-06-16T11:45:12", end="2019-09-16T12:45:20"},
                new MyOrderData{ fsmTaskId="400", taskStatus = "HOLD", lid = "EF506051", schedulingArea = "Columbus", addressLine1 = "Skyttehusvejen 6 7100", addressLine2 = "Dæmningen 58 - 60", addressLine3 = "SIXABCVEJ 5 27 TV", addressLine4 = "Solvænget 1", city = "AALBORG", zippost = "8000", hfNumber = "0201002002", installationNumber = "0000329796", analegNumber = "0000876008", orderNo ="20171018-007233", orderType = "INSTALL", product = "product2", description= "FLYTNING", start="2019-04-16T10:00:12", end="2019-07-16T11:15:12"},
                new MyOrderData{ fsmTaskId="300", taskStatus = "INPROGRESS", lid = "YC201711", schedulingArea = "Etray", addressLine1 = "Skyttehusvejen 6 7100", addressLine2 = "Dæmningen 58 - 60", addressLine3 = "SIXABCVEJ 5 27 TV", addressLine4 = "Solvænget 1", city = "AALBORG", zippost = "7000", hfNumber = "0201002003", installationNumber = "0000329797", analegNumber = "0000876009", orderNo ="20171018-007233", orderType = "OVFASO", product = "product3", description= "Test", start="2020-04-16T10:20:12", end="2020-05-16T10:45:12"},
            };
        }
    }
}


