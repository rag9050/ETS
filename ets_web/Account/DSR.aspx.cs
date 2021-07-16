using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DataAccess;
using Model;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using Model;
using System.Web.Script.Services;

namespace ets_web.Account
{
    public partial class DSR : ets_web.ETSBasePage
    {
        Task _objXml = null;
        DA_DSR _daXml = null;
        DataTable _dtTemplate = null;
        DataTable _dtDSRRecord = null;
        bool blResult = false;
        User _UserModel = null;
        DA_Task _daTask = null;
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateUserLogin();
            if (!IsPostBack)
            {
                _UserModel = (User)Session["user"];
                if(_UserModel.Role == 1)
                {
                    Response.Redirect("DSRReport.aspx");
                }
                Session["dsr"] = null;
                LoadTemplateGrid();
            }
       }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> GetAutoCompleteData(string username)
       {
           DA_Task _daTask = new DA_Task();
            Task _objxml=new Task();
            _objxml.TaskId = Convert.ToUInt16(username);
            List<string> result = new List<string>();
            result = _daTask.auto_fetchTaskDetails(_objxml);
            return result;
             
         }

        protected void bindData()
        {
            dvDSRData.DataSource = _dtDSRRecord;
            dvDSRData.DataBind();
        }

        public void CreateDataTable()
        {
            _dtTemplate = new DataTable();
            _dtTemplate.Columns.Add(new DataColumn("SNO"));
            _dtTemplate.Columns.Add(new DataColumn("TITLE"));
            _dtTemplate.Columns.Add(new DataColumn("TASKID"));
            _dtTemplate.Columns.Add(new DataColumn("DETAILS"));
            _dtTemplate.Columns.Add(new DataColumn("EFFORTSPERFORMED"));
            _dtTemplate.Columns.Add(new DataColumn("EFFORTSREMAINING"));
            _dtTemplate.Columns.Add(new DataColumn("PROGRESS"));
            _dtTemplate.Columns.Add(new DataColumn("STATUS"));
            _dtTemplate.Columns.Add(new DataColumn("STATUSID"));
        }

        public void LoadTemplateGrid()
        {
            CreateDataTable();
            DataRow _NewRow = _dtTemplate.NewRow();
            _dtTemplate.Rows.Add(_NewRow);
            gvDSRTemplate.DataSource = _dtTemplate;
            gvDSRTemplate.DataBind();
        }

        protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
                DropDownList _ddlTitle = sender as DropDownList;
                GridViewRow _GridRow = (GridViewRow)_ddlTitle.Parent.Parent;
                TextBox txtid = _GridRow.FindControl("txtTaskID") as TextBox;
                TextBox txtdetails = _GridRow.FindControl("txtDetails") as TextBox;
                TextBox txteffperf = _GridRow.FindControl("txtEffertsPerformed") as TextBox;
                TextBox txteffrem = _GridRow.FindControl("txtEffertsRemaining") as TextBox;
                TextBox txtprogress = _GridRow.FindControl("txtProgress") as TextBox;
                DropDownList dropstatus = _GridRow.FindControl("ddlStatus") as DropDownList;
                Button BtnAdd = _GridRow.FindControl("btnAdd") as Button;
                ImageButton btnClear = _GridRow.FindControl("btnClear") as ImageButton;
                txtid.Text = null;
                txtdetails.Text = null;
                txteffperf.Text = null;
                txteffrem.Text = null;
                txtprogress.Text = null;
                dropstatus.SelectedItem.Value = "0";
                if (_ddlTitle.SelectedItem.Text.ToUpper() == "WORK ITEM")
                {
                    txtid.Enabled = true;
                    txtdetails.Enabled = true;
                    txteffperf.Enabled = true;
                    txteffrem.Enabled = true;
                    txtprogress.Enabled = true;
                    dropstatus.Enabled = true;
                    BtnAdd.Enabled = true;
                    btnClear.Enabled = true;
                }
                else
                    if (_ddlTitle.SelectedItem.Text.ToUpper() == "SELECT")
                    {
                        txtid.Enabled = false;
                        txtdetails.Enabled = false;
                        txteffperf.Enabled = false;
                        txteffrem.Enabled = false;
                        txtprogress.Enabled = false;
                        dropstatus.Enabled = false;
                        BtnAdd.Enabled = false;
                        btnClear.Enabled = false;
                    }
                    else
                    {
                        txtid.Enabled = false;
                        txtdetails.Enabled = true;
                        txteffperf.Enabled = true;
                        txteffrem.Enabled = true;
                        txtprogress.Enabled = true;
                        dropstatus.Enabled = true;
                        BtnAdd.Enabled = true;
                        btnClear.Enabled = true;
                    }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            LoadDSRGridData();
            
