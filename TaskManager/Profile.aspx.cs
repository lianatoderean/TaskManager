using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TaskManager.Model;
using Telerik.Web.UI;

namespace TaskManager
{
    public partial class Profile : System.Web.UI.Page
    {
        TaskContext _db = new TaskContext();
        private int userId = 1;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RadDataFormUser_PreRender(object sender, EventArgs e)
        {

        }

        protected void RadDataFormUser_NeedDataSource(object sender, Telerik.Web.UI.RadDataFormNeedDataSourceEventArgs e)
        {
            RadDataFormUser.DataSource = _db.Users.Where(u => u.userId == userId);
        }

        protected void RadDataFormUser_ItemEditing(object sender, Telerik.Web.UI.RadDataFormCommandEventArgs e)
        {
            RadDataFormEditableItem editedItem = e.DataFormItem as RadDataFormEditableItem;
            Hashtable newValues = new Hashtable();
            editedItem.ExtractValues(newValues);
            var user = _db.Users.Where(u => u.userId == userId).FirstOrDefault();
            user.firstname = newValues["firstname"].ToString();
            user.lastname = newValues["lastname"].ToString();
            _db.SaveChanges();

        }
    }
}