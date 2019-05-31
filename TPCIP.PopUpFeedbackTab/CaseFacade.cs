using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.PopUpFeedbackTab.DataModel;
using TPCIP.PopUpFeedbackTab.Domain;
using TPCIP.ServiceLocatorInterfaces;

namespace TPCIP.PopUpFeedbackTab
{
    public class CaseFacade
    {
        private readonly FasoFacade _fasoFacade; //for code reuse
        private readonly IColumbusAgent _columbusAgent;

        public CaseFacade(IServiceLocator serviceLocator)
        {
            _fasoFacade = new FasoFacade(serviceLocator);
            _columbusAgent = serviceLocator.GetService<IColumbusAgent>();
        }
        //For Feedback Tab

        public List<ColumbusOrder> GetFeedbackOrders(string lid)
        {
            List<ColumbusOrder> OpenColumbusOrders = new List<ColumbusOrder>();
            var Orders = _columbusAgent.GetColumbusOpenOrders(ColumbusRequestType.ORDER_STATUS.ToString(), lid);
            OpenColumbusOrders = Orders.columbusOpenOrderList;
            return OpenColumbusOrders;
        }

        public List<Case> GetFeedbackFasoOrders(string customerId)
        {
            List<Case> fasoOpenOrders = new List<Case>();
            var fasoOrders = _fasoFacade.GetFasoStatus(FasoRequestType.OPEN, FasoQueryBy.LID, customerId);
            fasoOpenOrders = fasoOrders.faso.Select(CaseMapper.MapCaseFromFas).ToList();
            return fasoOpenOrders;

        }

    }
}
