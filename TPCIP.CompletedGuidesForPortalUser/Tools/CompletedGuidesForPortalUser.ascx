<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompletedGuidesForPortalUser.ascx.cs" Inherits="TPCIP.Web.ControlTemplates.TPCIP.Web.Tools.CompletedGuidesForPortalUser" %>
<%@ Register TagPrefix="uc1" Src="../IncludedControls/Paginator.ascx" tagName="Paginator" %>
<%@ Assembly Name="TPCIP.CommonTranslations,Version=1.0.0.0, Culture=neutral, PublicKeyToken=41aedb422eaf56b2" %>

<div class="webpart completedGuidesForPortalUserWP" runat="server" id="completedGuidesForPortalUser" data-wpzone="#cip_body .contentCompletedSessions">
    <div class="panel panel-default">
        <div class="panel-heading">
            <span class="ion-ios7-albums-outline"></span>
            <small>Mine seneste sessioner</small>
        </div>
        <div class="padding-5 parked-text panel-content">
            <asp:Repeater ID="rptCompletedGuidesForPortalUser" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped table-hover table-datatable">
                        <thead>
                            <tr>
                                <th><%= TPCIP.CommonTranslations.Translations.Last_updated %></th>
                                <th><%= TPCIP.CommonTranslations.Translations.Customer %></th>
                                <th>LID</th>
                                <th><%= TPCIP.CommonTranslations.Translations.Area %></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr data-customerid='<%# Eval("CustomerId") %>'
                        data-noteid='<%# Eval("NoteId") %>' 
                        data-entitytitle='<%# Eval("EntityTitle") %>' 
                        data-entityid='<%# Eval("EntityId") %>' >
                        <td><%# Eval("Date") %></td>
                        <td><%# Eval("CustomerName") %></td>
                        <td><%# Eval("CustomerId") %></td>
                        <td><%# Eval("Section") %></td>
                        <td>
                            <div style="text-decoration: underline" class="completedGuidesForPortalUserClassLink"><%= TPCIP.CommonTranslations.Translations.View_customer %></div>
                        </td>
                        <td>
                           
                           <div class="showGuideForPortalUser" style='<%#Convert.ToString(Eval("NoteText"))=="Ingen note angivet"?"display:none;":"text-decoration:underline;"%>'>
                                <%= TPCIP.CommonTranslations.Translations.Show_guide %>
                           </div>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <uc1:Paginator ID="paginator" runat="server" OnClientPageClick="TdcCompletedGuidesForPortalUser.paginatorClicked" />
        <asp:Label ID="lblNoSessions" runat="server" Visible="False"><%= TPCIP.CommonTranslations.Translations.There_are_no_sessions  %></asp:Label>

    </div>

</div>
