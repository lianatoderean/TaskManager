using System;

namespace TaskManager.Admin
{
    public partial class Comments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            comS.taskId = Convert.ToInt32(Request.QueryString["taskId"]); ;
        }
    }
}