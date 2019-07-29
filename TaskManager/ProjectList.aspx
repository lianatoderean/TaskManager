<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectList.aspx.cs" Inherits="TaskManager.ProjectList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>PROJECTS</h1>

    <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxProject" runat="server" Height="200" Width="315"
        DropDownWidth="315" EmptyMessage="ProjectName" HighlightTemplatedItems="true"
        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="RadComboBoxProject_ItemsRequested"
        Label="Project: " OnSelectedIndexChanged="RadComboBoxProject_SelectedIndexChanged">
        <HeaderTemplate>
            Project Name
        </HeaderTemplate>
        <ItemTemplate>

            <%# DataBinder.Eval(Container, "Text")%>
        </ItemTemplate>
    </telerik:RadComboBox>

    <telerik:RadButton runat="server" ButtonType="StandardButton" CommandName="Search" OnClick="Search_Click" Text="Search"></telerik:RadButton>

    <telerik:RadGrid ID="RadProjectGrid" runat="server" AutoGenerateColumns="False" ShowHeader="true" OnItemCommand="RadProjectGrid_ItemCommand" OnUpdateCommand="RadProjectGrid_UpdateCommand" OnInsertCommand="RadProjectGrid_InsertCommand" OnDeleteCommand="RadProjectGrid_DeleteCommand" DataSourceID="projectSources" OnItemDataBound="RadProjectGrid_ItemDataBound">
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

        <MasterTableView ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top" Name="MasterTable" runat="server" EditMode="InPlace" DataKeyNames="projectId" DataSourceID="projectSources">
            <CommandItemSettings ShowAddNewRecordButton="true" AddNewRecordText="Add new project" />

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
                <telerik:GridTemplateColumn HeaderText="name" UniqueName="name">

                    <ItemTemplate>
                        <%# Eval("name") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadTextBox runat="server" ID="tbName" Text='<%# Eval("name") %>' />
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn HeaderText="startDate">
                    <ItemTemplate>
                        <%# Eval("startDate") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="RadDatePickerStartDate" runat="server" AutoPostBack="false" SelectedDate=' <%# Eval("startDate") %>'></telerik:RadDatePicker>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                    </InsertItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn HeaderText="dueDate">
                    <ItemTemplate>
                        <%# Eval("dueDate") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="RadDatePickerEndDate" runat="server" AutoPostBack="false" SelectedDate='<%# Eval("dueDate") %>'></telerik:RadDatePicker>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <telerik:RadDatePicker ID="RadDatePicker2" runat="server" AutoPostBack="false"></telerik:RadDatePicker>
                    </InsertItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn HeaderText="description">
                    <ItemTemplate>
                        <%#Eval("description") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadTextBox runat="server" ID="tbDescription" Text='<%# Eval("description") %>' />
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridEditCommandColumn HeaderText="Edit" ButtonType="PushButton" EditText="Edit" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn HeaderText="Delete" ButtonType="PushButton" Text="Delete" CommandName="Delete" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
            </Columns>
            <EditFormSettings>
                <EditColumn UniqueName="EditCommandColumn1" FilterControlAltText="Filter EditCommandColumn1 column"></EditColumn>
            </EditFormSettings>
        </MasterTableView>
    </telerik:RadGrid>

    <asp:SqlDataSource ID="dsOwner" runat="server" ConnectionString="<%$ ConnectionStrings:TaskContext %>" SelectCommand="SELECT username, userId FROM Users UNION SELECT 'Select user...' AS username, 0 as userId" />

    <asp:SqlDataSource ID="projectSources" runat="server" ConnectionString="<%$ ConnectionStrings:TaskContext %>"
        SelectCommand="SelectProject" SelectCommandType="StoredProcedure">

        <SelectParameters>
            <asp:ControlParameter Name="filter" ControlID="RadComboBoxProject" PropertyName="Text" DefaultValue="" ConvertEmptyStringToNull="false" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
