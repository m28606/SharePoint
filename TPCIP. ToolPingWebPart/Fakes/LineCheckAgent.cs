using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.ToolPingWebPart.DataModel;
using TPCIP.CommonDataModel;

namespace TPCIP.ToolPingWebPart.Fakes
{
    public class LineCheckAgent : ILineCheckAgent,IOssiAgent
    {
        public virtual PingResultDataModel getPingResult(string sik, string dslam, string port)
        {
            if (new Random().Next(100) % 2 == 0)
            {
                return
                    new PingResultDataModel
                    {
                        pingUdstyr = "ikke ok",
                        pakkerSendt = new List<string> {
                            "3 og pakker modtaget: 3. Tabt: 0% Role: CPE Wan IP: 195.215.97.154",
                            "3 og pakker modtaget: 3. Tabt: 100% Role: LAN IP: 10.92.168.1",
                            "3 og pakker modtaget: 3. Tabt: 0% Role: GateWay IP: 10.198.2.157",
                        }
                    };
            }
            else
            {
                return
                    new PingResultDataModel
                    {
                        pingUdstyr = "OK",
                        pakkerSendt = new List<string> {
                         "3 og pakker modtaget: 3. Tabt: 0%",
                        //"3 og pakker modtaget: 3. Tabt: 100% Role: PE WAN IP: 10.198.2.157",
                        //"3 og pakker modtaget: 3. Tabt: 0% Role: LAN IP: 10.92.168.1",
                    }
                    };
            }

        }

        public virtual List<IhtsoaService> getService(string lid)
        {
            return new List<IhtsoaService>
            {
                new IhtsoaService
                {
                    dslam = "aptxda1",
                    port = "636",
                    lid = lid,
                    serviceInfo = new List<ServiceInfo>
                    {
                          new ServiceInfo
                        {
                            serviceType="Management Channel",
                            sik = "B4146112",
                            urlMrtg = "/_Layouts/Images/TPCIP.Web/chart.jpg",
                            tpNo = "7"
                        },
                        new ServiceInfo
                        {
                            serviceType="DATA",
                            sik = "B4146275",
                            urlMrtg = "/_Layouts/Images/TPCIP.Web/chart.jpg",
                            tpNo = "17"
                        },
                         new ServiceInfo
                        {
                            serviceType="Voice",
                            sik = "B4146111",
                            urlMrtg = "/_Layouts/Images/TPCIP.Web/chart.jpg",
                            tpNo = "16"
                        }
                    }
                }
            };
        }

        public virtual List<IhtsoaService> getServiceResult(string lid)
        {
            return new List<IhtsoaService>
            {
                new IhtsoaService
                {
                    dslam = "aptxda1",
                    port = "636",
                    lid = lid,
                    services = new List<ServiceInfo>
                    {
                          new ServiceInfo
                        {
                            serviceType="Management Channel",
                            sik = "B4146112",
                            urlMrtg = "/_Layouts/Images/TPCIP.Web/chart.jpg",
                            tpNo = "7"
                        },
                        new ServiceInfo
                        {
                            serviceType="DATA",
                            sik = "B4146275",
                            urlMrtg = "/_Layouts/Images/TPCIP.Web/chart.jpg",
                            tpNo = "17"
                        },
                         new ServiceInfo
                        {
                            serviceType="Voice",
                            sik = "B4146111",
                            urlMrtg = "/_Layouts/Images/TPCIP.Web/chart.jpg",
                            tpNo = "16"
                        }
                    }
                }
            };
        }

        public virtual DslLineTestResult ihtsoaTestDslLine(string sik)
        {
            return new DslLineTestResult
            {
                equipment = "*equipment",
                ip = "173.194.33.130",
                macAddress = "AA:AA:AA:AA:AA:AA"
            };
        }