            Button btnAdd = sender as Button;
            GridViewRow _TemplateRow = (GridViewRow)btnAdd.Parent.Parent;
            DataRow _DSRRow = _dtDSRRecord.NewRow();
            _DSRRow["SNO"] = _dtDSRRecord.Rows.Count + 1;
            _DSRRow["TITLE"] = ((DropDownList)_TemplateRow.FindControl("ddlTitle")).SelectedItem.Text;
            _DSRRow["TASKID"] = ((TextBox)_TemplateRow.FindControl("txtTaskID")).Text;
            _DSRRow["DETAILS"] = ((TextBox)_TemplateRow.FindControl("txtDetails")).Text;
            _DSRRow["EFFORTSPERFORMED"] = ((TextBox)_TemplateRow.FindControl("txtEffertsPerformed")).Text;
            _DSRRow["EFFORTSREMAINING"] = ((TextBox)_TemplateRow.FindControl("txtEffertsRemaining")).Text;
            _DSRRow["PROGRESS"] = ((TextBox)_TemplateRow.FindControl("txtProgress")).Text;
            _DSRRow["STATUS"] = ((DropDownList)_TemplateRow.FindControl("ddlStatus")).SelectedItem.Text;
            _DSRRow["STATUSID"] = ((DropDownList)_TemplateRow.FindControl("ddlStatus")).SelectedItem.Value;             
            clearDSR();
            _dtDSRRecord.Rows.Add(_DSRRow);
            bindData();
            Session["dsr"] = _dtDSRRecord;
            btnSubmit.Visible = true;
            btnRemove.Visible = true;
            dvDSRData.Visible = true;
        }

        protected void LoadDSRGridData()
        {
            if (Session["dsr"] == null)
            {
                CreateDataTable();
                _dtDSRRecord = _dtTemplate.Copy();
            }
            else
            {
                _dtDSRRecord = (DataTable)Session["dsr"];
            }
        }

        public void clearDSR()
        {
            gvDSRTemplate.Dispose();
            LoadTemplateGrid();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                _objXml = new Task();
                User _objuser = new User();
                _objuser = (User)Session["user"];

                if (Session["dsr"] != null)
                {
                    _dtDSRRecord = (DataTable)Session["dsr"];
                    DataSet dsData = new DataSet();
                    dsData.Tables.Add(_dtDSRRecord);
                    _objXml.strXMLData = dsData.GetXml();
                    _objXml.CreatedByCode = _objuser.UserCode;
                    _daXml = new DA_DSR();
                    blResult=_daXml.insertDSR(_objXml);
                    if (blResult == true)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "DSR", "alert('Task Submitted successfully');", true);
                        Response.Redirect("~/Account/DSR.aspx");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "New Rerequirement", "alert('Rquirement Updation is not successful');", true);
                    }
                    Session["dsr"] = null;
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "DSR", "alert(" + Message.EmptyRecords + ");", true);
                }
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "DSR", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _UserModel.LoginId, "DSR", "ETS", "loadDSR");
            }
        }

        protected void btnClear_Click(object sender, ImageClickEventArgs e)
        {
            clearDSR();
        }

        protected void dvDSRData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
            _dtDSRRecord = Session["dsr"] as DataTable;
            _dtDSRRecord.Rows[rowIndex].Delete();
            Session["dsr"] = _dtDSRRecord;
            bindData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Session["dsr"] = null;
            LoadDSRGridData();
            bindData();
            dvDSRData.Visible = false;
            btnSubmit.Visible = false;
            btnRemove.Visible = false;
        }
    }
}