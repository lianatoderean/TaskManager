<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectTasks.aspx.cs" Inherits="TaskManager.ProjectTasks" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="projectTasks" runat="server" class="ContentHead">
        <h1>Task for project 
            <asp:Label runat="server" ID="lblProjectName" /></h1>
        <h2>
            Owner: 
            <asp:Label runat="server" ID="lblOwner" />
        </h2>
    </div>
       <div>

 
        <telerik:RadLabel runat="server" Text="Users" ID="rlUsersLbl"></telerik:RadLabel>   
        <telerik:RadListBox ID="RadListUsers" runat="server" RenderMode="Lightweight" AutoPostBack="true" OnSelectedIndexChanged="RadListUsers_SelectedIndexChanged" >
            
        </telerik:RadListBox>
                 <telerik:RadLabel runat="server" Text="ProjectMembers" ID="rlPrMembersLbl"></telerik:RadLabel>  
           <telerik:RadListBox ID="RadListProjectMembers" runat="server" RenderMode="Lightweight">

          </telerik:RadListBox>
        
        <telerik:RadLabel ID="drgLbl" Text="lbl" runat="server"></telerik:RadLabel>
        <telerik:RadButton runat="server" Text="AddMember" OnClick="addMember_Click" ButtonType="StandardButton" ></telerik:RadButton>
        <telerik:RadButton runat="server" Text="DeleteMember" OnClick="deleteMember_Click" ButtonType="StandardButton" ></telerik:RadButton>
        
           </div>
    <%--<asp:GridView runat="server" ID="tasksGrid" ItemType="TaskApp.Model.Task" DataKeyNames="taskId" SelectMethod="GetTasks" AutoGenerateColumns="false">
        
        <Columns>
            <asp:BoundField DataField="taskId" HeaderText="ID" />
            <asp:BoundField DataField="projectId" HeaderText="ProjectId" />
            <asp:BoundField DataField="name" HeaderText="name" />
            <asp:BoundField DataField="description" HeaderText="description" />
            <asp:BoundField DataField="status" HeaderText="status" />
        </Columns>
    </asp:GridView>--%>

    <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxTask" runat="server" Height="200" Width="315"
        DropDownWidth="315" EmptyMessage="Task" HighlightTemplatedItems="true"
        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="RadComboBoxTask_ItemsRequested"
        Label="Task: " OnSelectedIndexChanged="RadComboBoxTask_SelectedIndexChanged">
        <HeaderTemplate>
            Task Name                                   
        </HeaderTemplate>
        <ItemTemplate>

            <%# DataBinder.Eval(Container, "Text")%>
        </ItemTemplate>
    </telerik:RadComboBox>

    <telerik:RadButton runat="server" ButtonType="StandardButton" CommandName="Search" OnClick="Search_Click" Text="Search"></telerik:RadButton>

    <telerik:RadGrid ID="RadTaskGrid" runat="server" AutoGenerateColumns="False" ShowHeader="true" DataSourceID="taskSources" OnDeleteCommand="RadTaskGrid_DeleteCommand" OnItemCommand="RadTaskGrid_ItemCommand" AllowAutomaticUpdates="true" OnItemUpdated="RadTaskGrid_ItemUpdated" AllowAutomaticInserts="true">
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

        <MasterTableView ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top" Name="MasterTable" runat="server" EditMode="InPlace" DataKeyNames="taskId" DataSourceID="taskSources" >
            <CommandItemSettings ShowAddNewRecordButton="true" AddNewRecordText="Add new task" />
            
            <Columns>
                <telerik:GridButtonColumn UniqueName="SelectColumn" CommandName="Select" Text="See comments" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />

                <telerik:GridBoundColumn HeaderText="taskId" DataField="taskId" UniqueName="taskId" DataType="System.Int32" FilterControlAltText="Filter taskId column" ReadOnly="True" SortExpression="taskId" />
               <%-- <telerik:GridBoundColumn HeaderText="Assigned" DataField="Assigned" />--%>
                <telerik:GridTemplateColumn HeaderText="Assigned">
                    <ItemTemplate>
                        <%# Eval("Assigned") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" DataTextField="username" DataValueField="userId" SelectedValue='<%# Bind("userId") %>' DataSourceID="dsAssignee" ID="rcbAssigned" />
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="name">
                    <ItemTemplate>
                        <%# Eval("name") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadTextBox runat="server" ID="tbName" Text='<%# Bind("name") %>' />
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridBoundColumn HeaderText="description" DataField="description" FilterControlAltText="Filter description column" SortExpression="description" UniqueName="description" />
     <%--           <telerik:GridBoundColumn DataField="status" FilterControlAltText="Filter status column" HeaderText="status" SortExpression="status" UniqueName="status" />
    --%>           <telerik:GridTemplateColumn HeaderText="status">
                   <ItemTemplate>
                       <%# Eval("status") %>
                   </ItemTemplate>
                     <EditItemTemplate>
                        <telerik:RadComboBox runat="server" DataTextField="status" ID="rcbStatus" OnItemsRequested="RadComboBoxStatus_ItemsRequested" EnableLoadOnDemand="true" EmptyMessage='<%# Eval("status") %>' OnSelectedIndexChanged="RadComboBoxStatus_SelectedIndexChanged"/>
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

    <asp:SqlDataSource ID="dsAssignee" runat="server" ConnectionString="<%$ ConnectionStrings:TaskContext %>" SelectCommand="SELECT username, userId FROM Users UNION SELECT 'Select user...' AS username, 0 as userId" />   
   
    <asp:SqlDataSource ID="taskSources" runat="server" ConnectionString="<%$ ConnectionStrings:TaskContext %>" 
        DeleteCommand="DeleteTask" DeleteCommandType="StoredProcedure"
        InsertCommand="InsertTask" InsertCommandType="StoredProcedure"
        SelectCommand="TaskFilter" SelectCommandType="StoredProcedure" 
        UpdateCommand="UpdateTask" UpdateCommandType="StoredProcedure"  OnUpdating="taskSources_Updating"      >
        <DeleteParameters>
            <asp:Parameter Name="taskId" Type="Int32" />
        </DeleteParameters>
        
        <InsertParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="projectId" QueryStringField="projectId" Type="Int32" />
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="userId" Type="Int32" />
        </InsertParameters>
       
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="projectId" QueryStringField="projectId" Type="Int32" />
            <asp:ControlParameter Name="filter" ControlID="RadComboBoxTask" PropertyName="Text" DefaultValue = "" ConvertEmptyStringToNull = "false" />  
        </SelectParameters>
        
        <UpdateParameters>     
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="description" Type="String" />
<%--            <asp:Parameter Name="status" Type ="String" />--%>
            <asp:Parameter Name="taskId" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>


 
    
</asp:Content>




