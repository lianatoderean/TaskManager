<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RolesPage.aspx.cs" Inherits="TaskManager.RolesPage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="height: 450px;">
        <h2>Admin Panel:</h2>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtrolename" runat="server"></asp:TextBox>
                    <asp:Button ID="btnCreateRole" runat="server" Text="CreateRole" OnClick="btnCreateRole_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>Available Users</td>
                            <td>Available Roles</td>
                        </tr>
                        <tr>
                            <td style="height: 72px">

                                <telerik:RadListBox ID="lstusers" runat="server" Height="150px" Width="120px" OnSelectedIndexChanged="lstusers_SelectedIndexChanged" AutoPostBack="true"></telerik:RadListBox>
                            </td>
                            <td style="height: 72px">
                                <telerik:RadListBox ID="lstRoles" runat="server" Height="150px" Width="120px"></telerik:RadListBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <telerik:RadLabel ID="RadLabel1" runat="server"></telerik:RadLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadPushButton ID="btnAssignRoleToUser" runat="server" Text="Assign Role To User" Width="250px" OnClick="btnAssignRoleToUser_Click"></telerik:RadPushButton>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadPushButton ID="btnRemoveUserFromUser" runat="server" Text="Remove User From Role" Width="250px" OnClick="btnRemoveUserFromUser_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadPushButton ID="btnRemoveRoles" runat="server" Text="Delete Roles" Width="250px" OnClick="btnRemoveRoles_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadLabel ID="Label1" runat="server"></telerik:RadLabel>
                </td>
            </tr>
        </table>
    </div>

    <!--
Url Rewriting
-->
</asp:Content>