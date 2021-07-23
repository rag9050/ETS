using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ets_web
{
    public partial class ChangePassword : ETSBasePage
    {

        User _member = null;
        DA_User _daUser = null;
        User _user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            _member = (User)Session["user"];
            ValidateUserLogin();
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            _daUser = new DA_User();
            _user = new User();
            try
            {
                _user.UserCode = _member.UserCode;
                _user.Password = txtNewPassword.Text;
                if(txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Change Password", "alert('Passwords are not Matching');", true);
                    return;
                }
                if(_daUser.ChangeUserPassword(_user))
                {
                    txtConfirmPassword.Text = string.Empty;
                    txtNewPassword.Text = string.Empty;
                    btnChangePassword.Enabled = false;
                    ClientScript.RegisterStartupScript(GetType(), "Change Password", "alert('Password has been changed successfully');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Change Password", "alert('Password change failed');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Rerequirement", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _member.LoginId, "Requirement", "ETS", "LoadRequirements");
            }
        }
    }
}