        public virtual LineDiagnoseResult getLineStateDiagnosticInformation(string lid)
        {
            Random rdm = new Random();

            return new LineDiagnoseResult
            {
                linestateDiagnose = new LineStateDiagnose
                {
                    //cpeType = "TDC HomeBox IV AC",
                    //estimatedDELTDistance = null,
                    //dslType = rdm.Next(0, 5) > 2 ? "non.shdsl.opermode vdsl2.g9932.profile.17a, line.type.pots" : "bonding.opermode.moreports technology.type.vdsl2, 2",
                    //dslType = "bonding.opermode.moreports technology.type.vdsl2, 2",
                    //lineUp = "true",
                    classificationHistoryImageUrls = new string[] 
                    {
                        "../Images/TPCIP.Web/NetworkAnalyzer/nas_history_plot.jpg",
                        "../Images/TPCIP.Web/NetworkAnalyzer/fifteen_min_interval_history_plot.jpg",
                    },
                    diagnosisResult = new[]
                    {
                        new Problem
                        {
                            location="Location1 Details are here",
                            description="Description1 about the problem for this particular location can be populated here. It can be a long description, and might alos span number of lines. There is not restriction here",
                            confidence=324
                        },
                        new Problem
                        {
                            location="Location2 Details are here",
                            description="Description2 about the problem for this particular location can be populated here. It can be a long description, and might alos span number of lines. There is not restriction here",
                            confidence=67
                        },
                        new Problem
                        {
                            location="Location2 Details are here",
                            description="Description3 about the problem for this particular location can be populated here. It can be a long description, and might alos span number of lines. There is not restriction here",
                            confidence=234
                        },
                    },
                    lineOperationalInfo = new LineOperationalInfo
                    {
                        lineUp = false,
                        estimatedDELTDistance = "1215",

                        bondingGroupLinkStatus = new[] 
                        {
                    
                      new GroupLinks
                       {                           
                           address="AR21XDA1.DSL.TELE.DK:1-1-3-22"
                       },
                       new GroupLinks
                       {                           
                            address="AR21XDA1.DSL.TELE.DK:1-1-3-21"
                       }
                        }
                    },
                    lineConfigurationInfo = new LineConfigurationInfo
                    {
                        cpeType = lid == "EM137716" ? "Hybrid" : "TDC HomeBox IV AC",
                        //dslType = rdm.Next(0, 5) > 2 ? "non.shdsl.opermode vdsl2.g9932.profile.17a, line.type.pots" : "bonding.opermode.moreports technology.type.vdsl2, 2"
                        dslType = rdm.Next(0, 5) > 2 ? "non.shdsl.opermode vdsl2.g9932.profile.17a, line.type.pots" : rdm.Next(0, 5) > 2 ? "bonding.opermode.moreports technology.type.vdsl2, 2" : "g.shdsl.regionalsettings.wiretype shdsl.g9912.ab, eight.wire"
                    },
                    shdslLineEstimatedDistance = new[] 
                    {
                      new Distance
                     {                           
                         distance= 1215, 
                         wirePair=1,
                     },
                     new Distance
                     {                           
                         distance= 1325,
                         wirePair=2,
                     },
                     new Distance
                     {                           
                         distance= 1326,
                         wirePair=3,
                     },
                     new Distance
                     {                           
                         distance= 1541,
                         wirePair=4,
                     }
                    },
                    lineParameters = new[]
                    {                      
                      new LineParameter
                       {                           
                         name="lqd.parameter.actual.bitrate" , 
                         unit="kb/s",
                         upstreamValue="4224.0",
                         downstreamValue="4224.0",
                       },
                       new LineParameter
                       {                           
                        name="lqd.parameter.actual.bitrate.first.pair" , 
                         unit="kb/s",
                         upstreamValue="1408.0",
                         downstreamValue="1408.0",    
                       }, 
                       new LineParameter
                       {                           
                         name="lqd.parameter.actual.bitrate.second.pair.wire.pair.2" , 
                         unit="kb/s",
                         upstreamValue="1408.0",
                         downstreamValue="1408.0",  
                       },
                       new LineParameter
                       {                           
                         name="lqd.parameter.actual.bitrate.second.pair.wire.pair.3" , 
                         unit="kb/s",
                         upstreamValue="1408.0",
                         downstreamValue="1408.0",  
                       }
                    }
                },
            };
        }

        public virtual SimpleResult<string> releaseip(string sik)
        {
            return new SimpleResult<string>() { value = "Some result" };
        }

