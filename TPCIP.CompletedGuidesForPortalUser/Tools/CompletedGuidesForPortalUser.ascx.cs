using System;
using System.Linq;
using System.Web.UI;
using TPCIP.CompletedGuidesForPortalUser;
using TPCIP.CompletedGuidesForPortalUser.Domain;
using TPCIP.Web.AppCode;
using TPCIP.Web.ControlTemplates.TPCIP.Web.IncludedControls;
using TPCIP.CommonDomain.Enums;
using TPCIP.ToolBox.User;
using TPCIP.ToolBox.Portal;
using TPCIP.ToolBox;

namespace TPCIP.Web.ControlTemplates.TPCIP.Web.Tools
{
    [ToolWebpart, WebpartStatus(WebpartStatus.Done)]
    public partial class CompletedGuidesForPortalUser : UserControl
    {
        public void Index(int pageNumber = 1, int pageSize = 5)
        {
            ServiceLocatorCompletedGuidesForPortalUser serviceLocator = new ServiceLocatorCompletedGuidesForPortalUser();
            var guideFacade = new GuideFacade(serviceLocator.objServiceLocator);

           var portalUserId = UserToolBox.GetPortalUserId();
           var portaltype = PortalToolBox.GetPortalId();

            
            if(!isPortalCIP(portaltype.ToString(), PortalMode.CIP)) 
            {
                completedGuidesForPortalUser.Visible = false;
            }

            var items = guideFacade.GetGuidesForPortalUser(portalUserId, pageNumber - 1, CustomerNoteStatus.Ended,  pageSize);

            ((Paginator)paginator).PageNumber = pageNumber;

            if (items == null || items.Count == 0)
            {
                // Fixed For Pagination case if the service returns no data 
                ((Paginator)paginator).IsLastPage = true;
                lblNoSessions.Visible = true;
                rptCompletedGuidesForPortalUser.Visible = false;
            }
            else
            {
                ((Paginator)paginator).IsLastPage = items.Count < pageSize;
                rptCompletedGuidesForPortalUser.DataSource = items;
                rptCompletedGuidesForPortalUser.DataBind();
            }

            if (items == null || (pageNumber == 1 && items.Count == 0))
            {
                ((Paginator)paginator).Visible = false;
            }
        }

        public bool isPortalCIP(string portaltype, PortalMode portalId)
        {

            PortalMode portalid = (PortalMode)Enum.Parse(typeof(PortalMode), portaltype);

            if (portalid == PortalMode.CIP)
            {
                return true;
            }
            return false;
        }
    }
}
