using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ets_web.Account
{
    public partial class Requirements : ets_web.ETSBasePage
    {
        User _member = null;
        Task _task = null;
        DA_Task _datask = null;
        List<Task> _lstTask = null;
        int i;


        protected void Page_Load(object sender, EventArgs e)
        {            
            ValidateUserLogin();
            _member = (User)Session["user"];
            if(!String.IsNullOrEmpty(Convert.ToString( Session["PageNumber"]))){
                Paging(Convert.ToInt16(Session["PageNumber"]));
            }
            if(!IsPostBack)
            {
                Session["taskid"] = null;
                ValidateMenu();
                LoadRequirements();
            }                 
            
        }

        

        private void ValidateMenu()
        {
            if (Session["user"] != null)
            {               
                var RoleCode = dcRole.FirstOrDefault(x => x.Key == _member.Role);
                if (RoleCode.Value == "MEMBER")
                {
                    btnnewrequirement.Visible = false;
                }
            }
        }

        protected void LoadRequirements()
        {
            try
            {
                _task = new Task();
                _task.CreatedByCode = _member.UserCode;
                _datask = new DA_Task();
                _lstTask = new List<Task>();
                _lstTask = _datask.GetTasks(_task);
                gvEmployeeData.DataSource = _lstTask;
                gvEmployeeData.DataBind();

                if (_member.Role == 1)
                    gvEmployeeData.Columns[6].Visible = true;
            }
            catch(Exception Ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Rerequirement", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(Ex, GetLogFilePath(), _member.LoginId, "Requirement", "ETS", "LoadRequirements");
            }
        }

       
        protected void gvEmployeeData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if(e.Row.RowType==DataControlRowType.DataRow)
                {
                    //updating text of Type based on value
                    int iType = Convert.ToInt32( e.Row.Cells[3].Text);
                    e.Row.Cells[3].Text = dcType.FirstOrDefault(x => x.Key == iType).Value;

                    //updating text of Status based on value
                    int iStatus = Convert.ToInt32(e.Row.Cells[4].Text);
                    switch (iStatus)
                    {
                        case 1: e.Row.CssClass = "assigned";
                            break;
                        case 2: e.Row.CssClass = "inprogress";
                            break;
                        case 3: e.Row.CssClass = "blocked";
                            break;
                        case 4: e.Row.CssClass = "rejected";
                            break;
                        case 5: e.Row.CssClass = "closed";
                            break;
                        default: e.Row.CssClass = "default";
                            break;
                    }
                    e.Row.Cells[4].Text = dcStatus.FirstOrDefault(x => x.Key == iStatus).Value;
                }
            }
            catch(Exception Ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "gvEmployeeData_RowDataBound", "alert("+ Message.ErrorMessage +");", true);
                EXCEPTION_UTILITY.WriteToLog(Ex, GetLogFilePath(), _member.LoginId, "Requirement", "ETS", "gvEmployeeData_RowDataBound");
            }
        }
        protected void gvEmployeeData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Paging( e.NewPageIndex);           
        }

        public void Paging(int PageNo)
        {
            gvEmployeeData.PageIndex = PageNo;
            LoadRequirements();
            Session["PageNumber"] = PageNo;
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            HiddenField hdnID = (HiddenField)grdrow.FindControl("hdnTaskID");
            Session["taskid"] = hdnID.Value;
            Response.Redirect("NewRequirement.aspx"); 
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            List<string> lstName = new List<string>();
            lstName.Add("Ramu");
            lstName.Add("Jaya");
            lstName.Add("Gopi");
            return lstName;
        }
        protected void lnkbtn_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            LinkButton lblTaskID = (LinkButton)grdrow.FindControl("lbtnTaskId");
            HiddenField hdnID = (HiddenField)grdrow.FindControl("hdnTaskID");
            Label hdnTitle = (Label)grdrow.FindControl("lblTitle");
            Session["taskid"] = hdnID.Value;
            Session["tasktitle"] = hdnTitle.Text;
            Response.Redirect("MapUserWithTask.aspx?taskid=" + lblTaskID.Text);
        }

        protected void btnnewrequirements_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewRequirement.aspx");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> AutoSearch(string Taskid)
        {
            DA_Task _datask = new DA_Task();
            Task _objxml = new Task();
            _objxml.TaskId = Convert.ToUInt16(Taskid);
            List<string> result = new List<string>(); ;
            result = _datask.auto_fetchTaskDetails(_objxml);
            return result;
        }

        protected void txtTaskSearch_TextChanged(object sender, EventArgs e)
        {
            _datask = new DA_Task();
            _lstTask = new List<Task>();
            string s = txtContactsSearch.Text;
            _lstTask=_datask.fetchTaskId(s);

            gvEmployeeData.DataSource = _lstTask;
            gvEmployeeData.DataBind();
        }

       protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
         {
          Session["PageNumber"] = null;
          Response.Redirect("Requirements.aspx");
         }

       protected void btnSearch_Click(object sender, EventArgs e)
        {
            _datask = new DA_Task();
            _lstTask = new List<Task>();
            string s = txtContactsSearch.Text;
            _lstTask = _datask.fetchTaskId(s);

            gvEmployeeData.DataSource = _lstTask;
            gvEmployeeData.DataBind();
       }

       
    }
}