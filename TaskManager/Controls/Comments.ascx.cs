using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TaskApp.DTOs;
using TaskManager.Model;

namespace TaskManager.Controls
{
    public partial class Comments : System.Web.UI.UserControl
    {
        public int taskId { get; set; }
        private TaskContext _db = new TaskContext();
        private string us = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var taskName = _db.Tasks.Where(t => t.taskId == taskId).FirstOrDefault().name;
            sb.Append("<span> Comments for task " + taskName + "</span>");
            us = Membership.GetUser().UserName;
            CommentFormTop.Controls.Add(new LiteralControl(sb.ToString()));
            CommentFormBottom.Controls.Add(new LiteralControl(createCommentsTable()));
        }

        protected String createCommentsTable()
        {
            CommentDTO comment = new CommentDTO();
            var type = comment.GetType();
            var properties = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id=\"commentTable\">");
            sb.Append("<tr> ");

            List<Comment> comments = _db.Comments.Where(p => p.taskId == taskId).OrderBy(c => c.postDate).ToList();

            comments.Reverse();

            foreach (var v in properties)
            {
                sb.Append("<th ID=" + v.Name + ">" + v.Name + "</th>");
            }

            sb.Append("</tr>");

            foreach (Comment c in comments)
            {
                sb.Append("<tr> ");
                sb.Append("<td >" + c.title + "</td>");

                sb.Append("<td >" + c.comment + "</td>");

                sb.Append("<td >" + c.postDate.ToString() + "</td>");

                String username = _db.Users.Where(u => u.userId == c.userId).FirstOrDefault().username;

                sb.Append("<td >" + username + "</td>"); sb.Append("</tr>");
            }

            sb.Append("</table>");
            return sb.ToString();
        }

        protected void AddComment_Click(object sender, EventArgs e)
        {
            String title = CommentTitle.Text;
            String commentC = comment.InnerText;
            int userId = _db.Users.Where(u => u.username == us).FirstOrDefault().userId;

            _db.Comments.Add(new Comment { taskId = taskId, userId = userId, title = title, comment = commentC, postDate = DateTime.Now });
            _db.SaveChanges();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}