        public virtual DslPingTest getDslResult(string port, string dslam, string country, string tp, string sik, string channelId)
        {
            return new DslPingTest
            {
                pingTestResponse = new PingTestResponse
                {
                    statuscode = "0",
                    statusmessage = "ok",
                    ipPingTestResponse = new IpPingTestResponse
                    {
                        rawOutput = "",
                        ipv6PingResult = new Ipv4PingResult{
                            summary = "ok",
                            version = "4",
                            optionalResult = "No packet loss at all",
                            ipResult = new List<IpResultList>(){
                            new IpResultList{
                                dscp = "-",
                                dstIp = "195.215.97.158",
                                lossPct = "0.0 (OK)",
                                maxDelayMs = "22.165",
                                minDelayMs = "21.535",
                                avgDelayMs = "21.912",
                                ipType = "CPE WAN"
                            },
                             new IpResultList{
                                dscp = "-",
                                dstIp = "195.215.97.158",
                                lossPct = "100.0 (OK)",
                                maxDelayMs = "22.165",
                                minDelayMs = "21.535",
                                avgDelayMs = "21.912",
                                ipType = "GateWay"
                            },
                             new IpResultList{
                                dscp = "-",
                                dstIp = "195.215.97.158",
                                lossPct = "10.0 (OK)",
                                maxDelayMs = "22.165",
                                minDelayMs = "21.535",
                                avgDelayMs = "21.912",
                                ipType = "LAN"
                            }
                            }
                            
                        },
                        ipv4PingResult = new Ipv4PingResult
                        {
                            summary = "ok",
                            version = "4",
                            optionalResult = "No packet loss at all",
                            ipResult = new List<IpResultList>(){
                            new IpResultList{
                                dscp = "-",
                                dstIp = "195.215.97.158",
                                lossPct = "0.0 (OK)",
                                maxDelayMs = "22.165",
                                minDelayMs = "21.535",
                                avgDelayMs = "21.912",
                                ipType = "CPE WAN"
                            },
                             new IpResultList{
                                dscp = "-",
                                dstIp = "195.215.97.158",
                                lossPct = "100.0 (OK)",
                                maxDelayMs = "22.165",
                                minDelayMs = "21.535",
                                avgDelayMs = "21.912",
                                ipType = "GateWay"
                            },
                             new IpResultList{
                                dscp = "-",
                                dstIp = "195.215.97.158",
                                lossPct = "10.0 (OK)",
                                maxDelayMs = "22.165",
                                minDelayMs = "21.535",
                                avgDelayMs = "21.912",
                                ipType = "LAN"
                            }
                            }
                            
                        }
                    }
                }

            };
        }

        public virtual HybridLineInfo hybridLineInfo(string id)
        {
            return new HybridLineInfo()
            {
                networkStatus = new List<NetworkStatus>()
                {
                    new NetworkStatus()
                    {
                        wanUUmtsInterfaceConfig=new WanUUmtsInterfaceConfig()
                        {
                          simStatus= "Valid",
                          serviceStatus = "AvailableService",
                          systemMode= "LTE",
                          ltebandValue= "Band3(1800MHz)",
                          signalQuality= "54",
                          downLoadData= "28 MB",
                          upLoadData= "8 MB",
                          antennaSet= "Auto"
                        },
                        wanAppConnection=new WanAppConnection()
                        {
                            connectionStatus= "Connected",
                            externalIPAddress= "80.198.32.170",
                            ipV4Enable= "1",
                            ipV6Enable= "0",
                            ipV6ConnectionStatus= "Disconnected",
                            ipV6Address= "/0"
                        },
                        bondingStatus=new BondingStatus()
                        {
                            bondingStatus= "Offline",
                            bondingMode= "NonTunnel",
                            bondingServerName= "195.249.192.142",
                            bondingServerIPAddress= "195.249.192.142",
                            username= "YC140639"
                        },
                        simCardInfo=new SimCardInfo()
                        {
                            iccid= "89450130141203678277",
                            imei= "868761020004907",
                            imsi= "238010156423920"
                        }
                    }
                }
            };
        }
    }
}
