using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using DataAccess;
using Model;

namespace ets_web.Account
{
    public partial class MapUserWithTask : ets_web.ETSBasePage
    {
        User _member = null;
        DA_Task daTask = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //validating User login
            ValidateUserLogin();
            _member = (User)Session["user"];
            if(!IsPostBack)
            {
                lblTaskID.Text = Convert.ToString(Request.QueryString["taskid"]); 
                hdnTaksID.Value = Convert.ToString(Session["taskid"]);
                lblTaskTitle.Text = Convert.ToString(Session["tasktitle"]);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("Requirements.aspx");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> GetCountryName(string pre)
        {
            List<string> lstMailId = new List<string>();
            DA_User daUser = new DA_User();
            try
            {
                lstMailId = daUser.autoGetUserMailID( pre);
            }
            catch
            {
                //ClientScript.RegisterStartupScript(GetType(), "Map Task To User", "alert(" + Message.ErrorMessage + ");", true);
                //EXCEPTION_UTILITY.WriteToLog(Ex, GetLogFilePath(), "", "New Notification", "ETS", "btnSubmit_Click");
            }
            return lstMailId;
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            daTask = new DA_Task();
            try
            {
                bool blResult = daTask.MapTaskToUser(txtMemberName.Text, Convert.ToInt32(hdnTaksID.Value), Convert.ToInt32(txtEfforts.Text));
               if(!blResult)
               {
                   ClientScript.RegisterStartupScript(GetType(), "Map Task To User", "alert(" + Message.TransactionFailed + ");", true);
               }
               else
               {
                   ClientScript.RegisterStartupScript(GetType(), "Map Task To User", "alert(" + Message.NewRecordMessage + ");", true);
                   Response.Redirect("Requirements.aspx");
               }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Map Task To User", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _member.UserCode.ToString(), "Map Task To User", "ETS", "btnAssign_Click");
            }

        }


    }
}