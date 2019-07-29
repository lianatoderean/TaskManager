using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TaskManager.Model;

namespace TaskManager
{
    public partial class Register : System.Web.UI.Page
    {
        private TaskContext _db = new Model.TaskContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            MembershipCreateStatus p = MembershipCreateStatus.Success;
            Membership.CreateUser(CreateUserWizard1.UserName,
               CreateUserWizard1.Password, CreateUserWizard1.Email,
            CreateUserWizard1.Question, CreateUserWizard1.Answer, true, out p);
            Model.User user = new Model.User();
            user.username = CreateUserWizard1.UserName.ToString();
            user.email = CreateUserWizard1.Email.ToString();
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        protected void CreateUserWizard1_ContinueButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}