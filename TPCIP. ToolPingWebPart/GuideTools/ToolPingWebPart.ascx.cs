using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TPCIP.Web.AppCode;
using TPCIP.Web.AppCode.Mvc;
using System.Collections.Generic;
using TPCIP.Web.ControlTemplates.TPCIP.Web.IncludedControls;
using TPCIP.ToolPingWebPart;
using TPCIP.CommonDomain;
using TPCIP.CommonTranslations;
using TPCIP.ToolPingWebPart.Domain;
using System.Linq;
using System.Diagnostics;
using System.Net;
using TPCIP.ToolBox.Portal;
using TPCIP.ToolBox;

namespace TPCIP.ToolPingWebPart.ControlTemplates.TPCIP.ToolPingWebPart
{
    public class SikResult
    {
        public string Type;
        public string PingResult;
        public string WanIp;
        public string Cep;
        public string CepType;
        public List<pingData> detailedPingResult;
        public string tpnumber;
        public string SikID;
    }

    [ToolWebpart, WebpartStatus(WebpartStatus.Done)]
    public partial class ToolPingWebPart : UserControl
    {
        protected string NoteId { get; set; }
        protected string[] sikLst { get; set; }
        protected SikResult objSikResult { get; set; }
        protected IpPingTestResult objDslResult { get; set; }
        public HybridIpPingResultInfo hybridPingResult { get; set; }

        [Action]
        public void Index(string customerId, string noteId, string sikId = null)
        {
            List<string> li_str = new List<string>();
            List<string> li_strdata = new List<string>();
            NoteId = noteId;
            ((UserControlHeader)UCToolHeader).Title = "Ping Status";
            ServiceLocatorToolPingWebPart serviceLocator=new ServiceLocatorToolPingWebPart();
            var deviceFacade = new DeviceFacade(serviceLocator.objServiceLocator, serviceLocator.ossiservicelocator);
            var serviceInfo = deviceFacade.GetServiceInfo(customerId);
            var count = serviceInfo.SikDetails.Count;
            var SikIDData = " ";

            #region InitialLoad
            if (sikId == null)
            {
               
                for (int i = 0; i < count; i++)
                {
                    if (serviceInfo.SikDetails[i].serviceType.ToLower() != "data")
                    {
                        string sik = serviceInfo.SikDetails[i].sik.ToString();
                        string siktype = serviceInfo.SikDetails[i].serviceType;
                        li_str.Add(sik + " - " + siktype);
                    }
                    else if (serviceInfo.SikDetails[i].serviceType.ToLower() == "data")
                    {
                        string sik = serviceInfo.SikDetails[i].sik.ToString();
                        string siktype = serviceInfo.SikDetails[i].serviceType;
                        li_strdata.Add(sik + " - " + siktype);
                    }

                }
             
                if (li_strdata.Count > 1)
                {
                    li_strdata.Sort();
                }

                li_strdata.AddRange(li_str);
                sikLst = li_strdata.ToArray();
                var OtherData = deviceFacade.TestDslLine(SikIDData);
                var CPEdata = deviceFacade.GetCPEType(customerId);
                var portalType = PortalToolBox.GetPortalId();
                if (CPEdata == "Hybrid" && (portalType == PortalMode.TP || portalType == PortalMode.MyTP)){
                    var list = sikLst.ToList();
                    list.Add("LTE");
                    sikLst = list.ToArray();
                }
                sikId = sikLst[0];
                string[] rawsik = sikId.Split('-');
                SikIDData = rawsik[0].Trim();
                objSikResult = new SikResult();
                objSikResult.SikID = SikIDData; // get raw SIK ID
                objSikResult.Type = serviceInfo.SikDetails.Where(m => m.sik == SikIDData).FirstOrDefault().serviceType.ToString();
                
                objSikResult.PingResult = "Ping Result";
                objSikResult.tpnumber = serviceInfo.SikDetails.Where(m => m.sik == SikIDData).FirstOrDefault().tpNo.ToString();


                toolPingWebPart.Attributes.Add("data-sik", SikIDData);
                toolPingWebPart.Attributes.Add("data-dslam", serviceInfo.Dslam);
                toolPingWebPart.Attributes.Add("data-portid", serviceInfo.Port);
                var data = deviceFacade.GetPingResult(SikIDData, serviceInfo.Dslam, serviceInfo.Port);
                objDslResult = deviceFacade.GetDslResult(customerId, SikIDData, objSikResult.tpnumber).PingTestResponse.IpPingTestResponse;
                objSikResult.PingResult = data.IsOkString;

                try
                {
                    objSikResult.detailedPingResult = data.resultPing;
                }
                catch { }
               
              
                if (OtherData != null)
                {
                    objSikResult.WanIp = OtherData.IpAddress;
                    objSikResult.Cep = OtherData.Equipment;
                }
                else
                {
                    objSikResult.WanIp = "";
                    objSikResult.Cep = "";
                }
                objSikResult.CepType = CPEdata;

            }
            #endregion InitialLoad
            #region Hybrid LTE
            else if (sikId == "LTE")
            {
                hybridPingResult = deviceFacade.GetHybridPingInfo(customerId);
                objSikResult = new SikResult();

                objSikResult.CepType = "Hybrid";
                objSikResult.SikID = sikId;
            }
            #endregion
            #region WithSIKdata
            else
            {
                objSikResult = new SikResult();

                objSikResult.PingResult = "Ping ResultElse";

                toolPingWebPart.Attributes.Add("data-sik", sikId);
                toolPingWebPart.Attributes.Add("data-dslam", serviceInfo.Dslam);
                toolPingWebPart.Attributes.Add("data-portid", serviceInfo.Port);
                for (int i = 0; i < count; i++)
                {
                    string sik = serviceInfo.SikDetails[i].sik.ToString();
                    if (sik == sikId)
                    {
                        objSikResult.Type = serviceInfo.SikDetails[i].serviceType;
                        objSikResult.tpnumber = serviceInfo.SikDetails[i].tpNo;
                    }


                }
                var data = deviceFacade.GetPingResult(sikId, serviceInfo.Dslam, serviceInfo.Port);
                objDslResult = deviceFacade.GetDslResult(customerId, sikId, objSikResult.tpnumber).PingTestResponse.IpPingTestResponse;
                objSikResult.PingResult = data.IsOkString;

                try
                {
                    objSikResult.detailedPingResult = data.resultPing;
                }
                catch { }
                var OtherData = deviceFacade.TestDslLine(sikId);
                var CPEdata = deviceFacade.GetCPEType(customerId);

                if (OtherData != null)
                {
                    objSikResult.WanIp = OtherData.IpAddress;
                    objSikResult.Cep = OtherData.Equipment;
                }
                else
                {
                    objSikResult.WanIp = "";
                    objSikResult.Cep = "";
                }

                objSikResult.CepType = CPEdata;
                objSikResult.SikID = sikId;
            }
            #endregion WithSIKdata

        }

