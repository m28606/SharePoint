<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToolPingWebPart.ascx.cs" Inherits="TPCIP.ToolPingWebPart.ControlTemplates.TPCIP.ToolPingWebPart.ToolPingWebPart" %>
<%@ Register Src="../IncludedControls/UserControlHeader.ascx" TagPrefix="uc1" TagName="UserControlHeader" %>
<%@ Assembly Name="TPCIP.CommonTranslations,Version=1.0.0.0, Culture=neutral, PublicKeyToken=41aedb422eaf56b2" %>

<div class="webpart tool TdcPingWebPart" data-counter="0" id="toolPingWebPart" runat="server">
    <div class="panel guide-step-tool">
        <uc1:UserControlHeader runat="server" id="UCToolHeader" />
        <div class="panel-body panel-content">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding-bottom-5">
                    <b>Vælg det SIK du gerne vil pinge</b>
                </div>
                 <br/>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <ul class="nav nav-tabs" role="tablist" id="sikLst" data-ds='<%=JsonHelper.Serialize(sikLst)%>'>
                    </ul>
                </div>

            </div>
           
            <div class="row no-gutters">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 pd-t-20 topBorder">
                    <table id="dslResult" class="table table-condensed form-table-list customerInformationDetailsTable" data-dslresult='<%=JsonHelper.Serialize(objDslResult)%>'>
                      
                        <tbody id="sikResult" data-sikresult='<%=JsonHelper.Serialize(objSikResult)%>'>
                            <tr class="bottomBorder">
                                <td class="col-lg-6 col-md-6 col-sm-6 col-xs-6 topNoBorder" style="padding-left:20px">
                                     <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 fix-width-40 padding-left-0" >
                                    <img src="../../../_Layouts/Images/TPCIP.ToolPingWebPart/PingTime.svg" />
                                    </div>
                                     <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 padding-left-0">
                                    <label><b>Ping Tid</b></label>&nbsp
                                    <label id="pingTime"></label>
                                    </div>
                                </td>

                                <td class="col-lg-6 col-md-6 col-sm-6 col-xs-6 topNoBorder"  style="padding-left:20px">
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 fix-width-40 padding-left-0" >
                                    <img src="../../../_Layouts/Images/TPCIP.ToolPingWebPart/CPE.svg" />
                                    </div>
                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 padding-left-0">
                                    <label><b>CPE</b></label>&nbsp                             
                                     <label id="pingSingleResult"></label>
                                         </div>
                                </td>
                            </tr>

                        </tbody>
                    </table>
                </div>

                <br/>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <table class="table table-condensed form-table-list customerInformationDetailsTable">
                            <tbody id="pingResultContainer">
                            </tbody>
                        </table>
                        <table class="table table-condensed form-table-list customerInformationDetailsTable">
                            <tbody id="pingIPv6Container"></tbody>
                        </table>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <table class="table table-condensed form-table-list tableNoBorder">
                          
                        </table>
                    </div>
                </div>
                <div id="hybridPingResultInfo">
                    <table  class="table col-lg-12 col-md-12 col-sm-12 col-xs-12 customerInformationDetailsTable">
                        <tbody id="hybridResult" class="hybridPingResultContainer" data-hybridresult='<%=JsonHelper.Serialize(hybridPingResult)%>' ></tbody>
                    </table>
                </div>
            </div>
            <br />

            <div id="btnPingWebpart" class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                <div id="btnRelease" class="btn btn btn-primary btn-sm" onclick="TdcToolPingWebPart.release(this)">Frigiv IP</div>
                <div id="btnStart" class="btn btn btn-primary btn-sm" onclick="TdcToolPingWebPart.loadSikData(this,1)" >Start Ping Test</div>
            </div>
            
            <script id="pingResults" type="text/x-jsrender">      
                <tr>
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"></td>
                {{for Ipv4PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp leftBorder textAlignZero">
                     
                     <label><b>{{:IpType}}</b></label>  
                    
                </td>
                {{/for}}
                </tr>             
                <tr class="bgOffWhite">              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Ping Result</b></td>
                    {{for Ipv4PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero"  >
                     <label class="Lossperc"></label>
                     <label class="pingLossStatus{{:#index}}"></label>  
                     <label id="pingStatus{{:#index}}" class="hideCommDetails">{{:LossPct}}</label>
                    
                </td>
                {{/for}}
                </tr>
                <tr>              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Ip Type</b></td>
                    {{for Ipv4PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero"  >
                     
                     <label>IPv4</label>  
                    
                </td>
                {{/for}}
                </tr>
                <tr class="bgOffWhite">              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Pakketab</b></td>
                    {{for Ipv4PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center  virticalMarginImp textAlignZero"  >
                     
                     <label class="packageLoss">{{:LossPct}}</label>  
                    
                </td>
                {{/for}}
                </tr>
                <tr>              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Wan Ip</b></td>
                    {{for Ipv4PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero" >
                     
                     <label>{{:DstIp}}</label>  
                    
                </td>
                {{/for}}
                </tr>
                <tr class="bgOffWhite">              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Minimum Delay (MS)</b></td>
                    {{for Ipv4PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero" >
                     
                     <label>{{:MinDelayMs}}</label>  
                    
                </td>
                {{/for}}
                </tr>
                <tr>              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Gennemsnitligt Delay (MS)</b></td>
                    {{for Ipv4PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero" >
                     
                     <label>{{:AvgDelayMs}}</label>  
                    
                </td>
                {{/for}}
                </tr>
                <tr class="bottomBorder bgOffWhite">              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Maximum Delay (MS)</b></td>
                    {{for Ipv4PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero" >
                     
                     <label>{{:MaxDelayMs}}</label>  
                    
                </td>
                {{/for}}
                </tr>

            </script>

            <script id="pingIpv6"  type="text/x-jsrender">
                  <tr>
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"></td>
                {{for Ipv6PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center leftBorder virticalMarginImp textAlignZero" >
                     
                     <label><b>{{:IpType}}</b></label>  
                    
                </td>
                {{/for}}
                </tr>             
                 <tr class="bgOffWhite">              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Ping Result</b></td>
                    {{for Ipv6PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero"  >
                     <label class="Lossperc6"></label>
                     <label class="pingLossStatus6{{:#index}}"></label>  
                     <label id="pingIpv6Status{{:#index}}" class="hideCommDetails">{{:LossPct}}</label>
                    
                </td>
                {{/for}}
                </tr>
                   <tr>              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Ip Type</b></td>
                    {{for Ipv6PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero">
                     
                     <label>IPv6</label>  
                    
                </td>
                {{/for}}
                </tr>
                <tr class="bgOffWhite">              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Pakketab</b></td>
                    {{for Ipv6PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero" >
                     
                     <label class="packageLoss">{{:LossPct}}</label>  
                    
                </td>
                {{/for}}
                </tr>
                <tr>              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Wan Ip</b></td>
                    {{for Ipv6PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero">
                     
                     <label>{{:DstIp}}</label>  
                    
                </td>
                {{/for}}
                </tr>
                <tr class="bgOffWhite">              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Minimum Delay (MS)</b></td>
                    {{for Ipv6PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero" >
                     
                     <label>{{:MinDelayMs}}</label>  
                    
                </td>
                {{/for}}
                </tr>
                <tr>              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Gennemsnitligt Delay (MS)</b></td>
                    {{for Ipv6PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero"  >
                     
                     <label>{{:AvgDelayMs}}</label>  
                    
                </td>
                {{/for}}
                </tr>
                <tr class="bottomBorder bgOffWhite">              
                    <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 leftBorder virticalMarginImp"><b>Maximum Delay (MS)</b></td>
                    {{for Ipv6PingResult.IpResultList}}    
                <td  class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center virticalMarginImp textAlignZero" >
                     
                     <label>{{:MaxDelayMs}}</label>  
                    
                </td>
                {{/for}}
                </tr>

            </script>

             <script id="pingResultsimage" type="text/x-jsrender">           
                 {{: CepType}}   
             </script>
            <script id="pingResultHybrid" type="text/x-jsrender">
                <tr>
                    <td class="leftBorder virticalMarginImp" style="width: 50%;"></td>
                    <td class="leftBorder text-center virticalMarginImp"><b>CPE WAN</b></td>
                </tr>
                <tr class="bgOffWhite">
                    <td class="leftBorder virticalMarginImp" style="width: 50%;">
                        <b>IP type</b>
                    </td>
                    <td class="leftBorder text-center virticalMarginImp">{{:IPType}}</td>
                </tr>
                <tr class="bottomBorder" >
                    <td class="leftBorder virticalMarginImp" style="width: 50%;">
                        <b>IP connection status</b>
                    </td>
                    <td class="leftBorder text-center virticalMarginImp">{{:ConnectionStatus}}</td>
                </tr>
            </script>



        </div>
    </div>
</div>

