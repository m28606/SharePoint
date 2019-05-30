using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TPCIP.ToolPingWebPart.DataModel;
using TPCIP.ToolPingWebPart.Domain;
using TPCIP.ToolBox;
using TPCIP.ToolBox.Portal;

namespace TPCIP.ToolPingWebPart
{
    public static class DeviceMapper
    {
        public static TPCIP.ToolPingWebPart.Domain.ServiceInfo MapServiceInfo(IhtsoaService service)
        {
            return new TPCIP.ToolPingWebPart.Domain.ServiceInfo
            {
                Dslam = service.dslam,
                Port = service.port,
                DslamCountry = service.dslamCountry,
                SikDetails = service.serviceInfo!=null? service.serviceInfo.Select(n => new ServiceInfoDetails
                {
                    serviceClosed = n.serviceClosed,
                    serviceSpeedDown = n.serviceSpeedDown,
                    serviceSpeedUp = n.serviceSpeedUp,
                    serviceType = n.serviceType,
                    sik = n.sik,
                    tpNo = n.tpNo,
                    urlMrtg = n.urlMrtg,
                    urlMtman = n.urlMtman,
                    urlTestman = n.urlTestman,
                    urlTvman = n.urlTvman,
                    urlWebman = n.urlWebman,

                }).ToList() : service.services!=null?service.services.Select(n => new ServiceInfoDetails
                {
                    serviceClosed = n.serviceClosed,
                    serviceSpeedDown = n.serviceSpeedDown,
                    serviceSpeedUp = n.serviceSpeedUp,
                    serviceType = n.serviceType,
                    sik = n.sik,
                    tpNo = n.tpNo,
                    urlMrtg = n.urlMrtg,
                    urlMtman = n.urlMtman,
                    urlTestman = n.urlTestman,
                    urlTvman = n.urlTvman,
                    urlWebman = n.urlWebman,

                }).ToList() : new List<ServiceInfoDetails>(),
            };
        }

        public static TPCIP.ToolPingWebPart.Domain.DslLineTestResult MapDslTestResult(TPCIP.ToolPingWebPart.DataModel.DslLineTestResult testResult)
        {
            return new TPCIP.ToolPingWebPart.Domain.DslLineTestResult
            {
                Equipment = testResult.equipment,
                IpAddress = testResult.ip,
                MacAddress = testResult.macAddress
            };
        }

        public static DslPingTestResult MapPingResult(DslPingTest response)
        {
            var data = new IpResultList();
            return new DslPingTestResult
            {
                PingTestResponse = response.pingTestResponse == null ? new PingTestResult() { } : new PingTestResult
                {
                    Statuscode = response.pingTestResponse.statuscode,
                    Statusmessage = response.pingTestResponse.statusmessage,
                    IpPingTestResponse = new IpPingTestResult
                    {
                        RawOutput = response.pingTestResponse.ipPingTestResponse.rawOutput,
                        Ipv4PingResult = response.pingTestResponse.ipPingTestResponse.ipv4PingResult == null ? new Ipv4PingResultDomain() : new Ipv4PingResultDomain
                        {
                            OptionalResult = response.pingTestResponse.ipPingTestResponse.ipv4PingResult.optionalResult,
                            Summary = response.pingTestResponse.ipPingTestResponse.ipv4PingResult.summary,
                            Version = response.pingTestResponse.ipPingTestResponse.ipv4PingResult.version,
                            IpResultList = response.pingTestResponse.ipPingTestResponse.ipv4PingResult.ipResult == null ? new List<IpResultListDomain>() : response.pingTestResponse.ipPingTestResponse.ipv4PingResult.ipResult.Select(result => new IpResultListDomain()
                            {
                                Dscp = string.IsNullOrEmpty(result.dscp) ? "" : result.dscp,
                                DstIp = string.IsNullOrEmpty(result.dstIp) ? "" : result.dstIp,
                                LossPct = string.IsNullOrEmpty(result.lossPct) ? "" : result.lossPct,
                                MaxDelayMs = string.IsNullOrEmpty(result.maxDelayMs) ? "" : result.maxDelayMs,
                                AvgDelayMs = string.IsNullOrEmpty(result.avgDelayMs) ? "" : result.avgDelayMs,
                                MinDelayMs = string.IsNullOrEmpty(result.minDelayMs) ? "" : result.minDelayMs,
                                IpType = string.IsNullOrEmpty(result.ipType) ? "" : result.ipType
                            }).ToList(),
                        },
                        Ipv6PingResult = response.pingTestResponse.ipPingTestResponse.ipv6PingResult == null?new Ipv4PingResultDomain() : new Ipv4PingResultDomain
                        {
                            OptionalResult = response.pingTestResponse.ipPingTestResponse.ipv6PingResult.optionalResult,
                            Summary = response.pingTestResponse.ipPingTestResponse.ipv6PingResult.summary,
                            Version = response.pingTestResponse.ipPingTestResponse.ipv6PingResult.version,
                            IpResultList = response.pingTestResponse.ipPingTestResponse.ipv6PingResult.ipResult == null ? new List<IpResultListDomain>() : response.pingTestResponse.ipPingTestResponse.ipv6PingResult.ipResult.Select(result => new IpResultListDomain()
                            {
                                Dscp = string.IsNullOrEmpty(result.dscp) ? "" : result.dscp,
                                DstIp = string.IsNullOrEmpty(result.dstIp) ? "" : result.dstIp,
                                LossPct = string.IsNullOrEmpty(result.lossPct) ? "" : result.lossPct,
                                MaxDelayMs = string.IsNullOrEmpty(result.maxDelayMs) ? "" : result.maxDelayMs,
                                AvgDelayMs = string.IsNullOrEmpty(result.avgDelayMs) ? "" : result.avgDelayMs,
                                MinDelayMs = string.IsNullOrEmpty(result.minDelayMs) ? "" : result.minDelayMs,
                                IpType = string.IsNullOrEmpty(result.ipType) ? "" : result.ipType
                            }).ToList(),
                        },

                        // Ipv6PingResult = new Ipv4PingResultDomain { }
                    }
                }
            };
        }