        [WebMethod]
        public void ReleaseIP(string sikId)
        {
            ServiceLocatorToolPingWebPart serviceLocator = new ServiceLocatorToolPingWebPart();
            var deviceFacade = new DeviceFacade(serviceLocator.objServiceLocator, serviceLocator.ossiservicelocator);
            var response = deviceFacade.ReleaseIpSik(sikId);

            if (response == true)
            {
            }
            else
            {
                throw new FriendlyException(string.Format("{0}", Translations.Ping_ReleaseIP_Failed) + " " + sikId);
            }
        }

        [WebMethod]
        public bool TimerRequest(string customerId, string sik, string dslam, string portid)
        {
            ServiceLocatorToolPingWebPart serviceLocator = new ServiceLocatorToolPingWebPart();
            var deviceFacade = new DeviceFacade(serviceLocator.objServiceLocator, serviceLocator.ossiservicelocator);
            if (string.IsNullOrEmpty(sik) || string.IsNullOrEmpty(dslam) || string.IsNullOrEmpty(portid))
            {
                var serviceInfo = deviceFacade.GetServiceInfo(customerId);
                sik = serviceInfo.SikDetails[0].sik.ToString();
                dslam = serviceInfo.Dslam;
                portid = serviceInfo.Port;

                toolPingWebPart.Attributes.Add("data-sik", sik);
                toolPingWebPart.Attributes.Add("data-dslam", dslam);
                toolPingWebPart.Attributes.Add("data-portid", serviceInfo.Port);
            }
            var data = deviceFacade.GetPingResult(sik, dslam, portid);

            return data.IsOk;
        }

    }
}
