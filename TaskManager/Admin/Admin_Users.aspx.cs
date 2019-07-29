using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TaskApp.DTOs;
using TaskManager.Model;
using Telerik.Web.UI;

namespace TaskManager.Admin
{
    public partial class Admin_Users : System.Web.UI.Page
    {
        private TaskContext _db = new TaskContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RadGridUsers_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridTableView tableView = RadGridUsers.MasterTableView.Items[0].ChildItem.NestedTableViews[0] as GridTableView;
            tableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
        }

        protected void RadGridUsers_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                RadGridUsers.DataSource = _db.Users.ToList();
            }
        }

        private int UserID;

        protected void RadGridUsers_DetailTableDataBind1(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "ProjectsDetails":
                    {
                        UserID = Convert.ToInt32(dataItem.GetDataKeyValue("UserID").ToString());
                        /* var projects = (from pm in _db.ProjectMembers
                                         join p in _db.Projects
                                         on pm.projectId equals p.projectId
                                         where pm.userId == UserID
                                         select new
                                         {
                                             projectID = p.projectId,
                                             name = p.name,
                                             StartDate = p.startDate,
                                             DueDate = p.dueDate,
                                             description = p.description,
                                             owner = p.ownerId
                                         }).ToList();
                         */

                        var projMembers = _db.ProjectMembers.Where(p => p.userId == UserID).Select(s => s.projectId).ToList<int>();
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

                        e.DetailTableView.DataSource = dt;

                        break;
                    }

                case "TasksDetails":
                    {
                        int projectID = Convert.ToInt32(dataItem.GetDataKeyValue("ProjectID"));
                        int userID = Convert.ToInt32(dataItem.OwnerTableView.ParentItem.GetDataKeyValue("UserID"));

                        /*  var tasks = (from tu in _db.UsersTasks
                                          join t in _db.Tasks
                                          on tu.taskId equals t.taskId
                                          where tu.userId == UserID
                                          select new
                                          {
                                             TaskID = t.taskId,
                                              name = t.name,
                                              description = t.description,
                                              status = t.status
                                          }).ToList();
                          */
                        var userTsk = _db.UsersTasks.Where(p => p.userId == userID).Select(s => s.taskId).ToList<int>();
                        var tasks = _db.Tasks.Where(t => t.projectId == projectID && userTsk.Contains(t.taskId));
                        e.DetailTableView.DataSource = tasks.ToList();
                        break;
                    }
            }
        }

        protected void RadGridUsers_PreRender(object sender, EventArgs e)
        {
        }
    }
}