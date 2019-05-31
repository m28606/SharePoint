using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI;
using TPCIP.Web.AppCode;
using TPCIP.Web.AppCode.Mvc;
using TPCIP.Web.GlobalResources;
using TPCIP.Web.Layouts.TPCIP.Web.WebPages;

namespace TPCIP.Web.ControlTemplates.TPCIP.Web.GuideTools
{
    [ToolWebpart, WebpartStatus(WebpartStatus.Done)]
    public partial class ToolCustomerClosedCasesDetails : UserControl
    {
        [Action]
        public void Index()
        {
        }
    }
}

