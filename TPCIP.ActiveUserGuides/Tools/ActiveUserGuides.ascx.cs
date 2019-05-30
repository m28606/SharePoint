using System;
using System.Web.UI;
using TPCIP.Web.AppCode;
using TPCIP.Web.AppCode.Mvc;
using TPCIP.Web.ControlTemplates.TPCIP.Web.IncludedControls;
using TranslationsNew = TPCIP.CommonTranslations;
using TPCIP.ActiveUserGuides.Domain;
using TPCIP.ToolBox.User;

namespace TPCIP.ActiveUserGuides.ControlTemplates.TPCIP.ActiveUserGuides
{
    [WebpartStatus(WebpartStatus.Done)]
    [ToolWebpart]
    public partial class ActiveUserGuides : UserControl
    {
        [Action]
        public void Index(CustomerNoteStatus guideSessionStatus, int pageNumber = 1, int pageSize = 5)
        {
            if (guideSessionStatus == CustomerNoteStatus.Deleted)
            {
                throw new UnauthorizedAccessException();
            }

            var portalUserId = UserToolBox.GetPortalUserId();

            ServiceLocatorActiveUserGuides serviceLocator = new ServiceLocatorActiveUserGuides();
            var guideFacade = new GuideFacade(serviceLocator.objServiceLocator);
            var items = guideFacade.GetGuidesForPortalUser(portalUserId, pageNumber - 1, guideSessionStatus, pageSize);

            ((Paginator)paginator).PageNumber = pageNumber;
            if (items == null || items.Count == 0)
            {
                // Fixed For Pagination case if the service returns no data 
                ((Paginator)paginator).IsLastPage = true;
                lblNoSessions.Visible = true;
                repeater1.Visible = false;
            }
            else
            {
                ((Paginator)paginator).IsLastPage = items.Count < pageSize;
                repeater1.DataSource = items;
                repeater1.DataBind();
            }

            if (items == null || (pageNumber == 1 && items.Count == 0))
            {
                ((Paginator)paginator).Visible = false;
            }

            ActiveUserGuidesWP.Attributes.Add("data-guidesessionstatus", guideSessionStatus.ToString());
            if (guideSessionStatus == CustomerNoteStatus.Active)
                title.Text = "Mine aktive sessioner";
            else if (guideSessionStatus == CustomerNoteStatus.Parked)
                title.Text = "Mine parkerede sessioner";

        }
    }
}
