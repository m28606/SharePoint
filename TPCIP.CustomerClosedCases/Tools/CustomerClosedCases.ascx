<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerClosedCases.ascx.cs" Inherits="TPCIP.CustomerClosedCases.ControlTemplates.TPCIP.CustomerClosedCases.CustomerClosedCases" %>
<%@ Register Src="../IncludedControls/PaginatorClientSide.ascx" TagPrefix="uc1" TagName="PaginatorClientSide" %>
<%@ Register Src="../IncludedControls/UserControlHeader.ascx" TagPrefix="uc1" TagName="UserControlHeader" %>
<%@ Assembly Name="TPCIP.CommonTranslations,Version=1.0.0.0, Culture=neutral, PublicKeyToken=41aedb422eaf56b2" %>

<div class="panel panel-default">
    <uc1:UserControlHeader ID="UCToolHeader" runat="server" IconClass="" />

    <div class="Inner-pd-5 panel-content">
        <div class="CustomerClosedCases" id="ClosedCasesView" runat="server">

            <input type="hidden" class="fasErrorHtml" runat="server" id="fasErrorHtml" />
            <input type="hidden" class="etrayErrorHtml" runat="server" id="etrayErrorHtml" />
            <input type="hidden" class="bierErrorHtml" runat="server" id="bierErrorHtml" />
            <input type="hidden" class="columbusErrorHtml" runat="server" id="columbusErrorHtml" />

            <dl>
                  <dt class="closedCasesHeader" onclick="TdcCustomerOpenCases.ToggleConfigCategory(this)" style="border: 1px solid rgba(0, 94, 156, 0.42); display: <%=this.ColumbusCasesCount > 0 ? "block" : "none" %>;">
                    <label>
                        <h5>eOrdre/CU</h5>
                    </label>
                    <span class="<%=(this.ColumbusCasesCount > 0 ? "ion-chevron-up" : "ion-chevron-down") %> pull-right margin-to-arrow" style="margin-top:10px; margin-right: 6px;" id="pullUpDown"></span>
                    <span id="ltrcolumbuscount" class="pull-right mg-t-10 mg-r-40"><%= this.ColumbusCasesCount %></span>
                </dt>
                <dd class="closedCasesDetails configProductList list-hidden inner-mg-1" style="display: <%=this.ColumbusCasesCount > 0 ? "block" : "none" %>;">
                    <table class="table table-striped table-hover table-hover-pointer table-condensed CustomerClosedCases_Table">
                        <thead>
                            <tr>
                                <th>Dato</th>
                                <th>Ordrenummer</th>
                                <th><%= TPCIP.CommonTranslations.Translations.OrderType %></th>
                            </tr>
                        </thead>
                        <tbody class="columbusCasesTemplateContainer">
                        </tbody>
                    </table>
                </dd>

                <dt class="closedCasesHeader" onclick="TdcCustomerClosedCases.ToggleConfigCategory(this)" style="border: 1px solid rgba(0, 94, 156, 0.42); display: <%=this.EtrayCasesCount > 0 ? "block" : "none" %>;">
                    <label>
                        <h5>eTray</h5>
                    </label>
                    <span class="<%=(this.EtrayCasesCount > 0 ? "ion-chevron-up" : "ion-chevron-down") %> pull-right margin-to-arrow mg-t-10 mg-r-6" id="Span1"></span>
                    <span id="ltretraycount" class="pull-right  mg-t-10 mg-r-40"><%= this.EtrayCasesCount %></span>
                </dt>
                <dd class="closedCasesDetails configProductList list-hidden inner-mg-1" style="display: <%=this.EtrayCasesCount > 0 ? "block" : "none" %>;">
                    <table class="table table-striped table-hover table-hover-pointer table-condensed CustomerClosedCases_Table">
                        <thead>
                            <tr>
                                <th><%= TPCIP.CommonTranslations.Translations.Created %></th>
                                <th><%= TPCIP.CommonTranslations.Translations.Id %></th>
                                <th><%= TPCIP.CommonTranslations.Translations.Note %></th>
                            </tr>
                        </thead>
                        <tbody class="etrayCasesTemplateContainer">
                        </tbody>
                    </table>
                </dd>

                <dt class="closedCasesHeader" onclick="TdcCustomerClosedCases.ToggleConfigCategory(this)" style="border: 1px solid rgba(0, 94, 156, 0.42);display: <%=this.FasoCasesCount > 0 ? "block" : "none" %>;"> 

                    <label>
                        <h5>Fejlordre (FAS)</h5>
                    </label>

                    <span class="<%=(this.FasoCasesCount > 0 ? "ion-chevron-up" : "ion-chevron-down") %> pull-right margin-to-arrow mg-t-10 mg-r-6" id="Span2"></span>

                    <span id="ltrfasocount" class="pull-right mg-t-10 mg-r-40"><%= this.FasoCasesCount %></span>
                </dt>


                <dd class="closedCasesDetails configProductList list-hidden inner-mg-1" style="display: <%=this.FasoCasesCount > 0 ? "block" : "none" %>;">
                    <table class="table table-striped table-hover table-hover-pointer table-condensed CustomerClosedCases_Table">
                        <thead>
                            <tr>
                                <th><%= TPCIP.CommonTranslations.Translations.Created %></th>
                                <th><%= TPCIP.CommonTranslations.Translations.Id %></th>
                                <th><%= TPCIP.CommonTranslations.Translations.Note %></th>
                            </tr>
                        </thead>
                        <tbody class="fasoCasesTemplateContainer">
                        </tbody>
                    </table>
                </dd>

                <%--<dt class="closedCasesHeader" onclick="TdcCustomerClosedCases.ToggleConfigCategory(this)" style="border: 1px solid rgba(0, 94, 156, 0.42); display: <%=this.BierCasesCount > 0 ? "block" : "none" %>;">
                    <label>
                        <h5>Fejlordre (BIER)</h5>
                    </label>
                    <span class="<%=(this.BierCasesCount > 0 ? "ion-chevron-up" : "ion-chevron-down") %> pull-right margin-to-arrow" style="margin-top: 10px;  margin-right:6px;" id="Span3"></span>
                    <span id="ltrbiercount" class="pull-right" style="margin-right: 40px; margin-top: 10px;"><%= this.BierCasesCount %></span>
                </dt>
                <dd class="closedCasesDetails configProductList list-hidden" style="margin: 1px; display: <%=this.BierCasesCount > 0 ? "block" : "none" %>;">
                    <table class="table table-striped table-hover table-hover-pointer table-condensed CustomerClosedCases_Table">
                        <thead>
                            <tr>
                                <th><%= TPCIP.CommonTranslations.Translations.Created %></th>
                                <th><%= TPCIP.CommonTranslations.Translations.Id %></th>
                                <th><%= TPCIP.CommonTranslations.Translations.Note %></th>
                            </tr>
                        </thead>
                        <tbody class="bierCasesTemplateContainer">
                        </tbody>
                    </table>
                </dd>--%>  
                                          
            </dl>
        </div>
        <asp:Label ID="lblNoData" runat="server"><%= TPCIP.CommonTranslations.Translations.ClosedCases_NoCasesMessage %></asp:Label>

    </div>
