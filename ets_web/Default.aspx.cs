using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using ets_dataaccess;
using DataAccess;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using ets_dataaccess.Common;

namespace ets_web
{
    public partial class _Default : ETSBasePage
    {

        User beMember = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                {
                    ((CheckBox)frmLogin.FindControl("RememberMe")).Checked = true;
                    ((TextBox)frmLogin.FindControl("UserName")).Text = Request.Cookies["UserName"].Value;
                    ((TextBox)frmLogin.FindControl("Password")).Attributes["value"] = Request.Cookies["Password"].Value;
                }
            }

        }


        public string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars += "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";


            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }
            return NewPassword;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ValidateUser();
        }

        protected void ValidateUser()
        {
            try
            {
                beMember = new User();
                beMember.LoginId = ((TextBox)frmLogin.FindControl("UserName")).Text;
                beMember.Password = ((TextBox)frmLogin.FindControl("Password")).Text;
                DA_User daUser = new DA_User();
                if (((CheckBox)frmLogin.FindControl("RememberMe")).Checked)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                }
                Response.Cookies["UserName"].Value = ((TextBox)frmLogin.FindControl("UserName")).Text.Trim();
                Response.Cookies["Password"].Value = ((TextBox)frmLogin.FindControl("Password")).Text.Trim();

                beMember = daUser.CheckUserLogin(beMember);
                if (beMember != null)
                {
                    Session["user"] = beMember;
                    if (IsUserExistInShiftXML(beMember.UserCode.ToString()))
                    {
                        Response.Redirect("~/Account/DashBoard.aspx");
                    }
                    {
                        Response.Redirect("~/Shift.aspx");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Invalid", "alert('Invalid credentials');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "New Rerequirement", "alert('Error Occured. Contact IT Department');", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), beMember.LoginId, "Login", "ETS", "ValidateUser");
            }
        }

        protected bool IsUserExistInShiftXML(string strUserCode)
        {
            bool isExist = false;
            DataSet dt = new DataSet();
            dt.ReadXml(Server.MapPath(ConfigurationManager.AppSettings["shiftfilepath"].ToString()));

            DataRow[] rows = dt.Tables[0].Select("USERCODE = '" + strUserCode + "'");
            if (rows.Count() > 0)
            {
                isExist = true;
            }
            return isExist;
        }
        protected void Linkforget_Click(object sender, EventArgs e)
        {
            dforgetpassword.Visible = true;
        }
        protected void lnkreset_Click(object sender, EventArgs e)
        {
            beMember = (User)Session["user"];
            string strResetPassword = GeneratePassword();
            try
            {
                //send mail
                string strFromMailid = Convert.ToString(ConfigurationManager.AppSettings["infomailid"]);
                string strFromMailPassword = Convert.ToString(ConfigurationManager.AppSettings["infomailpassword"]);
                string strDestinationMailID = txtPasswordMail.Text + "@goggery.com";
                string strSubject = "ETS - Reset Password";
                string strDetails = "Hi,  " + strResetPassword;

                CommonUtility.COMMON_UTILITY c = new CommonUtility.COMMON_UTILITY();
                c.SendMail(strFromMailid, strFromMailPassword, strDestinationMailID, strSubject, strDetails, 'T');

                ClientScript.RegisterStartupScript(this.GetType(), "Password Reset", "alert('Password has been reset and sent to given mail id');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "New Rerequirement", "alert('Error Occured. Contact IT Department');", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), beMember.LoginId, "Login", "ETS", "lnkreset_Click");
            }

            User u = new Model.User();
            u.Password = strResetPassword;
            u.OfficialMailID = txtPasswordMail.Text + "@goggery.com";
            DA_User da = new DA_User();
            bool i = da.ResetPassword(u);
            if (i)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Password Reset", "alert('Password has been reset and sent to given mail id');", true);
                dforgetpassword.Visible = false;            
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Password Reset", "alert('Password not Resent');", true);
            }
           
        }

        protected void lnkcancel_Click(object sender, EventArgs e)
        {
            dforgetpassword.Visible = false;
        }
    }
}

