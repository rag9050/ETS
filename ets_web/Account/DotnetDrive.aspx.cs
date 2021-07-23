using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace ets_web.Account
{
    public partial class DotnetDrive : ets_web.ETSBasePage
    {
        User _UserModel = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateUserLogin();
            _UserModel = (User)Session["user"];
            if (_UserModel.Role != 1)
            {
                Response.Redirect("DashBoard.aspx");
            }
        }
    }
}