</div>

<script id="closedCases" type="text/x-jsrender">
    <tr data-id="{{:Id}}" data-type="{{:Type}}" data-link="{{:Link}}" data-selectedlid="{{:SelectedLID}}" data-newlid="{{:NewLid}}" data-oldlid="{{:OldLid}}" data-kusagkd="{{:Kusagkd}}" data-note="{{:Note}}" data-transcode="{{:Transcode}}" data-ordertext="{{:OrderText}}" data-createddatetime="{{:CreatedDateTime}}" data-fakdate="{{:FakDate}}" data-cancellationdate="{{:CancellationDate}}"  data-orderdate="{{:OrderDate}}">
        <td>{{:Created}}</td>
        <td>{{:Id}}</td>
        <td class="caseNote wordBreakAll">{{:Note}}</td>
    </tr>
</script>
<script>
    $(".CustomerClosedCases .fasoCasesTemplateContainer").html($("#closedCases").render(<%= JsonHelper.Serialize(FasoCases) %>));
   <%-- $(".CustomerClosedCases .bierCasesTemplateContainer").html($("#closedCases").render(<%= JsonHelper.Serialize(BierCases) %>));--%>
    $(".CustomerClosedCases .etrayCasesTemplateContainer").html($("#closedCases").render(<%= JsonHelper.Serialize(EtrayCases) %>));
    $(".CustomerClosedCases .columbusCasesTemplateContainer").html($("#closedCases").render(<%= JsonHelper.Serialize(ColumbusCases) %>));
</script>

