using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Model;
using DataAccess;
using System.Configuration;
using System.Data;

namespace ets_web.Account
{
    public partial class NewRequirements : ets_web.ETSBasePage
    {
        Task _requirement = null;
        User _member = null;
        DA_Task _daTask = null;
        bool blResult = false;
        
        protected void Page_Load(object sender, EventArgs e)
        {
                     
                ValidateUserLogin();
                _member = (User)Session["user"];
                if (!IsPostBack)
                {
                    LoadStatus();
                    LoadType();
                    LoadFormWithRequirement();
                }
        }

        //Loading stauts from dictionary
        private void LoadStatus()
        {
            ddlStatus.DataSource = dcStatus;
            ddlStatus.DataTextField = "Value";
            ddlStatus.DataValueField = "Key";
            ddlStatus.DataBind();
        }

        //Loading type from dictionary
        private void LoadType()
        {
            ddlType.DataSource = dcType;
            ddlType.DataTextField = "Value";
            ddlType.DataValueField = "Key";
            ddlType.DataBind();
        }

        private void LoadFormWithRequirement()
        {
            try
            {
                if (Session["taskid"] != null)
                {
                    trTaskId.Visible = true;
                    TaskTextBox.Text = Session["taskid"].ToString();
                    btnRegister.Text = "Update";
                    GetTasks();
                    trUpload.Visible = true;
                    filesBind();
                    Label1.Visible = true;
                    trgrid.Visible = true;
                    GridView1.Visible = true;
                }
                else
                {
                    lblStatus.Visible = false;
                    ddlStatus.Visible = false;
                    fupUpload.Visible = true;
                    btnUpload.Visible = true;
                }
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _member.LoginId, "New Requirement", "ETS", "LoadFormWithRequirement");
            }
        }
        protected void GetTasks()
        {
            try
            {
                _member = (User)Session["user"];
                if (_member.Role != 1)
                {
                    txtTitle.Enabled = false;
                    ddlType.Enabled = false;
                    txtEffHours.Enabled = false;
                    txtDescription.Enabled = false;
                }
                _requirement = new Task();
                _requirement.TaskId = Convert.ToInt32(Session["taskid"]);
                _daTask = new DA_Task();
                _requirement = _daTask.GetTaskDetails(_requirement);
                txtTitle.Text = _requirement.Title;
                ddlType.SelectedIndex = _requirement.Type;
                txtEffHours.Text = _requirement.EstimatedEfforts.ToString();
                ddlStatus.SelectedIndex= _requirement.TaskStatus;
                txtDescription.Text = _requirement.Description;
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _member.LoginId, "New Requirement", "ETS", "GetTasks");
            }
        }
        protected void UpdateTask()
        {
            try
            {
                    _requirement = new Task();
                    _requirement.TaskId = Convert.ToInt32(Session["taskid"]);
                    _requirement.Title = txtTitle.Text;
                    _requirement.Type = Convert.ToInt16(ddlType.SelectedItem.Value);
                    _requirement.EstimatedEfforts = Convert.ToInt16(txtEffHours.Text);
                    _requirement.TaskStatus = Convert.ToInt16(ddlStatus.SelectedItem.Value);
                    _requirement.Description = txtDescription.Text;
                    _daTask = new DA_Task();
                    blResult = _daTask.UpdateTask(_requirement);
                    if (blResult == true)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "New Rerequirement", "alert('Requirement Updated successfully');", true);
                        Response.Redirect("~/Account/Requirements.aspx");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "New Rerequirement", "alert('Rquirement Updation is not successful');", true);
                    }
             }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _member.LoginId, "New Requirement", "ETS", "UpdateTask");
            }
        }
        protected void AssignTask()
        {
            try
            {
                _requirement = new Task();
                _requirement.CreatedByCode = _member.UserCode;
                _requirement.TaskId = Convert.ToInt32(Session["taskid"]);
                _requirement.Title = txtTitle.Text;
                _requirement.Type = Convert.ToInt16(ddlType.SelectedItem.Value);
                _requirement.EstimatedEfforts = Convert.ToInt16(txtEffHours.Text);
                _requirement.TaskStatus = Convert.ToInt16(ddlStatus.SelectedItem.Value);
                _requirement.Description = txtDescription.Text;
 
                _daTask = new DA_Task();
                blResult = _daTask.NewTasks(_requirement);
                if (blResult == true)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "New Rerequirement", "alert('Requirement Updated successfully');", true);
                    Response.Redirect("~/Account/Requirements.aspx");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "New Rerequirement", "alert('Rquirement Updation is not successful');", true);
                }
             }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _member.LoginId, "New Requirement", "ETS", "AssignTask");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (btnRegister.Text == " Post ")
            {
                AssignTask();
            }
            else if (btnRegister.Text == "Update")
            {
                UpdateTask();
            }
            Session["taskid"] = null;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Requirements.aspx");
        }

        protected void txtAssignTo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string s = Session["taskid"].ToString();
            string path = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["requirementattachment"]));
            try
            {
                if (fupUpload.HasFile)
                {
                    string[] substrings = fupUpload.FileName.Split('.');
                    string fileName = substrings[0] + "_" + DateTime.Now.ToString("ddMMyy") + "." + substrings[substrings.Length - 1];

                    string subDictionaryPath = path + s;
                    var _directoryInfo = new DirectoryInfo(subDictionaryPath);

                    var directoryInfo = new DirectoryInfo(path);
                    if (directoryInfo.Exists)
                    {
                        if (!_directoryInfo.Exists)
                        {
                            directoryInfo.CreateSubdirectory(s);
                            fupUpload.SaveAs(subDictionaryPath + @"\" + fileName);
                        }
                        else
                        {
                            fupUpload.SaveAs(subDictionaryPath + @"\" + fileName);
                        }
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('File Uploaded Successfully.');", true);
                        filesBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Dictionart Not Exist');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No File was selected.');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _member.LoginId, "DSR Report", "ETS", "loadDSR");
            }
        }

        public void filesBind()
        {
            try
            {
                string s = Session["taskid"].ToString();
                string path = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["requirementattachment"]));
                string _path = path + s;
                var _directoryInfo = new DirectoryInfo(_path);

                if (_directoryInfo.Exists)
                {
                    
                    DataTable dt = new DataTable("Fetch_File");
                    dt.Columns.Add("FileName");
                    dt.Columns.Add("Download");
                    dt.Columns.Add("Date");
                    string[] filePaths = Directory.GetFiles(_path);

                    foreach (string filePath in filePaths)
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        string[] str = filePath.Split(new string[] { @"\" }, StringSplitOptions.None);
                        dt.Rows.Add(str[str.Length - 1], filePath, fileInfo.CreationTime);
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "No Files Uploaded";
                }
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _member.LoginId, "DSR Report", "ETS", "loadDSR");
            }
        }
        protected void lnkdownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                string filePath = btn.CommandArgument;
                string[] str = filePath.Split(new string[] { @"\" }, StringSplitOptions.None);
                Response.ContentType = "image/jpg/text";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + str[str.Length - 1] + "\"");
                Response.TransmitFile(filePath);
                Response.End();
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _member.LoginId, "DSR Report", "ETS", "loadDSR");
            }
        }     
    }
}