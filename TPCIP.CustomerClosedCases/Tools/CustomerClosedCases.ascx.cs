using System;
using System.Web.UI;
using TPCIP.Web.AppCode;
using TPCIP.Web.AppCode.Mvc;
using TPCIP.CommonServiceFacade;
using System.Configuration;
using System.IO;
using System.Linq;
using TPCIP.Web.Layouts.TPCIP.Web.WebPages;
using TPCIP.CommonDomain;
using TPCIP.CommonTranslations;
using TPCIP.CustomerClosedCases.Domain;
using System.Collections.Generic;
using TPCIP.Web.ControlTemplates.TPCIP.Web.IncludedControls;
using TPCIP.ToolBox.Portal;
using TPCIP.ToolBox;

namespace TPCIP.CustomerClosedCases.ControlTemplates.TPCIP.CustomerClosedCases
{
    [ToolWebpart, WebpartStatus(WebpartStatus.InProgress)]
    public partial class CustomerClosedCases : UserControl
    {
        public string BierTtUrl = ConfigurationManager.AppSettings["BierTTServiceUrl"];
        public string BierPoUrl = ConfigurationManager.AppSettings["BierPOServiceUrl"];
        public List<Case> FasoCases { get; set; }
        public List<Case> BierCases { get; set; }
        public List<Case> EtrayCases { get; set; }
        public List<Case> ColumbusCases { get; set; }
        public int FasoCasesCount { get; set; }
        public int BierCasesCount { get; set; }
        public int EtrayCasesCount { get; set; }
        public int ColumbusCasesCount { get; set; }
        public int TotalCasesCount { get; set; }
        public Boolean IsMinCaseCount { get; set; }

        [Action]
        public string RenderCustomError(Exception ex)     //This function is used to throw the exception in Etray, Fas and Bier to the CustomErrorControl.ascx.cs
        {
            string str1 = "/_layouts/TPCIP.Web/WebPages/CustomErrorControl.ascx";

            System.IO.StringWriter htmlStringWriter = new System.IO.StringWriter();

            try
            {
                Page pageHolder = new Page();
                UserControl viewControl = (UserControl)pageHolder.LoadControl(str1);

                ((CustomErrorControl)viewControl).BcEx = ex;
                pageHolder.Controls.Add(viewControl);
                StringWriter output = new StringWriter();
                Server.Execute(pageHolder, output, false);
                return Convert.ToString(output);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public void Index(string customerId, string bierInstallationId = "", string orderType = "")
        {
            var portaltype = PortalToolBox.GetPortalId();
            ((UserControlHeader)UCToolHeader).Title = PortalMode.CIP == portaltype ? "Afsluttede eTray sager" : "Afsluttede sager";

            ServiceLocatorCustomerClosedCases serviceLocator = new ServiceLocatorCustomerClosedCases();
            var caseFasade = new CaseFacade(serviceLocator.objServiceLocator);
            try
            {
                var closedCases = caseFasade.GetClosedCases(customerId, bierInstallationId, BierTtUrl, BierPoUrl, orderType);

                closedCases = closedCases.Where(closedCase => closedCase.Type != CaseType.MsgNoEtray && closedCase.Type != CaseType.MsgNoFaso && closedCase.Type != CaseType.MsgNoColumbus && closedCase.Type != CaseType.MsgNoBier).Select(closedCase =>
                {
                    closedCase.Note = GetNoteOrErrorMessage(closedCase);
                    return closedCase;
                }).ToList();

                if (closedCases.Count > 0)
                {
                    FasoCases = closedCases.Where(m => (m.Type.ToString().ToUpper().Contains("FASO")) && (m.CreatedDateTime >= DateTime.Now.AddDays(-90))).ToList();
                    FasoCasesCount = FasoCases.Count(m => m.Id != null);

                    BierCases = closedCases.Where(m => (m.Type.ToString().ToUpper().Contains("BIER")) && (m.CreatedDateTime >= DateTime.Now.AddDays(-90))).ToList();
                    BierCasesCount = BierCases.Count(m => m.Id != null);

                    EtrayCases = closedCases.Where(m => m.Type.ToString().ToUpper().Contains("ETRAY")).ToList();
                    EtrayCasesCount = EtrayCases.Count(m => m.Id != null);

                    ColumbusCases = closedCases.Where(m => m.Type.ToString().ToUpper().Contains("COLUMBUS")).ToList();
                    ColumbusCasesCount = ColumbusCases.Count(m => m.Id != null);

                    TotalCasesCount = FasoCasesCount + BierCasesCount + EtrayCasesCount + ColumbusCasesCount;
                    IsMinCaseCount = TotalCasesCount <= 4;

                    lblNoData.Visible = false;


                    if (FasoCases.Count == 0 && BierCases.Count == 0 && EtrayCases.Count == 0 && ColumbusCases.Count == 0)
                    {
                        ClosedCasesView.Visible = false;
                        lblNoData.Visible = true;
                    }
                }
                else
                {
                    ClosedCasesView.Visible = false;
                }
                DataBind();
            }
            catch (BusinessCoreException ex)
            {
                if (ex.ErrorData != null && ex.ErrorData.Code == FasoFacade.NotReadyErrorCode)
                {
                    throw new FriendlyException(Translations.BusinessCoreErrorFasoNotReady, ex);
                }
                throw;
            }
            //added to show data in badge
            if (TotalCasesCount > 0 && (portaltype == PortalMode.MyTP))
            {
                ((UserControlHeader)UCToolHeader).ShowBadge_Data = TotalCasesCount.ToString();
            }
        }

        public string GetNoteOrErrorMessage(Case customerCase)
        {
            switch (customerCase.Type)
            {
                case CaseType.MsgColumbusError:
                    columbusErrorHtml.Value = RenderCustomError(customerCase.BcException);
                    return string.Format("<span class=text-error>{0}</span>", Translations.Generic_Error_Message);
                case CaseType.MsgEtrayError:
                    etrayErrorHtml.Value = RenderCustomError(customerCase.BcException);
                    return string.Format("<span class=text-error>{0}</span>", Translations.Generic_Error_Message);
                case CaseType.MsgBierError:
                    bierErrorHtml.Value = RenderCustomError(customerCase.BcException);
                    return string.Format("<span class=text-error>{0}</span>", Translations.Generic_Error_Message);
                case CaseType.MsgFasoError:
                    fasErrorHtml.Value = RenderCustomError(customerCase.BcException);
                    return string.Format("<span class=text-error>{0}</span>", Translations.Generic_Error_Message);
                case CaseType.MsgFasoNotReady:
                    return string.Format("<span class=text-error>{0}</span>", Translations.BusinessCoreErrorFasoNotReady);
                default:
                    return customerCase.Note;
            }
        }
    }
}
