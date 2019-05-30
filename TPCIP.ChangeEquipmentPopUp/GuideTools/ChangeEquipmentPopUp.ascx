<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Src="../IncludedControls/UserControlHeader.ascx" TagPrefix="ucIp" TagName="UserControlHeader" %>
<%@ Register Src="../IncludedControls/MessageArea.ascx" TagPrefix="ucMsgArea" TagName="MessageArea" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeEquipmentPopUp.ascx.cs" Inherits="TPCIP.ChangeEquipmentPopUp.ControlTemplates.TPCIP.ChangeEquipmentPopUp.ChangeEquipmentPopUp" %>

<div class="TdcChangeEquipment" id="ChangeEquipmentWebpart">

                <div class="modal-body">
                    <div class="row" >
                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12">
                            <div id="FetchBtn" class="btn btn-primary btn-default pull-right" data-flag="OpdaterBtn" onclick="TdcChangeEquipment.showChangeEquipmentPopUp(this)">Opdater</div>
                        </div>
                    </div>
                    
                    <div class="row dataLoadEquipment" data-loadequipment="<%= Server.HtmlEncode(TPCIP.Web.AppCode.JsonHelper.Serialize(LoadEquipment)) %>">
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <label>Registreret modem MAC</label>
                            </small>
                        </div>
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <label class="RegesteredMac" id="RegesteredMac"></label>
                            </small>
                        </div>
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                        <small>
                            <input type="checkbox" class="chkRemoveEquipment" id="chkRegesteredMac" name="chkRegesteredMac" onchange="TdcChangeEquipment.onChkRegesteredMac(this)" value="chkRegesteredMac" />
                        </small>
                        </div>
      
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <label>Indtast ny modem MAC</label>
                            </small>
                        </div>
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <input class="newMac" id="newMac" style="width: 100%;" onkeyup="TdcChangeEquipment.ValidationNewMac(this)" />
                            </small>
                            <small class="row pull-right">
                                <label class="HelpText" style="margin-left: 9%">Kriterie for MAC: 12 karakterer, a-f eller 0-9</label>
                            </small>
                            <small class="row" id="InvalidMac" style="display:none;">
                                <label class="HelpText" style="color:red;margin-left: 8%;"> Kriterie for MAC ikke opfyldt</label>
                            </small>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <label>MTA MAC</label>
                            </small>
                        </div>
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <label class="MtaMac" id="MtaMac"></label>
                            </small>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <label>CPE</label>
                            </small>
                        </div>
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <select class="DdlCpeMac" onchange="TdcChangeEquipment.DropDownChangeEvent(this)" id="CpeMac" style="width:100%">
                                </select>
                            </small>
                            <small class="row pull-right" style="margin-left: 1%;">
                                <label class="HelpText">Vælg CPE MAC og benyt 'Skift udstyr' til at ændre</label>
                            </small>
                        </div>
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <input type="checkbox" class="chkRemoveEquipment" id="ChkCpeMac" name="ChkCpeMac" value="ChkCpeMac" />
                            </small>
                        </div>
                    </div>
                    <br/>
                    <div class="row" id="ExtraCpeVisible">
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <label>Ekstra CPE</label>
                            </small>
                        </div>
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <select class="DdlCpeMac" id="ExtraCpeMac" onchange="TdcChangeEquipment.DropDownChangeEvent(this)" style="width:100%">
                                </select>
                            </small>
                            <small class="row pull-right" style="margin-left: 1%;">
                                <label class="HelpText">Vælg ekstra CPE MAC ved behov og benyt 'Skift udstyr' til at ændre</label>
                            </small>
                        </div>
                        <div class="col-md-4 col-sm-4 col-xs-4  col-lg-4 ">
                            <small>
                                <input type="checkbox" class="chkRemoveEquipment" id="ChkExtraMac" name="ChkExtraMac" value="ChkExtraMac" />
                            </small>
                        </div>
                    </div>
                    <br />
                    <label class="HelpText divNone" id="AvailableMacAlert">Vær opmærksom på, at de valgte MAC adresser ikke bliver provisioneret, før du vælger 'Skift udstyr'</label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="TdcAsyncWebPartLoader.RefreshWP(this);">Annuller</button>
                    <button type="button" class="btn btn-primary" disabled="disabled" id="RemoveBtn" onclick="TdcChangeEquipment.showRemoveBtnPopUp(this)" >Fjern MAC</button>
                    <button type="button" class="btn btn-primary" id="UpdateBtn" onclick="TdcChangeEquipment.UpdateDataChangeEquipmentPopUp(this)" >Skift udstyr</button>
                </div>
                <ucMsgArea:MessageArea runat="server" class="popUpMsgArea" id="popUpMsgArea" />       

     <div class="modal" id="RemoveBtnPopUp" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="overflow:hidden;margin-top:35%;">
      <div class="modal-dialog">
        <div class="modal-content" style="width: 400px;">
            <div class="modal-header">
                <button type="button" class="close" aria-hidden="true" onclick="TdcChangeEquipment.OnClickCross(this)">&times;</button>
                <h4 class="modal-title highlight" id="H1">Fjern MAC</h4>
            </div>
            <div class="modal-body">
                <p>Du er ved at fjerne provisioneret udstyr fra kunden. Ønsker du at fortsætte?</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" onclick="TdcChangeEquipment.OnClickAnnuller(this)">Annuller</button>
                <button type="button" class="btn btn-primary" onclick="TdcChangeEquipment.OkClicked(this)">OK</button>
            </div>
        </div>
      </div>
     </div>
</div>