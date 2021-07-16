using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Drawing;

namespace ets_web.Account
{
    public partial class Notifications : ets_web.ETSBasePage
    {
        DA_Notification _daNotification;
        User _UserModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            _UserModel = (User)Session["user"];
            
            ValidateUserLogin();

            if (_UserModel.Role == 2)
            {
                Response.Redirect("DashBoard.aspx");
            }

            if (!IsPostBack)
            {
                LoadNotificationData();
            }
        }

        protected void LoadNotificationData()
        {
            try
            {
                _daNotification = new DA_Notification();
                List<Notification> lstNotification = _daNotification.getNotificationData(_UserModel.UserCode);

                gvNotificationData.DataSource = lstNotification;
                gvNotificationData.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Notifications", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _UserModel.LoginId, "Notifications", "ETS", "LoadNotificationData");         
            }
        }

        protected void btnNewNotification_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/NewNotification.aspx");
        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            try
            {
                _daNotification = new DA_Notification();
                LinkButton btn = (LinkButton)(sender);
                string b = btn.CommandArgument;
                int sno = Convert.ToInt32(b);

                _daNotification.updateNotificationData(sno);
                LoadNotificationData();
                Page.ClientScript.RegisterStartupScript(GetType(), "InActive ", "alert('Inactived Succesfully');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Notifications", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _UserModel.LoginId, "Notifications", "ETS", "LoadNotificationData");
            }
           
        }

        protected void gvNotificationData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label l= (Label)e.Row.FindControl("lblType");
                    int iType = Convert.ToInt32(l.Text);
                    switch (iType)
                    {
                        case 1: e.Row.CssClass = "notification";
                            break;
                        case 2: e.Row.CssClass = "alert";
                            break;
                        case 3: e.Row.CssClass = "wishes";
                            break;
                        case 4: e.Row.CssClass = "information";
                            break;
                        default: e.Row.CssClass = "default";
                            break;
                    }
                    e.Row.Cells[1].Text = dcNotificationType.FirstOrDefault(x => x.Key == iType).Value;

                    Label lb=(Label)e.Row.FindControl("lblControl");
                    int iControl_Type = Convert.ToInt32(lb.Text);
                    e.Row.Cells[2].Text = dcNotificationControl.FirstOrDefault(x => x.Key == iControl_Type).Value;
                }
            }
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Notifications", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(Ex, GetLogFilePath(), _UserModel.LoginId, "Notifications", "ETS", "LoadNotificationData");
            }

        }

        protected void gvNotificationData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNotificationData.PageIndex = e.NewPageIndex;
            LoadNotificationData();
        }  
    }
}