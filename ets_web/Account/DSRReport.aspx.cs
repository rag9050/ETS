using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

namespace ets_web.Account
{
    public partial class DSRReport : ets_web.ETSBasePage
    {
        DA_DSR _daDSR = null;
        List<Model.DSR> _lstDSR = null;
        User _UserModel = null;
 
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateUserLogin();
            _UserModel = (User)Session["user"];
            if (!IsPostBack)
            {
                loadDSR(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
                txtDSRDate.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");             
            }
        }

        private void loadDSR(string strDate)
        {
            try
            {
                _daDSR = new DA_DSR();
                _lstDSR = _daDSR.GetDSR(strDate);
                dvDSRData.DataSource = _lstDSR;
                dvDSRData.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "DSR Report", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _UserModel.LoginId, "DSR Report", "ETS", "loadDSR");
            }
        }

        private void bindGridView()
        {
            try
            {
                _daDSR = new DA_DSR();
                _lstDSR = _daDSR.GetDSR(ConvertToDBDate(txtDSRDate.Text));
                dvDSRData.DataSource = _lstDSR;
                dvDSRData.DataBind();
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "DSR Report", "alert(" + Message.ErrorMessage + ");", true);
                EXCEPTION_UTILITY.WriteToLog(ex, GetLogFilePath(), _UserModel.LoginId, "DSR Report", "ETS", "bindGridView");
            }
        }

        protected void dvDSRData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
                _UserModel = (User)Session["user"];
                dvDSRData.PageIndex = e.NewPageIndex;
                bindGridView();
        }

        protected void txtDSRDate_TextChanged1(object sender, EventArgs e)
        {
            _UserModel = (User)Session["user"];
            bindGridView();
        }
    }
}