        public static HybridIpPingResultInfo MapHybridIpPingInfo(HybridLineInfo bcHybridlineInfo)
        {
            HybridIpPingResultInfo hybridIpPingResultInfo = new HybridIpPingResultInfo();


            string[] constIPs = {"195.249.192.141", "195.249.192.142", "2001:6c8:9:1::1", "2001:6c8:9:3::1" }; //these are constant IPs as per business confirmation.

            if (bcHybridlineInfo.networkStatus.Count!=0)
            {
                NetworkStatus networkStatus = new NetworkStatus();
                networkStatus = bcHybridlineInfo.networkStatus[0];
                WanAppConnection wanAppConnection = new WanAppConnection();
                if (networkStatus.wanAppConnection != null)
                    wanAppConnection = networkStatus.wanAppConnection;
                var bondingIp = string.Empty;
                if (!string.IsNullOrEmpty(networkStatus.bondingStatus.bondingServerIPAddress))
                    bondingIp = networkStatus.bondingStatus.bondingServerIPAddress.ToLower().Trim();

                if (wanAppConnection.ipV4Enable == "1")
                {
                    hybridIpPingResultInfo.IPType = "IPv4";
                    hybridIpPingResultInfo.ConnectionStatus = string.IsNullOrEmpty(wanAppConnection.connectionStatus) ? "-" : wanAppConnection.connectionStatus;
                    hybridIpPingResultInfo.ConnectionStatus += " ";
                    hybridIpPingResultInfo.ConnectionStatus += bondingIp == string.Empty ? "( - )" : bondingIp == constIPs[0] ? "(Slet)" : bondingIp == constIPs[1] ? "(Borups Alle)" : "( - )";
                }
                else if (wanAppConnection.ipV6Enable == "1")
                {
                    hybridIpPingResultInfo.IPType = "IPv6";
                    hybridIpPingResultInfo.ConnectionStatus = string.IsNullOrEmpty(wanAppConnection.ipV6ConnectionStatus) ? "-" : wanAppConnection.ipV6ConnectionStatus;
                    hybridIpPingResultInfo.ConnectionStatus += " ";
                    hybridIpPingResultInfo.ConnectionStatus +=bondingIp == string.Empty ? "( - )" : bondingIp == constIPs[2] ? "(Slet)" : bondingIp == constIPs[3] ? "(Borups Alle)" : "( - )";
                }
                else
                {
                    hybridIpPingResultInfo.IPType = "-";
                    hybridIpPingResultInfo.ConnectionStatus = "-";
                }



            }
            else
            {
                hybridIpPingResultInfo.IPType = "-";
                hybridIpPingResultInfo.ConnectionStatus = "-";
            }

            return hybridIpPingResultInfo;
            
        }
    
    }
}
