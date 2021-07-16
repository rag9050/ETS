using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ets_web.Account
{
    public partial class NewNotification : ets_web.ETSBasePage
    {
        Notification _Notification = null;
        User _member = null;
        DA_Notification _daNotification = null;
        bool blResult;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _member = (User)Session["user"];
                
                //ValidateUserLogin();
                ValidateUserLogin();

                if (_member.Role == 2)
                {
                    Response.Redirect("DashBoard.aspx");
                }

                if (!IsPostBack)
                {
                    Loadtype();
                    LoadcontrolType();
                }
            }
            catch (Exception)
            {

            }
        }
        private void Loadtype()
        {
            ddlType.DataSource = dcNotificationType;
            ddlType.DataTextField = "Value";
            ddlType.DataValueField = "Key";
            ddlType.DataBind();
        }
        //Loading type from dictionary
        private void LoadcontrolType()
        {
            ddlcontroltype.DataSource = dcNotificationControl;
            ddlcontroltype.DataTextField = "Value";
            ddlcontroltype.DataValueField = "Key";
            ddlcontroltype.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                _daNotification = new DA_Notification();
                _Notification = new Notification();
                _Notification.Title = txtTitle.Text;
                _Notification.Description = txtDescription.Text;
                _Notification.Type = Convert.ToInt32(ddlType.SelectedItem.Value);
                _Notification.Control = Convert.ToInt32(ddlcontroltype.SelectedItem.Value);
                _Notification.PostedBy = _member.UserCode;
                blResult = _daNotification.InsertNotification(_Notification);
               
                if (blResult == true)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "New Notification", "alert('Notification Inserted successfully');", true);
                    clearRecords();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "New Notification", "alert('Notifications inserted Unsuccessful');", true);
                }
                Response.Redirect("~/Account/Notifications.aspx");
              
            }
          
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "New Notification", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(Ex, GetLogFilePath(), _member.LoginId, "New Notification", "ETS", "btnSubmit_Click");
            }
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Notifications.aspx");
        }
        private void clearRecords()
        {
            txtTitle.Text = "";
            txtDescription.Text = "";
        }
       
    }
}
           
      

           
     
             
      

       
     
  