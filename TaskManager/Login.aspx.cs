using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace TaskManager
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (Membership.ValidateUser(Login1.UserName, Login1.Password) == true)
            {

                Session["user"] = User.Identity.Name;
                //FormsAuthentication.RedirectFromLoginPage(Login1.UserName, true);
                FormsAuthentication.SetAuthCookie(Login1.UserName, true);


                Response.Redirect("Profile.aspx");
            }
            else
            {
                Response.Write("Invalid Login");
            }
        }

        protected void rbRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}