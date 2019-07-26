<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="TaskManager.Users" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="UsersList" runat="server" class="ContentHead">
        <h1>Users
        </h1>
    </div>

    <div class="demo-container no-bg">
        <telerik:RadGrid RenderMode="Lightweight" ID="RadGridUsers" runat="server" ShowStatusBar="true" AutoGenerateColumns="False"
            PageSize="7" AllowSorting="True" AllowMultiRowSelection="False" AllowPaging="True"
            OnDetailTableDataBind="RadGridUsers_DetailTableDataBind1" OnNeedDataSource="RadGridUsers_NeedDataSource"
            OnPreRender="RadGridUsers_PreRender">
            <PagerStyle Mode="NumericPages"></PagerStyle>
            <MasterTableView DataKeyNames="UserID" AllowMultiColumnSorting="True">
                <DetailTables>
                    <telerik:GridTableView DataKeyNames="ProjectID" Name="ProjectsDetails" Width="100%">
                        <DetailTables>
                            <telerik:GridTableView DataKeyNames="TaskID" Name="TasksDetails" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn SortExpression="TaskName" HeaderText="TaskName" HeaderButtonType="TextButton"
                                        DataField="name">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="TaskDescription" HeaderText="Task Description" HeaderButtonType="TextButton"
                                        DataField="description">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="TaskStatus" HeaderText="TaskStatus" HeaderButtonType="TextButton"
                                        DataField="status">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                        <Columns>
                            <telerik:GridBoundColumn SortExpression="ProjectName" HeaderText="ProjectName" HeaderButtonType="TextButton"
                                DataField="name">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="StartDate" HeaderText="Start Date" HeaderButtonType="TextButton"
                                DataField="StartDate" UniqueName="StartDate" DataType="System.DateTime">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="DueDate" HeaderText="Due Date" HeaderButtonType="TextButton"
                                DataField="DueDate" UniqueName="DueDate" DataType="System.DateTime">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="ProjectDescription" HeaderText="ProjectDescription" HeaderButtonType="TextButton"
                                DataField="description" UniqueName="ProjectDescription">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Owner" HeaderText="Owner" HeaderButtonType="TextButton"
                                DataField="owner" UniqueName="Owner">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
                <Columns>
                    <telerik:GridBoundColumn SortExpression="username" HeaderText="username" HeaderButtonType="TextButton"
                        DataField="Username">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="firstname" HeaderText="First Name" HeaderButtonType="TextButton"
                        DataField="FirstName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="lastname" HeaderText="Last Name" HeaderButtonType="TextButton"
                        DataField="LastName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="email" HeaderText="email" HeaderButtonType="TextButton"
                        DataField="email">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>