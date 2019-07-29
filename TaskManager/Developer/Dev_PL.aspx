<%@ Page Title="" Language="C#" MasterPageFile="~/Developer/Developer.Master" AutoEventWireup="true" CodeBehind="Dev_PL.aspx.cs" Inherits="TaskManager.Developer.Dev_PL" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Projects for user:
            <telerik:RadLabel ID="UserLbl1" runat="server"></telerik:RadLabel>
        </h1>
    </div>

    <div>
        <telerik:RadGrid ID="RadDevProjectGrid" runat="server" AutoGenerateColumns="False" ShowHeader="true" OnNeedDataSource="RadDevProjectGrid_NeedDataSource" OnItemCommand="RadDevProjectGrid_ItemCommand">
            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

            <MasterTableView ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top" Name="MasterTable" runat="server" EditMode="InPlace" DataKeyNames="projectId">
                <CommandItemSettings ShowAddNewRecordButton="false" />

                <Columns>
                    <telerik:GridButtonColumn UniqueName="SelectColumn" CommandName="Select" Text="See project" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />

                    <telerik:GridBoundColumn HeaderText="projectId" DataField="projectId" UniqueName="projectId" DataType="System.Int32" FilterControlAltText="Filter projectId column" ReadOnly="True" SortExpression="projectId" />

                    <telerik:GridTemplateColumn HeaderText="Owner">
                        <ItemTemplate>
                            <%# Eval("Owner") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadComboBox runat="server" DataTextField="username" DataValueField="userId" SelectedValue='<%# Eval("ownerId") %>' DataSourceID="dsOwner" ID="rcbOwner" />
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="name">

                        <ItemTemplate>
                            <%# Eval("name") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox runat="server" ID="tbName" Text='<%# Eval("name") %>' />
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridBoundColumn HeaderText="startDate" DataField="startDate" FilterControlAltText="Filter startdate column" SortExpression="startDate" UniqueName="startDate" />
                    <telerik:GridBoundColumn HeaderText="dueDate" DataField="dueDate" FilterControlAltText="Filter duedate column" SortExpression="dueDate" UniqueName="dueDate" />
                    <telerik:GridBoundColumn HeaderText="description" DataField="description" FilterControlAltText="Filter description column" SortExpression="description" UniqueName="description" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>