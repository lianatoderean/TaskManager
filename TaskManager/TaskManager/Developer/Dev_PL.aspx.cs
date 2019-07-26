using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using TaskApp.DTOs;
using TaskManager.Model;
using Telerik.Web.UI;

namespace TaskManager.Developer
{
    public partial class Dev_PL : System.Web.UI.Page
    {
        private String username = "";
        private int userId;
        private TaskContext _db = new TaskContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            UserLbl1.Text = Membership.GetUser().UserName;
        }

        protected void RadDevProjectGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            username = Membership.GetUser().UserName;
            userId = _db.Users.Where(u => u.username == username).FirstOrDefault().userId;

            var projMembers = _db.ProjectMembers.Where(p => p.userId == userId).Select(s => s.projectId).ToList<int>();
            var projects = _db.Projects.Where(p => projMembers.Contains(p.projectId));
            List<ProjectDTO> dt = new List<ProjectDTO>();

            foreach (Project pr in projects)
            {
                var owner = _db.Users.Where(p => p.userId == pr.ownerId).FirstOrDefault().username;
                ProjectDTO d = new ProjectDTO();
                d.projectId = pr.projectId;
                d.description = pr.description;
                d.dueDate = pr.dueDate;
                d.name = pr.name;
                d.startDate = pr.startDate;
                d.owner = owner;
                dt.Add(d);
            }

            RadDevProjectGrid.DataSource = dt;
        }

        protected void RadDevProjectGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.SelectCommandName && e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string projectId = dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["projectId"].ToString();
                Response.Redirect("~/Developer/Dev_PT.aspx?projectId=" + projectId);
            }
        }
    }
}