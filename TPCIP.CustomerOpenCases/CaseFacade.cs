using System;
using System.Collections.Generic;
using System.Linq;
using TPCIP.ServiceLocatorInterfaces;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CustomerOpenCases.DataModel;
using TPCIP.CustomerOpenCases.Domain;
using TPCIP.CommonServiceFacade;
using TPCIP.Instrumentation.Logging;
using TPCIP.ToolBox.Portal;

namespace TPCIP.CustomerOpenCases
{
    public class CaseFacade
    {
        private readonly BillingFacade _billingFacade;
        private readonly ISubscriptionAgent _subscriptionAgent;
        private readonly CustomerFacade _customerFacade;
        private readonly SubscriptionFacade _subscriptionFacade;
        private readonly IBierAgent _bierAgent;
        private readonly IColumbusAgent _columbusAgent;
        private readonly IEtrayAgent _etrayAgent;
        private readonly FasoFacade _fasoFacade; //for code reuse   

        private BierParameter _param = new BierParameter();
        public enum BierNotes
        {
            KundeAktivitet,
            KundeFejl,
            AnlægAktivitet,
            AnlægFejl
        }

        public enum OpenClosedCase
        {
            Åben,
            Lukket,
            Annulleret
        }

        public CaseFacade(IServiceLocator serviceLocator)
        {
            _billingFacade = new BillingFacade(serviceLocator);
            _subscriptionAgent = serviceLocator.GetService<ISubscriptionAgent>();
            _customerFacade = new CustomerFacade(serviceLocator);
            _subscriptionFacade = new SubscriptionFacade(serviceLocator);
            _bierAgent = serviceLocator.GetService<IBierAgent>();
            _columbusAgent = serviceLocator.GetService<IColumbusAgent>();
            _etrayAgent = serviceLocator.GetService<IEtrayAgent>();
            _fasoFacade = new FasoFacade(serviceLocator);
        }


        public List<string> GetLidList(string custId)
        {
            var lstLid = new List<string>();
            try
            {
                var subscriptions = _subscriptionFacade.searchSubscriptions(custId).Where(m => m.rootLid != null).ToList();

                foreach (var sub in subscriptions)
                {
                    lstLid.Add(sub.rootLid);
                }

                var paLidMsdnNo = _billingFacade.ExtractUniqueSubscriptionIds(subscriptions);
                if (paLidMsdnNo != null)
                {
                    lstLid.AddRange(paLidMsdnNo);
                }
            }
            catch (Exception)
            {
                lstLid.Add(custId);
            }



            return lstLid.Distinct().ToList();
        }

        public List<Case> GetOpenCases(string custId, string bierInstallationId, string bierPoUrl, string bierTtUrl, string orderType)
        {

            var cases = new List<Case>();

            var lstLid = GetLidList(custId);
            var portalId = PortalToolBox.GetPortalId().ToString();

            foreach (var customerId in lstLid)
            {
                if (portalId.ToUpper() != "CIP")
                {
                    // get Columbus Open Order
                    if (!_customerFacade.IsYouSeeCustomer(customerId))
                    {
                        try
                        {
                            var openColumbusOrders = _columbusAgent.GetColumbusOpenOrders(ColumbusRequestType.ORDER_STATUS.ToString(), customerId);

                            if (openColumbusOrders == null || openColumbusOrders.columbusOpenOrderList == null || openColumbusOrders.columbusOpenOrderList.Count == 0)
                                cases.Add(new Case { Type = CaseType.MsgNoCU });
                            else
                                cases.AddRange(openColumbusOrders.columbusOpenOrderList.Select(CaseMapper.MapCaseFromCu));

                        }
                        catch (Exception exception)
                        {
                            cases.Add(new Case { Type = CaseType.MsgCUError, BcException = exception });
                        }
                    }

                    //get FAS
                    if (!customerId.StartsWith("YK"))
                    {
                        try
                        {
                            var fasoStatus = _fasoFacade.GetFasoStatus(FasoRequestType.OPEN, FasoQueryBy.LID, customerId);
                            if (fasoStatus.faso == null)
                                cases.Add(new Case { Type = CaseType.MsgNoFaso });
                            else

                                cases.AddRange(fasoStatus.faso.Select(n => CaseMapper.MapCaseFromFas(n, customerId)));

                        }
                        catch (Exception exception)
                        {
                            var _case = new Case { Type = CaseType.MsgFasoError, BcException = exception };
                            cases.Add(_case);

                            var bcError = exception as BusinessCoreException;
                            if (bcError != null && bcError.ErrorData != null &&
                                bcError.ErrorData.Code == FasoFacade.NotReadyErrorCode)
                            {
                                _case.Type = CaseType.MsgFasoNotReady;
                            }

                            Logger.WriteLine(exception.ToString(), LogCategory.ServiceAgents);
                        }
                    }
                }


                //get etray
                try
                {
                    var eTrayDocuments = _etrayAgent.GetDocuments(customerId);
                    if (eTrayDocuments == null || eTrayDocuments.Length == 0)
                        cases.Add(new Case { Type = CaseType.MsgNoEtray });
                    else
                        cases.AddRange(eTrayDocuments.Select(CaseMapper.MapCaseFromETray).ToList());
                }
                catch (Exception exception)
                {
                    cases.Add(new Case { Type = CaseType.MsgEtrayError, BcException = exception });
                    Logger.WriteLine(exception.ToString(), LogCategory.ServiceAgents);
                }
            }
            if (cases.Count > 0)
            {
                var errorCases = cases.Where(m => m.Id == null).GroupBy(n => n.Type).Select(o => o.FirstOrDefault()).ToList();
                cases = cases.Where(l => l.Id != null).GroupBy(m => m.Id).Select(m => m.FirstOrDefault()).ToList();
                cases.AddRange(errorCases);
                cases.Sort((c1, c2) => c2.CreatedDateTime.CompareTo(c1.CreatedDateTime));
                return cases;
            }

            return cases;
        }
    }
}
