using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace ets_web.Account
{
    public partial class DashBoard : ets_web.ETSBasePage
    {
        User member;

        protected void Page_Load(object sender, EventArgs e)
        {
            member = (User)Session["user"];
            ValidateUserLogin();
            
        }
    }
}