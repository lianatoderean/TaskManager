using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TaskManager.Model;
using Telerik.Web.UI;

namespace TaskManager.Admin
{
    public partial class Admin_PT : System.Web.UI.Page
    {
        private String filter = "";
        private int? projectID;
        // protected String ProjectName = "";
        TaskManager.Model.TaskContext _db = new TaskContext();
        private String status = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["projectId"] != null)
            {
                projectID = Convert.ToInt32(Request.QueryString["projectId"]);
            }
            if (projectID != null)
            {
                lblProjectName.Text = _db.Projects.Where(p => p.projectId == projectID).FirstOrDefault().name;
                var ownerId = _db.Projects.Where(p => p.projectId == projectID).FirstOrDefault().ownerId;
                lblOwner.Text = _db.Users.Where(u => u.userId == ownerId).FirstOrDefault().username;
            }


            if (!IsPostBack)
            {
                var prMId = _db.ProjectMembers.Where(pm => pm.projectId == projectID).Select(u => u.userId).ToList();
                var uPrM = _db.Users.Where(u => prMId.Contains(u.userId)).ToList();
                var us = _db.Users.Where(u => (!prMId.Contains(u.userId))).ToList();

                RadListUsers.DataSource = us;
                RadListProjectMembers.DataSource = uPrM;

                RadListUsers.DataTextField = "username";
                RadListUsers.DataValueField = "userId";
                RadListUsers.DataBind();

                RadListProjectMembers.DataTextField = "username";
                RadListProjectMembers.DataValueField = "userId";
                RadListProjectMembers.DataBind();


            }





        }

        protected override void OnInitComplete(EventArgs e)
        {
            //base.OnInitComplete(e);
            if (IsPostBack)
            {
                //Mapper.CreateMap<TaskApp.Model.Task, TasksForViewDTO>()
                //   .ForMember(des => des.taskId, opt => opt.MapFrom(p => p.taskId))
                //   .ForMember(des => des.taskName, opt => opt.MapFrom(p => p.name))
                //   .ForMember(des => des.description, opt => opt.MapFrom(p => p.description))
                //   .ForMember(des => des.status, opt => opt.MapFrom(p => p.status));


                //Mapper.CreateMap<TaskApp.Model.UsersTask, TasksForViewDTO>()
                //.ForMember(des => des.assignedUser, opt => opt.MapFrom(p => p.userId));
                //Mapper.CreateMap<TaskApp.Model.User, TasksForViewDTO>()
                // .ForMember(des => des.assignedUser, opt => opt.MapFrom(p => p.username));


            }
        }



        protected void RadTaskGrid_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditFormItem item = e.Item as GridEditFormItem;
            RadComboBox rcbAssigned = item.FindControl("rcbAssigned") as RadComboBox;

            int taskID = Convert.ToInt32(item.GetDataKeyValue("taskId"));
            var task = _db.Tasks.Where(p => p.taskId == taskID).FirstOrDefault();


            task.name = (item["name"].Controls[0] as TextBox).Text;
            task.description = (item["description"].Controls[0] as TextBox).Text;

            var userTasks = _db.UsersTasks.Where(p => p.taskId == taskID).FirstOrDefault();
            userTasks.userId = Convert.ToInt32(rcbAssigned.SelectedValue);

            _db.SaveChanges();
        }


        //protected void AddButton_Click(object sender, EventArgs e)
        //{
        //    String name = NameTextBox.Text;
        //    String description = DescriptionTextBox.Text;

        //    _db.Tasks.Add(new Task(projectID.Value, name, description, "new"));
        //    _db.SaveChanges();

        //    //RadTaskGrid.DataSource = _db.Tasks.Where(p => p.projectId == projectID).ToList();
        //    RadTaskGrid.Rebind();
        //    Response.Redirect(Request.Url.AbsoluteUri);


        //}

        protected void RadTaskGrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {

            GridDataItem item = e.Item as GridDataItem;

            var taskID = Convert.ToInt32(item.GetDataKeyValue("taskId"));



            var tasks = _db.Tasks.Where(p => p.taskId == taskID);
            if (tasks != null)
            {
                var userstasks = _db.UsersTasks.Where(p => p.taskId == taskID).FirstOrDefault();
                if (userstasks != null)
                    _db.UsersTasks.Remove(_db.UsersTasks.Where(p => p.taskId == taskID).FirstOrDefault());


                _db.Tasks.Remove(_db.Tasks.Where(p => p.taskId == taskID).FirstOrDefault());
            }
            _db.SaveChanges();
            RadTaskGrid.Rebind();

        }

        protected void RadTaskGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //if (filter == "")
            //    RadTaskGrid.DataSource = GetTasks(Convert.ToInt32(Request.QueryString["projectId"]));
            //else
            //    RadTaskGrid.DataSource = GetTasks(Convert.ToInt32(Request.QueryString["projectId"])).FindAll(p => p.taskName == filter).ToList();


        }

        protected void RadComboBoxTask_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IQueryable<Task> tasks = (IQueryable<Task>)_db.Tasks.Where(p => p.projectId == projectID);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();

            foreach (Task t in tasks)
            {

                RadComboBoxItem item = new RadComboBoxItem();

                item.Text = t.name;
                item.Value = t.taskId.ToString();
                comboBox.Items.Add(item);
                item.DataBind();
            }
        }



        protected void Search_Click(object sender, EventArgs e)
        {
            //label1.Text = filter;
            //RadTaskGrid.Rebind();
            //using (var context = new)
            //{
            //    var owners = context.;

            //    foreach (Course cs in courses)
            //        Console.WriteLine(cs.CourseName);
            //}

        }

        protected void RadComboBoxTask_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            filter = e.Text;
        }

        protected void RadTaskGrid_ItemUpdated(object sender, GridUpdatedEventArgs e)
        {

            RadTaskGrid.Rebind();
        }



        protected void RadComboBoxStatus_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            List<String> items = new List<string>();
            items.Add("new");
            items.Add("postponed");
            items.Add("progress");
            items.Add("done");
            items.Add("in progress");
            RadComboBox comboBox = (RadComboBox)sender;

            comboBox.Items.Clear();

            foreach (String s in items)
            {

                RadComboBoxItem item = new RadComboBoxItem();

                item.Text = s;
                item.Value = items.IndexOf(s).ToString();
                comboBox.Items.Add(item);
                item.DataBind();
            }

        }

        protected void RadComboBoxStatus_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            status = e.Text;


        }

        protected void taskSources_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            var param = e.Command.CreateParameter();
            param.ParameterName = "status";
            e.Command.Parameters.Add(param);
            e.Command.Parameters["status"].Value = status;


        }

        //protected void RadListProjectMembers_Dropped(object sender, RadListBoxDroppedEventArgs e)
        //{

        //    int x = e.SourceDragItems.Count;
        //    drgLbl.Text = x.ToString();
        //    foreach (RadListBoxItem item in e.SourceDragItems)
        //    {

        //        var userID = _db.Users.Where(u => u.username == item.Text).FirstOrDefault().userId;

        //        _db.ProjectMembers.Add(new ProjectMember { projectId = (int)projectID, userId = userID, permissionId = 1 });
        //        _db.SaveChanges();
        //    }
        //}



        //protected void RadListUsers_Dropped(object sender, RadListBoxDroppedEventArgs e)
        //{


        //}

        //protected void RadListUsers_Dropping(object sender, RadListBoxDroppingEventArgs e)
        //{
        //    int x = e.SourceDragItems.Count;
        //    drgLbl.Text = x.ToString();
        //    foreach (RadListBoxItem item in e.SourceDragItems)
        //    {

        //        var userID = _db.Users.Where(u => u.username == item.Text).FirstOrDefault().userId;

        //        _db.ProjectMembers.Add(new ProjectMember { projectId = (int)projectID, userId = userID, permissionId = 1 });
        //        _db.SaveChanges();
        //    }
        //}

        //protected void RadListUsers_Transferred(object sender, RadListBoxTransferredEventArgs e)
        //{
        //    if (e.Items.Count == 1 && e.SourceListBox.ClientID == "RadListUsers")
        //    {
        //        var items = e.Items.ToList();
        //        drgLbl.Text = "AA";
        //        foreach (RadListBoxItem item in items)
        //        {

        //            drgLbl.Text = item.Text;
        //            var userID = _db.Users.Where(u => u.username == item.Text).FirstOrDefault().userId;

        //            _db.ProjectMembers.Add(new ProjectMember { projectId = (int)projectID, userId = userID, permissionId = 1 });
        //            _db.SaveChanges();
        //        }
        //    }
        //}

        protected void RadListUsers_Transferring(object sender, RadListBoxTransferringEventArgs e)
        {
            drgLbl.Text = "AA";
        }

        protected void RadListProjectMembers_Inserting(object sender, RadListBoxInsertingEventArgs e)
        {
            drgLbl.Text = "AA";
        }

        protected void addMember_Click(object sender, EventArgs e)
        {
            try
            {
                var username = RadListUsers.SelectedItem.Text;
                int userID = _db.Users.Where(u => u.username == username).FirstOrDefault().userId;


                _db.ProjectMembers.Add(new ProjectMember { projectId = (int)projectID, userId = userID, permissionId = 1 });

                _db.SaveChanges();

                var prMId = _db.ProjectMembers.Where(pm => pm.projectId == projectID).Select(u => u.userId).ToList();
                var uPrM = _db.Users.Where(u => prMId.Contains(u.userId)).ToList();
                var us = _db.Users.Where(u => (!prMId.Contains(u.userId))).ToList();

                RadListUsers.DataSource = us;
                RadListProjectMembers.DataSource = uPrM;

                RadListUsers.DataTextField = "username";
                RadListUsers.DataValueField = "userId";
                RadListUsers.DataBind();

                RadListProjectMembers.DataTextField = "username";
                RadListProjectMembers.DataValueField = "userId";
                RadListProjectMembers.DataBind();
            }
            catch (Exception ex)
            {

                drgLbl.Text = "Select a user";
            }

        }

        protected void RadListUsers_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void deleteMember_Click(object sender, EventArgs e)
        {
            try
            {
                var username = RadListProjectMembers.SelectedItem.Text;
                int userID = _db.Users.Where(u => u.username == username).FirstOrDefault().userId;

                _db.ProjectMembers.Remove(_db.ProjectMembers.Where(pm => pm.projectId == projectID && pm.userId == userID).FirstOrDefault());

                _db.SaveChanges();

                var prMId = _db.ProjectMembers.Where(pm => pm.projectId == projectID).Select(u => u.userId).ToList();
                var uPrM = _db.Users.Where(u => prMId.Contains(u.userId)).ToList();
                var us = _db.Users.Where(u => (!prMId.Contains(u.userId))).ToList();

                RadListUsers.DataSource = us;
                RadListProjectMembers.DataSource = uPrM;

                RadListUsers.DataTextField = "username";
                RadListUsers.DataValueField = "userId";
                RadListUsers.DataBind();

                RadListProjectMembers.DataTextField = "username";
                RadListProjectMembers.DataValueField = "userId";
                RadListProjectMembers.DataBind();
            }
            catch (Exception ex)
            {
                drgLbl.Text = "Select a projectMember";
            }
        }

        protected void RadTaskGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {

            if (e.CommandName == RadGrid.SelectCommandName && e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string taskId = dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["taskId"].ToString();
                Response.Redirect("./Comments.aspx?taskId=" + taskId);

            }
        }
    }
}