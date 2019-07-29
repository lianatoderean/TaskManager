using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TaskManager.Model;
using Telerik.Web.UI;

namespace TaskManager
{
    public partial class ProjectList : System.Web.UI.Page
    {
        private TaskContext _db = new TaskContext();
        private String filter = "";
        private int projectId;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<Project> GetProjects()
        {
            var _db = new TaskManager.Model.TaskContext();
            IQueryable<Project> query = _db.Projects;
            return query;
        }

        protected void RadProjectGrid_ItemUpdated(object sender, Telerik.Web.UI.GridUpdatedEventArgs e)
        {
            RadProjectGrid.Rebind();
        }

        protected void RadComboBoxProject_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            IQueryable<Project> projects = (IQueryable<Project>)_db.Projects;
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();

            foreach (Project p in projects)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Text = p.name;
                item.Value = p.projectId.ToString();
                comboBox.Items.Add(item);
                item.DataBind();
            }
        }

        protected void RadComboBoxProject_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            filter = e.Text;
        }

        protected void Search_Click(object sender, EventArgs e)
        {
        }

        protected void RadProjectGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.SelectCommandName && e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string projectId = dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["projectId"].ToString();
                Response.Redirect("./ProjectTasks.aspx?projectId=" + projectId);
            }
        }

        protected void RadProjectGrid_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            if ((e.CommandName == "Update") && (e.Item.OwnerTableView.Name == "MasterTable"))
            {
                GridEditableItem item = e.Item as GridEditableItem;

                int projectId = Convert.ToInt32(item.GetDataKeyValue("projectId"));

                RadComboBox rcbOwner = item.FindControl("rcbOwner") as RadComboBox;
                var project = _db.Projects.Where(p => p.projectId == projectId).FirstOrDefault();
                project.ownerId = Convert.ToInt32(rcbOwner.SelectedValue);
                project.name = (item.FindControl("tbName") as RadTextBox).Text;
                project.description = (item.FindControl("tbDescription") as RadTextBox).Text;
                project.startDate = (e.Item.FindControl("RadDatePickerStartDate") as RadDatePicker).SelectedDate;
                project.dueDate = (e.Item.FindControl("RadDatePickerEndDate") as RadDatePicker).SelectedDate;

                _db.SaveChanges();
            }
        }

        protected void RadProjectGrid_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            RadComboBox rcbOwner = item.FindControl("rcbOwner") as RadComboBox;

            int ownerId = Convert.ToInt32(rcbOwner.SelectedValue);
            String name = (item.FindControl("tbName") as RadTextBox).Text;
            String description = (item.FindControl("tbDescription") as RadTextBox).Text;
            DateTime? startDate = (e.Item.FindControl("RadDatePickerStartDate") as RadDatePicker).SelectedDate;
            DateTime? dueDate = (e.Item.FindControl("RadDatePickerEndDate") as RadDatePicker).SelectedDate;

            Project project = new Project();
            project.ownerId = ownerId;
            project.name = name;
            project.description = description;
            project.startDate = startDate;
            project.dueDate = dueDate;

            _db.Projects.Add(project);
            _db.SaveChanges();
        }

        protected void RadProjectGrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;

            var projectID = Convert.ToInt32(item.GetDataKeyValue("projectId"));

            var projectTasks = _db.Tasks.Where(t => t.projectId == projectID).ToList();
            if (projectTasks != null)
            {
                foreach (Task t in projectTasks)
                {
                    _db.UsersTasks.Remove(_db.UsersTasks.Where(ut => ut.taskId == t.taskId).FirstOrDefault());
                    _db.Tasks.Remove(_db.Tasks.Where(ts => ts.taskId == t.taskId).FirstOrDefault());
                }
            }
            _db.Projects.Remove(_db.Projects.Where(p => p.projectId == projectID).FirstOrDefault());

            var projectMembers = _db.ProjectMembers.Where(pm => pm.projectId == projectID);
            foreach (ProjectMember pm in projectMembers)
            {
                _db.ProjectMembers.Remove(pm);
            }
            _db.SaveChanges();
        }

        protected void RadProjectGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }
    }
}