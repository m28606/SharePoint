using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI;
using TPCIP.Web.AppCode;
using TPCIP.Web.AppCode.Mvc;
using TPCIP.Web.ControlTemplates.TPCIP.Web.IncludedControls;
using TPCIP.CommonTranslations;
using TPCIP.Web.Layouts.TPCIP.Web.WebPages;
using TPCIP.CustomerOpenCases.Domain;
using TPCIP.ToolBox.Portal;
using TPCIP.ToolBox;

namespace TPCIP.CustomerOpenCases.ControlTemplates.TPCIP.CustomerOpenCases
{
    [ToolWebpart, WebpartStatus(WebpartStatus.InProgress)]
    public partial class CustomerOpenCases : UserControl
    {

        public string BierTtUrl = ConfigurationManager.AppSettings["BierTTServiceUrl"];
        public string BierPoUrl = ConfigurationManager.AppSettings["BierPOServiceUrl"];
        public List<Case> FasoCases { get; set; }
        public List<Case> BierCases { get; set; }
        public List<Case> ColumbusCases { get; set; }
        public List<Case> EtrayCases { get; set; }
        public int FasoCasesCount { get; set; }
        public int BierCasesCount { get; set; }
        public int ColumbusCasesCount { get; set; }
        public int EtrayCasesCount { get; set; }
        public Boolean IsMinCaseCount { get; set; }
        public int TotalCasesCount { get; set; }

        /// <summary>
        /// This function is used to throw the exception in Etray, Fas and Bier to the CustomErrorControl.ascx.cs
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        [Action]
        public string RenderCustomError(Exception ex)
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
            ((UserControlHeader)UCToolHeader).Title = PortalMode.CIP == portaltype ? "Åbne eTray sager" : "Åbne sager";

            ServiceLocatorCustomerOpenCases serviceLocator = new ServiceLocatorCustomerOpenCases();
            var customerFacade = new CaseFacade(serviceLocator.objServiceLocator);
    
            var openCases = customerFacade.GetOpenCases(customerId, bierInstallationId, BierTtUrl, BierPoUrl, orderType);

            openCases = openCases.Where(openCase => openCase.Type != CaseType.MsgNoEtray && openCase.Type != CaseType.MsgNoFaso && openCase.Type != CaseType.MsgNoBier && openCase.Type != CaseType.MsgNoCU).Select(openCase =>
            {
                openCase.Note = GetNoteOrErrorMessage(openCase);
                return openCase;
            }).ToList();

            if (openCases.Count > 0)
            {
                FasoCases = openCases.Where(m => m.Type.ToString().ToUpper().Contains("FASO") && m.Type != CaseType.MsgFasoError).ToList();
                FasoCasesCount = FasoCases.Count(m => m.Id != null);

                BierCases = openCases.Where(m => m.Type.ToString().ToUpper().Contains("BIER") && m.Type != CaseType.MsgBierError).ToList();
                BierCasesCount = BierCases.Count(m => m.Id != null);

                ColumbusCases = openCases.Where(m => m.Type.ToString().ToUpper().Contains("CU") && m.Type != CaseType.MsgCUError).ToList();
                ColumbusCasesCount = ColumbusCases.Count(m => m.Id != null);

                EtrayCases = openCases.Where(m => m.Type.ToString().ToUpper().Contains("ETRAY") && m.Type != CaseType.MsgEtrayError).ToList();
                EtrayCasesCount = EtrayCases.Count(m => m.Id != null);

                TotalCasesCount = FasoCasesCount + BierCasesCount + EtrayCasesCount + ColumbusCasesCount;
                IsMinCaseCount = TotalCasesCount <= 4;

                lblOpenCasesNoData.Visible = false;


                if (FasoCases.Count == 0 && BierCases.Count == 0 && EtrayCases.Count == 0 && ColumbusCases.Count == 0)
                {
                    OpenCasesView.Visible = false;
                    lblOpenCasesNoData.Visible = true;
                }
            }
            else
            {
                OpenCasesView.Visible = false;
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
                case CaseType.MsgEtrayError:
                    etrayErrorHtml.Value = (RenderCustomError(customerCase.BcException));
                    return string.Format("<span class=text-error>{0}</span>", Translations.Generic_Error_Message);
                case CaseType.MsgBierError:
                    bierErrorHtml.Value = RenderCustomError(customerCase.BcException);
                    return string.Format("<span class=text-error>{0}</span>", Translations.Generic_Error_Message);
                case CaseType.MsgFasoError:
                    fasErrorHtml.Value = RenderCustomError(customerCase.BcException);
                    return string.Format("<span class=text-error>{0}</span>", Translations.Generic_Error_Message);
                case CaseType.MsgCUError:
                    cuErrorHtml.Value = RenderCustomError(customerCase.BcException);
                    return string.Format("<span class=text-error>{0}</span>", Translations.Generic_Error_Message);
                //Todo: Check this Shivani
                case CaseType.MsgFasoNotReady:
                    return string.Format("<span class=text-error>{0}</span>", Translations.BusinessCoreErrorFasoNotReady);
                default:
                    return customerCase.Note;
            }
        }
    }
}
