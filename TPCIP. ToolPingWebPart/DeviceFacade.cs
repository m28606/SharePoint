using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.ServiceLocatorInterfaces;
using TPCIP.ToolPingWebPart.Domain;
using TPCIP.ToolPingWebPart.DataModel;
using PingWebPartData = TPCIP.ToolPingWebPart.DataModel;

namespace TPCIP.ToolPingWebPart
{
    public class DeviceFacade
    {
        private readonly ILineCheckAgent _lineCheckAgent;
        private readonly IOssiAgent _OssiAgent;

        public DeviceFacade(IServiceLocator serviceLocator, IServiceLocator ossiservicelocator)
        {
            _lineCheckAgent = serviceLocator.GetService<ILineCheckAgent>();
            _OssiAgent = ossiservicelocator.GetService<IOssiAgent>();

        }

        public TPCIP.ToolPingWebPart.Domain.ServiceInfo GetServiceInfo(string lid)
        {
            var service = _lineCheckAgent.getService(lid).FirstOrDefault();
            if (service == null || service.serviceInfo.FirstOrDefault() == null)
            {
                return null;
            }
            return DeviceMapper.MapServiceInfo(service);
        }

        public PingResult GetPingResult(string sik, string dslam, string port)
        {
            try
            {
                PingResult pingResult = new PingResult();
                int indexOfRole, indexOfIP = 0;
                var result = _lineCheckAgent.getPingResult(sik, "00", "00"); // Passing port and dslam as 00 - Week 23 release TP
                pingResult.resultPing = new List<pingData>();
                result.pakkerSendt = result.pakkerSendt != null ? result.pakkerSendt : new List<string>();
                foreach (var detail in result.pakkerSendt)
                {
                    indexOfRole = detail.IndexOf("Role:");
                    indexOfIP = detail.IndexOf("IP");

                    if (indexOfRole > 0 && indexOfIP > 0 && detail.Substring(indexOfRole, indexOfIP - indexOfRole).Split(':')[1].Trim().ToLower() == "gateway")
                    {
                        pingResult.resultPing.Insert(0, new pingData
                        {
                            Role = indexOfRole > 0 ? detail.Substring(indexOfRole, indexOfIP - indexOfRole).Split(':')[1].Trim() : "",
                            Message = indexOfRole > 0 ? detail.Substring(0, indexOfRole - 1).Trim() : detail,
                            WanIP = indexOfIP > 0 ? detail.Substring(indexOfIP).Split(':')[1].Trim() : "",
                        });
                    }
                    else
                    {

                        pingResult.resultPing.Add(new pingData
                        {
                            Role = indexOfRole > 0 ? detail.Substring(indexOfRole, indexOfIP - indexOfRole).Split(':')[1].Trim() : "",
                            Message = indexOfRole > 0 ? detail.Substring(0, indexOfRole - 1).Trim() : detail,
                            WanIP = indexOfIP > 0 ? detail.Substring(indexOfIP).Split(':')[1].Trim() : "",
                        });
                    }


                }
                pingResult.IsOkString = !string.IsNullOrEmpty(result.pingUdstyr) ? result.pingUdstyr : "";
                pingResult.IsOk = !string.IsNullOrEmpty(result.pingUdstyr) ? result.pingUdstyr.ToLower() == "ok" ? true : false : false;

                return pingResult;
            }
            catch (Exception)
            {
                return new PingResult
                {
                    IsOkString = "",
                    resultPing = new List<pingData>(),
                    IsOk = false,
                };

            }
        }

        public TPCIP.ToolPingWebPart.Domain.DslLineTestResult TestDslLine(string sik)
        {
            if (string.IsNullOrEmpty(sik)) return null;

            PingWebPartData.DslLineTestResult testResult = null;

            try { testResult = _lineCheckAgent.ihtsoaTestDslLine(sik); }
            catch { }
            return testResult == null ? null : DeviceMapper.MapDslTestResult(testResult);
        }

        public string GetCPEType(string customerId)
        {
            try
            {
                LineDiagnoseResult getdsl = _lineCheckAgent.getLineStateDiagnosticInformation(customerId);
                var cpeTypedata = getdsl.linestateDiagnose.lineConfigurationInfo.cpeType;
                return cpeTypedata;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public bool ReleaseIpSik(string Sik)
        {
            try
            {
                var data = _lineCheckAgent.releaseip(Sik);
                if (data.ToString() == string.Empty || data == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DslPingTestResult GetDslResult(string lid,string sik,string tpNo)
        {
            var response = new DslPingTest();
            var result = new DslPingTestResult();
            try {
                var data = _lineCheckAgent.getServiceResult(lid).FirstOrDefault();
                var tpData = data.services.FirstOrDefault();
                if (data == null || data.services.Count<=0)
                {
                    return null;
                }
                var domainData= DeviceMapper.MapServiceInfo(data);
                tpNo = domainData.SikDetails.Where(m => m.sik == sik).FirstOrDefault().tpNo.ToString();
                response = _OssiAgent.getDslResult(data.port,data.dslam, data.dslamCountry, tpNo, sik,sik);

            }
            catch (Exception ex) { }
            result = DeviceMapper.MapPingResult(response);
            return result;
        }


        public HybridIpPingResultInfo GetHybridPingInfo(string lid)
        {
            HybridLineInfo bcHybridlineInfo = new HybridLineInfo();
            bcHybridlineInfo = _OssiAgent.hybridLineInfo(lid);

            var hybridIpPingResultInfo = DeviceMapper.MapHybridIpPingInfo(bcHybridlineInfo);
            return hybridIpPingResultInfo;
        }
    }
}
