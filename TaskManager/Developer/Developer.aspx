<%@ Page Title="" Language="C#" MasterPageFile="~/Developer/Developer.Master" AutoEventWireup="true" CodeBehind="Developer.aspx.cs" Inherits="TaskManager.Developer.Developer1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>User
              <telerik:RadLabel runat="server" ID="userLbl" Text="username" />
    </h1>
    <telerik:RadDataForm RenderMode="Lightweight" ID="RadDataFormUser" OnNeedDataSource="RadDataFormUser_NeedDataSource" OnItemUpdating="RadDataFormUser_ItemEditing"
        DataKeyNames="userId" runat="server">
        <ItemTemplate>
            FirstName:  <%# Eval("firstname") %>
            <br />
            LastName: <%# Eval("lastname") %>
            <br />
            Email: <%# Eval("email") %>
            <telerik:RadButton RenderMode="Lightweight" ID="EditButton" runat="server" ButtonType="SkinnedButton" CausesValidation="False" CommandName="Edit" Text="Edit" ToolTip="Edit" />
        </ItemTemplate>
        <EditItemTemplate>
            Edit user
            <br />
            FirstName:
            <asp:TextBox ID="FirstNameTextBox" Text='<%# Bind("firstname") %>' runat="server" />
            <br />
            LastName:
            <asp:TextBox ID="LastNameTextBox" Text='<%# Bind("lastname") %>' runat="server" />
            <br />
            ContactName:
            <asp:TextBox ID="EmailTextBox" Text='<%# Bind("email") %>' runat="server" />
            <telerik:RadButton RenderMode="Lightweight" ID="UpdateButton" runat="server" ButtonType="SkinnedButton" CommandName="Update" Text="Update" ToolTip="Update" />
            <telerik:RadButton RenderMode="Lightweight" ID="CancelButton" runat="server" ButtonType="SkinnedButton" CausesValidation="False" CommandName="Cancel" Text="Cancel" ToolTip="Cancel" />
        </EditItemTemplate>
    </telerik:RadDataForm>
</asp:Content>