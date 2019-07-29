using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace TaskManager
{
    public partial class RolesPage : System.Web.UI.Page
    {
        private SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskContext"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsers();
                BindRoles();
                Label1.Text = "";
            }
        }

        protected void btnCreateRole_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            RadLabel1.Text = "";
            try
            {
                if (!Roles.RoleExists(txtrolename.Text))
                {
                    Roles.CreateRole(txtrolename.Text);
                    BindUsers();
                    BindRoles();
                    Label1.Text = "Role(s) Created Successfully";
                }
                else
                {
                    Label1.Text = "Role(s) Already Exists";
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        protected void btnAssignRoleToUser_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            RadLabel1.Text = "";
            try
            {
                if (Roles.GetRolesForUser(lstusers.SelectedItem.Text).Length == 0)
                {
                    Roles.AddUserToRole(lstusers.SelectedItem.Text, lstRoles.SelectedItem.Text);
                    BindUsers();
                    BindRoles();
                    //Label1.Text = Convert.ToString(lstusers.SelectedValue) + " Assigned To " + lstRoles.SelectedValue.ToString() + " Successfully!";
                    Label1.Text = "User Assigned To Role Successfully!";
                }
                else
                {
                    String[] r = Roles.GetRolesForUser(lstusers.SelectedItem.Text);
                    Roles.RemoveUserFromRole(lstusers.SelectedItem.Text, r[0]);
                    Roles.AddUserToRole(lstusers.SelectedItem.Text, lstRoles.SelectedItem.Text);
                    BindUsers();
                    BindRoles();
                    var a = lstusers.SelectedValue.ToString();
                    //Label1.Text = a + " changed role to " + lstRoles.SelectedValue.ToString() + ".";
                    Label1.Text = "User Changed Role Successfully!";
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        protected void btnRemoveUserFromUser_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            RadLabel1.Text = "";
            try
            {
                Roles.RemoveUserFromRole(lstusers.SelectedItem.Text, lstRoles.SelectedItem.Text);
                BindUsers();
                BindRoles();
                Label1.Text = "User Is Removed From The Role Successfully";
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        protected void btnRemoveRoles_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            RadLabel1.Text = "";
            try
            {
                Roles.DeleteRole(lstRoles.SelectedItem.Text);
                BindUsers();
                BindRoles();
                Label1.Text = "Role(s) Removed Successfully";
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        public void BindRoles()
        {
            SqlDataAdapter da = new SqlDataAdapter("select RoleName from aspnet_Roles", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Roles");
            lstRoles.DataSource = ds;
            lstRoles.DataTextField = "RoleName";
            lstRoles.DataValueField = "RoleName";
            lstRoles.DataBind();
        }

        public void BindUsers()
        {
            SqlDataAdapter da = new SqlDataAdapter("select UserName from aspnet_users", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Roles");
            lstusers.DataSource = ds;
            lstusers.DataTextField = "UserName";
            lstRoles.DataValueField = "RoleName";
            lstusers.DataBind();
        }

        protected void lstusers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindUsers();
            //BindRoles();
            RadLabel1.Text = "aaaa";

            if (Roles.GetRolesForUser(lstusers.SelectedItem.Text).Length != 0)
                RadLabel1.Text = "Current user role: " + Roles.GetRolesForUser(lstusers.SelectedItem.Text)[0].ToString();
            else
                RadLabel1.Text = "User doesn't have any role.";
        }
    }
}