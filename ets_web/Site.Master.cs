using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Text;

namespace ets_web
{
    public partial class SiteMaster : MasterPage
    {
        DA_Notification _daNotification = null;
        List<Notification> _lstNotification = null;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            VerifyUser();
            NotificationsLabel();
        }
        private void NotificationsLabel()
        {

            _daNotification = new DA_Notification();
            _lstNotification = _daNotification.getNotificationData(1);

            //for getting notifications
            var notifications = from m in _lstNotification where m.Type == 1 select m;
            StringBuilder _sbNotificationMarque = new StringBuilder();

            foreach (var eachnotification in notifications)
            {
                _sbNotificationMarque.Append("<span style=\"color:red\">");
                _sbNotificationMarque.Append("<b> NOTIFICATIONS :: </b> " + eachnotification.Description + " ");
                _sbNotificationMarque.Append("</span>");
            }
            dvmarque.InnerHtml = _sbNotificationMarque.ToString();

            //for getting alerts
            var alerts = from m in _lstNotification where m.Type == 2 select m;

            foreach (var eachalert in alerts)
            {
                _sbNotificationMarque.Append("<span style=\"color:green\">");
                _sbNotificationMarque.Append("<b> ALERTS :: </b>" + eachalert.Description + " ");
                _sbNotificationMarque.Append("</span>");
            }
            dvmarque.InnerHtml = _sbNotificationMarque.ToString();

            //for getting wishes
            var wishes = from m in _lstNotification where m.Type == 3 select m;

            foreach (var eachwish in wishes)
            {
                _sbNotificationMarque.Append("<span style=\"color:blue\">");
                _sbNotificationMarque.Append("<b> WISHES :: </b>" + eachwish.Description + " ");
                _sbNotificationMarque.Append("</span>");
            }
            dvmarque.InnerHtml = _sbNotificationMarque.ToString();
        }



        private void VerifyUser()
        {
            if (Session["user"] != null)
            {
                User beUsre = (User) Session["user"];
                ((Label)lgnview.FindControl("lbl_name")).Text = beUsre.FullName + " | " + ETSBasePage.dcRole[beUsre.Role];
                loginboard.Visible = true;
                dvNotification.Visible = true;

                if (beUsre.Role == 1 || beUsre.Role == 0)
                {
                    aNotification.Visible = true;
                }
            }
            else
            {
                loginboard.Visible = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}