<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActiveUserGuides.ascx.cs" Inherits="TPCIP.ActiveUserGuides.ControlTemplates.TPCIP.ActiveUserGuides.ActiveUserGuides" %>
<%@ Register Src="../IncludedControls/Paginator.ascx" TagPrefix="uc1" TagName="Paginator" %>
<%@ Assembly Name="TPCIP.CommonTranslations,Version=1.0.0.0, Culture=neutral, PublicKeyToken=41aedb422eaf56b2" %>

<div id="ActiveUserGuidesWP" runat="server"
     class="webpart ActiveUserGuidesWP" data-wpzone="#cip_body .contentParkedSessions">

        <div class="panel panel-default">
            <div class="panel-heading">
                <span class="ion-ios7-albums-outline"></span>
                <small><asp:Literal ID="title" runat="server" /></small>
            </div>

            <div class="padding-5 parked-text panel-content">
                <asp:Repeater ID="repeater1" runat="server">
                    <HeaderTemplate>
                            <table class="table table-striped table-hover table-hover-pointer table-datatable">
                                <thead>
                                    <tr>
                                        <th><%= TPCIP.CommonTranslations.Translations.Last_updated %></th>
                                        <th><%= TPCIP.CommonTranslations.Translations.Customer %></th>
                                        <th>LID</th>
                                        <th><%= TPCIP.CommonTranslations.Translations.Area %></th>
                                        <th><%= TPCIP.CommonTranslations.Translations.Guides %></th>
                                        <th></th>
                                        <%--<th><%= TPCIP.Web.GlobalResources.Translations.Current_Step %></th>--%>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>

                        <ItemTemplate>
                            <tr class="activeUserGuideItem"
                                <%-- onclick is in TdcPortalUserGuides js class--%>
                                data-noteid='<%# Eval("NoteId") %>'
                                data-customerid='<%# Eval("CustomerId") %>'
                                data-entitytitle='<%# Eval("EntityTitle") %>'
                                data-entityid='<%# Eval("EntityId") %>'
                                data-portalid="<%# Eval("PortalId") %>"
                                data-section='<%# Eval("Section") %>'
                                data-parentAccountNo='<%# Eval("ParentAccountNumber") %>'
                                data-entityType='<%# Eval("EntityType") %>'
                                data-isresumable='<%# Eval("IsResumable")%>'
                                data-sessionId='<%# Eval("GuideSessionId")%>'
                                data-userid='<%# Eval("UserId")%>'
                                data-stepid='<%# Eval("StepId")%>'
                                data-notetext='<%# Eval("NoteText")%>'
                                data-toolName='ActiveUserGuides'>
                                <td><%# Eval("Date") %></td>
                                <td><%# Eval("CustomerName") %></td>
                                <td><%# Eval("CustomerId") %></td>
                                <td><%# Eval("Section") %></td>
                                <td><%# Eval("EntityTitle") %></td>
                                <td><i class='<%# Convert.ToBoolean(Eval("IsResumable")) == false ? "ion-alert" : ""  %>' style='color:red;' ></i> </td>
                                <%--<td><%# Eval("StepName") %></td>--%>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            </table>
                        </FooterTemplate>
                </asp:Repeater>
            </div>

            <uc1:Paginator ID="paginator" runat="server" OnClientPageClick="TdcActiveUserGuides.paginatorClicked" />
            <asp:Label ID="lblNoSessions" runat="server" Visible="False"><%= TPCIP.CommonTranslations.Translations.There_are_no_sessions  %></asp:Label>
    </div><!--end of parked cases table -->
 </div>

