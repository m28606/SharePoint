<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToolCustomerClosedCasesDetails.ascx.cs" Inherits="TPCIP.Web.ControlTemplates.TPCIP.Web.GuideTools.ToolCustomerClosedCasesDetails" %>
<%@ Register Src="../IncludedControls/MessageArea.ascx" TagPrefix="ucMsgArea" TagName="MessageArea" %>
<%@ Register Src="../IncludedControls/UserControlHeader.ascx" TagPrefix="ucIp" TagName="UserControlHeader" %>



<div class="tool webpart ToolCustomerClosedCasesDetails">
  <div class="panel panel-default">


          <div class="row">
            <div class="col col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <div data-toggle="collapse" data-target="#columbus" class="btn SpeedCalculatorheader SpdCalcAddLookUp" style="white-space: normal;">
                    <span class="ion-chevron-up pull-right margin-to-arrow" data-alternateicon="ion-chevron-down pull-right margin-to-arrow"></span>
                    <span class="pull-left">Lukkede ordre</span>
                </div>
            </div>
        </div>
        <div id="columbus" style="border: 1px solid #399aad;" class="collapse in">
            <div class="padding-1 panel-content col col-xs-12 col-sm-12 col-md-12 col-lg-12" style="height: auto; margin-bottom: 20px;">
                <div id="Div1" class="ColumbusDetails" runat="server" style="margin-left: -5px;">
                    <table class="table table-striped table-hover table-hover-pointer table-condensed">
                        <thead>
                            <tr>
                                <th class="col col-xs-2 col-sm-2 col-md-2 col-lg-2">Dato</th>
                                <th class="col col-xs-2 col-sm-2 col-md-2 col-lg-2">Ordrenummer</th>
                                <th class="col col-xs-2 col-sm-2 col-md-2 col-lg-2">LID</th>
                                <th class="col col-xs-2 col-sm-2 col-md-2 col-lg-2">Tidligere</th>
                                <th class="col col-xs-2 col-sm-2 col-md-2 col-lg-2">Bevægelse</th>
                                <th class="col col-xs-2 col-sm-2 col-md-2 col-lg-2">Ordretekst</th>
                                <th class="col col-xs-2 col-sm-2 col-md-2 col-lg-2">Kundetekst</th>
                            </tr>
                        </thead>
                        <tbody class="columbusDetails">
                            <tr>
                                <td class="col col-xs-2 col-sm-2 col-md-2 col-lg-2" id="orderDate"></td>
                                <td class="col col-xs-2 col-sm-2 col-md-2 col-lg-2" id="orderNo"></td>
                                <td class="col col-xs-2 col-sm-2 col-md-2 col-lg-2" id="newLid"></td>
                                <td class="col col-xs-2 col-sm-2 col-md-2 col-lg-2" id="oldLid"></td>
                                <td class="col col-xs-2 col-sm-2 col-md-2 col-lg-2" id="transcode"></td>
                                <td class="col col-xs-2 col-sm-2 col-md-2 col-lg-2" id="orderText" style="word-break: break-all"></td>
                                <td class="col col-xs-2 col-sm-2 col-md-2 col-lg-2" id="customerCaseText" style="word-break: break-all"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>
