﻿using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace TaskManager
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            UsernameLabel.Text = Membership.GetUser().UserName;
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}