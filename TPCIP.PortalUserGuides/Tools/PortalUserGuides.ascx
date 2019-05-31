<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortalUserGuides.ascx.cs" Inherits="TPCIP.PortalUserGuides.ControlTemplates.TPCIP.PortalUserGuides.PortalUserGuides" %>
<%@ Register Src="../IncludedControls/Paginator.ascx" TagPrefix="uc1" TagName="Paginator" %>
<%@ Assembly Name="TPCIP.CommonTranslations,Version=1.0.0.0, Culture=neutral, PublicKeyToken=41aedb422eaf56b2" %>

<div id="PortalUserGuidesWP" runat="server"
     class="webpart PortalUserGuidesWP" data-wpzone="#cip_body .contentParkedSessions">

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
                            <tr class="portalUserGuideItem"
                                <%-- onclick is in TdcPortalUserGuides js class--%>
                                data-noteid='<%# Eval("NoteId") %>'
                                data-customerid='<%# Eval("CustomerId") %>'
                                data-entitytitle='<%# Eval("EntityTitle") %>'
                                data-entityid='<%# Eval("EntityId") %>'
                                data-portalid='<%# Eval("PortalId") %>'
                                data-section='<%# Eval("Section") %>'
                                data-parentaccountno='<%# Eval("ParentAccountNumber") %>'
                                data-entitytype='<%# Eval("EntityType") %>'
                                data-isresumable='<%# Eval("IsResumable")%>'
                                data-sessionId='<%# Eval("GuideSessionId")%>'
                                data-userid='<%# Eval("UserId")%>'
                                data-stepid='<%# Eval("StepId")%>'
                                data-notetext='<%# Eval("NoteText")%>'
                                data-toolname='PortalUserGuides'>
                                <td><%# Eval("Date") %></td>
                                <td><%# Eval("CustomerName") %></td>
                                <td><%# Eval("CustomerId") %></td>
                                <td><%# Eval("Section") %></td>
                                <td><%# Eval("EntityTitle") %></td>
                                <td><i class='color-danger <%# Convert.ToBoolean(Eval("IsResumable")) == false ? "ion-alert" : ""  %>' ></i> </td>
                                <%--<td><%# Eval("StepName") %></td>--%>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            </table>
                        </FooterTemplate>
                </asp:Repeater>
            </div>

            <uc1:Paginator ID="paginator" runat="server" OnClientPageClick="TdcPortalUserGuides.paginatorClicked" />
            <asp:Label ID="lblNoSessions" runat="server" Visible="False"><%= TPCIP.CommonTranslations.Translations.There_are_no_sessions  %></asp:Label>
    </div><!--end of parked cases table -->
 </div>
